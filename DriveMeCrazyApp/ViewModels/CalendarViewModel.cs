using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DriveMeCrazyApp.ViewModels
{
    public class CalendarViewModel:ViewModelBase
    {
        private ObservableCollection<RequestCar> requests;
        public ObservableCollection<RequestCar> Requests
        {
            get => requests;
            set
            {
                requests = value;
                OnPropertyChanged();
            }
        }
        private DriveMeCrazyWebAPIProxy proxy;
        public CalendarViewModel(DriveMeCrazyWebAPIProxy proxy)
        { 
            this.proxy = proxy;
            Requests = new ObservableCollection<RequestCar>();
            Events = new EventCollection();
            ApproveCommand= new Command<RequestCar>(OnApprove);
            DeclineCommand= new Command<RequestCar>(OnDecline);
            RequestCarCommand = new Command(OnRequest);
            ReadRequestCar();


        }
        public async void ReadRequestCar()
        {
            List<RequestCar> list = await proxy.GetAllRequest12();
            if (list != null)
                requests = new ObservableCollection<RequestCar>(list);
            else
                requests = new ObservableCollection<RequestCar>();

            InitEvents();

        }

        public bool IsManager
        {
            get
            {
                TableUser currentUser = ((App)Application.Current).LoggedInUser;
                return currentUser.IsManager;
            }
        }

        public EventCollection Events { get; set; }

        private void InitEvents()
        {

            try
            {
                Events = new EventCollection();
                foreach (RequestCar request in requests)
                {
                    bool exist = false;
                    try
                    {
                        object o = Events[request.WhenIneedthecar.Value];
                        exist = true;
                    }
                    catch 
                    {
                        exist = false;
                    }
                    if (request.WhenIneedthecar != null && exist)
                    {

                        ObservableCollection<RequestCar> list = (ObservableCollection<RequestCar>)(Events[request.WhenIneedthecar.Value]);
                        list.Add(request);
                    }
                    else
                    {
                        ObservableCollection<RequestCar> list = new ObservableCollection<RequestCar>();
                        list.Add(request);
                        if (request.WhenIneedthecar != null)
                            Events.Add(request.WhenIneedthecar.Value, list);
                    }

                }

                OnPropertyChanged("Events");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }

        public ICommand ApproveCommand { get; set; }    
        public async void OnApprove(RequestCar r)
        {
            bool success = await this.proxy.ChangeRestStatusToApproved(r);
            Refresh();
        }

        public ICommand DeclineCommand { get; set; }
        public async void OnDecline(RequestCar r)
        {
            bool success = await this.proxy.ChangeRestStatusToReject(r);
            Refresh();
        }
        public ICommand RequestCarCommand { get; set; }
        public async void OnRequest()
        {
            //Navigate to the task details page
            await Shell.Current.GoToAsync("requestCar");
        }
        public override void Refresh()
        {
            ReadRequestCar();
          

        }
    }
}
