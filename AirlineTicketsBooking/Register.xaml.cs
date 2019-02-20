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
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace AirlineTicketsBooking
{
    public partial class Register : Window
    {
       
        public Register()
        {
            InitializeComponent();
        }

        private bool DoesDataExist(string DataEntry)

        {
            BazaDataContext kdc = new BazaDataContext();

            var query = from Korisnik in kdc.Korisniks
                        where Korisnik.Username.Contains(username_TB.Text)
                        select Korisnik.Username;

            // return false if the item already exists

            if (query.Any())
                return false;
            else
                return true;

        }
        private bool IsEmailValid(bool email)
        {
            if (!Regex.IsMatch(email_TB.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Polje 'Email' nije u validnom formatu! Primer: demo@gmail.com", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Warning);
                email_TB.Select(0, email_TB.Text.Length);
                email_TB.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (imeprezime_TB.Text != String.Empty && username_TB.Text != String.Empty && password_b.Password != String.Empty &&
                email_TB.Text != String.Empty && adresa_TB.Text != String.Empty && telefon_TB.Text != String.Empty && grad_TB.Text != String.Empty && drzava_TB.Text != String.Empty)
            { 
                try
                {
                    if(IsEmailValid(true))
                    { 
                        int telefon = int.Parse(telefon_TB.Text);

                        using (BazaDataContext DC = new BazaDataContext())
                        {
                            if (DoesDataExist(username_TB.Text))
                            {

                                Korisnik kor = new Korisnik();
                                kor.Username = username_TB.Text;
                                kor.Password = password_b.Password;
                                kor.ImePrezime = imeprezime_TB.Text;
                                kor.Adresa = adresa_TB.Text;
                                kor.Email = email_TB.Text;
                                kor.Telefon = telefon;
                                kor.Grad = grad_TB.Text;
                                kor.Drzava = drzava_TB.Text;
                                kor.Rank = "Korisnik";

                                DC.Korisniks.InsertOnSubmit(kor);
                                DC.SubmitChanges();

                                MessageBox.Show("Uspesno ste se registrovali.", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Information);

                                imeprezime_TB.Text = "";
                                username_TB.Text = "";
                                password_b.Password = "";
                                email_TB.Text = "";
                                adresa_TB.Text = "";
                                telefon_TB.Text = "";
                                grad_TB.Text = "";
                                drzava_TB.Text = "";
                                this.Hide();
                                Login log = new Login();
                                log.ShowDialog();
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
                    MessageBox.Show("Došlo je do greske!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
  
        private void username_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            username_TB.BorderBrush = Brushes.White;
        }

        private void password_b_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password_b.BorderBrush = Brushes.White;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.ShowDialog();
        }

        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //REGISTER VALIDACIJA
        private void telefon_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsNumber(c))
                {
                    telefon_TB.Text = "";
                    MessageBox.Show("Polje 'Telefon' moze sadrzati samo brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void imeprezime_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    imeprezime_TB.Text = "";
                    MessageBox.Show("Polje 'Ime i prezime' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

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

        private void password_b_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    password_b.Password = "";
                    MessageBox.Show("Polje 'Password' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }

        }

        private void adresa_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsNumber(c))
                {
                    adresa_TB.Text = "";
                    MessageBox.Show("Polje 'Adresa' moze sadrzati samo slova i brojeve!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void grad_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    grad_TB.Text = "";
                    MessageBox.Show("Polje 'Grad' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }

        private void drzava_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    drzava_TB.Text = "";
                    MessageBox.Show("Polje 'Drzava' moze sadrzati samo slova!",
                                       "INFO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Handled = true;
                }
            }
        }
    }
}
