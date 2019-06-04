using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekat.Model;
using Projekat.Help;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using Projekat.Dijalozi;
using System.ComponentModel;
using Projekat.Tabele;
using System.IO;
using Projekat.Tutorijal;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private BazaPodataka baza;
        private string naziv;
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        private Tip tip;
        public Tip Tip
        {
            get { return tip; }
            set { tip = value; }
        }


        private string opis;
        public string Opis
        {
            get { return opis; }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }

        }
        private string oznaka;

        public string Oznaka
        {
            get { return oznaka; }
            set { oznaka = value; }
        }

        private string slika;

        public string Slika
        {
            get { return slika; }
            set
            {
                if (value != slika)
                {
                    slika = value;
                    OnPropertyChanged("Slika");
                }
            }
        }

        private DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        /*private int mapaBroj;

        public int MapaBroj
        {
            get { return mapaBroj; }
            set
            {
                if (value != mapaBroj)
                {
                    mapaBroj = value;
                    OnPropertyChanged("MapaBroj");
                }
            }
        }
        */

        private ObservableCollection<Etiketa> etikete;


        private string listaEtiketa;
        public string ListaEtiketa
        {
            get { return listaEtiketa; }
            set
            {
                if (value != listaEtiketa)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }

        private string oznakaTipa;

        public string OznakaTipa
        {
            get { return oznakaTipa; }
            set
            {
                if (value != oznakaTipa)
                {
                    oznakaTipa = value;
                    OnPropertyChanged("OznakaTipa");
                }
            }
        }

        private string oznakaNovogTipa;

        public string OznakaNovogTipa
        {
            get { return oznakaNovogTipa; }
            set
            {
                if (value != oznakaNovogTipa)
                {
                    oznakaNovogTipa = value;
                    OnPropertyChanged("OznakaNovogTipa");
                }
            }
        }

        private string nazivNovogTipa;

        public string NazivNovogTipa
        {
            get { return nazivNovogTipa; }
            set
            {
                if (value != nazivNovogTipa)
                {
                    nazivNovogTipa = value;
                    OnPropertyChanged("NazivNovogTipa");
                }
            }
        }

        private string opisNovogTipa;

        public string OpisNovogTipa
        {
            get { return opisNovogTipa; }
            set
            {
                if (value != opisNovogTipa)
                {
                    opisNovogTipa = value;
                    OnPropertyChanged("OpisNovogTipa");
                }
            }
        }

        private string slikaNovogTipa;

        public string SlikaNovogTipa
        {
            get { return slikaNovogTipa; }
            set
            {
                if (value != slikaNovogTipa)
                {
                    slikaNovogTipa = value;
                    OnPropertyChanged("SlikaNovogTipa");
                }
            }
        }

        private string oznakaEtikete;

        public string OznakaEtikete
        {
            get { return oznakaEtikete; }
            set
            {
                if (value != oznakaEtikete)
                {
                    oznakaEtikete = value;
                    OnPropertyChanged("OznakaEtikete");
                }
            }
        }

        private string opisEtikete;

        public string OpisEtikete
        {
            get { return opisEtikete; }
            set
            {
                if (value != opisEtikete)
                {
                    opisEtikete = value;
                    OnPropertyChanged("OpisEtikete");
                }
            }
        }

        private System.Windows.Media.Color boja;

        public System.Windows.Media.Color Boja
        {
            get { return boja; }
            set { boja = value; }
        }

        private Manifestacija m;
        private ObservableCollection<Manifestacija> manifList;

        public ObservableCollection<Manifestacija> ManifList
        {
            get { return manifList; }
            set
            {
                if (manifList != value)
                {

                    manifList = value;
                    OnPropertyChanged("ManifList");
                }
            }
        }
        



        private void ucitajSve()
        {
            foreach (Manifestacija m in baza.Manifestacije)
            {
                var canvas = canvasMap;
                if (m.Mapa == 2)
                {
                    canvas = canvasMap2;
                }
                if (m.Mapa == 3)
                {
                    canvas = canvasMap3;
                }
                if (m.Mapa == 4)
                {
                    canvas = canvasMap4;
                }
                bool result = canvas.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result)
                    continue;
                if (m.X != -1 || m.Y != -1)
                {
                    Image img = new Image();
                    if(!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox pusenje = new TextBox();
                    pusenje.IsEnabled = false;
                    if (m.Pusenje)
                        pusenje.Text = "Pusenje: Dozvoljeno";
                    else
                        pusenje.Text = "Pusenje: Zabranjeno";
                    wp.Children.Add(pusenje);

                    TextBox invalidi = new TextBox();
                    invalidi.IsEnabled = false;
                    if (m.Invalidi)
                        invalidi.Text = "Hendikepirani: Ima pristup";
                    else
                        invalidi.Text = "Hendikepirani: Nema pristup";
                    wp.Children.Add(invalidi);


                    TextBox mesto = new TextBox();
                    mesto.IsEnabled = false;
                    mesto.Text = "Mesto: " + m.Mesto;
                    wp.Children.Add(mesto);

                    TextBox alkohol = new TextBox();
                    alkohol.IsEnabled = false;
                    alkohol.Text = "Alkohol: " + m.Alkohol;
                    wp.Children.Add(alkohol);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cene: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox publika = new TextBox();
                    publika.IsEnabled = false;
                    publika.Text = "Publika: " + m.Publika;
                    wp.Children.Add(publika);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;

                    canvas.Children.Add(img);
                    Canvas.SetLeft(img, m.X-20);
                    Canvas.SetTop(img, m.Y-20);
                }
            }
        }

        public MainWindow()
        {
            
            slika = null;
            datum = DateTime.Today;

            Alkohol = new ObservableCollection<string>();
            Alkohol.Add("Nema alkohola");
            Alkohol.Add("Može se doneti alkohol");
            Alkohol.Add("Može se kupiti alkohol");

            Mesto = new ObservableCollection<string>();
            Mesto.Add("Na otvorenom");
            Mesto.Add("Na zatvorenom");

            Publika = new ObservableCollection<string>();
            Publika.Add("Mladi");
            Publika.Add("Sredovečni");
            Publika.Add("Stariji");

            Cena = new ObservableCollection<string>();
            Cena.Add("Besplatno");
            Cena.Add("Niske cene");
            Cena.Add("Srednje cene");
            Cena.Add("Visoke cene");


            etikete = new ObservableCollection<Etiketa>();
            tip = new Tip();
            m = new Manifestacija();
            
            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;  //glavni prozor se prikazuje u centru
            this.DataContext = this;

            if (!System.Windows.Forms.MessageBoxManager.Yes.Equals("Da"))
            {
                System.Windows.Forms.MessageBoxManager.OK = "U redu";
                System.Windows.Forms.MessageBoxManager.Yes = "Da";
                System.Windows.Forms.MessageBoxManager.No = "Ne";
                System.Windows.Forms.MessageBoxManager.Cancel = "Odustani";

                System.Windows.Forms.MessageBoxManager.Register();
            }
            

            baza = new BazaPodataka();
            baza.ucitajManifestacije();
            ucitajSve();

            manifList = baza.Manifestacije;
            baza.ucitajEtikete();
            baza.ucitajTipove();
            slika = "";
        }
        private ObservableCollection<string> alkohol;
        public ObservableCollection<string> Alkohol
        {
            get { return alkohol; }
            set { alkohol = value; }
        }
        private ObservableCollection<string> mesto;
        public ObservableCollection<string> Mesto
        {
            get { return mesto; }
            set { mesto = value; }
        }
        private ObservableCollection<string> publika;
        public ObservableCollection<string> Publika
        {
            get { return publika; }
            set { publika = value; }
        }
        private ObservableCollection<string> cena;
        private Point startPoint;

        public ObservableCollection<string> Cena
        {
            get { return cena; }
            set { cena = value; }
        }

        #region Click

        private void NovaManifestacija_Click(object sender, RoutedEventArgs e)
        {
            var s = new NovaManifestacija();
            s.ShowDialog();
            /*Manifestacija m = s.m;
                        int count = baza.Manifestacije.Count - 1;
                        baza.Manifestacije[count] = m;
              */
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;
            manifestacijaBox.ItemsSource = manifList;
        }

        private void NoviTip_Click(object sender, RoutedEventArgs e)
        {
            var s = new NoviTip();
            s.Show();
        }

        private void NovaEtiketa_Click(object sender, RoutedEventArgs e)
        {
            var s = new NovaEtiketa();
            s.Show();
        }

        private void PregledManifestacija_Click(object sender, RoutedEventArgs e)
        {
            var s = new PregledManifestacija();
            s.ShowDialog();
            if (s.idx != -1)
            {
                baza.Manifestacije[s.idx] = s.izmenjena;
                baza.sacuvajManifestaciju();
                manifList = baza.Manifestacije;
                Manifestacija m = s.izmenjena;
                Image zaMenjanje = null;
                int idx = 0;
                foreach (Image img in canvasMap.Children)
                {
                    if (img.Tag.Equals(m.Oznaka))
                    {
                        zaMenjanje = img;
                        break;
                    }
                    idx++;
                }
                if (zaMenjanje != null) {
                    
                        WrapPanel wp = new WrapPanel();
                        wp.Orientation = Orientation.Vertical;

                        TextBox oznaka = new TextBox();
                        oznaka.IsEnabled = false;
                        oznaka.Text = "Oznaka: " + m.Oznaka;
                        wp.Children.Add(oznaka);

                        TextBox naziv = new TextBox();
                        naziv.IsEnabled = false;
                        naziv.Text = "Naziv: " + m.Naziv;
                        wp.Children.Add(naziv);


                        TextBox tip = new TextBox();
                        tip.IsEnabled = false;
                        tip.Text = "Tip: " + m.Tip.Oznaka;
                        wp.Children.Add(tip);


                        TextBox opis = new TextBox();
                        opis.IsEnabled = false;
                        opis.Text = "Opis: " + m.Opis;
                        wp.Children.Add(opis);


                        TextBox datum = new TextBox();
                        datum.IsEnabled = false;
                        datum.Text = "Datum: " + m.Datum.ToShortDateString();
                        wp.Children.Add(datum);

                        TextBox pusenje = new TextBox();
                        pusenje.IsEnabled = false;
                        if (m.Pusenje)
                            pusenje.Text = "Pusenje: Dozvoljeno";
                        else
                            pusenje.Text = "Pusenje: Zabranjeno";
                        wp.Children.Add(pusenje);

                        TextBox invalidi = new TextBox();
                        invalidi.IsEnabled = false;
                        if (m.Invalidi)
                            invalidi.Text = "Hendikepirani: Ima pristup";
                        else
                            invalidi.Text = "Hendikepirani: Nema pristup";
                        wp.Children.Add(invalidi);


                        TextBox mesto = new TextBox();
                        mesto.IsEnabled = false;
                        mesto.Text = "Mesto: " + m.Mesto;
                        wp.Children.Add(mesto);

                        TextBox alkohol = new TextBox();
                        alkohol.IsEnabled = false;
                        alkohol.Text = "Alkohol: " + m.Alkohol;
                        wp.Children.Add(alkohol);

                        TextBox etikete = new TextBox();
                        etikete.IsEnabled = false;
                        etikete.Text = "Etikete:" + System.Environment.NewLine;
                        ListaEtiketa = "";
                        StringBuilder sb = new StringBuilder(ListaEtiketa);
                        ObservableCollection<Etiketa> list = m.Etikete;
                        foreach (Etiketa et in list)
                        {
                            sb.Append(et.Oznaka);
                            sb.Append(System.Environment.NewLine);
                        }
                        ListaEtiketa = sb.ToString();
                        etikete.Text += ListaEtiketa;
                        ListaEtiketa = "";
                        wp.Children.Add(etikete);

                        TextBox cena = new TextBox();
                        cena.IsEnabled = false;
                        cena.Text = "Cene: " + m.Cena;
                        wp.Children.Add(cena);

                        TextBox publika = new TextBox();
                        publika.IsEnabled = false;
                        publika.Text = "Publika: " + m.Publika;
                        wp.Children.Add(publika);

                        ToolTip tt = new ToolTip();
                        tt.Content = wp;
                         zaMenjanje.ToolTip = tt;
                    if(!m.Slika.Equals(""))
                        zaMenjanje.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        zaMenjanje.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    canvasMap.Children[idx] = zaMenjanje;
                        
                    }
            }
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;

            manifestacijaBox.ItemsSource = manifList;
            bool nePostoji = true;
            List<Image> zaBrisanje = new List<Image>();
            if (manifList.Count == 0) {
                int count = canvasMap.Children.Count;
                canvasMap.Children.RemoveRange(0,count);
                return;
            }
            foreach (Image img in canvasMap.Children)
            {
                foreach(Manifestacija ma in manifList)
                {
                    if (ma.Oznaka.Equals(img.Tag))
                        nePostoji = false;                   
                }
                if (nePostoji)
                {
                    zaBrisanje.Add(img);

                }
                nePostoji = true;
            }
            if (zaBrisanje != null) { 
            foreach(Image i in zaBrisanje)
                {
                    canvasMap.Children.Remove(i);
                }
            }
        }

        private void PregledTipova_Click(object sender, RoutedEventArgs e)
        {
            var s = new pregledTipova();
            s.ShowDialog();
            baza.ucitajManifestacije();
            manifList = baza.Manifestacije;
            manifestacijaBox.ItemsSource = manifList;
            foreach(Manifestacija m in baza.Manifestacije)
            {
                foreach(Image img in canvasMap.Children)
                {
                    if (img.Tag.Equals(m.Oznaka) && m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));
                }
            }
            bool nePostoji = true;
            List<Image> zaBrisanje = new List<Image>();
            if (manifList.Count == 0)
            {
                int count = canvasMap.Children.Count;
                canvasMap.Children.RemoveRange(0, count);
            }
            foreach (Image img in canvasMap.Children)
            {
                foreach (Manifestacija ma in manifList)
                {
                    if (ma.Oznaka.Equals(img.Tag))
                        nePostoji = false;

                    if (nePostoji)
                    {
                        zaBrisanje.Add(img);
                        nePostoji = true;
                    }
                }
            }
            if (zaBrisanje != null)
            {
                foreach (Image i in zaBrisanje)
                {
                    canvasMap.Children.Remove(i);
                }
            }
        }

        private void PregledEtiketa_Click(object sender, RoutedEventArgs e)
        {
            var s = new pregledEtiketa();
            s.ShowDialog();
           
        }


       
        private void About_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.Help.ShowHelp(null, @"..\..\Pomoc.chm");
       
        }

        #endregion Click

        private void Window_Help_Executed(object sender, ExecutedRoutedEventArgs e)

        {

            FrameworkElement source = e.Source as FrameworkElement;

            if (source != null)

            {

                string helpString = HelpProvider.GetHelpString(source);

                if (helpString != null)

                {

                    System.Windows.Forms.Help.ShowHelp(null, @"Pomoc.chm", System.Windows.Forms.HelpNavigator.KeywordIndex, helpString);


                }
                

            }
            

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void odabirIkonice_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            fileDialog.Title = "Izaberi ikonicu";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ikonica.Source = new BitmapImage(new Uri(fileDialog.FileName));
                slika = ikonica.Source.ToString();
            }
        }

        private void btnIkonica_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        

        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            sacuvaj_manifestaciju();
        }
        private void sacuvaj_manifestaciju()
        {
            if (naziv == "" || oznaka == "" || tip_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste uneli neophodne informacije!", "Dodavanje manifestacije");
                return;
            }


            DateTime d = datum;
            bool invalidi = false, pusenje = false;
            if (invalidiDa.IsChecked == true)
            {
                invalidi = true;
            }

            if (pusenjeDa.IsChecked == true)
            {
                pusenje = true;
            }
            String alkohol = "";
            if (alkohol_comboBox.SelectedIndex.Equals(-1))
                alkohol = "";

            else if (alkohol_comboBox.SelectedItem.Equals("Nema alkohola"))
            {
                int idx = Alkohol.IndexOf("Nema alkohola");
                alkohol = Alkohol[idx];
            }
            else if (alkohol_comboBox.SelectedItem.Equals("Nema alkohola"))
            {
                int idx = Alkohol.IndexOf("Može se doneti alkohol");
                alkohol = Alkohol[idx];
            }
            else
            {
                int idx = Alkohol.IndexOf("Može se kupiti alkohol");
                alkohol = Alkohol[idx];
            }

            String mesto = "";

            if (mesto_comboBox.SelectedIndex.Equals(-1))
                mesto = "";

            else if (mesto_comboBox.SelectedItem.Equals("Na otvorenom"))
            {
                int idx = Mesto.IndexOf("Na otvorenom");
                mesto = Mesto[idx];
            }
            else
            {
                int idx = Mesto.IndexOf("Na zatvorenom");
                mesto = Mesto[idx];
            }
            ObservableCollection<Etiketa> listaEtiketa = etikete;

            String cena = "";
            if (cene_comboBox.SelectedIndex.Equals(-1))
                cena = "";
            else if (cene_comboBox.SelectedItem.Equals("Besplatno"))
            {
                int idx = Cena.IndexOf("Besplatno");
                cena = Cena[idx];
            }
            else if (cene_comboBox.SelectedItem.Equals("Niske cene"))
            {
                int idx = Cena.IndexOf("Niske cene");
                cena = Cena[idx];
            }
            else if (cene_comboBox.SelectedItem.Equals("Srednje cene"))
            {
                int idx = Cena.IndexOf("Visoke cene");
                cena = Cena[idx];
            }
            else
            {
                int idx = Cena.IndexOf("Niske cene");
                cena = Cena[idx];
            }

            String publika = "";
            if (publika_comboBox.SelectedIndex.Equals(-1))
                publika = "";
            else if (publika_comboBox.SelectedItem.Equals("Mladi"))
            {
                int idx = Publika.IndexOf("Mladi");
                publika = Publika[idx];
            }
            else if (publika_comboBox.SelectedItem.Equals("Sredovečni"))
            {
                int idx = Publika.IndexOf("Sredovečni");
                publika = Publika[idx];
            }
            else
            {
                int idx = Publika.IndexOf("Stariji");
                publika = Publika[idx];
            }

            Opis = opis_textBox.Text;

            datum = (DateTime)datumPicker.SelectedDate;
            if (oznaka_textBox.Text.Equals("") || naziv_textBox.Text.Equals("") || tip_textBox.Text.Equals(""))
            {
                System.Windows.MessageBox.Show("Niste uneli neophodne podatke!", "Dodavanje manifestacije");
                return;
            }
            Manifestacija m = new Manifestacija(oznaka, naziv, opis, tip, alkohol, invalidi, pusenje, cena, datum, etikete, slika, mesto, publika);
            bool passed = baza.novaManifestacija(m);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno ste dodali novu manifestaciju.", "Dodavanje manifestacije");
                manifList = baza.Manifestacije;
                manifestacijaBox.ItemsSource = manifList;

                obrisiPoljaManifestacije();

            }
            else
            {
                for (int i = 0; i < manifList.Count(); i++)
                {
                    if (manifList[i].Oznaka.Equals(m.Oznaka))
                    {
                        manifList[i] = m;
                        break;
                    }
                }
                baza.Manifestacije = manifList;
                manifestacijaBox.ItemsSource = manifList;
                baza.sacuvajManifestaciju();
                obrisiPoljaManifestacije();
                System.Windows.MessageBox.Show("Izmenjena manifestacija sa tom oznakom!", "Izmena manifestacije");
            }
        }


        private void dropOnMe_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent("manifestacija"))
            {
                Manifestacija m = e.Data.GetData("manifestacija") as Manifestacija;
                
                int mapa = tabMape.SelectedIndex+1;
                var canvas = canvasMap;
                if (mapa == 2)
                {
                    canvas = canvasMap2;
                }
                if (mapa == 3)
                {
                    canvas = canvasMap3;
                }
                if (mapa == 4)
                {
                    canvas = canvasMap4;
                }
                int brojMape = 0;
                bool result = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result) brojMape = 1;
                bool result2 = canvasMap2.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result2) brojMape = 2;
                bool result3 = canvasMap3.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result3) brojMape = 3;
                bool result4 = canvasMap4.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result4) brojMape = 4;
                System.Windows.Point d0 = e.GetPosition(canvas);
                foreach (Manifestacija r0 in baza.Manifestacije)
                {
                    if (m.Oznaka != r0.Oznaka) //ako hoce da pomeri manifesatciju jako malo da ne okida dole
                    {
                        if (r0.X != -1 && r0.Y != -1)
                        {
                            
                            if (Math.Abs(r0.X - d0.X) <= 30 && Math.Abs(r0.Y - d0.Y) <= 30 && mapa == r0.Mapa)
                            {
                                System.Windows.MessageBox.Show("Manifestacija sa ovom lokacijom već postoji na mapi! Ponovo unesite manifestaciju na mapu.", "Premeštanje manifestacije na mapi");
                                ucitajSve();
                                return;
                            }
                        }
                    }
                }
                if (result == false && result2 == false && result3 == false && result4 == false)
                {

                    Image img = new Image();
                    if(!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    var positionX = e.GetPosition(canvas).X;
                    var positionY = e.GetPosition(canvas).Y;
                    //if (preklapanje == false) { 
                        m.X = positionX;
                        m.Y = positionY;
                    //}
                    
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox pusenje = new TextBox();
                    pusenje.IsEnabled = false;
                    if (m.Pusenje)
                        pusenje.Text = "Pusenje: Dozvoljeno";
                    else
                        pusenje.Text = "Pusenje: Zabranjeno";
                    wp.Children.Add(pusenje);

                    TextBox invalidi = new TextBox();
                    invalidi.IsEnabled = false;
                    if (m.Invalidi)
                        invalidi.Text = "Hendikepirani: Ima pristup";
                    else
                        invalidi.Text = "Hendikepirani: Nema pristup";
                    wp.Children.Add(invalidi);


                    TextBox mesto = new TextBox();
                    mesto.IsEnabled = false;
                    mesto.Text = "Mesto: " + m.Mesto;
                    wp.Children.Add(mesto);

                    TextBox alkohol = new TextBox();
                    alkohol.IsEnabled = false;
                    alkohol.Text = "Alkohol: " + m.Alkohol;
                    wp.Children.Add(alkohol);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cene: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox publika = new TextBox();
                    publika.IsEnabled = false;
                    publika.Text = "Publika: " + m.Publika;
                    wp.Children.Add(publika);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;
                    ObservableCollection<Manifestacija> manifestLst = baza.Manifestacije;
                    int idx = 0;
                    m.Mapa = mapa;
                    foreach (Manifestacija ma in manifestLst)
                    {
                        if (ma.Oznaka.Equals(m.Oznaka))
                            break;
                        idx++;
                    }
                    manifestLst[idx] = m;
                    baza.Manifestacije = manifestLst;//ovo dodato
                    baza.sacuvajManifestaciju();
                    canvas.Children.Add(img);
                    Canvas.SetLeft(img, positionX-20);
                    Canvas.SetTop(img, positionY-20);
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("Manifestacija sa ovom oznakom već postoji na mapi " + brojMape + "!", "Dodavanje manifestacije na mapu");

                }
            }
 
        }
         

      
        private void DraggedImageMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                System.Windows.Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;
                if (e.LeftButton == MouseButtonState.Pressed &&
                   (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                   Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedValue;
                    var canvas = canvasMap;
                    if (selektovana.Mapa == 2)
                    {
                        canvas = canvasMap2;
                    }
                    if (selektovana.Mapa == 3)
                    {
                        canvas = canvasMap3;
                    }
                    if (selektovana.Mapa == 4)
                    {
                        canvas = canvasMap4;
                    }
                    if (selektovana != null)
                    {
                        Image img = sender as Image;
                        canvas.Children.Remove(img);
                        DataObject dragData = new DataObject("manifestacija", selektovana);
                        DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                        
                    }

                }
                
                
            }
            catch
            {
                return;
            }
            
          
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {


            if (!e.Data.GetDataPresent("manifestacija") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
           
        }


        private void odabirEtiketa_Click(object sender, RoutedEventArgs e)
        {

            odabirEtikete s = new odabirEtikete();
            s.ShowDialog();
            ObservableCollection<Etiketa> temp = s.Odabrana;
            if (s.dodavanje)
            {
                ObservableCollection<Etiketa> pomocna = new ObservableCollection<Etiketa>();
                if (temp != null)
                {
                    bool b = false;
                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);

                    foreach (Etiketa et in etikete)
                    {
                        foreach (Etiketa et2 in temp)
                        {
                            if (et.Oznaka.Equals(et2.Oznaka))
                            {
                                b = true;
                            }
                            else b = false;
                            if (b)
                            {
                                pomocna.Add(et2);

                                sb.Append(et2.Oznaka);
                                sb.Append(System.Environment.NewLine);
                            }

                        }
                    }
                    etikete = pomocna;
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
            }
            else
            {

                if(temp!= null && temp.Count>0)
                {

                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    ObservableCollection<Etiketa> pomocna2 = etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    int idx = -1;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        idx = 0;
                        foreach (Etiketa et in etikete)
                        {
                            if (et.Oznaka.Equals(temp.ElementAt(i).Oznaka))
                                break;
                            idx++;
                        }
                        if (idx != -1)
                        {
                            etikete.RemoveAt(idx);
                        }
                    }
                    foreach (Etiketa et in temp)
                    {
                        etikete.Remove(et);
                    }

                    foreach (Etiketa et in etikete)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
                
            }
        }


        private void odabirTipa_Click(object sender, RoutedEventArgs e)
        {
            odabirTipa s = new odabirTipa();
            s.ShowDialog();

            //ovde odmah nastavlja dalje umesto da saceka na odabir tipa i zato je tip dole uvek null
            Tip temp = s.Odabran;
            if (temp != null)
            {
                tip = temp;
                oznakaTipa = tip.Oznaka;
                tip_textBox.Text = OznakaTipa;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (manifestacijaBox.SelectedValue is Manifestacija)
            {
                
                Manifestacija m = (Manifestacija)manifestacijaBox.SelectedValue;
                if (!m.Slika.Equals(""))
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                else
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));

                oznaka_textBox.Text = m.Oznaka;
                tip_textBox.Text = m.Tip.Oznaka;
                tip = m.Tip;
                opis_textBox.Text = m.Opis;
                naziv_textBox.Text = m.Naziv;
                datumPicker.Text = m.Datum.ToString();

                if (m.Pusenje) pusenjeDa.IsChecked = true;
                else pusenjeNe.IsChecked = true;

                if (m.Invalidi) invalidiDa.IsChecked = true;
                else invalidiNe.IsChecked = true;

                if (!m.Slika.Equals(""))
                {
                    slika = m.Slika;
                    ikonica.Source = new BitmapImage(new Uri(m.Slika));
                }

                else
                {
                    slika = m.Tip.Slika;
                    ikonica.Source = new BitmapImage(new Uri(m.Tip.Slika));
                }

                mesto_comboBox.SelectedValue = m.Mesto;
                alkohol_comboBox.SelectedValue = m.Alkohol;
                cene_comboBox.SelectedValue = m.Cena;
                publika_comboBox.SelectedValue = m.Publika;
                etikete_textBox.Text = "";
                etikete = m.Etikete;
                foreach (Etiketa et in m.Etikete)
                {
                    etikete_textBox.Text += et.Oznaka + '\n';
                }

            }
            else { PrikazIkonice.Source = null; }

            
        }

        private void PrikazIkonice_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                try { 
                    Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedValue;
                    if (selektovana != null)
                    {
                        DataObject dragData = new DataObject("manifestacija", selektovana);
                        DragDrop.DoDragDrop(PrikazIkonice, dragData, DragDropEffects.Move);
                        
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void PrikazIkonice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DraggedImagePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Manifestacija m in ManifList)
            {
                if (m.Oznaka.Equals(img.Tag)) { 
                    manifestacijaBox.SelectedValue = m;





                    if (!m.Slika.Equals(""))
                        PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    oznaka_textBox.Text = m.Oznaka;
                    tip_textBox.Text = m.Tip.Oznaka;
                    tip = m.Tip;
                    opis_textBox.Text = m.Opis;
                    naziv_textBox.Text = m.Naziv;
                    datumPicker.Text = m.Datum.ToString();

                    if (m.Pusenje) pusenjeDa.IsChecked = true;
                    else pusenjeNe.IsChecked = true;

                    if (m.Invalidi) invalidiDa.IsChecked = true;
                    else invalidiNe.IsChecked = true;

                    if (!m.Slika.Equals(""))
                    {
                        slika = m.Slika;
                        ikonica.Source = new BitmapImage(new Uri(m.Slika));
                    }

                    else
                    {
                        slika = m.Tip.Slika;
                        ikonica.Source = new BitmapImage(new Uri(m.Tip.Slika));
                    }

                    mesto_comboBox.SelectedValue = m.Mesto;
                    alkohol_comboBox.SelectedValue = m.Alkohol;
                    cene_comboBox.SelectedValue = m.Cena;
                    publika_comboBox.SelectedValue = m.Publika;
                    etikete_textBox.Text = "";
                    etikete = m.Etikete;
                    foreach (Etiketa et in m.Etikete)
                    {
                        etikete_textBox.Text += et.Oznaka + '\n';
                    }






                    if (!m.Slika.Equals(""))
                        PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));

                }
            }
            if (e.LeftButton == MouseButtonState.Released)
                e.Handled=true;

        }

        private void DraggedImagePreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Manifestacija man in ManifList)
            {
                if (man.Oznaka.Equals(img.Tag))
                    manifestacijaBox.SelectedValue = man;
            }
           

        }

        private void izmeniManifestacijuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manifestacijaBox.SelectedValue is Manifestacija)
            {
                Manifestacija m = (Manifestacija)manifestacijaBox.SelectedValue;


                var s = new izmeniManifestaciju(m);
                s.ShowDialog();
                if (s.idx != -1)
                {
                    baza.Manifestacije[s.idx] = s.Izmenjena;
                    baza.sacuvajManifestaciju();
                    manifList = baza.Manifestacije;
                    Manifestacija mm = s.Izmenjena;
                    Image zaMenjanje = null;
                    int idx = 0;
                    foreach (Image img in canvasMap.Children)
                    {
                        if (img.Tag.Equals(m.Oznaka))
                        {
                            zaMenjanje = img;
                            break;
                        }
                        idx++;
                    }
                    if (zaMenjanje != null)
                    {

                        WrapPanel wp = new WrapPanel();
                        wp.Orientation = Orientation.Vertical;

                        TextBox oznaka = new TextBox();
                        oznaka.IsEnabled = false;
                        oznaka.Text = "Oznaka: " + mm.Oznaka;
                        wp.Children.Add(oznaka);

                        TextBox naziv = new TextBox();
                        naziv.IsEnabled = false;
                        naziv.Text = "Naziv: " + mm.Naziv;
                        wp.Children.Add(naziv);


                        TextBox tip = new TextBox();
                        tip.IsEnabled = false;
                        tip.Text = "Tip: " + mm.Tip.Oznaka;
                        wp.Children.Add(tip);


                        TextBox opis = new TextBox();
                        opis.IsEnabled = false;
                        opis.Text = "Opis: " + mm.Opis;
                        wp.Children.Add(opis);


                        TextBox datum = new TextBox();
                        datum.IsEnabled = false;
                        datum.Text = "Datum: " + mm.Datum;
                        wp.Children.Add(datum);

                        TextBox pusenje = new TextBox();
                        pusenje.IsEnabled = false;
                        if (mm.Pusenje)
                            pusenje.Text = "Pusenje: Dozvoljeno";
                        else
                            pusenje.Text = "Pusenje: Zabranjeno";
                        wp.Children.Add(pusenje);

                        TextBox invalidi = new TextBox();
                        invalidi.IsEnabled = false;
                        if (mm.Invalidi)
                            invalidi.Text = "Hendikepirani: Ima pristup";
                        else
                            invalidi.Text = "Hendikepirani: Nema pristup";
                        wp.Children.Add(invalidi);


                        TextBox mesto = new TextBox();
                        mesto.IsEnabled = false;
                        mesto.Text = "Mesto: " + mm.Mesto;
                        wp.Children.Add(mesto);

                        TextBox alkohol = new TextBox();
                        alkohol.IsEnabled = false;
                        alkohol.Text = "Alkohol: " + mm.Alkohol;
                        wp.Children.Add(alkohol);

                        TextBox etikete = new TextBox();
                        etikete.IsEnabled = false;
                        etikete.Text = "Etikete:" + System.Environment.NewLine;
                        ListaEtiketa = "";
                        StringBuilder sb = new StringBuilder(ListaEtiketa);
                        ObservableCollection<Etiketa> list = mm.Etikete;
                        foreach (Etiketa et in list)
                        {
                            sb.Append(et.Oznaka);
                            sb.Append(System.Environment.NewLine);
                        }
                        ListaEtiketa = sb.ToString();
                        etikete.Text += ListaEtiketa;
                        ListaEtiketa = "";
                        wp.Children.Add(etikete);

                        TextBox cena = new TextBox();
                        cena.IsEnabled = false;
                        cena.Text = "Cene: " + mm.Cena;
                        wp.Children.Add(cena);

                        TextBox publika = new TextBox();
                        publika.IsEnabled = false;
                        publika.Text = "Publika: " + mm.Publika;
                        wp.Children.Add(publika);

                        ToolTip tt = new ToolTip();
                        tt.Content = wp;
                        zaMenjanje.ToolTip = tt;
                        if(!mm.Slika.Equals(""))
                            zaMenjanje.Source = new BitmapImage(new Uri(mm.Slika));
                        else
                            zaMenjanje.Source = new BitmapImage(new Uri(mm.Tip.Slika));

                        canvasMap.Children[idx] = zaMenjanje;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijednu manifestaciju.", "Izmena manifestacije");

            }
        }

        private void obrisiManifestacijuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (manifestacijaBox.SelectedItems.Count == 1) {
                Manifestacija m = null;
                if (manifestacijaBox.SelectedValue is Manifestacija)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete manifestaciju?", "Brisanje manifestacije", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            
                            m = (Manifestacija)manifestacijaBox.SelectedValue;
                            baza.brisanjeManifestacije(m);
                            Image zaBrisanje = null;
                            foreach (Image img in canvasMap.Children)
                            {
                                if (m.Oznaka.Equals(img.Tag))
                                {
                                    zaBrisanje = img;
                                }
                            }
                            if (zaBrisanje != null)
                                canvasMap.Children.Remove(zaBrisanje);
                            manifList = baza.Manifestacije;
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }


                }
            }
            else if (manifestacijaBox.SelectedItems.Count > 1)
            {
                ObservableCollection<Manifestacija> list = new ObservableCollection<Manifestacija>();
                Manifestacija m = null;
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete manifestacije?", "Brisanje manifestacija", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try { 
                            foreach (Manifestacija ma in manifestacijaBox.SelectedItems)
                            {
                                if(ma is Manifestacija)
                                    list.Add(ma);
                            }
                            foreach (Manifestacija ma in list) { 
                                m = ma;
                                baza.brisanjeManifestacije(m);
                                Image zaBrisanje = null;
                                foreach (Image img in canvasMap.Children)
                                {
                                    if(m.Oznaka.Equals(img.Tag))
                                    {
                                        zaBrisanje = img;
                                    }
                                }
                                if (zaBrisanje != null)
                                    canvasMap.Children.Remove(zaBrisanje);
                            }
                            manifList = baza.Manifestacije;
                            break;
                        }
                        catch
                        {
                            //System.Windows.MessageBox.Show("Odabrana je prazna manifestacija za brisanje!", "Brisanje manifestacije");
                            return;
                        }
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
                

            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali manifestaciju za brisanje!", "Brisanje manifestacije");

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedItem;
                if (selektovana != null)
                {
                    var canvas = canvasMap;
                    if (selektovana.Mapa == 2)
                    {
                        canvas = canvasMap2;
                    }
                    if (selektovana.Mapa == 3)
                    {
                        canvas = canvasMap3;
                    }
                    if (selektovana.Mapa == 4)
                    {
                        canvas = canvasMap4;
                    }
                    bool postoji = canvas.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == selektovana.Oznaka);
                    if (postoji)
                    {
                        Image img = null;
                        foreach(Image i in canvas.Children)
                        {
                            if (i.Tag.Equals(selektovana.Oznaka))
                                img = i;
                        }
                        canvas.Children.Remove(img);
                        int idx = 0;
                        foreach (Manifestacija m in manifList)
                        {
                            if (selektovana.Oznaka.Equals(m.Oznaka))
                                break;
                            idx++;
                        }
                        baza.Manifestacije[idx].X = -1;
                        baza.Manifestacije[idx].Y = -1;
                        baza.Manifestacije[idx].Mapa = 0;
                        baza.sacuvajManifestaciju();
                        manifList = baza.Manifestacije;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Manifestacija se ne nalazi na mapi!", "Ukloni sa mape");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Nijedna manifestacija nije odabrana!", "Ukloni sa mape");
                }
            }
            catch
            {
                return;
            }
            
        }

        private void obrisiSve_Click(object sender, RoutedEventArgs e)
        {
            obrisiPoljaManifestacije();
        }

        private void obrisiPoljaManifestacije()
        {
            naziv_textBox.Text = "";
            tip_textBox.Text = "";
            oznaka_textBox.Text = "";
            datumPicker.SelectedDate = DateTime.Now;
            etikete_textBox.Text = "";
            opis_textBox.Text = "";
            mesto_comboBox.SelectedIndex = -1;
            publika_comboBox.SelectedIndex = -1;
            cene_comboBox.SelectedIndex = -1;
            alkohol_comboBox.SelectedIndex = -1;
            ikonica.Source = new BitmapImage(new Uri(@"Images\placeholder.png", UriKind.Relative));
            pusenjeDa.IsChecked = false;
            pusenjeNe.IsChecked = false;
            invalidiDa.IsChecked = false;
            invalidiNe.IsChecked = false;
            slika = "";
        }

        private void izaberiIkonicuTip_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            fileDialog.Title = "Izaberi ikonicu";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ikonicaTipa.Source = new BitmapImage(new Uri(fileDialog.FileName));
                slikaNovogTipa = fileDialog.FileName;
            }
        }

        private void sacuvajTip_Click(object sender, RoutedEventArgs e)
        {
            sacuvaj_tip();
        }
        private void sacuvaj_tip()
        {
            if (oznakaTipa_textBox.Text != "" && nazivTipa_textBox.Text != "" && slikaNovogTipa != null)
            {
                Tip tip = new Tip(oznakaNovogTipa, nazivNovogTipa, opisNovogTipa, slikaNovogTipa);
                bool passed = baza.novTip(tip);
                if (passed)
                {
                    System.Windows.MessageBox.Show("Uspešno dodavanje novog tipa.", "Uspeh!");
                    baza.sacuvajTip();
                }
                else
                    System.Windows.MessageBox.Show("Vec postoji tip sa tom oznakom!", "Greska!");
            }
            else
            {
                System.Windows.MessageBox.Show("Niste uneli sve obavezne podatke!", "Greska!");

            }
        }

        private void obrisiTip_Click(object sender, RoutedEventArgs e)
        {
            obrisi_polja_tipa();
        }
        
        private void obrisi_polja_tipa()
        {
            oznakaTipa_textBox.Text = "";
            nazivTipa_textBox.Text = "";
            opisTipa_textBox.Text = "";
            ikonicaTipa.Source = null;
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sacuvaj_etiketu();
        }
        
        private void sacuvaj_etiketu()
        {
            Etiketa etiketa = new Etiketa(oznakaEtikete, opisEtikete, boja);
            bool passed = baza.novaEtiketa(etiketa);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno dodavanje nove etikete.", "Uspeh!");
                baza.sacuvajEtiketu();
            }
            else
                System.Windows.MessageBox.Show("Vec postoji etiketa sa tom oznakom!", "Greska!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            obrisi_polja_etikete();
        }

        private void obrisi_polja_etikete()
        {
            oznakaEtikete_textBox.Text = "";
            opisEtikete_textBox.Text = "";
        }


        private void BojaEtiketePicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            System.Windows.Media.Color mediacolor = bojaEtiketePicker.SelectedColor.Value;
            boja = System.Windows.Media.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        }

        private void ManifestacijaBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ManifestacijaBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Manifestacija selektovana = (Manifestacija)manifestacijaBox.SelectedValue;
                if (selektovana != null)
                {
                    if (e.Key == Key.Delete)
                    {
                        Image img = null;
                        foreach (Image i in canvasMap.Children)
                        {
                            if (i.Tag.Equals(selektovana.Oznaka))
                            {
                                img = i;
                                break;
                            }
                        }
                        if (img != null)
                        {
                            canvasMap.Children.Remove(img);
                        }

                        foreach (Manifestacija m in manifList)
                        {
                            if (selektovana.Oznaka.Equals(m.Oznaka))
                            {
                                baza.brisanjeManifestacije(m);
                                break;
                            }
                        }
                        manifList = baza.Manifestacije;
                        obrisiPoljaManifestacije();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Nijedna manifestacija nije selektovana!", "Brisanje manifestacije!");
                }
            }
            catch
            {
                return;
            }
        }

        private void ManifestacijaBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (manifestacijaBox.SelectedValue is Manifestacija)
            {
                Manifestacija m = (Manifestacija)manifestacijaBox.SelectedValue;
                if (!m.Slika.Equals(""))
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                else
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));

                oznaka_textBox.Text = m.Oznaka;
                tip_textBox.Text = m.Tip.Oznaka;
                tip = m.Tip;
                opis_textBox.Text = m.Opis;
                naziv_textBox.Text = m.Naziv;
                datumPicker.Text = m.Datum.ToString();

                if (m.Pusenje) pusenjeDa.IsChecked = true;
                else pusenjeNe.IsChecked = true;

                if (m.Invalidi) invalidiDa.IsChecked = true;
                else invalidiNe.IsChecked = true;

                if (!m.Slika.Equals(""))
                {
                    slika = m.Slika;
                    ikonica.Source = new BitmapImage(new Uri(m.Slika));
                }

                else
                {
                    slika = m.Tip.Slika;
                    ikonica.Source = new BitmapImage(new Uri(m.Tip.Slika));
                }

                mesto_comboBox.SelectedValue = m.Mesto;
                alkohol_comboBox.SelectedValue = m.Alkohol;
                cene_comboBox.SelectedValue = m.Cena;
                publika_comboBox.SelectedValue = m.Publika;
                etikete_textBox.Text = "";
                etikete = m.Etikete;
                foreach (Etiketa et in m.Etikete)
                {
                    etikete_textBox.Text += et.Oznaka + '\n';
                }

            }
            else { PrikazIkonice.Source = null; }
        }

        private void MainWIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Equals("M") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabForme.SelectedIndex = 0;
                oznaka_textBox.Focus();
                oznaka_textBox.Focus();
            }
            if (e.Key.ToString().Equals("T") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabForme.SelectedIndex = 1;
                oznakaTipa_textBox.Focus();
            }
            if (e.Key.ToString().Equals("E") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabForme.SelectedIndex = 2;
                oznakaEtikete_textBox.Focus();
            }
            if (e.Key.ToString().Equals("D1") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabMape.SelectedIndex = 0;
            }
            if (e.Key.ToString().Equals("D2") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabMape.SelectedIndex = 1;
            }
            if (e.Key.ToString().Equals("D3") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabMape.SelectedIndex = 2;
            }
            if (e.Key.ToString().Equals("D4") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                tabMape.SelectedIndex = 3;
            }
            if (e.Key.ToString().Equals("Left"))
            {
                _ = tabMape.SelectedIndex == 0 ? tabMape.SelectedIndex = 3 : tabMape.SelectedIndex--;
            }
            if (e.Key.ToString().Equals("Right"))
            {
                _ = tabMape.SelectedIndex == 3 ? tabMape.SelectedIndex = 0 : tabMape.SelectedIndex++;
            }



        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            pretraga_filter();
        }

        private void pretraga_filter()
        {
            List<Manifestacija> temp = new List<Manifestacija>();
            foreach (Manifestacija m in manifList)
            {
                bool ind = true;
                if (!m.Naziv.Contains(pretraga_textBox.Text))
                {
                    ind = false;
                }
                if (alkohol_nema.IsChecked == false && m.Alkohol.Equals("Nema alkohola")) ind = false;
                if (alkohol_doneti.IsChecked == false && m.Alkohol.Equals("Može se doneti alkohol")) ind = false;
                if (alkohol_kupiti.IsChecked == false && m.Alkohol.Equals("Može se kupiti alkohol")) ind = false;

                if (cene_besplatno.IsChecked == false && m.Cena.Equals("Besplatno")) ind = false;
                if (cene_niske.IsChecked == false && m.Cena.Equals("Niske cene")) ind = false;
                if (cene_srednje.IsChecked == false && m.Cena.Equals("Srednje cene")) ind = false;
                if (cene_visoke.IsChecked == false && m.Cena.Equals("Visoke cene")) ind = false;

                if (mesto_napolju.IsChecked == false && m.Mesto.Equals("Na otvorenom")) ind = false;
                if (mesto_unutra.IsChecked == false && m.Mesto.Equals("Na zatvorenom")) ind = false;

                if (publika_mladi.IsChecked == false && m.Publika.Equals("Mladi")) ind = false;
                if (publika_sredovecni.IsChecked == false && m.Publika.Equals("Sredovečni")) ind = false;
                if (publika_stariji.IsChecked == false && m.Publika.Equals("Stariji")) ind = false;

                if (pusenje_da.IsChecked == false && m.Pusenje) ind = false;
                if (pusenje_ne.IsChecked == false && !m.Pusenje) ind = false;

                if (invalidi_da.IsChecked == false && m.Invalidi) ind = false;
                if (invalidi_ne.IsChecked == false && !m.Invalidi) ind = false;

                if (ind) temp.Add(m);

            }
            manifestacijaBox.ItemsSource = temp;
            canvasMap.Children.Clear();
            canvasMap2.Children.Clear();
            canvasMap3.Children.Clear();
            canvasMap4.Children.Clear();
            foreach (Manifestacija m in temp)
            {
                var canvas = canvasMap;
                if (m.Mapa == 2)
                {
                    canvas = canvasMap2;
                }
                if (m.Mapa == 3)
                {
                    canvas = canvasMap3;
                }
                if (m.Mapa == 4)
                {
                    canvas = canvasMap4;
                }
                bool result = canvas.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                if (result)
                    continue;
                if (m.X != -1 || m.Y != -1)
                {
                    Image img = new Image();
                    if (!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox pusenje = new TextBox();
                    pusenje.IsEnabled = false;
                    if (m.Pusenje)
                        pusenje.Text = "Pusenje: Dozvoljeno";
                    else
                        pusenje.Text = "Pusenje: Zabranjeno";
                    wp.Children.Add(pusenje);

                    TextBox invalidi = new TextBox();
                    invalidi.IsEnabled = false;
                    if (m.Invalidi)
                        invalidi.Text = "Hendikepirani: Ima pristup";
                    else
                        invalidi.Text = "Hendikepirani: Nema pristup";
                    wp.Children.Add(invalidi);


                    TextBox mesto = new TextBox();
                    mesto.IsEnabled = false;
                    mesto.Text = "Mesto: " + m.Mesto;
                    wp.Children.Add(mesto);

                    TextBox alkohol = new TextBox();
                    alkohol.IsEnabled = false;
                    alkohol.Text = "Alkohol: " + m.Alkohol;
                    wp.Children.Add(alkohol);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cene: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox publika = new TextBox();
                    publika.IsEnabled = false;
                    publika.Text = "Publika: " + m.Publika;
                    wp.Children.Add(publika);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;

                    canvas.Children.Add(img);
                    Canvas.SetLeft(img, m.X - 20);
                    Canvas.SetTop(img, m.Y - 20);
                }
            }
        }

        private void Pusenje_da_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Pusenje_ne_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Invalidi_da_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Invalidi_ne_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Alkohol_nema_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Alkohol_doneti_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Alkohol_kupiti_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Cene_besplatno_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Cene_niske_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Cene_srednje_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Cene_visoke_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Mesto_napolju_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Mesto_unutra_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Publika_mladi_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Publika_sredovecni_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Publika_stariji_Click(object sender, RoutedEventArgs e)
        {
            pretraga_filter();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Equals("S") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                sacuvaj_manifestaciju();
                e.Handled = true;
            }
            if (e.Key.ToString().Equals("D") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                obrisiPoljaManifestacije();
                e.Handled = true;
            }
        }

        private void Grid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Equals("S") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                sacuvaj_tip();
                obrisi_polja_tipa();
                e.Handled = true;
            }
            if (e.Key.ToString().Equals("D") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                obrisi_polja_tipa();
                e.Handled = true;
            }
        }

        private void Grid_KeyDown_2(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Equals("S") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                sacuvaj_etiketu();
                obrisi_polja_etikete();
                e.Handled = true;
            }
            if (e.Key.ToString().Equals("D") && Keyboard.Modifiers == ModifierKeys.Control)
            {
                obrisi_polja_etikete();
                e.Handled = true;
            }
        }

        private void Tutorijal_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window1();
            w.Show();
            this.Close();
        }

        
    }
}

