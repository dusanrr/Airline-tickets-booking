using System;
using System.Collections.Generic;
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

namespace AirlineTicketsBooking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.ShowDialog();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (username_TB.Text != String.Empty && password_PB.Password != String.Empty)
            {
                try
                {
                    BazaDataContext bdc = new BazaDataContext();
                    var upit = (from Korisnik in bdc.Korisniks where Korisnik.Username.Equals(username_TB.Text) & Korisnik.Password.Equals(password_PB.Password) select Korisnik).First();
                    Application.Current.Properties["ID"] = upit.ID;

                    string vrsta = upit.Rank;

                    if (vrsta == "Administrator")
                    {
                        if (MessageBox.Show("Uspešno ste se ulogovali!", "INFO", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            this.Hide();
                            AdminPocetna ap = new AdminPocetna();
                            ap.ShowDialog();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Uspešno ste se ulogovali!", "INFO", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            this.Hide();
                            KorisnikPocetna kp = new KorisnikPocetna();
                            kp.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne postoji korisnik sa tim korisnickim imenom i sifrom!"
                               , "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Polja 'Username' i 'Password' moraju biti popunjena! ",
                        "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Close();
            Application.Current.Shutdown();         
        }

        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /////////////////////////////////////////////////////////////////
        //LOGIN VALIDACIJA
        private void username_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    username_TB.Text = "";
                    MessageBox.Show("Polje 'Username' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void password_PB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    password_PB.Password = "";
                    MessageBox.Show("Polje 'Password' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }

        }
    }
}
