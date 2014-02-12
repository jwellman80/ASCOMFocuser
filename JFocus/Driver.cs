//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Focuser driver for JFocus
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Focuser interface version: 1.0
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	1.0.0	Initial edit, from ASCOM Focuser Driver template
// --------------------------------------------------------------------------------
//
using System;
using System.Collections;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.Globalization;
using System.IO.Ports;
using System.IO;

namespace ASCOM.JFocus
{
    //
    // Your driver's ID is ASCOM.JFocus.Focuser
    //
    // The Guid attribute sets the CLSID for ASCOM.JFocus.Focuser
    // The ClassInterface/None addribute prevents an empty interface called
    // _Focuser from being created and used as the [default] interface
    //
    [Guid("8757b080-092f-439a-9ca1-320e94b347db")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Focuser : IFocuserV2
    {
        private Config config = new Config();
        private Serial serialPort;
//        private SerialPort serialPort;
        private System.Windows.Forms.Timer timer;

        private TextWriter log;
        System.Threading.Thread updateThread;
        System.Threading.Mutex mutex = new System.Threading.Mutex();

        int lastPos = 0;
        double lastTemp = 0;
        bool lastMoving = false;
        bool lastLink = false;
        
        long UPDATETICKS = (long)(1 * 10000000.0); // 10,000,000 ticks in 1 second
        long lastUpdate = 0;

        long lastL = 0;

        #region Constants
        //
        // Driver ID and descriptive string that shows in the Chooser
        //
        internal const string driverId = "ASCOM.JFocus.Focuser";
        // TODO Change the descriptive string for your driver then remove this line
        private const string driverDescription = "JFocus Focuser";
        #endregion

        #region ASCOM Registration
        //
        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var p = new Profile())
            {
                p.DeviceType = "Focuser";
                if (bRegister)
                    p.Register(driverId, driverDescription);
                else
                    p.Unregister(driverId);
            }
        }

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }
        #endregion

        #region Implementation of IFocuserV2

        public void SetupDialog()
        {
            using (var f = new SetupDialogForm(ref config))
            {
                f.ShowDialog();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            throw new ASCOM.MethodNotImplementedException("Action");
        }
        
        
        public string CommandString(string command, bool raw)
        {
            if (!this.Connected)
                throw new ASCOM.NotConnectedException();
            string temp = "999";
            mutex.WaitOne();
            try
            {
                log.WriteLine("Sending Command: " + command);
                if (!command.EndsWith("$"))
                    command += "$";

                serialPort.ClearBuffers();

                serialPort.Transmit(command);

                // get the return value
                temp = serialPort.ReceiveTerminated("$");

                serialPort.ClearBuffers();

                log.WriteLine("Got Response: " + temp);
                log.Flush();
            }
            catch (Exception e)
            {
                log.WriteLine("Caught exception in CommandString " + e.Message);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            

            return temp;
        }


        public void Dispose()
        {
            if (serialPort == null)
                return;

            serialPort.Connected = false;
            serialPort.Dispose();
            serialPort = null;
        }

        public void Halt()
        {
            CommandString("h$", true);
        }

        public void Move(int value)
        {
            CommandString("m" + value + "$", false);
            lastMoving = true;
        }

        public bool Connected
        {
            get
            {
                if (serialPort == null)
                    return false;
                return serialPort.Connected;
            }
            set
            {
                if (value)
                {
                    bool setPos = false;
                    int posValue = 0;
                    string tempDisplay;

                    // check if we are connected, return if we are
                    if (serialPort != null && serialPort.Connected)
                        return;
                    // get the port name from the profile
                    string portName;
                    using (ASCOM.Utilities.Profile p = new Profile())
                    {
                        // get the values that are stored in the ASCOM Profile for this driver
                        // these were usually set in the settings dialog
                        p.DeviceType = "Focuser";
                        portName = p.GetValue(driverId, "ComPort");
                        setPos = p.GetValue(driverId, "SetPos").ToLower().Equals("true") ? true : false;
                        if (setPos)
                            posValue = System.Convert.ToInt32(p.GetValue(driverId, "Pos"));
                        tempDisplay = p.GetValue(driverId, "TempDisp");
                        //blValue = System.Convert.ToInt32(p.GetValue(driverId, "BackLight"));

                        if (string.IsNullOrEmpty(portName))
                        {
                            // report a problem with the port name
                            throw new ASCOM.NotConnectedException("no Com port selected");
                        }
                        // try to connect using the port
                        try
                        {
                            log = new StreamWriter("c:\\log.txt");
                            log.WriteLine("Connecting to serial port");


                            // setup the serial port.
                            serialPort = new Serial();
                            serialPort.PortName = portName;
                            serialPort.Speed = SerialSpeed.ps9600;
                            serialPort.StopBits = SerialStopBits.One;
                            serialPort.Parity = SerialParity.None;
                            serialPort.DataBits = 8;
                            serialPort.DTREnable = false;

                            if (!serialPort.Connected)
                                serialPort.Connected = true;

                            // flush whatever is there.
                            serialPort.ClearBuffers();

                            // wait for the Serial Port to come online...better way to do this???
                            System.Threading.Thread.Sleep(3000);

                            // if the user is setting a position in the Settings dialog set it here.
                            if (setPos)
                                CommandString("c" + posValue + "$", false);

                            char td = tempDisplay.Length > 0 ? tempDisplay.ToUpper().ToCharArray()[0] : 'C';
                            CommandString("a" + td + "$", false);
                            SetRpm(System.Convert.ToInt32(p.GetValue(driverId, "RPM")));
                        }
                        catch (Exception ex)
                        {
                            // report any error
                            throw new ASCOM.NotConnectedException("Serial port connectionerror", ex);
                        }
                    }
                }
                else
                {
                    // disconnect
                    if (serialPort != null && serialPort.Connected)
                    {
                        serialPort.Connected = false;
                        serialPort.Dispose();
                        serialPort = null;
                    }
                }
            }
        }


        public string Description
        {
            get { return "Words"; }
        }

        public string DriverInfo
        {
            get { return "Words"; }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
            }
        }

        public short InterfaceVersion
        {
            get { return 2; }
        }

        public string Name
        {
            get { return "JFocus"; }
        }

        public ArrayList SupportedActions
        {
            get { return new ArrayList(); }
        }

        public bool Absolute
        {
            get { return true; }
        }

        public bool IsMoving
        {
            get 
            {
                DoUpdate();
                return lastMoving; 
            }
        }

        // use the V2 connected property
        public bool Link
        {
            // TODO Replace this with your implementation
            get {
                long now = DateTime.Now.Ticks;
                if (now - lastL > UPDATETICKS)
                {
                    if (serialPort != null)
                        lastLink = serialPort.Connected;
                    
                    lastL = now;
                    return lastLink;
                }
                
                return lastLink;
            }
            set
            {
                this.Connected = value;
            }
        }

        public int MaxIncrement
        {
            get { return 30000; }
        }

        public int MaxStep
        {
            get { return 30000; }
        }

        public int Position
        {
            get {
                DoUpdate();
                return lastPos;
            }
        }

        public double StepSize
        {
            get { return 1; }
        }

        private void SetRpm(int rpm)
        {
            CommandString("p" + rpm.ToString() + "$", false);
        }

        public bool TempComp
        {
            get { return false; }
            set { return; }
        }

        public bool TempCompAvailable
        {
            get { return true; }
        }

        public double Temperature
        {
            get
            {
                DoUpdate();
                return lastTemp;
            }
        }

        private void DoUpdate()
        {
            // only allow access for "gets" once per second.
            // if inside of 1 second the buffered value will be used.
            if (DateTime.Now.Ticks > UPDATETICKS + lastUpdate)
            {
                lastUpdate = DateTime.Now.Ticks;

                // focuser returns a string like:
                // m:false;s:1000;t:25.20$
                //   m - denotes moving or not
                //   s - denotes the position in steps
                //   t - denotes the temperature, always in C

                String val = CommandString("s$", false);

                // split the values up.  Ideally you should check for null here.  
                // if something goes wrong this will throw an exception...no bueno...
                String[] vals = val.Replace('$', ' ').Trim().Split(';');

                // these values are used in the "Get" calls.  That way the client gets an immediate
                // response.  However it may up to 1 second out of date.
                // Thus "lastMoving" must be set to true when the move is initiated in "Move"
                lastMoving = vals[0].Substring(2) == "true" ? true : false;
                lastPos = Convert.ToInt16(vals[1].Substring(2));
                lastTemp = Convert.ToDouble(vals[2].Substring(2));
            }
        }

        #endregion


        public void CommandBlind(string Command, bool Raw = false)
        {
            throw new System.NotImplementedException();
        }

        public bool CommandBool(string Command, bool Raw = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
