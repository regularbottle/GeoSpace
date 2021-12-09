using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace GeoSpace
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MoreCities Lista = new MoreCities();
        double Longitudine1 = 0;
        double Longi1 = 0;
        double Longitudine2 = 0;
        double Latitudine1 = 0;
        double Lati1 = 0;
        double Latitudine2 = 0;
        string URL = "";
        string URL2 = "https://google.com";

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lbxLista1.ItemsSource = Lista;
            lbxLista2.ItemsSource = Lista;
            wbGoogle.Source = new Uri(URL2, UriKind.Absolute);
        }

        private void txtCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Lista != null)
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    lbxLista1.ItemsSource = Lista.Contiene(t.Text);
                    lbxLista1.Items.Refresh();
                }
            }
        }

        private void txtCity2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Lista != null)
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    lbxLista2.ItemsSource = Lista.Contiene(t.Text);
                    lbxLista2.Items.Refresh();
                }
            }
        }

        private void btnDistanza_Click(object sender, RoutedEventArgs e)
        {
            double Distanza = Lossodromica(Lati1, Latitudine2, Longi1, Longitudine2);
            double Distanza2 = Ortodromica(Lati1, Latitudine2, Longitudine2, Longi1);
            lblDistanzaL.Content = String.Format("{0:0.00}", Distanza) + "Km";
            lblDistanzaO.Content = String.Format("{0:0.00}", Distanza2) + "Km";
        }

        public double Lossodromica(double a1, double a2, double b2, double b1)
        {
            double D = Math.Acos(Math.Cos(RadiantNumber(a1 - a2)) * Math.Cos(RadiantNumber(b1)) * Math.Cos(RadiantNumber(b2)) + Math.Sin(RadiantNumber(b1)) * Math.Sin(RadiantNumber(b2)));
            return D * 6378;
        }

        public double Ortodromica(double LatitudineA, double LatitudineB, double LongitudineB, double LongitudineA)
        {
            var R = 6371; // km
            var dLat = RadiantNumber(LatitudineB - LatitudineA);
            var dLon = RadiantNumber(LongitudineB - LongitudineA);
            var lat1 = RadiantNumber(LatitudineA);
            var lat2 = RadiantNumber(LatitudineB);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        public double RadiantNumber(double Numero)
        { return Convert.ToDouble(Numero) * Math.PI / 180; }

        private void lbxLista1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bimg = new BitmapImage();
            string LAT, LONG;
            City Ciao = (City)lbxLista1.SelectedItem;
            City(Ciao.Index, out bimg, out LAT, out LONG, out Latitudine1, out Longitudine1);
            Lati1 = Latitudine1;
            Longi1 = Longitudine1;
            lblNome1.Content = Ciao.Nome;
            lblLatitudine1.Content = LAT;
            lblLongitudine1.Content = LONG;
            imgCity1.Source = bimg;
        }

        private void lbxLista2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnDistanza.IsEnabled = true;
            lblDistanza.IsEnabled = true;
            BitmapImage bimg = new BitmapImage();
            City Ciao = (City)lbxLista2.SelectedItem;
            string LAT, LONG;
            City(Ciao.Index, out bimg, out LAT, out LONG, out Latitudine2, out Longitudine2);
            lblNome2.Content = "Nome: " +Ciao.Nome;
            lblLatitudine2.Content = LAT;
            lblLongitudine2.Content = LONG;
            imgCity2.Source = bimg;
        }

        private void City(int Indice, out BitmapImage bimg, out string LAT, out string LONG, out double LAT1, out double LONG1)
        {
            txtCity2.IsEnabled = true;
            lblNome2.IsEnabled = true;
            lblLongitudine2.IsEnabled = true;
            lblLatitudine2.IsEnabled = true;
            lbxLista2.IsEnabled = true;
            imgCity2.IsEnabled = true;
            LAT = "";
            LONG = "";
            LAT1 = 0;
            LONG1 = 0;
            string Nome = "";
            string Longitudine = "";
            string Latitudine = "";
            int Trovato = 0;
            bimg = new BitmapImage();
            for (int i = 0; i < Lista.Count(); i++)
            {
                if (Indice == Lista[i].Index)
                {
                    Nome = Lista[i].Nome;
                    Longitudine = Lista[i].Longitudine;
                    Latitudine = Lista[i].Latitudine;
                    LONG1 = Lista[i].Longitudine1;
                    LAT1 = Lista[i].Latitudine1;
                    //URL2 = "https://en.m.wikipedia.org/wiki/" + Lista[i].Nome;
                    Trovato = 1;
                }
            }
            if (Trovato == 0)
            { MessageBox.Show("La Città che cerchi non si trova nel nostro database"); }
            if (Trovato == 1)
            {
                LAT = "Latitudine: " + Latitudine.Substring(0, 2) + "°" + Latitudine.Substring(2, 2) + "'" + Latitudine.Substring(4, 1);
                LONG = "Longitudine: " + Longitudine.Substring(0, 3) + "°" + Longitudine.Substring(3, 2) + "'" + Longitudine.Substring(5, 1);
                wbGoogle.Source = new Uri(URL2, UriKind.Absolute);
                wbGoogle.Refresh();
                bimg.BeginInit(); bimg.UriSource = new Uri(URL, UriKind.Absolute);
                bimg.EndInit();
            }
        }
    }
}
