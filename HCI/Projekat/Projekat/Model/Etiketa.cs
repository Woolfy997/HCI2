using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Projekat.Model
{
  public  class Etiketa: INotifyPropertyChanged
    {
    private string oznaka = "";
    private string opis = "";
    private System.Windows.Media.Color boja;


    public string Oznaka
    {
        get
        {
            return oznaka;
        }
        set
        {
            if (value != oznaka)
            {
                oznaka = value;
                OnPropertyChanged("Oznaka");
            }
        }
    }

    public System.Windows.Media.Color Boja
    {
        get
        {
            return boja;
        }
        set
        {
            if (value != boja)
            {
                boja = value;
                OnPropertyChanged("Boja");
            }
        }
    }

        public SolidColorBrush Color
        {
            get { return new SolidColorBrush(boja); }
            set { }
        }

    public string Opis
    {
        get
        {
            return opis;
        }
        set
        {
            if (value != opis)
            {
                opis = value;
                OnPropertyChanged("Opis");
            }
        }
    }

        public Etiketa(string o, string op, System.Windows.Media.Color c)
        {

            oznaka = o;
            opis = op;
            boja = c;

        }

        public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged(string v)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
    }


    public void setAll(Etiketa e)
    {
        oznaka = e.oznaka;
        boja = e.boja;
        opis = e.opis;
    }



}
}
