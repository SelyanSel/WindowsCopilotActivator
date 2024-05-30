using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopilotActivator
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "--quickstart")
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Copilot\BingChat", true);

                    if (key != null)
                    {
                        key.SetValue("IsUserEligible", 1, RegistryValueKind.DWord);
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
                    Application.Run(new CopilotActivator());
                }
            }
            catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CopilotActivator());
            }
        }
    }
}
