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
using Wypozyczalnia.Core;

namespace Wypozyczalnia.MVVM.View
{
    /// <summary>
    /// Interaction logic for ChangePasswordView.xaml
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        private int loggedUserId = (int)Application.Current.Properties["loggedUserId"];
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void changePasswordSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (changePasswordOld.Password == string.Empty || changePasswordNew.Password == string.Empty || changePasswordNewRepeat.Password == string.Empty)
            {
                changePasswordInfoText.Text = "Wypełnij wszystkie pola";
                return;
            }

            if (changePasswordNew.Password != changePasswordNewRepeat.Password)
            {
                changePasswordInfoText.Text = "Wprowadzone nowe hasła różnią się od siebie!";
                return;
            }

            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                foreach (var user in db.Uzytkownicy)
                {
                    if (user.ID == loggedUserId)
                    {
                        if(user.Haslo != EncryptDecrypt.EncryptPlainTextToCipherText(changePasswordOld.Password))
                        {
                            changePasswordInfoText.Text = "Podane hasło nie jest aktualnym hasłem konta!";
                            return;
                        }
                        user.Haslo = EncryptDecrypt.EncryptPlainTextToCipherText(changePasswordNewRepeat.Password);
                        break;
                    }
                }

                db.SaveChanges();
                changePasswordInfoText.Text = "Hasło zostało poprawnie zmienione!";
                changePasswordOld.IsEnabled = false;
                changePasswordNew.IsEnabled = false;
                changePasswordNewRepeat.IsEnabled = false;
                changePasswordSubmitButton.IsEnabled = false;
            }
        }
    }
}
