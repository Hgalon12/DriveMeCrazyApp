using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Android.Provider.ContactsContract.CommonDataKinds;

namespace DriveMeCrazyApp.ViewModels
{
    public class RequestCarViewModel : ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public RequestCarViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            ReasonError = "Request Is Require";
            Date = DateTime.Now;
            Cars = new ObservableCollection<TableCar>();
            ReadCarsFromServer();
            RequestCommand = new Command(OnRegister);

        }

        private async void ReadCarsFromServer()
        {
           
                List<TableCar> listCars = await proxy.GetAllCarByDriver();
                Cars = new ObservableCollection<TableCar>(listCars);
               

            
            //Call proxy method to read cars from servers
            //List<T....> l = proxy.tatatata...
            //Cars = new Obser///<////>(l);
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
        #region Reason
        private bool showReasonError;

        public bool ShowReasonError
        {
            get => showReasonError;
            set
            {
                showReasonError = value;
                OnPropertyChanged("ShowReasonError");
            }
        }

        private string reason;

        public string Reason
        {
            get => reason;
            set
            {
                reason = value;
                ValidateReason();
                OnPropertyChanged("Reason");
            }
        }

        private string reasonError;

        public string ReasonError
        {
            get => reasonError;
            set
            {
                reasonError = value;
                OnPropertyChanged("ReasonError");
            }
        }

        private void ValidateReason()
        {
            this.ShowReasonError = string.IsNullOrEmpty(Reason);
        }
        #endregion
        private TimeSpan hours;
        public TimeSpan Hours
        {
            get => hours;
            set { hours = value; OnPropertyChanged(); }
        }

        private TimeSpan hoursUntil;
        public TimeSpan HoursUntil
        {
            get => hoursUntil;
            set { hoursUntil = value; OnPropertyChanged(); }
        }
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public Command RequestCommand { get; }
        public async void OnRegister()
        {

            ValidateReason();

            // אם כל הוולידציות עברו
            if (!ShowReasonError)
            {
                // יוצרים אובייקט חדש של רכב עם הנתונים מהטופס
                var newRequest = new RequestCar
                {

                    UserId = ((App)Application.Current).LoggedInUser.Id,
                    IdCar = selectedCar.IdCar,
                    WhenIneedthecar = new DateTime(Date.Year, Date.Month, Date.Day, Hours.Hours, Hours.Minutes, 0, DateTimeKind.Unspecified),
                    UntilWhenIneedthecar = new DateTime(Date.Year, Date.Month, Date.Day, HoursUntil.Hours, HoursUntil.Minutes, 0, DateTimeKind.Unspecified),
                    Reason = Reason,

                };

                // מבצע את הקריאה לשירות לצורך רישום הרכב
                InServerCall = true;
                newRequest = await proxy.requestCar(newRequest); // יש לוודא שהשירות תומך ב-RegisterCar
                InServerCall = false;

                // אם הרישום היה מוצלח
                if (newRequest != null)
                {


                    InServerCall = false;

                    // אם הכל בסדר, נווט לאחור
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {
                    // אם הרישום נכשל, מציג הודעת שגיאה
                    string errorMsg = "Request failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }




        }
     
    }
}
