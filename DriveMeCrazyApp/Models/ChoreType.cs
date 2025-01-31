using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveMeCrazyApp.Models
{
    public class ChoreType
    {
        public int ChoreId { get; set; }

        public string NameChore { get; set; } = null;

        public int Score { get; set; }

        public string IdCar { get; set; } = null!;

    }
}
