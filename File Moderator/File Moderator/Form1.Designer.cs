namespace File_Moderator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartTracking = new Button();
            btnStopTracking = new Button();
            btnViewReport = new Button();
            chkLogKeys = new CheckBox();
            chkMonitorProcesses = new CheckBox();
            txtLogFilePath = new TextBox();
            txtRestrictedWords = new TextBox();
            txtRestrictedApps = new TextBox();
            SuspendLayout();
            // 
            // btnStartTracking
            // 
            btnStartTracking.Location = new Point(52, 293);
            btnStartTracking.Name = "btnStartTracking";
            btnStartTracking.Size = new Size(169, 56);
            btnStartTracking.TabIndex = 0;
            btnStartTracking.Text = "Start";
            btnStartTracking.UseVisualStyleBackColor = true;
            btnStartTracking.Click += StartTracking;
            // 
            // btnStopTracking
            // 
            btnStopTracking.Location = new Point(310, 293);
            btnStopTracking.Name = "btnStopTracking";
            btnStopTracking.Size = new Size(169, 56);
            btnStopTracking.TabIndex = 1;
            btnStopTracking.Text = "Stop";
            btnStopTracking.UseVisualStyleBackColor = true;
            btnStopTracking.Click += StopTracking;
            // 
            // btnViewReport
            // 
            btnViewReport.Location = new Point(589, 293);
            btnViewReport.Name = "btnViewReport";
            btnViewReport.Size = new Size(169, 56);
            btnViewReport.TabIndex = 2;
            btnViewReport.Text = "View";
            btnViewReport.UseVisualStyleBackColor = true;
            btnViewReport.Click += ViewReport;
            // 
            // chkLogKeys
            // 
            chkLogKeys.AutoSize = true;
            chkLogKeys.Location = new Point(52, 48);
            chkLogKeys.Name = "chkLogKeys";
            chkLogKeys.Size = new Size(46, 19);
            chkLogKeys.TabIndex = 3;
            chkLogKeys.Text = "Log";
            chkLogKeys.UseVisualStyleBackColor = true;
            // 
            // chkMonitorProcesses
            // 
            chkMonitorProcesses.AutoSize = true;
            chkMonitorProcesses.Location = new Point(140, 48);
            chkMonitorProcesses.Name = "chkMonitorProcesses";
            chkMonitorProcesses.Size = new Size(69, 19);
            chkMonitorProcesses.TabIndex = 4;
            chkMonitorProcesses.Text = "Monitor";
            chkMonitorProcesses.UseVisualStyleBackColor = true;
            // 
            // txtLogFilePath
            // 
            txtLogFilePath.Location = new Point(52, 85);
            txtLogFilePath.Name = "txtLogFilePath";
            txtLogFilePath.Size = new Size(427, 23);
            txtLogFilePath.TabIndex = 5;
            txtLogFilePath.Text = "logs.txt";
            // 
            // txtRestrictedWords
            // 
            txtRestrictedWords.Location = new Point(52, 147);
            txtRestrictedWords.Name = "txtRestrictedWords";
            txtRestrictedWords.PlaceholderText = "Restricted words (comma-separated)";
            txtRestrictedWords.Size = new Size(427, 23);
            txtRestrictedWords.TabIndex = 6;
            // 
            // txtRestrictedApps
            // 
            txtRestrictedApps.Location = new Point(52, 211);
            txtRestrictedApps.Name = "txtRestrictedApps";
            txtRestrictedApps.PlaceholderText = "Restricted apps (comma-separated)";
            txtRestrictedApps.Size = new Size(427, 23);
            txtRestrictedApps.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtRestrictedApps);
            Controls.Add(txtRestrictedWords);
            Controls.Add(txtLogFilePath);
            Controls.Add(chkMonitorProcesses);
            Controls.Add(chkLogKeys);
            Controls.Add(btnViewReport);
            Controls.Add(btnStopTracking);
            Controls.Add(btnStartTracking);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartTracking;
        private Button btnStopTracking;
        private Button btnViewReport;
        private CheckBox chkLogKeys;
        private CheckBox chkMonitorProcesses;
        private TextBox txtLogFilePath;
        private TextBox txtRestrictedWords;
        private TextBox txtRestrictedApps;
    }
}
