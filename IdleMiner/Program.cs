using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using IdleMiner.Properties;

namespace IdleMiner
{
    static class Program
    {
        public static bool IgnorePoclbmChecks = false;
        public static bool StartToTray = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var a in args)
            {
                switch (a)
                {
                    case @"/no-poclbm":
                        IgnorePoclbmChecks = true;
                        break;
                    case @"/tray":
                        StartToTray = true;
                        break;
                    case @"/clearsettings":
                        Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\IdleMiner\",false);
                        break;
                }
            }
            // Preflight checks
            if (!File.Exists("poclbm.exe") && !IgnorePoclbmChecks)
            {
                MessageBox.Show(
                    Resources.Unable_To_Find_poclbm_Message,
                    Resources.Unable_To_Find_poclbm_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
