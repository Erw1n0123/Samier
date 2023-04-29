using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
    /// Interaction logic for FeldolgozasPage.xaml
    /// </summary>
    public partial class FeldolgozasPage : Page
    {
        bool beolvasva = false;
        int ID = 0;
        List<Models.Munka> list = new List<Models.Munka>();
        List<int> szolgids = new List<int>();
        List<string> szolgnames = new List<string>();
        int SzolgId = 0;

        private void AllFeldolgozando()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Munka/Feldolgozas/{Clean.MainWindow.uId}";

            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Munka>>(result);
                if (list.Count > 0)
                {
                    SzolgaltatasBoxSet();
                    InputSet();
                }
                else
                {
                    UresList();
                }

            }
            catch (Exception ex) { }
        }

        private void UresList()
        {
            ID = 0;
            TeljesNevText.Text = "";
            EmailText.Text = "";
            TelefonszamText.Text = "";
            IranyitoszamText.Text = "";
            TelepulesText.Text = "";
            CimText.Text = "";
            SzolgaltatasBox.SelectedIndex = 0;
            IdopontDate.SelectedDate = null;
            IdopontOraBox.SelectedIndex = 0;
            IdopontPercBox.SelectedIndex = 0;
            ArText.Text = "";
            Leiras.Text = "";
            DatumLab.Content = "";
            MessageBox.Show("Jelenleg nincs több feldolgozandó adat!");
        }

        //szolgbox
        private int KezdoSzolg(int id)
        {
            int hely = -1;
            foreach (var item in szolgids)
            {
                hely++;
                if (item == id)
                {
                    break;
                }
            }

            return hely;
        }

        private void InputSet()
        {
            ID = list[0].MunkaId;
            TeljesNevText.Text = list[0].MunkaTeljesNev;
            EmailText.Text = list[0].MunkaEmail;
            TelefonszamText.Text = list[0].MunkaTelefonszam;
            IranyitoszamText.Text = list[0].MunkaIranyitoszam.ToString();
            TelepulesText.Text = list[0].MunkaTelepules;
            CimText.Text = list[0].MunkaCim;
            SzolgaltatasBox.SelectedIndex = KezdoSzolg(list[0].SzId);
            IdopontDate.SelectedDate = null;
            IdopontOraBox.SelectedIndex = 0;
            IdopontPercBox.SelectedIndex = 0;
            ArText.Text = "";
            Leiras.Text = list[0].MunkaLeiras;
            DatumLab.Content = list[0].Datum;
        }
        public FeldolgozasPage()
        {
            InitializeComponent();
            if (!beolvasva)
            {
                AllFeldolgozando();
                beolvasva = true;
                List<int> ora = new List<int>();
                for (int i = 0; i < 24; i++)
                {
                    ora.Add(i);
                }
                IdopontOraBox.ItemsSource = ora;
                List<int> perc = new List<int>();
                for (int i = 0; i < 60; i++)
                {
                    perc.Add(i);
                }
                IdopontPercBox.ItemsSource = perc;
            }

        }

        //szolgbox
        private void SzolgaltatasBoxSet()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Szolgaltatas/";
            List<Models.Szolgaltata> szolg = new List<Models.Szolgaltata>();
            try
            {
                string result = client.DownloadString(url);
                szolg = JsonConvert.DeserializeObject<List<Models.Szolgaltata>>(result);
            }
            catch (Exception ex) { }
            foreach (var item in szolg)
            {
                szolgids.Add(item.Id);
                szolgnames.Add(item.Nev);
            }
            SzolgaltatasBox.ItemsSource = szolgnames;
        }
        private string Ellenorzes()
        {
            return "";
        }

        private void KeszClick(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                if (ArText.Text != "" && IdopontDate.Text != null && IdopontOraBox.Text != null && IdopontPercBox.Text != null)
                {
                   
                if (ID != 0)
                {
                    Models.Munka m = new Models.Munka();
                    m.MunkaId = ID;
                    m.MunkaTeljesNev = TeljesNevText.Text;
                    m.MunkaEmail = EmailText.Text;
                    m.MunkaTelefonszam = TelefonszamText.Text;
                    m.MunkaIranyitoszam = int.Parse(IranyitoszamText.Text);
                    m.MunkaTelepules = TelepulesText.Text;
                    m.MunkaCim = CimText.Text;
                    SetSzolgId();
                    m.SzId = SzolgId;
                    m.Idopont = $"{IdopontDate.Text} {IdopontOraBox.Text}:{IdopontPercBox.Text}";
                    m.Ar = int.Parse(ArText.Text);
                    m.MunkaLeiras = Leiras.Text;
                    m.Datum = DatumLab.Content.ToString();
                    m.Allapot = 1;
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string url = $"https://localhost:6969/Munka/{Clean.MainWindow.uId}";
                    try
                    {
                        string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(m));
                        MessageBox.Show(result);
                        AllFeldolgozando();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
                else
                {
                    MessageBox.Show("Árat vagy időpontot nem adtál meg!");
                }
            }
            else
            {
                MessageBox.Show(uzenet);
            }
        }

        //szolgbox
        private void SetSzolgId()
        {
            string sz = SzolgaltatasBox.Text;
            int hely = -1;
            foreach (var item in szolgnames)
            {
                hely++;
                if (item == sz)
                {
                    break;
                }
            }
            SzolgId = szolgids[hely];
        }

        private void KihagyasClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan a sor végére küldi?",
                "A megbeszélt IDŐPONT és ÁR nem kerül mentésre!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string uzenet = Ellenorzes();
                if (uzenet == "")
                {

                    if (ID != 0)
                    {
                        Models.Munka m = new Models.Munka();
                        m.MunkaTeljesNev = TeljesNevText.Text;
                        m.MunkaEmail = EmailText.Text;
                        m.MunkaTelefonszam = TelefonszamText.Text;
                        m.MunkaIranyitoszam = int.Parse(IranyitoszamText.Text);
                        m.MunkaTelepules = TelepulesText.Text;
                        m.MunkaCim = CimText.Text;
                        SetSzolgId();
                        m.SzId = SzolgId;
                        m.MunkaLeiras = Leiras.Text;
                        m.Datum = DatumLab.Content.ToString();
                        WebClient client = new WebClient();
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Encoding = Encoding.UTF8;
                        string url = $"https://localhost:6969/Munka";
                        try
                        {
                            string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(m));
                            /*MessageBox.Show(result);
                            AllFeldolgozando();*/
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        url = $"https://localhost:6969/Munka/{Clean.MainWindow.uId}?id={ID}";
                        try
                        {
                            string result = client.UploadString(url, "DELETE", JsonConvert.SerializeObject(m));
                            /*MessageBox.Show(result);*/
                            AllFeldolgozando();
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

            }
        }

        private void FeldolgozhatatlanClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan feldolgozhatatlan?",
                "Ezzel kiveszi a listából, és jegeli!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string uzenet = Ellenorzes();
                if (uzenet == "")
                {
                    if (ID != 0)
                    {
                        Models.Munka m = new Models.Munka();
                        m.MunkaId = ID;
                        m.MunkaTeljesNev = TeljesNevText.Text;
                        m.MunkaEmail = EmailText.Text;
                        m.MunkaTelefonszam = TelefonszamText.Text;
                        m.MunkaIranyitoszam = int.Parse(IranyitoszamText.Text);
                        m.MunkaTelepules = TelepulesText.Text;
                        m.MunkaCim = CimText.Text;
                        SetSzolgId();
                        m.SzId = SzolgId;
                        //m.Idopont = $"{IdopontDate.Text} {IdopontOraBox.Text}:{IdopontPercBox.Text}";
                        //m.Ar = int.Parse(ArText.Text);
                        m.MunkaLeiras = Leiras.Text;
                        m.Datum = DatumLab.Content.ToString();
                        m.Allapot = -1;
                        WebClient client = new WebClient();
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Encoding = Encoding.UTF8;
                        string url = $"https://localhost:6969/Munka/{Clean.MainWindow.uId}";
                        try
                        {
                            string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(m));
                            MessageBox.Show(result);
                            AllFeldolgozando();
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
        }

        private void CsakSzamok(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
