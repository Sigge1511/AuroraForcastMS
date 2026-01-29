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
        public string Time { get; set; }
        public string KpValueString { get; set; }

        public double KpValue => double.TryParse(KpValueString,
            System.Globalization.CultureInfo.InvariantCulture, out var val) ? val : 0;

        public string Intensity => KpValue >= 5 ? "STORM!" : KpValue >= 3 ? "Aktivt" : "Lugnt";
    }
    // Root-objektet då NOAA returnerar en lista/array av objekt
    public class AuroraResponse
    {
        public List<KpIndexInfo> KpIndices { get; set; }
    }
}
