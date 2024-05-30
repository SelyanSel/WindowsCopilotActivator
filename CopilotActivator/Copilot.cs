using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopilotActivator
{
    public partial class Copilot : Form
    {

        private bool AllowClosing { get; set; } = false;

        public Copilot()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void copilotService_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Copilot\BingChat", true);

            if (key != null)
            {
                key.SetValue("IsUserEligible", 0, RegistryValueKind.DWord);
            }

            AllowClosing = true;
            Application.Exit();
        }

        private void Copilot_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing) { e.Cancel = true; this.Hide(); }
        }
    }
}
