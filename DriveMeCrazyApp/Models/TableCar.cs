using DriveMeCrazyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.Models
{
    public class TableCar
    {
        public string IdCar { get; set; }
    
        public int OwnerId { get; set; }
        public string NickName { get; set; } = null!;

        public string CarImagePath { get; set; } = "";

       public string FullImagePath
        {
            get
            
            {
                return DriveMeCrazyWebAPIProxy.ImageBaseAddress+ this.CarImagePath;
            }
        }


    }
}
