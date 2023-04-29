using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Webp;
using Image = SixLabors.ImageSharp.Image;
using static System.Net.Mime.MediaTypeNames;

namespace Clean.Pages
{
    /// <summary>
    /// Interaction logic for RaktarPage.xaml
    /// </summary>
    public partial class RaktarPage : Page
    {
        bool beolvasva = false;
        int ID = 0;
        List<Models.Raktar> list = new List<Models.Raktar>();
        byte[] bytes;

        private void AllRaktar()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:6969/Raktar/{Clean.MainWindow.uId}";
            InputReset();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Raktar>>(result);
            }
            catch (Exception ex) { }
            Raktar_adat.ItemsSource = list;
            beolvasva = true;
        }

        private void InputReset()
        {
            ID = 0;
            NevText.Text = "";
            ImageBox.Source = null;
            MennyisegText.Text = "0";
            AktivBox.SelectedIndex = 0;
        }

        private string Ellenorzes()
        {
            return "";
        }

        private string HozzaadEll()
        {
            foreach (var r in list)
            {
                if (r.Nev == NevText.Text)
                {
                    return "Már van ilyen eszköz";
                }
            }
            return "";
        }
        public RaktarPage()
        {
            
            InitializeComponent();
            if (!beolvasva)
            {
                AllRaktar();
                List<int> AktivLista = new List<int>() { 0, 1 };
                AktivBox.ItemsSource = AktivLista;
            }
        }

        private void CsakSzamok(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HozzaadClick(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes() + "" + HozzaadEll();
            if (uzenet == "")
            {
                Models.Raktar r = new Models.Raktar();
                r.Nev = NevText.Text;
                r.Mennyiseg = int.Parse(MennyisegText.Text);
                r.Kepfajl = bytes;
                if (AktivBox.Text == "0")
                {
                    r.Megjelenes = false;
                }
                else
                {
                    r.Megjelenes = true;
                }
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:6969/Raktar/{Clean.MainWindow.uId}";
                try
                {
                    string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(r));
                    MessageBox.Show(result);
                    AllRaktar();
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

        private void ModositasClick(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                if (ID != 0)
                {
                    Models.Raktar r = new Models.Raktar();
                    r.RId = ID;
                    r.Nev = NevText.Text;
                    r.Mennyiseg = int.Parse(MennyisegText.Text);
                    r.Kepfajl = bytes;
                    if (AktivBox.Text=="0")
                    {
                        r.Megjelenes = false;
                    }
                    else
                    {
                        r.Megjelenes = true;
                    }
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string url = $"https://localhost:6969/Raktar/{Clean.MainWindow.uId}";
                    try
                    {
                        string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(r));
                        MessageBox.Show(result);
                        AllRaktar();
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

        private void TorlesClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan törli a(z) {NevText.Text} nevű eszközt?",
                    "Felhasználó törlése",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Models.Raktar r = new Models.Raktar();
                r.RId = ID;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:6969/Raktar/{Clean.MainWindow.uId}?id={ID}";
                try
                {
                    string result = client.UploadString(url, "DELETE", "");
                    MessageBox.Show(result);
                    AllRaktar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Raktar_adatai_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Models.Raktar r = Raktar_adat.SelectedItems[0] as Models.Raktar;
                ID = r.RId;
                NevText.Text = r.Nev;
                MennyisegText.Text = r.Mennyiseg.ToString(); 
                bytes = r.Kepfajl;
                using (var stream = new MemoryStream(bytes))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    ImageBox.Source = bitmapImage;
                }
                AktivBox.SelectedItem = r.Megjelenes.ToString();
            }
            catch (Exception ex)
            {
                InputReset();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Az OpenFileDialog megjelenítése
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.webp) | *.jpg; *.jpeg; *.png; *.webp";
            if (openFileDialog.ShowDialog() == true)
            {
                // Kép betöltése és megjelenítése az ImageBox-ban
                var bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                ImageBox.Source = bitmapImage;

                // Kép konvertálása byte tömbbé
                using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                using (var memStream = new MemoryStream())
                {
                    stream.CopyTo(memStream);
                    memStream.Position = 0;
                    using (var image = Image.Load<Rgba32>(memStream))
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, SixLabors.ImageSharp.Formats.Webp.WebpFormat.Instance);
                        bytes = ms.ToArray();
                    }
                }
            }
        }
    }
}
