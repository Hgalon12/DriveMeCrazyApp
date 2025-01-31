using CommunityToolkit.Maui.Converters;
using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class AddCarViewModel:ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;

        public AddCarViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            this.RegisterCarCommand = new Command(OnRegister);
       
            UploadPhotoCommand = new Command(OnUploadPhoto);
            GalleryPhotoCommand = new Command(OnGalleryPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            CarIdError = "CarId is required";
            NickNameError = "NickName is required";
        }
        #region Car Type

        private int carType;

        public int CarType
        {
            get => carType;
            set
            {
                carType = value;
                OnPropertyChanged("CarType");
            }
        }




        #endregion

        #region CarId
        private bool showCarIdError;

        public bool ShowCarIdError
        {
            get => showCarIdError;
            set
            {
                showCarIdError = value;
                OnPropertyChanged("ShowCarIdError");
            }
        }

        private string carId;

        public string CarId
        {
            get => carId;
            set
            {
                carId = value;
                ValidateCarIdError();
                OnPropertyChanged("CarId");
            }
        }

        private string carIdError;

        public string CarIdError
        {
            get => carIdError;
            set
            {
                carIdError = value;
                OnPropertyChanged("CarIdError");
            }
        }

        private void ValidateCarIdError()
        {
            if (CarId == null)
            {
                this.ShowCarIdError = true;
            }
          
        }
        #endregion

     
        #region Num of places
        private bool showNumOfPlacesError;

        public bool ShowNumOfPlacesError
        {
            get => showNumOfPlacesError;
            set
            {
                showNumOfPlacesError = value;
                OnPropertyChanged("ShowNumOfPlacesError");
            }
        }

        private int numOfPlaces;

        public int NumOfPlaces
        {
            get => numOfPlaces;
            set
            {
                numOfPlaces = value;
                ValidateNumOfPlaces();
                OnPropertyChanged("numOfPlaces");
            }
        }

        private string numOfPlacesError;

        public string NumOfPlacesError
        {
            get => numOfPlacesError;
            set
            {
                numOfPlacesError = value;
                OnPropertyChanged("numOfPlacesError");
            }
        }

        private void ValidateNumOfPlaces()
        {
            if (numOfPlaces == null)
            {
                this.ShowNumOfPlacesError = true;
            }

        }
        #endregion

        #region NickName
        private bool showNickNameError;

        public bool ShowNickNameError
        {
            get => showNickNameError;
            set
            {
                showNickNameError = value;
                OnPropertyChanged("ShowNickNameError");
            }
        }

        private string nickName;

        public string NickName
        {
            get => nickName;
            set
            {
                nickName = value;
                ValidateNickName();
                OnPropertyChanged("NickName");
            }
        }

        private string nickNameError;

        public string NickNameError
        {
            get => nickNameError;
            set
            {
                nickNameError = value;
                OnPropertyChanged("NickNameError");
            }
        }

        private void ValidateNickName()
        {
            this.ShowNickNameError = string.IsNullOrEmpty(NickName);
        }
        #endregion

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }
        public Command GalleryPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnGalleryPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion
        public Command RegisterCarCommand { get; }

     


        public async void OnRegister()
        {
          
            ValidateCarIdError();
            ValidateNickName(); // לדוגמה, וולידציה לשם הרכב
             // וולידציה לנתיב התמונה של הרכב

            // אם כל הוולידציות עברו
            if (!ShowCarIdError && !ShowNickNameError)
            {
                // יוצרים אובייקט חדש של רכב עם הנתונים מהטופס
                var newCar = new TableCar
                {
                    NickName = NickName,   // שם הרכב
                    OwnerId = ((App)Application.Current).LoggedInUser.Id,  
                    IdCar=CarId
                    // מזהה בעל הרכב (אם יש)
                   // נתיב לתמונה של הרכב
                };

                // מבצע את הקריאה לשירות לצורך רישום הרכב
                InServerCall = true;
                newCar = await proxy.RegisterCar(newCar); // יש לוודא שהשירות תומך ב-RegisterCar
                InServerCall = false;

                // אם הרישום היה מוצלח
                if (newCar != null)
                {
                    // אם צריך להעלות תמונה של הרכב
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        TableCar? updatedCar = await proxy.UploadCarImage(LocalPhotoPath,newCar.IdCar); 
                        if (updatedCar == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Registration", "Car Data Was Saved BUT Car image upload failed", "ok");
                        }
                    }

                    InServerCall = false;
                  
                    // אם הכל בסדר, נווט לאחור
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {
                    // אם הרישום נכשל, מציג הודעת שגיאה
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
        }
        






    }
    

   
}
