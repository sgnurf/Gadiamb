using System;
using System.Collections.Generic;
using System.Text;

namespace GadiamBlazor.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; } = default!;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
