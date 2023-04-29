using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clean.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        int szamlalo = 3;
        public Login()
        {
            InitializeComponent();
        }

        public string SaltRequest(string UserName)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                string result = client.UploadString("https://localhost:6969/Login/SaltRequest/" + UserName, "POST");
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string[] LoginUser(string nev, string tmpHash)
        {
            string[] valasz = new string[3];
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string url = $"https://localhost:6969/Login?nev={nev}&tmpHash={tmpHash}";
            try
            {
                string result = client.UploadString(url, "POST");
                valasz = JsonConvert.DeserializeObject<string[]>(result);
                return valasz;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                valasz[0] = ex.Message;
                valasz[1] = "";
                return valasz;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string salt = SaltRequest(Felh.Text);
            string tmpHash = MainWindow.CreateSHA256(Jelsz.Password + salt);
            string[] result = LoginUser(Felh.Text, tmpHash);
            if (szamlalo > 0)
            {
                if (result[1] != "")
                {
                    Clean.MainWindow.uId = result[0];
                    Clean.MainWindow.UserName = result[1];
                    Clean.MainWindow.Jogosultsag = int.Parse(result[2]);
                    this.Close();
                }
                else
                {
                    szamlalo--;
                    MessageBox.Show($"Sikertelen bejelentkezés!");
                    if (szamlalo == 0)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void FelhChanged(object sender, TextChangedEventArgs e)
        {
            if (Felh.Text == "")
            {
                ImageBrush userNameTextImageBrush = new ImageBrush();
                userNameTextImageBrush.ImageSource = new BitmapImage(new Uri(@"../../../img/felhasznalo1.png", UriKind.Relative));
                userNameTextImageBrush.AlignmentX = AlignmentX.Left;
                userNameTextImageBrush.AlignmentY = AlignmentY.Center;
                userNameTextImageBrush.Stretch = Stretch.Fill;
                Felh.Background = userNameTextImageBrush;
            }
            else
            {
                Felh.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDFAFF"));
            }
        }

        private void JeszChanged(object sender, RoutedEventArgs e)
        {
            if (Jelsz.Password == "")
            {
                ImageBrush userNameTextImageBrush = new ImageBrush();
                userNameTextImageBrush.ImageSource = new BitmapImage(new Uri(@"../../../img/jelszo1.png", UriKind.Relative));
                userNameTextImageBrush.AlignmentX = AlignmentX.Left;
                userNameTextImageBrush.AlignmentY = AlignmentY.Center;
                userNameTextImageBrush.Stretch = Stretch.Fill;
                Jelsz.Background = userNameTextImageBrush;
            }
            else
            {
                Jelsz.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDDFAFF"));
            }
        }
    }
}
