﻿using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class RegisterViewModel:ViewModelBase
    {
        private bool iskid;
        public bool Iskid
        {
            get => iskid;
            set
            {
                iskid = value;
                OnPropertyChanged("Iskid");


            }
        }
        private DriveMeCrazyWebAPIProxy proxy;

        public RegisterViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            GalleryPhotoCommand = new Command(OnGalleryPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            IsPassword = true;
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            PhoneNumError = "Phone number must be 10 digits";
            Iskid = false;
           
        }



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

        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
        }
        #endregion
        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion
        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        //This property will indicate if the password entry is a password
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
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

        #region Email
        private bool showEmailOwnerError;

        public bool ShowEmailOwnerError
        {
            get => showEmailOwnerError;
            set
            {
                showEmailOwnerError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string emailOwner;

        public string EmailOwner
        {
            get => emailOwner;
            set
            {
                emailOwner = value;
                ValidateEmailOwner();
                OnPropertyChanged("Email");
            }
        }

        private string emailErrorOwner;

        public string EmailErrorOwner
        {
            get => emailErrorOwner;
            set
            {
                emailErrorOwner = value;
                OnPropertyChanged("EmailErrorOwner");
            }
        }

        private void ValidateEmailOwner()
        {
            this.ShowEmailOwnerError = string.IsNullOrEmpty(Email);
            if (!ShowEmailOwnerError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailErrorOwner = "Email is not valid";
                    ShowEmailOwnerError = true;
                }
                else
                {
                    EmailErrorOwner = "";
                    ShowEmailOwnerError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion



        #region PhoneNum
        private bool showPhoneNumError;

        public bool ShowPhoneNumError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPhoneNumError");
            }
        }

        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                ValidatePassword();
                OnPropertyChanged("PhoneNum");
            }
        }

        private string phoneNumError;

        public string PhoneNumError
        {
            get => phoneNumError;
            set
            {
                phoneNumError = value;
                OnPropertyChanged("PhoneNumError");
            }
        }

        private void ValidatePhoneNumError()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(phoneNum) ||
                phoneNum.Length < 9 ||
                !phoneNum.Any(char.IsDigit))
            {
                this.ShowPhoneNumError = true;
            }
            else
                this.ShowPhoneNumError = false;
        }

        //This property will indicate if the password entry is a password

        #endregion

        #region radio

        

        

        #endregion




        //Define a command for the register button
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }
        public async void OnRegister()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhoneNumError();
           

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowPhoneNumError )
            {
                //Create a new AppUser object with the data from the registration form
                var newUser = new TableUser
                {
                    UserName = Name,
                    UserLastName = LastName,
                    UserEmail = Email,
                    UserPassword = Password,
                    UserPhoneNum=PhoneNum,
                     IsManager = false
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await proxy.Register(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new Login { UserEmail = newUser.UserEmail, UserPassword = newUser.UserPassword });
                        TableUser? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                    }
                    InServerCall = false;

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
        }

        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }



    }
}
