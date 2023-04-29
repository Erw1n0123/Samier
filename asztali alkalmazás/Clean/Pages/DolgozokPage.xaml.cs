using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clean.Pages
{
    /// <summary>
    /// Interaction logic for DolgozokPage.xaml
    /// </summary>
    public partial class DolgozokPage : Page
    {
        bool beolvasva = false;
        int ID = 0;
        string Salt = "";
        string Hash = "";
        List<Models.Felhasznalo> list = new List<Models.Felhasznalo>();

        private void AllDolgozo()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Felhasznalo/Dolgozo/{Clean.MainWindow.uId}";
            InputReset();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Felhasznalo>>(result);
            }
            catch (Exception ex) { }
            Felhasznalok_adatai.ItemsSource = list;
            beolvasva = true;
        }

        private void InputReset()
        {
            ID = 0;
            FelhasznaloNevText.Text = "";
            TeljesNevText.Text = "";
            EmailText.Text = "";
            TelefonszamText.Text = "";
            IranyitoszamText.Text = "";
            TelepulesText.Text = "";
            CimText.Text = "";
            RankBox.SelectedIndex = 0;
            PasswordPwd.Password = "";
            AktivBox.SelectedIndex = 1;
        }

        private string Ellenorzes()
        {
            return "";
        }

        private string HozzaadEll()
        {
            foreach (var f in list)
            {
                if (f.FelhasznaloNev == FelhasznaloNevText.Text)
                {
                    return "Ez a felhasználónév már foglalt.";
                }
                if (f.Email == EmailText.Text)
                {
                    return "Ez az email cím már foglalt.";
                }
            }
            return "";
        }
        public DolgozokPage()
        {
            InitializeComponent();
            if (!beolvasva)
            {
                AllDolgozo();
                List<int> JogLista = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                List<int> AktivLista = new List<int>() { 0, 1 };
                RankBox.ItemsSource = JogLista;
                AktivBox.ItemsSource = AktivLista;
            }
        }

        private void Felhasznalok_adatai_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Models.Felhasznalo f = Felhasznalok_adatai.SelectedItems[0] as Models.Felhasznalo;
                ID = f.Id;
                FelhasznaloNevText.Text = f.FelhasznaloNev;
                TeljesNevText.Text = f.TeljesNev;
                EmailText.Text = f.Email;
                TelefonszamText.Text = f.Telefonszam.ToString();
                IranyitoszamText.Text = f.Iranyitoszam.ToString();
                TelepulesText.Text = f.Telepules;
                CimText.Text = f.Cim;
                RankBox.SelectedItem = f.Rank;
                PasswordPwd.Password = "";
                Salt = f.Salt;
                Hash = f.Hash;
                AktivBox.SelectedItem = f.Aktiv;
            }
            catch (Exception ex)
            {
                InputReset();
            }
        }

        private void HozzaadBtnClick(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes() + "" + HozzaadEll();
            if (uzenet == "")
            {
                Models.Felhasznalo f = new Models.Felhasznalo();
                f.FelhasznaloNev = FelhasznaloNevText.Text;
                f.TeljesNev = TeljesNevText.Text;
                f.Email = EmailText.Text;
                f.Telefonszam = TelefonszamText.Text;
                f.Iranyitoszam = int.Parse(IranyitoszamText.Text);
                f.Telepules = TelepulesText.Text;
                f.Cim = CimText.Text;
                f.Rank = int.Parse(RankBox.Text);
                f.Salt = MainWindow.GenerateSalt();
                f.Hash = MainWindow.CreateSHA256(MainWindow.CreateSHA256(PasswordPwd.Password + f.Salt));
                f.Aktiv = int.Parse(AktivBox.Text);
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:6969/Felhasznalo/{Clean.MainWindow.uId}";
                try
                {
                    string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(f));
                    MessageBox.Show(result);
                    AllDolgozo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(uzenet);
            }
        }

        private void ModositasBtnClick(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                if (ID != 0)
                {
                    Models.Felhasznalo f = new Models.Felhasznalo();
                    f.Id = ID;
                    f.FelhasznaloNev = FelhasznaloNevText.Text;
                    f.TeljesNev = TeljesNevText.Text;
                    f.Email = EmailText.Text;
                    f.Telefonszam = TelefonszamText.Text;
                    f.Iranyitoszam = int.Parse(IranyitoszamText.Text);
                    f.Telepules = TelepulesText.Text;
                    f.Cim = CimText.Text;
                    f.Rank = int.Parse(RankBox.Text);
                    if (PasswordPwd.Password != "")
                    {
                        f.Salt = MainWindow.GenerateSalt();
                        f.Hash = MainWindow.CreateSHA256(MainWindow.CreateSHA256(PasswordPwd.Password + f.Salt));
                    }
                    else
                    {
                        f.Salt = Salt;
                        f.Hash = Hash;
                    }
                    f.Aktiv = int.Parse(AktivBox.Text);
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string url = $"https://localhost:6969/Felhasznalo/{Clean.MainWindow.uId}";
                    try
                    {
                        string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(f));
                        MessageBox.Show(result);
                        AllDolgozo();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(uzenet);
            }
        }

        private void TorlesBtnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan törli a(z) {FelhasznaloNevText.Text} nevű felhasználót?",
                    "Felhasználó törlése",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Models.Felhasznalo f = new Models.Felhasznalo();
                f.Id = ID;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:6969/Felhasznalo/{Clean.MainWindow.uId}?id={ID}";
                try
                {
                    string result = client.UploadString(url, "DELETE", "");
                    MessageBox.Show(result);
                    AllDolgozo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CsakSzamok(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
