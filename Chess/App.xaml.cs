using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Chess
{

    public partial class App : Application
    {

        private void InitApp(object sender, StartupEventArgs e)
        {
            //MessageBox.Show("Anwendung wird gestartet");
            MainWindow mainWindow = new MainWindow();
            //Chess.MainWindow.appInstance = mainWindow;
            //MainWindow.Title = "Hallo";
            mainWindow.Show();
            
        }

    }
}
