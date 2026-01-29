using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuroraForcastMS.Models
{
    public class KpIndexInfo
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("kp_index")]
        public string KpValueString { get; set; }

        // En hjälp-property för att få värdet som en siffra (double) direkt
        public double KpValue => double.TryParse(KpValueString, out var val) ? val : 0;

        // En hjälp-property för att ge användaren en textbeskrivning baserat på värdet
        public string Intensity => KpValue switch
        {
            < 3 => "Lugnt",
            < 5 => "Aktivt",
            < 7 => "Starkt norrsken!",
            _ => "Extrem geomagnetisk storm!"
        };
    }

    // Root-objektet då NOAA returnerar en lista/array av objekt
    public class AuroraResponse
    {
        public List<KpIndexInfo> KpIndices { get; set; }
    }
}
