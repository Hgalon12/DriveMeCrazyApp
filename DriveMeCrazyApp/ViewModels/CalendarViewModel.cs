using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.ViewModels
{
    public class CalendarViewModel:ViewModelBase
    {

        private DriveMeCrazyWebAPIProxy proxy;
        public CalendarViewModel(DriveMeCrazyWebAPIProxy proxy)
        { 
            this.proxy = proxy;
        }

    }
}
