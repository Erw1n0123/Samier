using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
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
    /// Interaction logic for FolyamatbanPage.xaml
    /// </summary>
    public partial class FolyamatbanPage : Page
    {
        bool beolvasva = false;
       // bool kivalasztva = false;
        int ID = 0;
        int DBID = 0;
        Dictionary<string, int> dolgozolist = new Dictionary<string, int>();
        Dictionary<string, int> dolgozoboxlist = new Dictionary<string, int>();
        int EHID = 0;
        Dictionary<string, int> eszkozlist = new Dictionary<string, int>();
        List<Models.ElvegzesRaktarRovid> Eraktar = new List<Models.ElvegzesRaktarRovid>();

        private void AllFolyamatban()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Munka/Egyeztetve/{Clean.MainWindow.uId}";
            List<Models.Munka> list = new List<Models.Munka>();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Munka>>(result);
            }
            catch (Exception ex) { }
            MunkakFolyamatbanGrid.ItemsSource = list;
            beolvasva = true;
        }

        private void AllDolgozo()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Felhasznalo/Dolgozo/{Clean.MainWindow.uId}";
            List<Models.Felhasznalo> list = new List<Models.Felhasznalo>();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Felhasznalo>>(result);
            }
            catch (Exception ex) { }
            foreach (var item in list)
            {
                dolgozolist.Add(item.FelhasznaloNev, item.Id);
            }
            Dolgozok.ItemsSource = dolgozolist.Keys;
            Dolgozok.Items.Refresh();
        }
        private void AllEszkoz()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Raktar/{Clean.MainWindow.uId}";
            List<Models.Raktar> list = new List<Models.Raktar>();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Raktar>>(result);
            }
            catch (Exception ex) { }
            foreach (var item in list)
            {
                eszkozlist.Add(item.Nev, item.RId);
            }
            Eszkozok.ItemsSource = eszkozlist.Keys;
            beolvasva = true;
        }

        public FolyamatbanPage()
        {
            InitializeComponent();
            if(!beolvasva)
            {
                AllFolyamatban();
                AllDolgozo();
                AllEszkoz();
               
            }
        }
        private void ListReset()
        {
            DolgozoBox.ItemsSource = "";
            EszkozBox.ItemsSource = "";
        }

        private void MunkaDolgozok()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Elvegzes/Dolgozo/{Clean.MainWindow.uId}/{ID}";
            List<Models.ElvegzesDolgozoRovid> Edolgozok = new List<Models.ElvegzesDolgozoRovid>();
            try
            {
                string result = client.DownloadString(url);
                Edolgozok = JsonConvert.DeserializeObject<List<Models.ElvegzesDolgozoRovid>>(result);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            dolgozoboxlist.Clear();
            foreach (var item in Edolgozok)
            {
                dolgozoboxlist.Add(item.FelhasznaloNev, item.DbId);
            }
            DolgozoBox.ItemsSource = (dolgozoboxlist.Count() > 0 ? dolgozoboxlist.Keys : null);
            DolgozoBox.Items.Refresh();
        }

        private void MunkaEszkozok()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Elvegzes/Raktar/{Clean.MainWindow.uId}/{ID}";
            try
            {
                string result = client.DownloadString(url);
                Eraktar = JsonConvert.DeserializeObject<List<Models.ElvegzesRaktarRovid>>(result);
            }
            catch (Exception ex) { MessageBox.Show(""); }
            EszkozBox.ItemsSource = Eraktar;
            
        }

        private void MunkakFolyamatbanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Models.Munka m = MunkakFolyamatbanGrid.SelectedItems[0] as Models.Munka;
                ID = m.MunkaId;
                MunkaDolgozok();
                MunkaEszkozok();
            }
            catch (Exception ex)
            {
                ListReset();
            }
            
            
        }

        private void Dolgozok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dolgozok.SelectedItem == null)
            {
                DolgozoHozzaadas.IsEnabled = false;
            }
            else
            {
                DolgozoHozzaadas.IsEnabled = true;
            }
        }

        private void Eszkozok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Eszkozok.SelectedItem == null)
            {
                EszkozHozzaadas.IsEnabled = false;
            }
            else
            {
                EszkozHozzaadas.IsEnabled = true;
            }
        }

        private void DolgozoHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            Models.DolgozoBeoszta db = new Models.DolgozoBeoszta();
            db.DbMId = ID;
            db.DId = dolgozolist[Dolgozok.Text];
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/DolgozoBeoszta/{Clean.MainWindow.uId}";
            try
            {
                string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(db));
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MunkaDolgozok();
            Dolgozok.SelectedItem = null;
        }

        private void EszkozHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            Models.EszkozHasznalat eh = new Models.EszkozHasznalat();
            eh.EhMId = ID;
            eh.EhRId = eszkozlist[Eszkozok.Text];
            eh.ElhasznaltMennyiseg = int.Parse(EszkozMennyiseg.Text);
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/EszkozHasznalat/{Clean.MainWindow.uId}";
            try
            {
                string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(eh));
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MunkaEszkozok();
            Eszkozok.SelectedItem = null;

        }

        private void DolgozoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
            if (DolgozoBox.SelectedItem == null)
            {
                DolgozoTorles.IsEnabled = false;

            }
            else
            { 
                DBID = dolgozoboxlist[DolgozoBox.SelectedItem.ToString()];
                DolgozoTorles.IsEnabled = true;
            }
        }

        private void EszReset()
        {
            Eszkozok.SelectedItem = null;
            EszkozMennyiseg.Text = "";
        }

        private void EszkozBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EszkozBox.SelectedItem == null)
            {
                EszkozTorles.IsEnabled = false;

            }
            else
            {
                try
                {
                    Models.ElvegzesRaktarRovid eszk = EszkozBox.SelectedItems[0] as Models.ElvegzesRaktarRovid;
                    EHID = eszk.EhId;
                    Eszkozok.SelectedItem = eszk.Nev;
                    EszkozMennyiseg.Text = eszk.ElhasznaltDb.ToString();
                    EszkozTorles.IsEnabled = true;
                }
                catch
                {
                    EszReset();
                }
                
            }

        }

        private void DolgozoTorles_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/DolgozoBeoszta/{Clean.MainWindow.uId}?id={DBID}";
            try
            {
                string result = client.UploadString(url, "DELETE", "");
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MunkaDolgozok();
            DolgozoBox.SelectedItem = null;
        }

        private void EszkozTorles_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/EszkozHasznalat/{Clean.MainWindow.uId}?id={EHID}";
            try
            {
                string result = client.UploadString(url, "DELETE", "");
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MunkaEszkozok();
            EszkozBox.SelectedItem = null;
        }

        private void CsakSzamok(object sender, TextCompositionEventArgs e)
        {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text); 
        }
    }
}
