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
        private List<Samochody> userRentedCars = new List<Samochody>();
        private List<Wypozyczone> userRentedList = new List<Wypozyczone>();
        public RentaCarView()
        {
            InitializeComponent();
        }

        private void ReloadContent(int userId, int carTypeIndex)
        {
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                userRentedCars = (from car in db.Samochody select car).ToList();
                userRentedList = (from list in db.Wypozyczone select list).ToList();

                var QueryAll = (
                    from RentedCars in userRentedCars
                    orderby RentedCars.Marka, RentedCars.Model
                    select new
                    {
                        RentedCars.Marka,
                        RentedCars.Model,
                        RentedCars.Generacja,
                        Rocznik = (int)RentedCars.Rocznik,
                        MocKM = (int)RentedCars.MocKM,
                        CenaZaDobe = RentedCars.CenaDoba,
                        CenaZaDlugoTerm = RentedCars.CenaDlugoTerm,
                    }).Distinct();

                var QuerySpecific = (
                    from RentedCars in userRentedCars
                    where RentedCars.ID_KlasaPojazdu == carTypeIndex
                    orderby RentedCars.Marka, RentedCars.Model
                    select new
                    {
                        RentedCars.Marka,
                        RentedCars.Model,
                        RentedCars.Generacja,
                        Rocznik = (int)RentedCars.Rocznik,
                        MocKM = (int)RentedCars.MocKM,
                        CenaZaDobe = RentedCars.CenaDoba,
                        CenaZaDlugoTerm = RentedCars.CenaDlugoTerm,
                    }).Distinct();


                if(carTypeIndex == 0)
                {
                    rentCarDataGrid.ItemsSource = QueryAll;
                }
                else
                {
                    rentCarDataGrid.ItemsSource = QuerySpecific;
                }
            }
        }

        

        private void rentTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadContent(loggedUserId, rentTypeCombo.SelectedIndex);
        }


        private void rentChoosenCarButton_Click(object sender, RoutedEventArgs e)
        {
            var xd = ((Button)e.Source).DataContext.ToString();
            rentCarGrid.Visibility = Visibility.Hidden;
            rentChoosenCarGrid.Visibility = Visibility.Visible;
            carValuesTab =  xd.Split(' ');
            for(int i = 0; i < carValuesTab.Length; i++)
            {
                carValuesTab[i] = carValuesTab[i].Replace(",", "");
                //Marka - 3; Model - 6; Generacja - 9; Rocznik - 12; MocKM - 15; CenaDoba - 18; CenaDlugoTerm - 21;
            }
            rentChoosenCarInfo.Text = $"{carValuesTab[3]} {carValuesTab[6]} {carValuesTab[9]} {carValuesTab[12]} {carValuesTab[15]}KM";            
        }

        private void rentChosenCarReturnButton_Click(object sender, RoutedEventArgs e)
        {
            rentCarGrid.Visibility = Visibility.Visible;
            rentChoosenCarGrid.Visibility = Visibility.Hidden;
            rentRentedCarGrid.Visibility = Visibility.Hidden;
            rentChoosenInfoText.Text = "Wprowadź datę i wypożycz!";
            ReloadContent(loggedUserId, rentTypeCombo.SelectedIndex);
        }

        private void rentChoosenCarSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            rentChoosenWrongDateText.Visibility = Visibility.Hidden;
            using (WypozyczalniaEntities db = new WypozyczalniaEntities())
            {
                DateTime? from = rentChoosenDateFrom.SelectedDate;
                DateTime? to = rentChoosenDateTo.SelectedDate;
                int carIdHolder = 0;

                if (from > to || from == null || to == null)
                {
                    rentChoosenWrongDateText.Text = "Sprawdź poprawność wprowadzonych dat!";
                    rentChoosenWrongDateText.Visibility = Visibility.Visible;
                    return;
                }


                foreach(var car in userRentedCars)
                {
                    if(car.Marka == carValuesTab[3] && car.Model == carValuesTab[6] && car.Generacja == carValuesTab[9] && car.Rocznik == short.Parse(carValuesTab[12]) &&
                        car.MocKM == short.Parse(carValuesTab[15]) && car.CenaDoba == decimal.Parse(carValuesTab[18]) && car.CenaDlugoTerm == decimal.Parse(carValuesTab[21]))
                    {
                        carIdHolder = car.ID;
                        break;
                    }
                }
                
                foreach(var rent in userRentedList)
                {
                    if(rent.ID_Samochod != carIdHolder)
                    {
                        continue;
                    }

                    DateTime _from = (DateTime)from;
                    while (_from <= to)
                    {
                        if(_from == rent.DataOdbioru || _from == rent.DataZwrotu)
                        {
                            rentChoosenWrongDateText.Text = $"To auto jest już wypożyczone w dniach między {rent.DataOdbioru:dd-MM-yyyy} a {rent.DataZwrotu:dd-MM-yyyy}";
                            rentChoosenWrongDateText.Visibility = Visibility.Visible;
                            return;
                        }
                        _from = _from.AddDays(1);
                    }
                }

                db.Wypozyczone.Add(new Wypozyczone { ID_Samochod = carIdHolder, ID_Uzytkownik = loggedUserId, DataOdbioru = (DateTime)from, DataZwrotu = (DateTime)to});
                db.SaveChanges();
                rentRentedCarInfoText.Text = $"{carValuesTab[3]} {carValuesTab[6]}";
                rentChoosenWrongDateText.Visibility = Visibility.Hidden;
                rentChoosenCarGrid.Visibility = Visibility.Hidden;
                rentRentedCarGrid.Visibility = Visibility.Visible;
            }
            
        }

        
    }
}
