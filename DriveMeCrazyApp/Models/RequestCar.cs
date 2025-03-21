﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.Models
{
    public class RequestCar
    {
        public int RequestId { get; set; }

        public int UserId { get; set; }
        public TableUser? Requester { get; set; } 

        public string IdCar { get; set; } = null!;
        public TableCar? Car { get; set; } = null!;

        public DateTime? WhenIneedthecar { get; set; }

        public DateTime? UntilWhenIneedthecar { get; set; }
        public string Reason { get; set; } = null;

        public int StatusId { get; set; }

     

    }
}
