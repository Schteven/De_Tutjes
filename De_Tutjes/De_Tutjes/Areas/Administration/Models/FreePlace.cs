using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Tutjes.Areas.Administration.Models
{
    public class FreePlace
    {
        public DateTime Day { get; set; }
        public int FreePlaces { get; set; }

        public FreePlace()
        {

        }

        public FreePlace(DateTime Day, int FreePlaces)
        {
            this.Day = Day;
            this.FreePlaces = FreePlaces;
        }


    }
}
