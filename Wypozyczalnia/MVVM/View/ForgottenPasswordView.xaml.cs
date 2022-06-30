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
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Text.RegularExpressions;


namespace Wypozyczalnia.MVVM.View
{
    /// <summary>
    /// Interaction logic for ForgottenPasswordView.xaml
    /// </summary>
    public partial class ForgottenPasswordView : Window
    {
        private bool isEmailSent = false;
        private string newPasswordCode = string.Empty;
        private int userID;

        public ForgottenPasswordView()
        {
            InitializeComponent();
        }
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void forgottenPasswordSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isEmailSent)
            {
                if(forgottenPasswordInsertCode.Text != newPasswordCode)
                {
                    forgottenPasswordInfoText.Text = "Wprowadzony kod jest błędny!";
                    return;
                }
                if(forgottenPasswordInsertNewPassword.Password == string.Empty)
                {
                    forgottenPasswordInfoText.Text = "Wprowadź nowe hasło!";
                    return;
                }

                using (WypozyczalniaEntities db = new WypozyczalniaEntities())
                {
                    foreach (var user in db.Uzytkownicy)
                    {
                        if (user.ID == userID)
                        {
                            user.Haslo = forgottenPasswordInsertNewPassword.Password;
                            break;
                        }
                    }

                    db.SaveChanges();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    mainWindow.loginInfoText.Text = "Hasło zostało poprawnie zmienione! Możesz się teraz zalogować do konta!";
                    this.Close();
                }
            }
            else
            {
                
                using (WypozyczalniaEntities db = new WypozyczalniaEntities())
                {
                    foreach (var user in db.Uzytkownicy)
                    {
                        if (user.Nazwisko == forgottenPasswordInsertSurname.Text && user.PESEL == forgottenPasswordInsertPesel.Text && user.Email == forgottenPasswordInsertEmail.Text)
                        {
                            userID = user.ID;
                            break;
                        }
                    }
                }

                if (userID == 0)
                {
                    forgottenPasswordInfoText.Text = "Nie znaleziono użytkownika!";
                    return;
                }

                string insertedEmail = forgottenPasswordInsertEmail.Text.Trim();
                SendEmail(insertedEmail);
                isEmailSent = true;
                forgottenPasswordInsertEmail.IsEnabled = false;
                forgottenPasswordInsertPesel.IsEnabled = false;
                forgottenPasswordInsertSurname.IsEnabled = false;
                forgottenPasswordInsertCode.IsEnabled = true;
                forgottenPasswordInsertNewPassword.IsEnabled = true;
                forgottenPasswordInfoText.Text = "Wprowadź nowe hasło oraz kod wysłany na adres E-mail";
                forgottenPasswordSubmitBtn.Content = "Zmień hasło";
            }
            
            
            
            
        }

        private void fpBackToLoginBtn_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void SendEmail(string insertedEmail)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("grupawariatuw@interia.pl", "Wypożyczalnia Samochodowa");
                mail.To.Add(insertedEmail);
                mail.Subject = "Kod do zresetowania hasła do konta";
                mail.Body = GenerateResetPasswordCode(10);
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("poczta.interia.pl", 587))
                {
                    smtp.Credentials = new NetworkCredential("grupawariatuw@interia.pl", "tomczyk1");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                    forgottenPasswordInfoText.Text = "E-mail wysłano!";
                }
            }
        }

        public string GenerateResetPasswordCode(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            newPasswordCode = res.ToString();
            return newPasswordCode;
        }

        private void forgottenPasswordInsertPesel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
