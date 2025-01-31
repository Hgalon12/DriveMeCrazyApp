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
    public class AssignmentViewModel : ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public AssignmentViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            NameError = " Chore Name Is Require";
            ScoreError = "Score is Require";
           
            AddCommand=new Command(OnAdd);
            Cars = new ObservableCollection<TableCar>();
            ReadCarsFromServer();

        }
        private async void ReadCarsFromServer()
        {

            List<TableCar> listCars = await proxy.GetAllCar();
            Cars = new ObservableCollection<TableCar>(listCars);



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
       


        #region score
        private bool showScoreError;

        public bool ShowScoreError
        {
            get => showScoreError;
            set
            {
                showScoreError = value;
                OnPropertyChanged("showScoreError");
            }
        }

        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                ValidateScore();
                OnPropertyChanged("score");
            }
        }

        private string scoreError;

        public string ScoreError
        {
            get => scoreError;
            set
            {
                scoreError = value;
                OnPropertyChanged("scoreError");
            }
        }

        private void ValidateScore()
        {
            if (score == null)
            {
                this.ShowScoreError = true;
            }

        }
        #endregion


        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion


        public Command AddCommand { get; }

        public async void OnAdd()
        {

           
            ValidateName();
            ValidateScore();// לדוגמה, וולידציה לשם הרכב
                            // וולידציה לנתיב התמונה של הרכב

            // אם כל הוולידציות עברו
            if ( !ShowNameError && !ShowScoreError)
            {

                var chore = new ChoreType
                {
                    NameChore = Name,   // שם הרכב
                    Score = Score,
                    IdCar = SelectedCar.IdCar,

                };

                // מבצע את הקריאה לשירות לצורך רישום הרכב
                InServerCall = true;
                chore = await proxy.AddChore(chore); // יש לוודא שהשירות תומך ב-RegisterCar
                InServerCall = false;

                // אם הרישום היה מוצלח
                if (chore != null)
                {
                    

                    InServerCall = false;

                    // אם הכל בסדר, נווט לאחור
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {
                    // אם הרישום נכשל, מציג הודעת שגיאה
                    string errorMsg = "Add chore  failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
        }
    }
}