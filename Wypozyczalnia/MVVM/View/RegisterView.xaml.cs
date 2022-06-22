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
using MaterialDesignThemes.Wpf;
using Wypozyczalnia.MVVM.View;

namespace Wypozyczalnia.MVVM.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void registerSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void backToLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Register() //TODO
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                string insertedName = registerInsertName.Text.Trim();
                string insertedSurname = registerInsertSurname.Text.Trim();
                string insertedEmail = registerInsertEmail.Text.Trim();
                string insertedPESEL = registerInsertPesel.Text.Trim();
                string insertedPassword = registerInsertPassword.Password.Trim();
                string insertedConfirmPassword = registerConfirmPassword.Password.Trim();
                bool parsedPhoneNumber = Int32.TryParse(registerInsertNumber.Text.Trim(), out int insertedPhoneNumber);
                //bool parsedDrivingLicenseYears = short.TryParse(Console.ReadLine().Trim(), out short insertedDrivingLicenseYears);

                if (insertedName == string.Empty || insertedSurname == string.Empty || insertedEmail == string.Empty || insertedPESEL == string.Empty || insertedPassword == string.Empty || insertedConfirmPassword == string.Empty || !parsedPhoneNumber)
                {
                    registerInfoText.Text = "Wprowadzone dane są błędne!";
                    return;
                }
                
                if (insertedPESEL.Length != 11)
                {
                    registerInfoText.Text = "Podany numer PESEL jest błędny!";
                    return;
                }

                if (insertedPhoneNumber < 100000000 || insertedPhoneNumber > 999999999)
                {
                    registerInfoText.Text = "Podany numer telefonu jest błędny!";
                    return;
                }

                foreach (var user in db.Uzytkownicy)
                {
                    if (insertedEmail == user.Email)
                    {
                        registerInfoText.Text = "Podany E-mail już istnieje!";
                        return;
                    }
                    if (insertedPESEL == user.PESEL)
                    {
                        registerInfoText.Text = "Podany PESEL już istnieje!";
                        return;
                    }
                    if (insertedPhoneNumber == user.NrTelefonu)
                    {
                        registerInfoText.Text = "Podany numer telefonu już istnieje!";
                        return;
                    }
                }

                db.Uzytkownicy.Add(new Uzytkownicy { Imie = insertedName, Nazwisko = insertedSurname, PESEL = insertedPESEL, NrTelefonu = insertedPhoneNumber, Email = insertedEmail, Haslo = insertedPassword});
                db.SaveChanges();
                registerInfoText.Text = "Użytkownik został stworzony";

                foreach (var x in registerStackPanel.Children)
                {
                    if(x.GetType() == typeof(TextBox))
                    {
                        ((TextBox)x).Text = string.Empty;
                    }
                    if(x.GetType() == typeof(PasswordBox))
                    {
                        ((PasswordBox)x).Password = string.Empty;
                    }
                }
                
            }
        }

        private void registerInsertPesel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
