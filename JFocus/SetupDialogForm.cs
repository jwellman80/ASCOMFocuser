using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace ASCOM.JFocus
{
    [ComVisible(false)]
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm(ref Config config)
        {
            InitializeComponent();
            _c = config;
        }

        private void CmdOkClick(object sender, EventArgs e)
        {

            using (ASCOM.Utilities.Profile p = new Utilities.Profile())
            {
               p.DeviceType = "Focuser";
               p.WriteValue(Focuser.driverId, "ComPort",(string)cbComPort.SelectedItem);
               p.WriteValue(Focuser.driverId, "SetPos", (string)checkSetPos.Checked.ToString());
               p.WriteValue(Focuser.driverId, "RPM", textBoxRpm.Text);
               if (checkSetPos.Checked)
               {
                   p.WriteValue(Focuser.driverId, "Pos", (string)textPos.Text.ToString());
               }
               p.WriteValue(Focuser.driverId, "TempDisp", radioCelcius.Checked ? "C" : "F");
            }
            Dispose();
   
        }

        private void CmdCancelClick(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BrowseToAscom(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            string portName;
            using (ASCOM.Utilities.Profile p = new ASCOM.Utilities.Profile())
            {
                p.DeviceType = "Focuser";
                portName = p.GetValue(Focuser.driverId, "ComPort");
                if (p.GetValue(Focuser.driverId, "TempDisp").Equals("C"))
                    radioCelcius.Checked = true;
                else
                    radioCelcius.Checked = false;
                textBoxRpm.Text = p.GetValue(Focuser.driverId, "RPM");
                if (!(textBoxRpm.Text.Length > 0))
                    textBoxRpm.Text = "75";
            }

            foreach (string s in SerialPort.GetPortNames())
            {
                cbComPort.Items.Add(s);
            }
            
            cbComPort.SelectedItem = portName;
        }

        private void checkSetPos_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = false;
            if (checkSetPos.Checked)
                enable = true;

            label2.Enabled = enable;
            textPos.Enabled = enable;
        }
    }
}