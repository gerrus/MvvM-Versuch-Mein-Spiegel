using System;

namespace MvvMVersuch.Model.Wetter
{
    public class WetterDetails
    {
        #region Public Properties

        public DateTime Time { get; set; }
        public int? Temperature { get; set; }
        public int? TemperatureHigh { get; set; }
        public int? TemperatureLow { get; set; }
        public int? Rainfall { get; set; }
        public int? Snowfall { get; set; }
        public string Conditions { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"{Time.ToString("dddd, d h:mm")}: {Conditions}";
        }

        #endregion Public Methods
    }
}