using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DriveMeCrazyApp.ViewModels
{
   public class DriverCarViewModel:ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public DriverCarViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Cars = new ObservableCollection<TableCar>();
            Users = new ObservableCollection<TableUser>();
            AddCommand=new Command (OnAdd); 
            ReadCarsFromServer();
            ReadUsersFromServer();
            
        }
        private async void ReadCarsFromServer()
        {

            List<TableCar> listCars = await proxy.GetAllCar();
            if (listCars != null)
                Cars = new ObservableCollection<TableCar>(listCars);



        }
        private ObservableCollection<TableUser> users;
        public ObservableCollection<TableUser> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }
        private TableUser selectedUser;
        public TableUser SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; OnPropertyChanged(); }
        }
        private async void ReadUsersFromServer()
        {

            List<TableUser> listUsers = await proxy.GetAllUser();
            if (listUsers != null)
                Users = new ObservableCollection<TableUser>(listUsers);



        }
        private ObservableCollection<TableCar> cars;
        public ObservableCollection<TableCar> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged();
            }
        }
        private TableCar selectedCar;
        public TableCar SelectedCar
        {
            get => selectedCar;
            set { selectedCar = value; OnPropertyChanged(); }
        }

        public Command AddCommand { get; }


        public async void OnAdd()
        {

            var driver = new DriverCar
            {
                UserId = SelectedUser.Id,
                IdCar = SelectedCar.IdCar,
                Status = 1

            };

                // מבצע את הקריאה לשירות לצורך רישום הרכב
                InServerCall = true;
                driver = await proxy.AddDriver(SelectedCar.IdCar,SelectedUser.Id); // יש לוודא שהשירות תומך ב-RegisterCar
                InServerCall = false;

                // אם הרישום היה מוצלח
                if (driver != null)
                {


                    InServerCall = false;

                    // אם הכל בסדר, נווט לאחור
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {
                    // אם הרישום נכשל, מציג הודעת שגיאה
                    string errorMsg = "Add Driver   failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            
        }

    }
}
