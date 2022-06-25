using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Core;

namespace Wypozyczalnia.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {


       

        public RelayCommand HomeContentViewCommand { get; set;}
        public RelayCommand ContactViewCommand{get; set;}
        public RelayCommand RentaCarViewCommand { get; set; }

        public RelayCommand RentHistoryViewCommand { get; set; }


        public HomeContentViewModel HomeContentVM { get; set; }
        

        public ContactViewModel ContactVM { get; set; }

        public RentaCarViewModel RentVM { get; set; }

        public RentHistoryViewModel RentHistoryVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView;  }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel()
        {
            
            HomeContentVM = new HomeContentViewModel();
            ContactVM = new ContactViewModel();
            RentVM = new RentaCarViewModel();
            RentHistoryVM = new RentHistoryViewModel();
            CurrentView = HomeContentVM;

            HomeContentViewCommand = new RelayCommand(o=>
        {
            CurrentView = HomeContentVM;
        });
            

            ContactViewCommand = new RelayCommand(o =>
            {
                CurrentView = ContactVM;
            });

            RentaCarViewCommand = new RelayCommand(o =>
            {
                CurrentView = RentVM;
            });
            RentHistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = RentHistoryVM;
            });
            RentHistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = RentHistoryVM;
            });
        }
    }
}
