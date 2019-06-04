using Microsoft.Win32;
using Projekat.Dijalozi;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Projekat.Tutorijal
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : INotifyPropertyChanged
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

        private ObservableCollection<Etiketa> etikete;

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

        public ObservableCollection<string> Cena
        {
            get { return cena; }
            set { cena = value; }
        }
        private Manifestacija m;

        


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

        public Window1()
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
            

            baza = new BazaPodataka();

            baza.ucitajEtikete();
            baza.ucitajTipove();
            slika = "";
        }

        private void Popuni_formu_Click(object sender, RoutedEventArgs e)
        {
            naziv_textBox.Text = "EXIT";
            tip_textBox.Text = "Muzicka manifestacija";
            oznaka_textBox.Text = "EXT2019";
            datumPicker.SelectedDate = DateTime.Now;
            etikete_textBox.Text = "Tribe";
            opis_textBox.Text = "Najveci muzicki festival u Srbiji.";
            mesto_comboBox.SelectedIndex = 0;
            publika_comboBox.SelectedIndex = 0;
            cene_comboBox.SelectedIndex = 3;
            alkohol_comboBox.SelectedIndex = 2;
            ikonica.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"..\..\Images\exit.png"), UriKind.Absolute));
            pusenjeDa.IsChecked = true;
            pusenjeNe.IsChecked = false;
            invalidiDa.IsChecked = false;
            invalidiNe.IsChecked = true;
            slika = System.IO.Path.GetFullPath(@"..\..\Images\exit.png");
        }

        private void Kraj_Click(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void OdabirTipa_Click(object sender, RoutedEventArgs e)
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

        private void OdabirEtiketa_Click(object sender, RoutedEventArgs e)
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

                if (temp != null && temp.Count > 0)
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

        private void OdabirIkonice_Click(object sender, RoutedEventArgs e)
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

        private void ObrisiSve_Click(object sender, RoutedEventArgs e)
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
            ikonica.Source = new BitmapImage(new Uri(@"..\Images\placeholder.png", UriKind.Relative));
            pusenjeDa.IsChecked = false;
            pusenjeNe.IsChecked = false;
            invalidiDa.IsChecked = false;
            invalidiNe.IsChecked = false;
            slika = "";
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (naziv == "" || oznaka == "" || tip_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste uneli obavezne podatke!\nDodavanje/izmena neuspesno!", "Tutorijal");
                return;
            }
            else
            {
                System.Windows.MessageBox.Show("Obavezni podaci su uneti!\nDodavanje/izmena uspesno!", "Tutorijal");
            }
        }
    }
}
