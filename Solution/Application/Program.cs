using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iTrak.ImporterMain
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                CCGeneral.Logging.LogToDefault();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            CCGeneral.LogAndDisplay.Error(e.Exception.Message, e.Exception);
        }
    }
}