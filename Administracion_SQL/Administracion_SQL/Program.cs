// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 24/06/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System;

namespace Administracion_SQL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Asignando el formulario principal a presentarse
            Application.Run(new Cliente());
        }
    }
}