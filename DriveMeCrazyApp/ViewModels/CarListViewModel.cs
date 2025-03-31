using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class CarListViewModel : ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public CarListViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Cars = new ObservableCollection<TableCar>();
            ReadCarsFromServer();
            button = new Command(OnEdit);
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
        private async void ReadCarsFromServer()
        {

            List<TableCar> listCars = await proxy.GetAllCar();
            if (listCars != null)
                Cars = new ObservableCollection<TableCar>(listCars);
            
        }

        public Command button { get; }
        public async void OnEdit()
        {
            //Navigate to the task details page
            await Shell.Current.GoToAsync("addDriver");
        }
     
    }
}
