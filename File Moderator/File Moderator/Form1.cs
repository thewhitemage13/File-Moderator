using System.Diagnostics;
using System.Runtime.InteropServices;

namespace File_Moderator
{

    /// <summary>
    /// Головна форма програми для налаштування та запуску моніторингу.
    /// </summary>
    public partial class Form1 : Form
    {
        private bool tracking = false;
        private Thread trackingThread;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);


        /// <summary>
        /// Ініціалізація головної форми.
        /// </summary>
        public Form1()
        {
            InitializeComponent(); 
            this.Controls.AddRange
                (new Control[]
                {
                    chkLogKeys,
                    chkMonitorProcesses, 
                    txtLogFilePath, 
                    txtRestrictedWords, 
                    txtRestrictedApps, 
                    btnStartTracking, 
                    btnStopTracking, 
                    btnViewReport 
                }
                );
        }

        /// <summary>
        /// Запуск моніторингу активності користувача.
        /// </summary>
        private void StartTracking(object sender, EventArgs e)
        {
            tracking = true;
            trackingThread = new Thread(TrackUserActivity);
            trackingThread.IsBackground = true;
            trackingThread.Start();
        }

        /// <summary>
        /// Зупинка моніторингу активності користувача.
        /// </summary>
        private void StopTracking(object sender, EventArgs e)
        {
            tracking = false;
            trackingThread?.Join();
        }

        /// <summary>
        /// Перегляд звіту про моніторинг у текстовому файлі.
        /// </summary>
        private void ViewReport(object sender, EventArgs e)
        {
            if (File.Exists(txtLogFilePath.Text))
                Process.Start("notepad.exe", txtLogFilePath.Text);
        }

        /// <summary>
        /// Фоновий процес моніторингу активності користувача.
        /// </summary>
        private void TrackUserActivity()
        {
            string logPath = txtLogFilePath.Text;
            string[] restrictedWords = txtRestrictedWords.Text.Split(',');
            string[] restrictedApps = txtRestrictedApps.Text.Split(',');

            while (tracking)
            {
                if (chkMonitorProcesses.Checked)
                {
                    var processes = Process.GetProcesses().Select(p => p.ProcessName);
                    foreach (string process in processes)
                    {
                        if (restrictedApps.Contains(process))
                        {
                            File.AppendAllText(logPath, $"Restricted app closed: {process} {DateTime.Now}\n");
                            foreach (var proc in Process.GetProcessesByName(process)) proc.Kill();
                        }
                    }
                }

                if (chkLogKeys.Checked)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (GetAsyncKeyState(key) < 0)
                        {
                            File.AppendAllText(logPath, key.ToString() + " ");
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}