using Clean.Pages;
using Clean.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clean
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void MenuPontokBekapcsolasa(MainWindow window)
        {
            if (Clean.MainWindow.Jogosultsag >= 5)
            {
                window.egyeztetendomunka.IsEnabled = true;
                window.elvegezendomunka.IsEnabled = true;
                window.keszmunka.IsEnabled = true;
            }
            if (Clean.MainWindow.Jogosultsag == 9)
            {
                
                window.osszfelhasznalo.IsEnabled = true;
                window.dolgozofelhasznalo.IsEnabled = true;
            }
            
        }
        private void Login(object sender, StartupEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainWindow = login;
            MainWindow window = new MainWindow();
            login.ShowDialog();
            if (Clean.MainWindow.UserName == "" || Clean.MainWindow.Jogosultsag==0)
            {
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Sikeresen bejelentkezve: " + Clean.MainWindow.UserName + (Clean.MainWindow.Jogosultsag==9? " (admin)": Clean.MainWindow.Jogosultsag == 5?" (dolgozó)":""));
                MenuPontokBekapcsolasa(window);
                window.Show();
            }
        }
    }
}
