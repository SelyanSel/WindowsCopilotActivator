using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopilotActivator
{
    internal static class Program
    {

        static void LegacyStartupProgram(string[] args)
        {
            try
            {
                if (args[0] == "--quickstart")
                {
                    RegistryKey UserEligibleKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Copilot\BingChat", true);

                    if (UserEligibleKey != null)
                    {
                        UserEligibleKey.SetValue("IsUserEligible", 1, RegistryValueKind.DWord);
                    }
                    else
                    {
                        MessageBox.Show("Your computer is incompatible. Make sure to have the latest windows version," +
                                        " and check for the latest updates on the github.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    System.Diagnostics.Process.Start(@"microsoft-edge:///?ux=copilot&tcp=1&source=taskbar");
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Copilot());
                }
            }
            catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Copilot());
            }
        }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Process[] pdetect = Process.GetProcessesByName("CopilotActivator");

            if (pdetect.Length == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Copilot());
            }
            else
            {
                RegistryKey UserEligibleKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Copilot\BingChat", true);

                RegistryKey CopilotEnabled = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Copilot\", true);

                if (UserEligibleKey != null)
                {
                    UserEligibleKey.SetValue("IsUserEligible", 1, RegistryValueKind.DWord);
                }
                else
                {
                    MessageBox.Show("Your computer is incompatible. Make sure to have the latest windows version," +
                                    " and check for the latest updates on the github.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (CopilotEnabled != null)
                {
                    CopilotEnabled.SetValue("IsCopilotAvailable", 1, RegistryValueKind.DWord);
                }
                else
                {
                    MessageBox.Show("Your computer is incompatible. Make sure to have the latest windows version," +
                                    " and check for the latest updates on the github.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                System.Diagnostics.Process.Start(@"microsoft-edge:///?ux=copilot&tcp=1&source=taskbar");

                Application.Exit();
            }
        }
    }
}
