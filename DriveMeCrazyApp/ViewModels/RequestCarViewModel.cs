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
                    IdCar ="1234",
                    WhenIneedthecar = Date,
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
