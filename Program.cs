using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Perodua
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string mutexName = "PeroduaAppMutex";

            mutex = new Mutex(true, mutexName, out bool isNewInstance);

            if (isNewInstance)
            {
                // This is the first instance, proceed to run the application
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                // Another instance is already running
                MessageBox.Show("The application is already running.", "Instance Running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
