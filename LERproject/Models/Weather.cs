using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LERproject.Models
{
    public class Weather
    {

        public string zipCode { get; set; }
        public string date { get; set; }
        public string tempMax { get; set; }
        public string tempMin { get; set; }
        public string windSpeedMax { get; set; }


        public Weather()
        {
            zipCode = string.Empty;
            date = string.Empty;
            tempMax = string.Empty;
            tempMin = string.Empty;
            windSpeedMax = string.Empty;

        }



    }
}
