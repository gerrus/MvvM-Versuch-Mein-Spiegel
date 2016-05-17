using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace MvvMVersuch.Model.Wetter
{
    public class WetterModelUndergroundwetter : WetterModel
    {
        private readonly string _apiToken = "dcd59d2bacae2e58";
        private string _request10Day;
        private string _requestAstronomy;
        private string _requestHourly;

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

        private async Task GetWeatherData()
        {
            var stundlich = new Uri($"http://api.wunderground.com/api/{_apiToken}/hourly/lang:DL/q/Germany/Kiel.json");
            var vorschau10Tage =
                new Uri($"http://api.wunderground.com/api/{_apiToken}/forecast10day/lang:DL/q/Germany/Kiel.json");
            var astronomy = new Uri($"http://api.wunderground.com/api/{_apiToken}/astronomy/lang:DL/q/Germany/Kiel.json");

            // Alternative Anfrage falls andere nicht funktioniert

            //using (var client2 = new HttpClient())
            //{
            //    var requestString = await client2.GetAsync(stundlich);
            //    var stringtemp = await requestString.Content.ReadAsStringAsync();
            //}

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