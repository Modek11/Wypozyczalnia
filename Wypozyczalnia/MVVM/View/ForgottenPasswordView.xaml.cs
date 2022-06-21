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

namespace Wypozyczalnia.MVVM.View
{
    /// <summary>
    /// Interaction logic for ForgottenPasswordView.xaml
    /// </summary>
    public partial class ForgottenPasswordView : Window
    {
        public ForgottenPasswordView()
        {
            InitializeComponent();
        }

        //fagib96160@tagbert.com

        private void fpBackToLoginBtn_Click(object sender, EventArgs e)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("grupastudentuw@gmail.com");
                mail.To.Add("technikinformatykprogramista@gmail.com");
                mail.Subject = "Test Sending Mail";
                mail.Body = "<h1>DUPCIA</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com",587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("grupastudentuw@gmail.com","Klisiewicz1");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    forgottenPasswordInfoText.Text = "E-mail wysłano!";
                }

            }
        }

    }
}
