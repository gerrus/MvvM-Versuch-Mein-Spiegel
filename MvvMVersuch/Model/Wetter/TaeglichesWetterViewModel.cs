namespace MvvMVersuch.Model.Wetter
{
    public class TaeglichesWetterViewModel
    {
        #region Public Properties

        public string Date { get; set; }
        public string DayOfWeek { get; set; }

        public int TemperatureHigh { get; set; }
        public int TemperatureLow { get; set; }
        public string Conditions { get; set; }

        #endregion Public Properties
    }
}