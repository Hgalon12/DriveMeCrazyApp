using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DriveMeCrazyApp.ViewModels
{
    public class AprroveRequestViewModel:ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public AprroveRequestViewModel(DriveMeCrazyWebAPIProxy proxy) 
        {
            this.proxy = proxy;
            Req = new ObservableCollection<RequestCar>();
            ReadReqFromServer();
            ApproveCommand = new Command(ChangeRestStatusToApprove);
            DeclineCommand = new Command(ChangeRestStatusToDecline);
        }

        private async void ReadReqFromServer()
        {

            List<RequestCar> list = await proxy.GetAllRequest();    
            Req = new ObservableCollection<RequestCar>(list);

        }

        private ObservableCollection<RequestCar> req;
        public ObservableCollection<RequestCar> Req
        {
            get => req;
            set
            {
                req = value;
                OnPropertyChanged();
            }
        }
        private RequestCar selectedReq;
        public RequestCar SelectedReq
        {
            get => selectedReq;
            set { selectedReq = value; OnPropertyChanged(); }
        }
        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }
        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public async void ChangeRestStatusToApprove()
        {
            ErrorMsg = "";
            bool success = await this.proxy.ChangeRestStatusToApproved(SelectedReq);
            
            if (success)
            {
                ReadReqFromServer();

                await Application.Current.MainPage.DisplayAlert("Success!", "Request status changed to approved", "ok");
                ReadReqFromServer();
            }
            else
                ErrorMsg = "Something Went Wrong";
           
        }

        public async void ChangeRestStatusToDecline()
        {
            ErrorMsg = "";
            bool success = await this.proxy.ChangeRestStatusToReject(SelectedReq);
            if (success)
            {
                ReadReqFromServer();
                
                await Application.Current.MainPage.DisplayAlert("Success!", "request's status changed to declined", "ok");
                ReadReqFromServer();
            }
            else
                ErrorMsg = "Something went Wrong";
           
        }
    }
}
