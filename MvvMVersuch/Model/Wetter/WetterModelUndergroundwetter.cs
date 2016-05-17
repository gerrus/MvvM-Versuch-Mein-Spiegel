using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace MvvMVersuch.Model.Wetter
{
    public class WetterModelUndergroundwetter : WetterModel
    {
        public WetterModelUndergroundwetter()
        {
            Vorschau10Tage = new List<WetterDetails>();
            VorschauStuendlich = new List<WetterDetails>();
        }

        public override TimeSpan Interval => TimeSpan.FromSeconds(15);

        public override async Task Update()
        {
            await GetWeatherData();
        }

        private readonly string _apiToken = "";
        private string _request10Day;
        private string _requestAstronomy;
        private string _requestHourly;

        private async Task GetWeatherData()
        {
            var stundlich = new Uri($"http://api.wunderground.com/api/{_apiToken}/hourly/q/Germany/Kiel.json");
            var vorschau10Tage =
                new Uri($"http://api.wunderground.com/api/{_apiToken}/forecast10day/q/Germany/Kiel.json");
            var astronomy = new Uri($"http://api.wunderground.com/api/{_apiToken}/astronomy/q/Germany/Kiel.json");

            using (var client = new HttpClient())
            {
                _requestHourly = await client.GetStringAsync(stundlich);

                _request10Day = await client.GetStringAsync(vorschau10Tage);

                _requestAstronomy = await client.GetStringAsync(astronomy);
            }
        }

        private void UpdateAstronomy(string rohRequest)
        {
        }
    }
}