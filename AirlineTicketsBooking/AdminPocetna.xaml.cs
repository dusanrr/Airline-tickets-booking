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

namespace AirlineTicketsBooking
{
    public partial class AdminPocetna : Window
    {
        public AdminPocetna()
        {
            InitializeComponent();

        }

        private bool IsEmailValid(bool email)
        {
            if (!Regex.IsMatch(email_tb.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Polje 'Email' nije u validnom formatu! Primer: demo@gmail.com", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Warning);
                email_tb.Select(0, email_tb.Text.Length);
                email_tb.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool IsEmailValidd(bool iemail)
        {
            if (!Regex.IsMatch(iemail_tb.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Polje 'Email' nije u validnom formatu! Primer: demo@gmail.com", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Warning);
                iemail_tb.Select(0, iemail_tb.Text.Length);
                iemail_tb.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Korisnik
                BazaDataContext bdc = new BazaDataContext();

                var query =
                from Korisnik in bdc.Korisniks
                select new { Korisnik.ID, Korisnik.ImePrezime, Korisnik.Username, Korisnik.Password, Korisnik.Email, Korisnik.Telefon, Korisnik.Adresa, Korisnik.Grad, Korisnik.Drzava, Korisnik.Rank };

                dataGrid_korisnici.ItemsSource = query.ToList();
                dataGrid_korisnici.Items.Refresh();


                //avio kompanije
                var query1 =
                from AvioKompanija in bdc.AvioKompanijas
                select new { AvioKompanija.ID, AvioKompanija.NazivAvioKompanije };

                dataGrid_aviokompanije.ItemsSource = query1.ToList();
                dataGrid_aviokompanije.Items.Refresh();

                //Let
                var query2 =
                from Let in bdc.Lets
                select new { Let.ID, Let.AKID, Let.LetBR, Let.SedistaPolazak, Let.SedistaPovratak, Let.CenaEkonomska, Let.CenaBiznis, Let.CenaPrva, Let.PolazakDrzava, Let.PolazakGrad, Let.DestinacijaDrzava, Let.DestinacijaGrad, Let.Polazak, Let.PolazakDatum, Let.Dolazak, Let.DolazakDatum, Let.PovratakPolazakVreme, Let.PovratakPolazakDatum, Let.PovratakDolazakVreme, Let.PovratakDolazakDatum };

                dataGrid_letovi.ItemsSource = query2.ToList();
                dataGrid_letovi.Items.Refresh();

                //Rezervacija
                var query3 =
                from Rezervacija in bdc.Rezervacijas
                select new { Rezervacija.ID, Rezervacija.IDLeta, Rezervacija.IDKorisnika, Rezervacija.IPKorisnika, Rezervacija.AKID, Rezervacija.AKNaziv, Rezervacija.LetBR, Rezervacija.Sedista, Rezervacija.Klasa, Rezervacija.PolazakDrzava, Rezervacija.PolazakGrad, Rezervacija.DestinacijaDrzava, Rezervacija.DestinacijaGrad, Rezervacija.Polazak, Rezervacija.PolazakDatum, Rezervacija.Dolazak, Rezervacija.DolazakDatum, Rezervacija.PovratakPolazakVreme, Rezervacija.PovratakPolazakDatum, Rezervacija.PovratakDolazakVreme, Rezervacija.PovratakDolazakDatum, Rezervacija.Cena, Rezervacija.Karta };

                dataGrid_rezervacije.ItemsSource = query3.ToList();
                dataGrid_rezervacije.Items.Refresh();
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
                MessageBox.Show("Došlo je do greške! "+ex,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //Korisnik FJE
        //funkcija za proveru postojanja usernamea
        private bool DoesDataExist(string DataEntry)

        {
            BazaDataContext kdc = new BazaDataContext();

            var query = from Korisnik in kdc.Korisniks
                        where Korisnik.Username.Contains(username_tb.Text)
                        select Korisnik.Username;

            // return false if the item already exists

            if (query.Any())
                return false;
            else
                return true;

        }
        private bool AKExist(string AK)

        {
            BazaDataContext kdc = new BazaDataContext();

            var query = from AvioKompanija in kdc.AvioKompanijas
                        where AvioKompanija.NazivAvioKompanije.Contains(nazivAK_tb.Text)
                        select AvioKompanija.NazivAvioKompanije;

            // return false if the item already exists

            if (query.Any())
                return false;
            else
                return true;

        }

        private void dodaj_korisnika_Selected(object sender, RoutedEventArgs e)
        {
            canvas_dodajkorisnika.Visibility = Visibility.Visible;
        }
        private void btn_closecan_Click(object sender, RoutedEventArgs e)
        {
            canvas_dodajkorisnika.Visibility = Visibility.Hidden;

            //dodaj korisnika kontrole
            imeprezime_tb.Text = "";
            username_tb.Text = "";
            pw_pb.Password = "";
            email_tb.Text = "";
            telefon_tb.Text = "";
            adresa_tb.Text = "";
            grad_tb.Text = "";
            drzava_tb.Text = "";
            rank_cb.SelectedIndex = -1;
            korisnici_cb1.SelectedIndex = -1;
        }

        private void dodajkorisnika_btn_Click(object sender, RoutedEventArgs e)
        {
            if (imeprezime_tb.Text != String.Empty && username_tb.Text != String.Empty && pw_pb.Password != String.Empty &&
               email_tb.Text != String.Empty && adresa_tb.Text != String.Empty && telefon_tb.Text != String.Empty && grad_tb.Text != String.Empty && drzava_tb.Text != String.Empty && rank_cb.Text != String.Empty)
            {
                try
                {
                    if (IsEmailValid(true))
                    {
                        int telefon = int.Parse(telefon_tb.Text);

                        using (BazaDataContext DC = new BazaDataContext())
                        {
                            if (DoesDataExist(username_tb.Text))
                            {
                                Korisnik kor = new Korisnik();
                                kor.Username = username_tb.Text;
                                kor.Password = pw_pb.Password;
                                kor.ImePrezime = imeprezime_tb.Text;
                                kor.Adresa = adresa_tb.Text;
                                kor.Email = email_tb.Text;
                                kor.Telefon = telefon;
                                kor.Grad = grad_tb.Text;
                                kor.Drzava = drzava_tb.Text;
                                kor.Rank = rank_cb.Text;

                                DC.Korisniks.InsertOnSubmit(kor);
                                DC.SubmitChanges();

                                dataGrid_korisnici.ItemsSource = null;
                                dataGrid_korisnici.ItemsSource = DC.Korisniks;
                                dataGrid_korisnici.Items.Refresh();

                                MessageBox.Show("Uspesno ste dodali novog korisnika", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                                canvas_dodajkorisnika.Visibility = Visibility.Hidden;
                                imeprezime_tb.Text = "";
                                username_tb.Text = "";
                                pw_pb.Password = "";
                                email_tb.Text = "";
                                telefon_tb.Text = "";
                                adresa_tb.Text = "";
                                grad_tb.Text = "";
                                drzava_tb.Text = "";
                                rank_cb.SelectedIndex = -1;
                                korisnici_cb1.SelectedIndex = -1;
                            }
                            else
                            {
                                MessageBox.Show("Username koji ste izabrali postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void obrisi_korisnika_Selected(object sender, RoutedEventArgs e)
        {
            canvas_obrisikorisnika.Visibility = Visibility.Visible;
            obrisikorisnika_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Korisnik in bdc.Korisniks
                select Korisnik.ID;

                foreach (var item in upit)
                {
                    obrisikorisnika_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void obrisikorisnika_btn_Click(object sender, RoutedEventArgs e)
        {
            if (obrisikorisnika_cb.Text != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    int broj = int.Parse(obrisikorisnika_cb.SelectedValue.ToString());

                    var ala = from Korisnik in bdc.Korisniks
                              where Korisnik.ID == broj
                              select Korisnik;


                    bdc.Korisniks.DeleteAllOnSubmit(ala);
                    bdc.SubmitChanges();
                    dataGrid_korisnici.ItemsSource = null;
                    dataGrid_korisnici.ItemsSource = bdc.Korisniks;
                    dataGrid_korisnici.Items.Refresh();
                    MessageBox.Show("Uspesno ste obrisali korisnika", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                    obrisikorisnika_cb.SelectedIndex = -1;
                    obrisilet_cb.Items.Clear();
                    obrisikorisnika_cb.Items.Clear();
                    canvas_obrisikorisnika.Visibility = Visibility.Hidden;
                    korisnici_cb1.SelectedIndex = -1;
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Polje 'ID korisnika' mora biti popunjeno!",
                           "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        //EDIT KORISNIKA
        private void izmeni_korisnika_Selected(object sender, RoutedEventArgs e)
        {
            canvas_izmenikorisnika.Visibility = Visibility.Visible;
            korisnikzaizmenu_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Korisnik in bdc.Korisniks
                select Korisnik.ID;

                foreach (var item in upit)
                {
                    korisnikzaizmenu_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btn_closecans_Click(object sender, RoutedEventArgs e)
        {
            canvas_obrisikorisnika.Visibility = Visibility.Hidden;
            obrisikorisnika_cb.SelectedIndex = -1;
            obrisikorisnika_cb.Items.Clear();
            korisnici_cb1.SelectedIndex = -1;
        }

        private void btn_closecaniK_Click(object sender, RoutedEventArgs e)
        {
            canvas_izmenikorisnika.Visibility = Visibility.Hidden;
            korisnici_cb1.SelectedIndex = -1;

            korisnikzaizmenu_cb.Items.Clear();
            iimeprezime_tb.Text = "";
            iusername_tb.Text = "";
            ipw_tb.Text = "";
            iemail_tb.Text = "";
            itelefon_tb.Text = "";
            iadresa_tb.Text = "";
            igrad_tb.Text = "";
            idrzava_tb.Text = "";
            irank_cb.SelectedIndex = -1;
        }

        private void izmenikorisnika_btn_Click(object sender, RoutedEventArgs e)
        {

            if (iimeprezime_tb.Text != String.Empty && iusername_tb.Text != String.Empty && ipw_tb.Text != String.Empty &&
               iemail_tb.Text != String.Empty && iadresa_tb.Text != String.Empty && itelefon_tb.Text != String.Empty && igrad_tb.Text != String.Empty && idrzava_tb.Text != String.Empty && irank_cb.Text != String.Empty)
            {
                try
                {
                    if (IsEmailValidd(true))
                    {
                        int telefon = int.Parse(itelefon_tb.Text);

                        using (BazaDataContext DC = new BazaDataContext())
                        {
                           
                                var test = from Korisnik in DC.Korisniks
                                           where Korisnik.ID.Equals(iID_tb.Text)
                                           select Korisnik;

                                var kor = test.FirstOrDefault();
                                /*int idbre;
                                int.TryParse(iID_tb.Text, out idbre);
                                kor.ID = idbre;*/
                                kor.Username = iusername_tb.Text;
                                kor.Password = ipw_tb.Text;
                                kor.ImePrezime = iimeprezime_tb.Text;
                                kor.Adresa = iadresa_tb.Text;
                                kor.Email = iemail_tb.Text;
                                kor.Telefon = telefon;
                                kor.Grad = igrad_tb.Text;
                                kor.Drzava = idrzava_tb.Text;
                                kor.Rank = irank_cb.Text;

                                DC.SubmitChanges();

                                dataGrid_korisnici.ItemsSource = null;
                                dataGrid_korisnici.ItemsSource = DC.Korisniks;
                                dataGrid_korisnici.Items.Refresh();

                                MessageBox.Show("Uspesno ste izmenili korisnika", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                                canvas_izmenikorisnika.Visibility = Visibility.Hidden;
                                korisnikzaizmenu_cb.SelectedIndex = -1;
                                korisnici_cb1.SelectedIndex = -1;
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void korisnikzaizmenu_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Korisnik in bdc.Korisniks
                where Korisnik.ID.Equals(Convert.ToInt32(korisnikzaizmenu_cb.SelectedValue))
                select Korisnik;

                var zaza = upit.FirstOrDefault();
                if (zaza == null)
                {
                    iID_tb.Text = "";
                    iimeprezime_tb.Text = "";
                    iusername_tb.Text = "";
                    ipw_tb.Text = "";
                    iemail_tb.Text = "";
                    itelefon_tb.Text = "";
                    iadresa_tb.Text = "";
                    igrad_tb.Text = "";
                    idrzava_tb.Text = "";
                    irank_cb.SelectedIndex = -1;
                }
                else
                {
                    iID_tb.Text = zaza.ID.ToString();
                    iimeprezime_tb.Text = zaza.ImePrezime;
                    iusername_tb.Text = zaza.Username;
                    ipw_tb.Text = zaza.Password;
                    iemail_tb.Text = zaza.Email;
                    itelefon_tb.Text = zaza.Telefon.ToString();
                    iadresa_tb.Text = zaza.Adresa;
                    igrad_tb.Text = zaza.Grad;
                    idrzava_tb.Text = zaza.Drzava;
                    if (zaza.Rank.ToString().Equals("Korisnik"))
                    {
                        irank_cb.SelectedIndex = 0;
                    }
                    else
                    {
                        irank_cb.SelectedIndex = 1;

                    }
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //AVIO KOMPANIJE
        private void btn_closecanDAK_Click(object sender, RoutedEventArgs e)
        {
            canvas_dodajAK.Visibility = Visibility.Hidden;
            nazivAK_tb.Text = "";
            aviokompanije_cb1.SelectedIndex = -1;
        }

        private void dodajAK_btn_Click(object sender, RoutedEventArgs e)
        {
            if (nazivAK_tb.Text != String.Empty)
            {
                try
                {
                    using (BazaDataContext DC = new BazaDataContext())
                    {
                        if (AKExist(nazivAK_tb.Text))
                        {
                            AvioKompanija ak = new AvioKompanija();

                            ak.NazivAvioKompanije = nazivAK_tb.Text;

                            DC.AvioKompanijas.InsertOnSubmit(ak);
                            DC.SubmitChanges();

                            dataGrid_aviokompanije.ItemsSource = null;
                            dataGrid_aviokompanije.ItemsSource = DC.AvioKompanijas;
                            dataGrid_aviokompanije.Items.Refresh();

                            MessageBox.Show("Uspesno ste dodali novu avio kompaniju", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                            canvas_dodajAK.Visibility = Visibility.Hidden;
                            aviokompanije_cb1.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("Avio kompanija sa tim imenom postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dodajAK_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_dodajAK.Visibility = Visibility.Visible;
        }

        //IZMENA AVIO KOMPANIJE
        private void izmeniAK_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_izmeniAK.Visibility = Visibility.Visible;
            iIDAK_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from AvioKompanija in bdc.AvioKompanijas
                select AvioKompanija.ID;

                foreach (var item in upit)
                {
                    iIDAK_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_closecanIAK_Click(object sender, RoutedEventArgs e)
        {
            canvas_izmeniAK.Visibility = Visibility.Hidden;
            aviokompanije_cb1.SelectedIndex = -1;

            nazivAK_tb.Text = "";
        }

        private void iIDAK_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from AvioKompanija in bdc.AvioKompanijas
                where AvioKompanija.ID.Equals(Convert.ToInt32(iIDAK_cb.SelectedValue))
                select AvioKompanija;

                var zaza = upit.FirstOrDefault();
                if (zaza == null)
                {
                    inazivAK_tb.Text = "";

                }
                else
                {
                    IDAK_tb.Text = zaza.ID.ToString();
                    inazivAK_tb.Text = zaza.NazivAvioKompanije;
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void izmeniAK_btn_Click(object sender, RoutedEventArgs e)
        {
            if (inazivAK_tb.Text != String.Empty && iIDAK_cb.Text != String.Empty)
            {
                try
                {
                    using (BazaDataContext DC = new BazaDataContext())
                    {
                        var test = from AvioKompanija in DC.AvioKompanijas
                                   where AvioKompanija.ID.Equals(IDAK_tb.Text)
                                   select AvioKompanija;

                        var ak = test.FirstOrDefault();
                        ak.NazivAvioKompanije = inazivAK_tb.Text;


                        DC.SubmitChanges();

                        dataGrid_aviokompanije.ItemsSource = null;
                        dataGrid_aviokompanije.ItemsSource = DC.AvioKompanijas;
                        dataGrid_aviokompanije.Items.Refresh();

                        MessageBox.Show("Uspesno ste izmenili avio kompaniju", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                        canvas_izmeniAK.Visibility = Visibility.Hidden;
                        iIDAK_cb.SelectedIndex = -1;
                        aviokompanije_cb1.SelectedIndex = -1;
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                         "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        //BRISANJE AVIO KOMPANIJE
        private void btn_closecanOAK_Click(object sender, RoutedEventArgs e)
        {
            canvas_obrisiAK.Visibility = Visibility.Hidden;
            obrisiAK_cb.SelectedIndex = -1;
            obrisiAK_cb.Items.Clear();
            aviokompanije_cb1.SelectedIndex = -1;
        }

        private void obrisiAK_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_obrisiAK.Visibility = Visibility.Visible;
            obrisiAK_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from AvioKompanija in bdc.AvioKompanijas
                select AvioKompanija.ID;

                foreach (var item in upit)
                {
                    obrisiAK_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void obrisiAK_btn_Click(object sender, RoutedEventArgs e)
        {
            if (obrisiAK_cb.Text != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    int broj = int.Parse(obrisiAK_cb.SelectedValue.ToString());

                    var ala = from AvioKompanija in bdc.AvioKompanijas
                              where AvioKompanija.ID == broj
                              select AvioKompanija;


                    bdc.AvioKompanijas.DeleteAllOnSubmit(ala);
                    bdc.SubmitChanges();
                    dataGrid_aviokompanije.ItemsSource = null;
                    dataGrid_aviokompanije.ItemsSource = bdc.AvioKompanijas;
                    dataGrid_aviokompanije.Items.Refresh();
                    MessageBox.Show("Uspesno ste obrisali avio kompaniju", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                    obrisiAK_cb.SelectedIndex = -1;
                    aviokompanije_cb1.SelectedIndex = -1;
                    obrisiAK_cb.Items.Clear();
                    canvas_obrisiAK.Visibility = Visibility.Hidden;
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                           "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Polje 'ID avio kompanije za brisanje' mora biti popunjeno!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //Let
        //DODAVANJE LETOVA
        private void btn_closecanDL_Click(object sender, RoutedEventArgs e)
        {
            canvas_dodajlet.Visibility = Visibility.Hidden;
            cb_letovi1.SelectedIndex = -1;
            akid_cb.SelectedIndex = -1;
            akid_cb.Items.Clear();
            brojleta_tb.Text = "";
            sedistapolazak_tb.Text = "";
            sedistapovratak_tb.Text = "";
            cenaekonomska_tb.Text = "";
            cenabiznis_tb.Text = "";
            cenaprva_tb.Text = "";
            drzpol_tb.Text = "";
            gradpol_tb.Text = "";
            destdrz_tb.Text = "";
            destgrad_tb.Text = "";
            polvreme_cb.Text = "";
            poldatum_dp.Text = "";
            dolvreme_cb.Text = "";
            doldatum_dp.Text = "";
            povpolvr_cb.Text = "";
            dpovp_dp.Text = "";
            povdolvr_cb.Text = "";
            dpovdol_dp.Text = "";
        }

        private void dodajlet_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_dodajlet.Visibility = Visibility.Visible;
            akid_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from AvioKompanija in bdc.AvioKompanijas
                select AvioKompanija.ID;

                foreach (var item in upit)
                {
                    akid_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dodajlet_btn_Click(object sender, RoutedEventArgs e)
        {
            if (akid_cb.Text != String.Empty && brojleta_tb.Text != String.Empty && sedistapolazak_tb.Text != String.Empty && sedistapovratak_tb.Text != String.Empty &&
              cenaekonomska_tb.Text != String.Empty && cenabiznis_tb.Text != String.Empty && cenaprva_tb.Text != String.Empty && drzpol_tb.Text != String.Empty && gradpol_tb.Text != String.Empty && destdrz_tb.Text != String.Empty && destgrad_tb.Text != String.Empty && polvreme_cb.Text != String.Empty && poldatum_dp.Text != String.Empty && dolvreme_cb.Text != String.Empty && doldatum_dp.Text != String.Empty && povpolvr_cb.Text != String.Empty && dpovp_dp.Text != String.Empty && povdolvr_cb.Text != String.Empty && dpovdol_dp.Text != String.Empty)
            {
                try
                {
                   
                    int idak = int.Parse(akid_cb.Text);
                    int brleta = int.Parse(brojleta_tb.Text);
                    int sedistapolazak = int.Parse(sedistapolazak_tb.Text);
                    int sedistapovratak = int.Parse(sedistapovratak_tb.Text);

                    using (BazaDataContext DC = new BazaDataContext())
                    {
                        Let let = new Let();
                        let.AKID = idak;
                        let.LetBR = brleta;
                        let.SedistaPolazak = sedistapolazak;
                        let.SedistaPovratak = sedistapovratak;
                        let.CenaEkonomska = float.Parse(cenaekonomska_tb.Text);
                        let.CenaBiznis = float.Parse(cenabiznis_tb.Text);
                        let.CenaPrva = float.Parse(cenaprva_tb.Text);
                        let.PolazakDrzava = drzpol_tb.Text;
                        let.PolazakGrad = gradpol_tb.Text;
                        let.DestinacijaDrzava = destdrz_tb.Text;
                        let.DestinacijaGrad = destgrad_tb.Text;
                        let.Polazak = polvreme_cb.Text;
                        let.PolazakDatum = poldatum_dp.Text.ToString();
                        let.Dolazak = dolvreme_cb.Text;
                        let.DolazakDatum = doldatum_dp.Text.ToString();
                        let.PovratakPolazakVreme = povpolvr_cb.Text;
                        let.PovratakPolazakDatum = dpovp_dp.Text.ToString();
                        let.PovratakDolazakVreme = povdolvr_cb.Text;
                        let.PovratakDolazakDatum = dpovdol_dp.Text.ToString();

                        DC.Lets.InsertOnSubmit(let);
                        DC.SubmitChanges();

                        dataGrid_letovi.ItemsSource = null;
                        dataGrid_letovi.ItemsSource = DC.Lets;
                        dataGrid_letovi.Items.Refresh();

                        MessageBox.Show("Uspesno ste dodali novi let", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                        canvas_dodajlet.Visibility = Visibility.Hidden;
                        cb_letovi1.SelectedIndex = -1;
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                           "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //IZMENA LETOVA
        private void btn_closecanIL_Click(object sender, RoutedEventArgs e)
        {
            canvas_izmenilet.Visibility = Visibility.Hidden;
            iletid_cb.Items.Clear();
            iletid_cb.SelectedIndex = -1;
            iakid_cb.SelectedIndex = -1;
            cb_letovi1.SelectedIndex = -1;
            ibrojleta_tb.Text = "";
            isedistapolazak_tb.Text = "";
            isedistapovratak_tb.Text = "";
            icenaekonomska_tb.Text = "";
            icenabiznis_tb.Text = "";
            icenaprva_tb.Text = "";
            idrzpol_tb.Text = "";
            igradpol_tb.Text = "";
            idestdrz_tb.Text = "";
            idestgrad_tb.Text = "";
            ipolvreme_cb.SelectedIndex = -1;
            ipoldatum_dp.Text = "";
            idolvreme_cb.SelectedIndex = -1;
            idoldatum_dp.Text = "";
            ipovpolvr_cb.SelectedIndex = -1;
            idpovp_dp.Text = "";
            ipovdolvr_cb.SelectedIndex = -1;
            idpovdol_dp.Text = "";
        }

        private void izmenilet_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_izmenilet.Visibility = Visibility.Visible;
            iletid_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Let in bdc.Lets
                select Let.ID;

                foreach (var item in upit)
                {
                    iletid_cb.Items.Add(item);
                }              
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void iletid_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            iakid_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Let in bdc.Lets
                where Let.ID.Equals(Convert.ToInt32(iletid_cb.SelectedValue))
                select Let;


                var zaza = upit.SingleOrDefault();
                if (zaza == null)
                {
                    iakid_cb.Items.Clear();
                    ibrojleta_tb.Text = "";
                    isedistapolazak_tb.Text = "";
                    isedistapovratak_tb.Text = "";
                    icenaekonomska_tb.Text = "";
                    icenabiznis_tb.Text = "";
                    icenaprva_tb.Text = "";
                    idrzpol_tb.Text = "";
                    igradpol_tb.Text = "";
                    idestdrz_tb.Text = "";
                    idestgrad_tb.Text = "";
                    ipolvreme_cb.Text = "";
                    ipoldatum_dp.Text = "";
                    idolvreme_cb.Text = "";
                    idoldatum_dp.Text = "";
                    ipovpolvr_cb.Text = "";
                    idpovp_dp.Text = "";
                    ipovdolvr_cb.Text = "";
                    idpovdol_dp.Text = "";

                }
                else
                {
                    iakid_cb.Items.Clear();
                    var upits =
                    from AvioKompanija in bdc.AvioKompanijas
                    select AvioKompanija.ID;

                    foreach (var item in upits)
                    {
                        iakid_cb.Items.Add(item);
                    }
                    //iakid_cb.Items.Add(zaza.AKID);
                    //iakid_cb.SelectedIndex = 0;
                    iakid_cb.Text = zaza.AKID.ToString();
                    ibrojleta_tb.Text = zaza.LetBR.ToString();
                    isedistapolazak_tb.Text = zaza.SedistaPolazak.ToString();
                    isedistapovratak_tb.Text = zaza.SedistaPovratak.ToString();
                    icenaekonomska_tb.Text = zaza.CenaEkonomska.ToString();
                    icenabiznis_tb.Text = zaza.CenaBiznis.ToString();
                    icenaprva_tb.Text = zaza.CenaPrva.ToString();
                    idrzpol_tb.Text = zaza.PolazakDrzava;
                    igradpol_tb.Text = zaza.PolazakGrad;
                    idestdrz_tb.Text = zaza.DestinacijaDrzava;
                    idestgrad_tb.Text = zaza.DestinacijaGrad;
                    ipolvreme_cb.Text = zaza.Polazak;
                    ipoldatum_dp.Text = zaza.PolazakDatum.ToString();
                    idolvreme_cb.Text = zaza.Dolazak;
                    idoldatum_dp.Text = zaza.DolazakDatum.ToString();
                    ipovpolvr_cb.Text = zaza.PovratakPolazakVreme;
                    idpovp_dp.Text = zaza.PovratakPolazakDatum.ToString();
                    ipovdolvr_cb.Text = zaza.PovratakDolazakVreme;
                    idpovdol_dp.Text = zaza.PovratakDolazakDatum.ToString();
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void iizmenilet_btn_Click(object sender, RoutedEventArgs e)
        {
            if (iakid_cb.Text != String.Empty && ibrojleta_tb.Text != String.Empty && isedistapolazak_tb.Text != String.Empty && isedistapovratak_tb.Text != String.Empty && icenaekonomska_tb.Text != String.Empty && icenabiznis_tb.Text != String.Empty && icenaprva_tb.Text != String.Empty
                && idrzpol_tb.Text != String.Empty && igradpol_tb.Text != String.Empty && idestdrz_tb.Text != String.Empty && idestgrad_tb.Text != String.Empty
                && ipolvreme_cb.Text != String.Empty && ipoldatum_dp.Text != String.Empty && idolvreme_cb.Text != String.Empty && idoldatum_dp.Text != String.Empty && ipovpolvr_cb.Text != String.Empty && idpovp_dp.Text != String.Empty && ipovdolvr_cb.Text != String.Empty && idpovdol_dp.Text != String.Empty)
            {
                try
                {
                    using (BazaDataContext DC = new BazaDataContext())
                    {
                        var test = from Let in DC.Lets
                                   where Let.ID.Equals(iletid_cb.Text)
                                   select Let;

                        var le = test.FirstOrDefault();

                        le.AKID = int.Parse(iakid_cb.Text);
                        le.LetBR = int.Parse(ibrojleta_tb.Text);
                        le.SedistaPolazak = int.Parse(isedistapolazak_tb.Text);
                        le.SedistaPovratak = int.Parse(isedistapovratak_tb.Text);
                        le.CenaEkonomska = int.Parse(icenaekonomska_tb.Text);
                        le.CenaBiznis = int.Parse(icenabiznis_tb.Text);
                        le.CenaPrva = int.Parse(icenaprva_tb.Text);
                        le.PolazakDrzava = idrzpol_tb.Text;
                        le.PolazakGrad = igradpol_tb.Text;
                        le.DestinacijaDrzava = idestdrz_tb.Text;
                        le.DestinacijaGrad = idestgrad_tb.Text;
                        le.Polazak = ipolvreme_cb.Text;
                        le.PolazakDatum = idoldatum_dp.Text.ToString();
                        le.Dolazak = idolvreme_cb.Text;
                        le.DolazakDatum = idoldatum_dp.Text.ToString(); ;
                        le.PovratakPolazakVreme = ipovpolvr_cb.Text;
                        le.PovratakPolazakDatum = idpovp_dp.Text.ToString(); ;
                        le.PovratakDolazakVreme = ipovdolvr_cb.Text;
                        le.PovratakDolazakDatum = idpovdol_dp.Text;

                        DC.SubmitChanges();

                        dataGrid_letovi.ItemsSource = null;
                        dataGrid_letovi.ItemsSource = DC.Lets;
                        dataGrid_letovi.Items.Refresh();

                        MessageBox.Show("Uspesno ste izmenili let", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                        canvas_izmenilet.Visibility = Visibility.Hidden;
                        iletid_cb.SelectedIndex = -1;
                        iakid_cb.SelectedIndex = -1;
                        cb_letovi1.SelectedIndex = -1;
                    }
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //BRISANJE LETOVA
        private void obrisilet_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_obrisilet.Visibility = Visibility.Visible;
            obrisilet_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Let in bdc.Lets
                select Let.ID;

                foreach (var item in upit)
                {
                    obrisilet_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_closecanOLet_Click(object sender, RoutedEventArgs e)
        {
            canvas_obrisilet.Visibility = Visibility.Hidden;
            obrisilet_cb.SelectedIndex = -1;
            cb_letovi1.SelectedIndex = -1;
            obrisilet_cb.Items.Clear();
        }

        private void obrisilet_btn_Click(object sender, RoutedEventArgs e)
        {
            if (obrisilet_cb.Text != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    int broj = int.Parse(obrisilet_cb.SelectedValue.ToString());

                    var ala = from Let in bdc.Lets
                              where Let.ID == broj
                              select Let;


                    bdc.Lets.DeleteAllOnSubmit(ala);
                    bdc.SubmitChanges();
                    dataGrid_letovi.ItemsSource = null;
                    dataGrid_letovi.ItemsSource = bdc.Lets;
                    dataGrid_letovi.Items.Refresh();
                    MessageBox.Show("Uspesno ste obrisali let.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                    obrisilet_cb.SelectedIndex = -1;
                    cb_letovi1.SelectedIndex = -1;
                    obrisilet_cb.Items.Clear();
                    canvas_obrisilet.Visibility = Visibility.Hidden;
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Polje 'ID leta za brisanje' mora biti popunjeno!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        //Rezervacija
        //BRISANJE REZEVACIJA
        private void btn_closecanORez_Click(object sender, RoutedEventArgs e)
        {
            canvas_obrisirez.Visibility = Visibility.Hidden;
            obrisirez_cb.SelectedIndex = -1;
            rezervacije_cb1.SelectedIndex = -1;
            obrisirez_cb.Items.Clear();
        }

        private void obrisirez_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_obrisirez.Visibility = Visibility.Visible;
            obrisirez_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Rezervacija in bdc.Rezervacijas
                select Rezervacija.ID;

                foreach (var item in upit)
                {
                    obrisirez_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void obrisirez_btn_Click(object sender, RoutedEventArgs e)
        {
            if (obrisirez_cb.Text != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    int broj = int.Parse(obrisirez_cb.SelectedValue.ToString());

                    var ala = from Rezervacija in bdc.Rezervacijas
                              where Rezervacija.ID == broj
                              select Rezervacija;


                    bdc.Rezervacijas.DeleteAllOnSubmit(ala);
                    bdc.SubmitChanges();
                    dataGrid_rezervacije.ItemsSource = null;
                    dataGrid_rezervacije.ItemsSource = bdc.Rezervacijas;
                    dataGrid_rezervacije.Items.Refresh();
                    MessageBox.Show("Uspesno ste obrisali rezervaciju.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                    obrisirez_cb.SelectedIndex = -1;
                    rezervacije_cb1.SelectedIndex = -1;
                    obrisirez_cb.Items.Clear();
                    canvas_obrisirez.Visibility = Visibility.Hidden;
                }
                catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Polje 'ID rezervacije za brisanje' mora biti popunjeno!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //IZMENA REZERVACIJA

        private void ibtn_closecanIR_Click(object sender, RoutedEventArgs e)
        {
            canvas_izmenirez.Visibility = Visibility.Hidden;
            rezervacije_cb1.SelectedIndex = -1;
            idrez_cb.Items.Clear();
            idrez_cb.SelectedIndex = -1;
            polgrad_tb.Text = "";
            poldrz_tb.Text = "";
            drzdest_tb.Text = "";
            graddest_tb.Text = "";
            vremepol_cb.SelectedIndex = -1;
            datumpol_dp.Text = "";
            vremedol_cb.SelectedIndex = -1;
            datumdol_dp.Text = "";
            povpolvreme_cb.SelectedIndex = -1;
            povpoldatum_dp.Text = "";
            povdolvreme_cb.SelectedIndex = -1;
            povdoldatum_dp.Text = "";
        }

        private void izmenirez_cbi_Selected(object sender, RoutedEventArgs e)
        {
            canvas_izmenirez.Visibility = Visibility.Visible;
            idrez_cb.Items.Clear();
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Rezervacija in bdc.Rezervacijas
                select Rezervacija.ID;

                foreach (var item in upit)
                {
                    idrez_cb.Items.Add(item);
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void idrez_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BazaDataContext bdc = new BazaDataContext();
                var upit =
                from Rezervacija in bdc.Rezervacijas
                where Rezervacija.ID.Equals(Convert.ToInt32(idrez_cb.SelectedValue))
                select Rezervacija;

                var rez = upit.SingleOrDefault();
                if (rez == null)
                {
                    idrez_cb.Items.Clear();
                    idrez_cb.SelectedIndex = -1;
                    polgrad_tb.Text = "";
                    poldrz_tb.Text = "";
                    drzdest_tb.Text = "";
                    graddest_tb.Text = "";
                    vremepol_cb.SelectedIndex = -1;
                    datumpol_dp.Text = "";
                    vremedol_cb.SelectedIndex = -1;
                    datumdol_dp.Text = "";
                    povpolvreme_cb.SelectedIndex = -1;
                    povpoldatum_dp.Text = "";
                    povdolvreme_cb.SelectedIndex = -1;
                    povdoldatum_dp.Text = "";

                }
                else
                {
                    polgrad_tb.Text = rez.PolazakGrad;
                    poldrz_tb.Text = rez.PolazakDrzava;
                    drzdest_tb.Text = rez.DestinacijaDrzava;
                    graddest_tb.Text = rez.DestinacijaGrad;
                    vremepol_cb.Text = rez.Polazak;
                    datumpol_dp.Text = rez.PolazakDatum.ToString();
                    vremedol_cb.Text = rez.Dolazak;
                    datumdol_dp.Text = rez.DolazakDatum.ToString();
                    povpolvreme_cb.Text = rez.PovratakPolazakVreme;
                    povpoldatum_dp.Text = rez.PovratakPolazakDatum.ToString();
                    povdolvreme_cb.Text = rez.PovratakDolazakVreme;
                    povdoldatum_dp.Text = rez.PovratakDolazakDatum.ToString();
                }
            }
            catch (Exception fe)
            {
                MessageBox.Show("Došlo je do greške! " + fe,
                       "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void izmenirez_btn_Click(object sender, RoutedEventArgs e)
        {
            if (idrez_cb.Text != String.Empty && polgrad_tb.Text != String.Empty && poldrz_tb.Text != String.Empty && drzdest_tb.Text != String.Empty && graddest_tb.Text != String.Empty && vremepol_cb.Text != String.Empty && datumpol_dp.Text != String.Empty &&
                vremedol_cb.Text != String.Empty && datumdol_dp.Text != String.Empty && povpolvreme_cb.Text != String.Empty && povpoldatum_dp.Text != String.Empty && povdolvreme_cb.Text != String.Empty && povdoldatum_dp.Text != String.Empty)
            {
                try
                {
                    using (BazaDataContext DC = new BazaDataContext())
                    {
                        var query = from Rezervacija in DC.Rezervacijas
                                    where Rezervacija.ID.Equals(idrez_cb.Text)
                                    select Rezervacija;

                        var rez = query.FirstOrDefault();
                        rez.PolazakGrad = polgrad_tb.Text;
                        rez.PolazakDrzava = poldrz_tb.Text;
                        rez.DestinacijaDrzava = drzdest_tb.Text;
                        rez.DestinacijaGrad = graddest_tb.Text;
                        rez.Polazak = vremepol_cb.Text;
                        rez.PolazakDatum = datumpol_dp.Text.ToString();
                        rez.Dolazak = vremedol_cb.Text;
                        rez.DolazakDatum = datumdol_dp.Text.ToString();
                        rez.PovratakPolazakVreme = povpolvreme_cb.Text;
                        rez.PovratakPolazakDatum = povpoldatum_dp.Text.ToString();
                        rez.PovratakDolazakVreme = povdolvreme_cb.Text;
                        rez.PovratakDolazakDatum = povdoldatum_dp.Text.ToString();


                        DC.SubmitChanges();

                        dataGrid_rezervacije.ItemsSource = null;
                        dataGrid_rezervacije.ItemsSource = DC.Rezervacijas;
                        dataGrid_rezervacije.Items.Refresh();

                        MessageBox.Show("Uspesno ste izmenili rezervaciju.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);
                        canvas_izmenirez.Visibility = Visibility.Hidden;
                        idrez_cb.Items.Clear();
                        idrez_cb.SelectedIndex = -1;
                        polgrad_tb.Text = "";
                        poldrz_tb.Text = "";
                        drzdest_tb.Text = "";
                        graddest_tb.Text = "";
                        vremepol_cb.SelectedIndex = -1;
                        datumpol_dp.Text = "";
                        vremedol_cb.SelectedIndex = -1;
                        datumdol_dp.Text = "";
                        povpolvreme_cb.SelectedIndex = -1;
                        povpoldatum_dp.Text = "";
                        povdolvreme_cb.SelectedIndex = -1;
                        povdoldatum_dp.Text = "";
                        rezervacije_cb1.SelectedIndex = -1;
                    }
                }
                 catch (System.Data.SqlClient.SqlException fe)
                {
                    MessageBox.Show("Došlo je do greške! " + fe,
                            "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!",
                            "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //////////////////////////VALIDACIJA//////////////////////////////
        //////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        //DODAJ KORISNIKA - VALIDACIJA
        private void telefon_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    telefon_tb.Text = "";
                    MessageBox.Show("Polje 'Telefon' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void imeprezime_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    imeprezime_tb.Text = "";
                    MessageBox.Show("Polje 'Ime i prezime' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void username_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    username_tb.Text = "";
                    MessageBox.Show("Polje 'Username' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void pw_pb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    pw_pb.Password = "";
                    MessageBox.Show("Polje 'Password' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }

        }

        private void adresa_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c)  && !char.IsNumber(c))
                {
                    adresa_tb.Text = "";
                    MessageBox.Show("Polje 'Adresa' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void grad_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    grad_tb.Text = "";
                    MessageBox.Show("Polje 'Grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void drzava_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    drzava_tb.Text = "";
                    MessageBox.Show("Polje 'Drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }
        //FALI EMAIL I ZA IME I PREZIME RAZMAK MOZDA
        //////////////////////////////////////////////////////////////////////////////////////////
        //IZMENI KORISNIKA - VALIDACIJA
        private void itelefon_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    itelefon_tb.Text = "";
                    MessageBox.Show("Polje 'Telefon' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void iimeprezime_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    iimeprezime_tb.Text = "";
                    MessageBox.Show("Polje 'Ime i prezime' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void iusername_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    iusername_tb.Text = "";
                    MessageBox.Show("Polje 'Username' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void ipw_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    ipw_tb.Text = "";
                    MessageBox.Show("Polje 'Password' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }

        }

        private void iadresa_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    iadresa_tb.Text = "";
                    MessageBox.Show("Polje 'Adresa' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void igrad_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    igrad_tb.Text = "";
                    MessageBox.Show("Polje 'Grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void idrzava_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    idrzava_tb.Text = "";
                    MessageBox.Show("Polje 'Drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        //DODAJ AVIO KOMPANIJU - VALIDACIJA
        private void nazivAK_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    nazivAK_tb.Text = "";
                    MessageBox.Show("Polje 'Naziv avio kompanije' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }
        //IZMENI AVIO KOMPANIJU - VALIDACIJA
        private void inazivAK_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    inazivAK_tb.Text = "";
                    MessageBox.Show("Polje 'Naziv avio kompanije' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //DODAJ LET - VALIDACIJA
        private void brojleta_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    brojleta_tb.Text = "";
                    MessageBox.Show("Polje 'Broj leta' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void sedistapolazak_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    sedistapolazak_tb.Text = "";
                    MessageBox.Show("Polje 'Sedista - polazak' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void sedistapovratak_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    sedistapovratak_tb.Text = "";
                    MessageBox.Show("Polje 'Sedista - povratak' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void cenaekonomska_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    cenaekonomska_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - ekonomska' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void cenabiznis_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    cenabiznis_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - biznis' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void cenaprva_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    cenaprva_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - prva' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void gradpol_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    gradpol_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void drzpol_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    drzpol_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void destgrad_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    destgrad_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void destdrz_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    destdrz_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////
        //IZMENI LET - VALIDACIJA
        private void ibrojleta_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    ibrojleta_tb.Text = "";
                    MessageBox.Show("Polje 'Broj leta' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void isedistapolazak_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    isedistapolazak_tb.Text = "";
                    MessageBox.Show("Polje 'Sedista - polazak' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void isedistapovratak_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    isedistapovratak_tb.Text = "";
                    MessageBox.Show("Polje 'Sedista - povratak' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void icenaekonomska_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    icenaekonomska_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - ekonomska' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void icenabiznis_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    icenabiznis_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - biznis' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void icenaprva_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    icenaprva_tb.Text = "";
                    MessageBox.Show("Polje 'Cena - prva' moze sadrzati samo brojeve/decimalne brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void igradpol_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    igradpol_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void idrzpol_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    idrzpol_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void idestgrad_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    idestgrad_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void idestdrz_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    idestdrz_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        //IZMENI REZERVACIJU - VALIDACIJA
        private void polgrad_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    polgrad_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void poldrz_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    poldrz_tb.Text = "";
                    MessageBox.Show("Polje 'Polazak - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void drzdest_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    drzdest_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void graddest_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    destgrad_tb.Text = "";
                    MessageBox.Show("Polje 'Destinacija - grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

    }
}
