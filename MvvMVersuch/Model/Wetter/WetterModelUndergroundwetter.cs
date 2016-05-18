using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace MvvMVersuch.Model.Wetter
{
    public class WetterModelUndergroundwetter : WetterModel
    {
        #region Private Fields

        private readonly string _apiToken = "dcd59d2bacae2e58";
        private string _request10Tage;
        private string _requestAstronomy;
        private string _requestStuendlich;

        #endregion Private Fields

        #region Public Properties

        public override TimeSpan Interval => TimeSpan.FromSeconds(15);

        #endregion Public Properties

        #region Public Constructors

        public WetterModelUndergroundwetter()
        {
            Vorschau10Tage = new List<WetterDetails>();
            VorschauStuendlich = new List<WetterDetails>();
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task Update()
        {
            await GetWeatherData();
            UpdateAstronomy(_requestAstronomy);
            UpdateStundlich(_requestStuendlich);
            Update10Tage(_request10Tage);
            Ready = true;
        }

        #endregion Public Methods

        #region Private Methods

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
                _requestStuendlich = await client.GetStringAsync(stundlich);

                _request10Tage = await client.GetStringAsync(vorschau10Tage);

                _requestAstronomy = await client.GetStringAsync(astronomy);
            }
        }

        private void UpdateAstronomy(string rohRequest)
        {
            try
            {
                dynamic jsonString = JsonConvert.DeserializeObject(rohRequest);

                Sonnenaufgang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                    int.Parse(jsonString.sun_phase.sunrise.hour.ToString()), int.Parse(jsonString.sun_phase.sunrise.minute.ToString()), 0);

                Sonnenuntergang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                    int.Parse(jsonString.sun_phase.sunset.hour.ToString()), int.Parse(jsonString.sun_phase.sunset.minute.ToString()), 0);

                Mondaufgang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                    int.Parse(jsonString.moon_phase.moonrise.hour.ToString()), int.Parse(jsonString.moon_phase.moonrise.minute.ToString()), 0);

                Monduntergang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                    int.Parse(jsonString.moon_phase.moonset.hour.ToString()), int.Parse(jsonString.moon_phase.moonset.minute.ToString()), 0);
                Mondgroesse = short.Parse(jsonString.moon_phase.percentIlluminated.ToString());
            }
            catch (Exception e)
            {
                MessageDialog tem = new MessageDialog(e.Message);
            }

            //var tempModel = new AstronomyModel(sonneauf, sonneunter, mondaufgang, monduntergang, mondgrosse);
        }

        private void UpdateStundlich(string rohRequest)
        {
            var tempListe = new List<WetterDetails>();

            try
            {
                dynamic jsonString = JsonConvert.DeserializeObject(rohRequest);
                foreach (var einzelneStunde in jsonString.hourly_forecast)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(einzelneStunde.FCTTIME.epoch.ToString()));

                    WetterDetails wetterTDetails = new WetterDetails
                    {
                        Time = dateTimeOffset.DateTime,
                        Conditions = einzelneStunde.wx.ToString(),
                        Rainfall = int.Parse(einzelneStunde.qpf.metric.ToString()),
                        Snowfall = int.Parse(einzelneStunde.snow.metric.ToString()),
                        Temperature = int.Parse(einzelneStunde.temp.metric.ToString())
                    };
                    tempListe.Add(wetterTDetails);
                }
                VorschauStuendlich = tempListe;
            }
            catch (Exception e)
            {
                VorschauStuendlich = null;
            }
        }

        private void Update10Tage(string rohRequest)
        {
            var tempListe = new List<WetterDetails>();

            try
            {
                dynamic jsonString = JsonConvert.DeserializeObject(rohRequest);
                foreach (var einzelnerTag in jsonString.forecast.simpleforecast.forecastday)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(einzelnerTag.date.epoch.ToString()));
                    WetterDetails wetterTDetails = new WetterDetails
                    {
                        Time = dateTimeOffset.DateTime,
                        Conditions = einzelnerTag.conditions.ToString(),
                        Rainfall = int.Parse(einzelnerTag.qpf_allday.mm.ToString()),
                        Snowfall = int.Parse(einzelnerTag.snow_allday.cm.ToString()),
                        TemperatureHigh = int.Parse(einzelnerTag.high.celsius.ToString()),
                        TemperatureLow = int.Parse(einzelnerTag.low.celsius.ToString())
                    };
                    tempListe.Add(wetterTDetails);
                }
                Vorschau10Tage = tempListe;
            }
            catch (Exception e)
            {
                Vorschau10Tage = null;
            }
        }

        #endregion Private Methods
    }
}