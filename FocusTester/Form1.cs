using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FocusTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string id = ASCOM.DriverAccess.Focuser.Choose("");

            ASCOM.Interface.IFocuser foc = new ASCOM.DriverAccess.Focuser(id);

            foc.Link = true;
            foc.Move(0);
            while (foc.IsMoving)
                ;

            foc.Move(1000);
            while (foc.IsMoving)
                ;

            while(true)
            {
                bool val = foc.Link;
                //foc.Halt();
                int pos = foc.Position;
                double temp = foc.Temperature;
                val = foc.IsMoving;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
