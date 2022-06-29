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

namespace Wypozyczalnia.MVVM.View
{
    /// <summary>
    /// Interaction logic for RentHistoryView.xaml
    /// </summary>
    public partial class RentHistoryView : UserControl
    {
        int loggedUserId = (int)Application.Current.Properties["loggedUserId"];
        bool isLoggedUserAdmin = (bool)Application.Current.Properties["isLoggedUserAdmin"];

        public RentHistoryView()
        {
            InitializeComponent();
            CheckIfAdmin(isLoggedUserAdmin);
            ReloadContent(loggedUserId);
        }

        public void CheckIfAdmin(bool isLoggedUserAdmin)
        {
            if (isLoggedUserAdmin)
            {
                adminHistoryInsert.Visibility = Visibility.Visible;
                adminHistoryButton.Visibility = Visibility.Visible;
                adminHistoryText.Visibility = Visibility.Visible;
                userHistoryText.Visibility = Visibility.Hidden;
            }
        }

        public void ReloadContent(int userId)
        {
            List<Samochody> userRentedCars = new List<Samochody>();
            List<Wypozyczone> userRentedList = new List<Wypozyczone>();
            
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                userRentedCars = (from car in db.Samochody select car).ToList();
                userRentedList = (from list in db.Wypozyczone select list).ToList();

                
                var Query = (
                    from RentedList in userRentedList
                    join RentedCars in userRentedCars
                    on RentedList.ID_Samochod equals RentedCars.ID
                    where RentedList.ID_Uzytkownik == userId
                    select new
                    {
                        Marka = RentedCars.Marka,
                        Model = RentedCars.Model,
                        DataOdbioru = RentedList.DataOdbioru.ToString("dd-MM-yyyy"),
                        DataZwrotu = RentedList.DataZwrotu.ToString("dd-MM-yyyy")
                    });


                carRentalHistory.ItemsSource = Query;
            }

                
        }

        private void adminHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            
            string insertedEmail = adminHistoryInsert.Text;
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                foreach (var user in db.Uzytkownicy)
                {
                    if(insertedEmail == user.Email)
                    {
                        ReloadContent(user.ID);
                        adminHistoryText.Text = $"{user.Imie} {user.Nazwisko}";
                        return;
                    }
                }
                adminHistoryText.Text = "Nie odnaleziono E-maila w bazie danych!";
            }


        }
    }
}
