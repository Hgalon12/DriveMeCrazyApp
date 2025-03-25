using System;
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

        public string Description
        {
            get
            {
                if (StatusId == 1)
                {
                    if (Requester != null)
                        return $"{Requester.UserName} - Between {WhenIneedthecar.Value.Hour} to {UntilWhenIneedthecar.Value.Hour}  Status: Approve";
                }

                if (Requester != null)
                    return $"{Requester.UserName} - Between {WhenIneedthecar.Value.Hour} to {UntilWhenIneedthecar.Value.Hour}  Status: Pannding";
                else
                    return $"Unknown - Between {WhenIneedthecar.Value.Hour} to {UntilWhenIneedthecar.Value.Hour}";
            }
        }

     

    }
}
