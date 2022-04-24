using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_Application {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private static Mutex? s_ApplicationMutex = null;
        private const string MUTEX_NAME = "Global\\CalculatorInstanceMutex";

        protected override void OnStartup(StartupEventArgs e) {
            bool createdNew;
            s_ApplicationMutex = new Mutex(true, MUTEX_NAME, out createdNew);
            if (!createdNew) {
                s_ApplicationMutex = null;
            }
        }
    }
}
