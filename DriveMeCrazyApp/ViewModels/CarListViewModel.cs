using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public  class CarListViewModel:ViewModelBase
    {
        private DriveMeCrazyWebAPIProxy proxy;
        public CarListViewModel(DriveMeCrazyWebAPIProxy proxy)
        {
            this.proxy = proxy;

        }

    }
}
