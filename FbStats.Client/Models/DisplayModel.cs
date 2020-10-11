using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbStats.Client.Models
{
    public class DisplayModel
    {
        public long StatId { get; set; }
        public string TeamName { get; set; }
        public string OpponentName { get; set; }
        public int WeekNumber { get; set; }


    }
}
