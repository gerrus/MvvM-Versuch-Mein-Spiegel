namespace MvvMVersuch.Model.Wetter
{
    public class StundlichesWetterViewModel
    {
        #region Public Properties

        public int Time { get; set; }
        public int Temperature { get; set; }
        public string Conditions { get; set; }
        public int Rainfall { get; set; }
        public int Snowfall { get; set; }

        #endregion Public Properties
    }
}