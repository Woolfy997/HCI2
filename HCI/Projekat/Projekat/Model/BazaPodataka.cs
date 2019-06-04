using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Projekat.Model
{
    class BazaPodataka
    {
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();
        private ObservableCollection<Manifestacija> manifestacije = new ObservableCollection<Manifestacija>();
        private ObservableCollection<Tip> tipovi = new ObservableCollection<Tip>();
        

        private string pathEtiketa = null;
        private string pathManifestacija = null;
        private string pathTipova = null;
        
        
        public BazaPodataka()
        {
            pathEtiketa = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "etikete.txt");
            pathManifestacija = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "manifestacije.txt");
            pathTipova = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tipovi.txt");
            ucitajEtikete();
            ucitajManifestacije();
            ucitajTipove();
            sacuvajManifestaciju();

        }

        public void ucitajEtikete()
        {

            if (File.Exists(pathEtiketa))
            {

                using (StreamReader reader = File.OpenText(pathEtiketa))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    etikete = (ObservableCollection<Etiketa>)serializer.Deserialize(reader, typeof(ObservableCollection<Etiketa>));
                }
            }
            else
            {
                etikete = new ObservableCollection<Etiketa>();
            }


        }


        public void ucitajManifestacije()
        {


            if (File.Exists(pathManifestacija))
            {

                using (StreamReader reader = File.OpenText(pathManifestacija))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    manifestacije = (ObservableCollection<Manifestacija>)serializer.Deserialize(reader, typeof(ObservableCollection<Manifestacija>));
                }
               
            }
            else
            {
                manifestacije = new ObservableCollection<Manifestacija>();
            }



        }


        public void ucitajTipove()
        {
            if (File.Exists(pathTipova))
            {

                using (StreamReader reader = File.OpenText(pathTipova))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    tipovi = (ObservableCollection<Tip>)serializer.Deserialize(reader, typeof(ObservableCollection<Tip>));
                }
            }
            else
            {
                tipovi = new ObservableCollection<Tip>();
            }


        }

        
        public void save()
        {

            using (StreamWriter writer = File.CreateText(pathEtiketa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, etikete);
                writer.Close();
            }

            using (StreamWriter writer = File.CreateText(pathManifestacija))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, manifestacije);
                writer.Close();
            }


            using (StreamWriter writer = File.CreateText(pathTipova))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, tipovi);
                writer.Close();
            }

       
        }


        public void sacuvajManifestaciju()
        {
            using (StreamWriter writer = File.CreateText(pathManifestacija))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, manifestacije);
                writer.Close();
            }


        }



        public void sacuvajEtiketu()
        {
            using (StreamWriter writer = File.CreateText(pathEtiketa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, etikete);
                writer.Close();
            }


        }



        public void sacuvajTip()
        {
            using (StreamWriter writer = File.CreateText(pathTipova))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, tipovi);
                writer.Close();
            }


        }

   

        public bool novaManifestacija(Manifestacija m)
        {
            foreach (Manifestacija m1 in manifestacije)
            {
                if (m1.Oznaka == m.Oznaka)
                {

                    return false;
                }
            }
            Manifestacije.Add(m);
            sacuvajManifestaciju();
          
            return true;
        }


        public bool novaEtiketa(Etiketa e)
        {
            foreach (Etiketa e1 in etikete)
            {
                if (e1.Oznaka == e.Oznaka)
                {

                    return false;
                }
            }
            etikete.Add(e);
            sacuvajEtiketu();
            return true;

        }

        public bool novTip(Tip t)
        {

            foreach (Tip t1 in tipovi)
            {
                if (t1.Oznaka == t.Oznaka)
                {

                    return false;
                }
            }
            tipovi.Add(t);
            sacuvajTip();
            return true;

        }

        public bool brisanjeManifestacije(Manifestacija m)
        {

            foreach (Manifestacija l1 in manifestacije)
            {
                if (l1.Oznaka == m.Oznaka)
                {
                    manifestacije.Remove(m);
                    sacuvajManifestaciju();

                    return true;
                }
            }

            return false;
        }



        public bool brisanjeEtikete(Etiketa e)
        {

            foreach (Etiketa e1 in etikete)
            {
                if (e1.Oznaka == e.Oznaka)
                {
                    etikete.Remove(e);
                    sacuvajEtiketu();
                    return true;
                }
            }

            return false;
        }


        public bool brisanjeTipa(Tip t)
        {

            foreach (Tip t1 in tipovi)
            {
                if (t1.Oznaka == t.Oznaka)
                {
                    tipovi.Remove(t);
                    sacuvajTip();
                    return true;
                }
            }

            return false;
        }
        

        public ObservableCollection<Manifestacija> Manifestacije
        {
            get{ return manifestacije; }
            set { manifestacije = value; }
        }

        public ObservableCollection<Tip> Tipovi
        {
            get{ return tipovi; }
            set { tipovi = value; }
        }

        public ObservableCollection<Etiketa> Etikete
        {

            get{ return etikete; }
            set { etikete = value; }
        }

    }
}

