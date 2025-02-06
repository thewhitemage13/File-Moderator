using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace File_Moderator
{
    /// <summary>
    /// Головна форма програми File Moderator, яка забезпечує моніторинг натискань клавіш та відстеження запущених процесів.
    /// Дозволяє користувачеві вести логування клавіатурного введення та запуску програм.
    /// </summary>
    public partial class Form1 : Form
    {
        private bool tracking = false; // Флаг активності моніторингу
        private Thread trackingThread; // Потік для моніторингу натискань клавіш
        private Thread trackingProcessesThread; // Потік для моніторингу запущених процесів

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey); // Функція для отримання стану клавіш у реальному часі

        /// <summary>
        /// Конструктор класу Form1. Ініціалізує компоненти форми та додає контролери для взаємодії з користувачем.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.Controls.AddRange(new Control[]
            {
                chkLogKeys, // Чекбокс для логування клавіш
                chkMonitorProcesses, // Чекбокс для моніторингу процесів
                txtLogFilePath, // Поле введення шляху до лог-файлу клавіш
                txtLogProccess, // Поле введення шляху до лог-файлу процесів
                txtRestrictedWords, // Поле для заборонених слів
                txtRestrictedApps, // Поле для заборонених програм
                btnStartTracking, // Кнопка запуску моніторингу
                btnStopTracking, // Кнопка зупинки моніторингу
                btnViewReport // Кнопка перегляду звіту
            });
        }

        /// <summary>
        /// Запускає процес моніторингу натискань клавіш та процесів (якщо активовано відповідний чекбокс).
        /// </summary>
        private void StartTracking(object sender, EventArgs e)
        {
            try
            {
                tracking = true;
                trackingThread = new Thread(TrackUserActivity) { IsBackground = true };
                trackingThread.Start();

                if (chkMonitorProcesses.Checked)
                {
                    trackingProcessesThread = new Thread(TrackProcesses) { IsBackground = true };
                    trackingProcessesThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка запуску моніторингу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Зупиняє процес моніторингу та завершує роботу потоків.
        /// </summary>
        private void StopTracking(object sender, EventArgs e)
        {
            try
            {
                tracking = false;
                trackingThread?.Join();
                trackingProcessesThread?.Join();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка зупинки моніторингу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Відкриває лог-файли збережених даних користувачем у текстовому редакторі.
        /// </summary>
        private void ViewReport(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtLogFilePath.Text))
                    Process.Start("notepad.exe", txtLogFilePath.Text);
                else
                    MessageBox.Show("Файл логів клавіш не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (File.Exists(txtLogProccess.Text))
                    Process.Start("notepad.exe", txtLogProccess.Text);
                else
                    MessageBox.Show("Файл логів процесів не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відкриття звіту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Моніторинг натискань клавіш, записує введення у лог-файл.
        /// </summary>
        private void TrackUserActivity()
        {
            try
            {
                string logPath = txtLogFilePath.Text;
                StringBuilder currentWord = new StringBuilder();
                StringBuilder pressedKeys = new StringBuilder();

                while (tracking)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (GetAsyncKeyState(key) < 0)
                        {
                            int keyCode = (int)key;

                            if ((key >= Keys.A && key <= Keys.Z) || (key >= Keys.D0 && key <= Keys.D9))
                            {
                                currentWord.Append(key.ToString());
                                pressedKeys.Append($"[{keyCode}]");
                            }
                            else if (key == Keys.Space || key == Keys.Enter)
                            {
                                if (currentWord.Length > 0)
                                {
                                    string logEntry = $"{currentWord} ({pressedKeys})\n";
                                    File.AppendAllText(logPath, logEntry);
                                    currentWord.Clear();
                                    pressedKeys.Clear();
                                }
                            }
                            else
                            {
                                pressedKeys.Append($"[{keyCode}]");
                            }
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка моніторингу клавіш: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Відстежує запущені процеси на комп'ютері та записує нові запущені додатки у лог-файл.
        /// </summary>
        private void TrackProcesses()
        {
            try
            {
                string logPath = txtLogProccess.Text;
                HashSet<string> previousProcesses = new HashSet<string>();

                while (tracking)
                {
                    var currentProcesses = Process.GetProcesses().Select(p => p.ProcessName).ToHashSet();

                    foreach (string process in currentProcesses)
                    {
                        if (!previousProcesses.Contains(process))
                        {
                            string logEntry = $"Запущено процес: {process} ({DateTime.Now})\n";
                            File.AppendAllText(logPath, logEntry);
                        }
                    }

                    previousProcesses = currentProcesses;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка моніторингу процесів: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
