using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.Models
{
  public  class TableUser
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string CarId { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

    

        public string UserPhoneNum { get; set; } = null!;
        public bool IsManager { get; set; }
    }
}
