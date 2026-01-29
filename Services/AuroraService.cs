using AuroraForcastMS.Models;
using System;
using System.Collections.Generic;
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
                // NOAA returnerar en lista av listor/objekt. Vi hämtar hela och tar det senaste värdet.
                var response = await _httpClient.GetFromJsonAsync<List<List<string>>>(Url);

                if (response != null && response.Count > 1)
                {
                    // Sista raden i listan brukar vara det senaste mätta värdet
                    var latestData = response.Last();

                    return new KpIndexInfo
                    {
                        Time = latestData[0],      // Tidstämpel
                        KpValueString = latestData[1] // Kp-index värdet
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fel vid hämtning av norrskensdata: {ex.Message}");
            }

            return null;
        }
    }
}
