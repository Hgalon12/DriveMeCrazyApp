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
            bool success = await this.proxy.ChangeRestStatusToApproved(SelectedReq.RequestId);
            if (success)
            {
                ErrorMsg = "Status Changed to Approved";
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
                ErrorMsg = "Status Changed To Declined";
            }
            else
                ErrorMsg = "Something went Wrong";
           
        }
    }
}
