using CommunityToolkit.Maui.Converters;
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

        }

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

        private int carId;

        public int CarId
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


    }
}
