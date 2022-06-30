using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        private void CheckIfDatabaseFound()
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                if (!db.Database.Exists())
                {
                    loginInfoText.Text = "Błąd połączenia z bazą danych!";
                    foreach (var box in loginStackPanel.Children)
                    {
                        var type = box.GetType();
                        if (type == typeof(Button))
                        {
                            ((Button)box).IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void Login()
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                string insertedEmail = loginInsertEmail.Text;
                string insertedPassword = loginInsertPassword.Password;

                foreach (var user in db.Uzytkownicy)
                {
                    if (insertedEmail != user.Email)
                    {
                        continue;
                    }

                    if (insertedPassword == user.Haslo)
                    {
                        HomeView homepage = new HomeView();
                        homepage.Show();
                        Application.Current.Properties["loggedUserId"] = user.ID;
                        Application.Current.Properties["isLoggedUserAdmin"] = user.CzyPracownik;
                        this.Close();
                    }
                    else
                    {
                        loginInfoText.Text = "Błędne hasło!";
                        return;
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
            ForgottenPasswordView forgottenPasswordView = new ForgottenPasswordView();
            forgottenPasswordView.Show();
            this.Close();
        }
    }
}
