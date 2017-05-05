using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Chess
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        private void InitApp(object sender, StartupEventArgs e)
        {
            //MessageBox.Show("Anwendung wird gestartet");
            MainWindow mainWindow = new MainWindow();
            //MainWindow.Title = "Hallo";
            mainWindow.Show();
            Chess.MainWindow.appInstance = mainWindow;
        }

    }
}
