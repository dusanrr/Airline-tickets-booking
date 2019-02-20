using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AirlineTicketsBooking
{
    public partial class KorisnikPocetna : Window
    {
        DispatcherTimer timer;
        int ctr = 0;
        public KorisnikPocetna()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr > 4)
            {
                ctr = 1;
            }
            PlaySlideShow(ctr);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SLIDESHOW
            ctr = 1;
            PlaySlideShow(ctr);
            timer.IsEnabled = true;
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Let in bdc.Lets
                select Let.PolazakGrad;

                foreach (var item in upit)
                {
                    polaziste_cb.Items.Add(item);
                }

                var upit1 =
                from Let in bdc.Lets
                select Let.DestinacijaGrad;

                foreach (var item in upit1)
                {
                    destinacija_cb.Items.Add(item);
                }


                var query1 =
                from AvioKompanija in bdc.AvioKompanijas
                select new { AvioKompanija.NazivAvioKompanije };


                dataGrid.ItemsSource = query1.ToList();
                dataGrid.Items.Refresh();

                dataGrid_search.Visibility = Visibility.Hidden;
                label_info.Visibility = Visibility.Hidden;
                rezervisi_btn.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške! " + ex,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void PlaySlideShow(int ctr)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string filename = "";
            if (ctr < 10) filename = "Slideshow/Ra0" + ctr + ".jpg";
            image.UriSource = new Uri(filename, UriKind.Relative);
            image.EndInit();
            myImage.Source = image;
            myImage.Stretch = Stretch.Fill;
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties.Remove("ID");
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
        }

        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void home_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            KorisnikPocetna kp = new KorisnikPocetna();
            kp.ShowDialog();
            kp.Show();
        }

        private void rezervacije_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            KorisnikRezervacije kr = new KorisnikRezervacije();
            kr.ShowDialog();
        }
        private void letovi_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            KorisnikLetovi kl = new KorisnikLetovi();
            kl.ShowDialog();
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Properties.Remove("ID");
                this.Hide();
                Login log = new Login();
                log.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Došlo je do greške! ",
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void povratna_rb_Checked(object sender, RoutedEventArgs e)
        {
            dpolaska_dp.Visibility = Visibility.Visible;
            dpovratka_dp.Visibility = Visibility.Visible;
            dpovratka_label.Visibility = Visibility.Visible;
        }

        private void jedanpravac_rb_Checked(object sender, RoutedEventArgs e)
        {
            dpovratka_dp.Visibility = Visibility.Hidden;
            dpovratka_label.Visibility = Visibility.Hidden;
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (polaziste_cb.Text != String.Empty && destinacija_cb.Text != String.Empty && dpolaska_dp.Text != String.Empty && dpovratka_dp.Text != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    var upit =
                        from Let in bdc.Lets
                        where Let.PolazakGrad.Equals(polaziste_cb.Text) && Let.DestinacijaGrad.Equals(destinacija_cb.Text)
                        && Let.PolazakDatum.Equals(dpolaska_dp.Text) && Let.PovratakDolazakDatum.Equals(dpovratka_dp.Text)
                        join avio in bdc.AvioKompanijas
                        on Let.AKID equals avio.ID
                        select new { Let.ID, avio.NazivAvioKompanije, Let.PolazakDrzava, Let.PolazakGrad, Let.DestinacijaDrzava, Let.DestinacijaGrad, Let.PolazakDatum, Let.Polazak, Let.DolazakDatum, Let.Dolazak, Let.PovratakPolazakDatum, Let.PovratakPolazakVreme, Let.PovratakDolazakDatum, Let.PovratakDolazakVreme, Let.CenaEkonomska, Let.CenaBiznis, Let.CenaPrva };
                    if (upit.Any())
                    {
                        dataGrid_search.ItemsSource = upit.ToList();
                        dataGrid_search.Items.Refresh();
                        dataGrid_search.Visibility = Visibility.Visible;
                        label_info.Visibility = Visibility.Visible;
                        rezervisi_btn.Visibility = Visibility.Visible;

                        dataGrid_search.Columns[0].Header = "ID leta"; dataGrid_search.Columns[1].Header = "Prevoznik"; dataGrid_search.Columns[2].Header = "Polazak - drzava"; dataGrid_search.Columns[3].Header = "Polazak - grad";
                        dataGrid_search.Columns[4].Header = "Destinacija - drzava"; dataGrid_search.Columns[5].Header = "Destinacija - grad"; dataGrid_search.Columns[6].Header = "Datum polaska"; dataGrid_search.Columns[7].Header = "Vreme polaska"; dataGrid_search.Columns[8].Header = "Datum dolaska"; dataGrid_search.Columns[9].Header = "Vreme dolaska"; dataGrid_search.Columns[10].Header = "Povratak - polazak - datum";
                        dataGrid_search.Columns[11].Header = "Povratak - polazak - vreme"; dataGrid_search.Columns[12].Header = "Povratak - dolazak - datum"; dataGrid_search.Columns[13].Header = "Povratak - dolazak - vreme"; dataGrid_search.Columns[14].Header = "Cena - ekonomska(EURA)"; dataGrid_search.Columns[15].Header = "Cena - biznis(EURA)"; dataGrid_search.Columns[16].Header = "Cena - prva(EURA)";
                    }
                    else
                    {
                        MessageBox.Show("Nema leta u ponudi sa unetim podacima!",
                           "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Došlo je do greške! " + ex,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            polaziste_cb.SelectedIndex = -1;
            destinacija_cb.SelectedIndex = -1;
            dpolaska_dp.Text = "";
            dpovratka_dp.Text = "";
        }
        

        private void rezervisi_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            KorisnikLetovi le = new KorisnikLetovi();
            le.ShowDialog();
        }
    }
}
