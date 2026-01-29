using AuroraForcastMS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuroraForcastMS.Services
{
    public class AuroraService
    {
        private readonly HttpClient _httpClient;
        // NOAA:s officiella endpoint för geomagnetisk aktivitet (Kp-index)
        private const string Url = "https://services.swpc.noaa.gov/products/noaa-scales.json";

        public AuroraService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<KpIndexInfo> GetCurrentKpIndexAsync()
        {
            try
            {
                // Vi hämtar rå-strängen först för att undvika krasch vid deserialisering
                var jsonString = await _httpClient.GetStringAsync(Url);
                using var document = System.Text.Json.JsonDocument.Parse(jsonString);

                // NOAA:s format är en array där sista elementet är en array med data
                var root = document.RootElement;
                if (root.GetArrayLength() > 1)
                {
                    var latestEntry = root[root.GetArrayLength() - 1];

                    // Vi hämtar värdet från index 1 (Kp-värdet)
                    string kpVal = latestEntry[1].GetString();

                    return new KpIndexInfo
                    {
                        KpValueString = kpVal,
                        Time = latestEntry[0].GetString()
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON-fel: {ex.Message}");
            }

            return new KpIndexInfo { KpValueString = "0", Time = "N/A" };
        }
    }
}
