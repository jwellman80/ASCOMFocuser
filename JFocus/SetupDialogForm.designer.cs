namespace ASCOM.JFocus
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Config _c;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.radioCelcius = new System.Windows.Forms.RadioButton();
            this.radioFahrenheit = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkSetPos = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textPos = new System.Windows.Forms.TextBox();
            this.textBoxRpm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(7, 179);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOkClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(87, 178);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
            // 
            // cbComPort
            // 
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(72, 9);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(78, 21);
            this.cbComPort.TabIndex = 4;
            // 
            // radioCelcius
            // 
            this.radioCelcius.AutoSize = true;
            this.radioCelcius.Checked = true;
            this.radioCelcius.Location = new System.Drawing.Point(30, 21);
            this.radioCelcius.Name = "radioCelcius";
            this.radioCelcius.Size = new System.Drawing.Size(59, 17);
            this.radioCelcius.TabIndex = 5;
            this.radioCelcius.TabStop = true;
            this.radioCelcius.Text = "Celcius";
            this.radioCelcius.UseVisualStyleBackColor = true;
            // 
            // radioFahrenheit
            // 
            this.radioFahrenheit.AutoSize = true;
            this.radioFahrenheit.Location = new System.Drawing.Point(30, 44);
            this.radioFahrenheit.Name = "radioFahrenheit";
            this.radioFahrenheit.Size = new System.Drawing.Size(75, 17);
            this.radioFahrenheit.TabIndex = 6;
            this.radioFahrenheit.Text = "Fahrenheit";
            this.radioFahrenheit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioCelcius);
            this.groupBox1.Controls.Add(this.radioFahrenheit);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 68);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LCD Temp Disp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Com Port:";
            // 
            // checkSetPos
            // 
            this.checkSetPos.AutoSize = true;
            this.checkSetPos.Location = new System.Drawing.Point(12, 110);
            this.checkSetPos.Name = "checkSetPos";
            this.checkSetPos.Size = new System.Drawing.Size(140, 17);
            this.checkSetPos.TabIndex = 9;
            this.checkSetPos.Text = "Set Postion On Connect";
            this.checkSetPos.UseVisualStyleBackColor = true;
            this.checkSetPos.CheckedChanged += new System.EventHandler(this.checkSetPos_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(9, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Position";
            // 
            // textPos
            // 
            this.textPos.Enabled = false;
            this.textPos.Location = new System.Drawing.Point(60, 130);
            this.textPos.Name = "textPos";
            this.textPos.Size = new System.Drawing.Size(86, 20);
            this.textPos.TabIndex = 11;
            // 
            // textBoxRpm
            // 
            this.textBoxRpm.Enabled = false;
            this.textBoxRpm.Location = new System.Drawing.Point(60, 153);
            this.textBoxRpm.Name = "textBoxRpm";
            this.textBoxRpm.Size = new System.Drawing.Size(86, 20);
            this.textBoxRpm.TabIndex = 12;
            this.textBoxRpm.Text = "75";
            this.textBoxRpm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(9, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "RPM";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 215);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxRpm);
            this.Controls.Add(this.textPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkSetPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbComPort);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JFocus Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.RadioButton radioCelcius;
        private System.Windows.Forms.RadioButton radioFahrenheit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkSetPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textPos;
        private System.Windows.Forms.TextBox textBoxRpm;
        private System.Windows.Forms.Label label3;
    }
}