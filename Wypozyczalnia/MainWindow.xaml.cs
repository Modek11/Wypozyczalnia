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
using MaterialDesignThemes.Wpf;
using Wypozyczalnia.MVVM.View;

namespace Wypozyczalnia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if(isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                isDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                isDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
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

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                string insertedEmail = loginInsertEmail.Text;
                string insertedPassword = loginInsertPassword.Password;

                foreach (var user in db.Uzytkownicy)
                {
                    if (insertedEmail == user.Email)
                    {
                        if (insertedPassword == user.Haslo)
                        {
                            loginInfoText.Text = "Zalogowano poprawnie!";
                            return;
                        }
                        else
                        {
                            loginInfoText.Text = "Błędne hasło!";
                            return;
                        }
                    }
                }
                loginInfoText.Text = "Błędny email!";
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerPage = new RegisterView();
            registerPage.Show();
            this.Close();
        }

        private void forgotBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
