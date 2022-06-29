using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for RentaCarView.xaml
    /// </summary>
    public partial class RentaCarView : UserControl
    {
        private int loggedUserId = (int)Application.Current.Properties["loggedUserId"];
        private string[] carValuesTab;
        public RentaCarView()
        {
            InitializeComponent();
            ReloadContent(loggedUserId);
        }

        private void ReloadContent(int userId)
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
                    where RentedList.DataZwrotu < DateTime.Today
                    select new
                    {
                        RentedCars.Marka,
                        RentedCars.Model,
                        RentedCars.Generacja,
                        Rocznik = (int)RentedCars.Rocznik,
                        MocKM = (int)RentedCars.MocKM,
                        CenaZaDobe = RentedCars.CenaDoba,
                        CenaZaDlugoTerm = RentedCars.CenaDlugoTerm,
                        //DataOdbioru = RentedList.DataOdbioru.ToString("dd-MM-yyyy"),
                        //DataZwrotu = RentedList.DataZwrotu.ToString("dd-MM-yyyy")
                    });
                
                rentCarDataGrid.ItemsSource = Query;
                
            }
        }
        //= String.Concat(a.Split(','));

        private void rentChoosenCarButton_Click(object sender, RoutedEventArgs e)
        {
            var xd = ((Button)e.Source).DataContext.ToString();
            rentCarGrid.Visibility = Visibility.Hidden;
            rentChoosenCar.Visibility = Visibility.Visible;
            carValuesTab =  xd.Split(' ');
            //a = a.Replace(",", "");
            for(int i = 0; i < carValuesTab.Length; i++)
            {
                carValuesTab[i] = carValuesTab[i].Replace(",", "");
                //Marka - 3; Model - 6; Generacja - 9; Rocznik - 12; MocKM - 15; CenaDoba - 18; CenaDlugoTerm - 21;
            }
            rentChoosenCarInfo.Text = $"{carValuesTab[3]} {carValuesTab[6]} {carValuesTab[9]} {carValuesTab[12]} {carValuesTab[15]}KM";


            foreach (string x in carValuesTab)
            {
                //MessageBox.Show(x);
            }


            
        }

        private void rentChosenCarReturnButton_Click(object sender, RoutedEventArgs e)
        {
            rentCarGrid.Visibility = Visibility.Visible;
            rentChoosenCar.Visibility = Visibility.Hidden;
        }

        private void rentChoosenCarSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                DateTime from = rentChoosenDateFrom.SelectedDate.Value;
                DateTime to = rentChoosenDateTo.SelectedDate.Value;
                db.Wypozyczone.Add(new Wypozyczone { ID_Samochod = 1, ID_Uzytkownik = loggedUserId, DataOdbioru = from, DataZwrotu = to});
                db.SaveChanges();
            }
            
        }
    }
}
