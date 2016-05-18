using GalaSoft.MvvmLight;
using MvvMVersuch.Model.Uhr;
using MvvMVersuch.Model.Wetter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MvvMVersuch.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Public Methods

        public DateTime Zeit
        {
            get { return _zeit; }
            set { Set(ref _zeit, value); }
        }

        public IEnumerable<StundlichesWetterViewModel> VorhersageStuendlich
        {
            get { return _vorhersageStuendlich; }
            set { Set(ref _vorhersageStuendlich, value); }
        }

        public string LastUpdate
        {
            get { return _lastUpdate; }
            set { Set(ref _lastUpdate, value); }
        }

        public IEnumerable<TaeglichesWetterViewModel> VorherageWoche
        {
            get { return _vorherageWoche; }
            set { Set(ref _vorherageWoche, value); }
        }

        public StundlichesWetterViewModel AktuellesWetter
        {
            get { return _aktuellesWetter; }
            set { Set(ref _aktuellesWetter, value); }
        }

        public void Init(WetterModel wettermodel, UhrModel uhrmodel)
        {
            _wettermodel = wettermodel;
            _uhrmodel = uhrmodel;
            _wettermodel.PropertyChanged += WetterModelPropertyChanged;
            _uhrmodel.PropertyChanged += UhrModelPropertyChanged;
            if (_wettermodel.Ready)
            {
                UpdateAktuellesWetter();
                UpdateStuendlichesWetter();
                Update10TageWetter();
                updateTimestamp();
            }
        }

        private DateTime _zeit;

        private IEnumerable<StundlichesWetterViewModel> _vorhersageStuendlich;

        private void UhrModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Zeit = _uhrmodel.AktuelleZeit;
        }

        private void UpdateAktuellesWetter()
        {
            var currentWeather = _wettermodel.VorschauStuendlich.FirstOrDefault();

            var aktuell = new StundlichesWetterViewModel()
            {
                Conditions = currentWeather.Conditions,
                Time = Int32.Parse(currentWeather.Time.ToString("HH")),

                Temperature = currentWeather.Temperature.Value,
                Rainfall = currentWeather.Rainfall.Value,
                Snowfall = currentWeather.Snowfall.Value,
            };
            AktuellesWetter = aktuell;
        }

        private void updateTimestamp()
        {
            LastUpdate = DateTime.Now.ToString("T");
        }

        private void Update10TageWetter()
        {
            var taeglichListe = new List<TaeglichesWetterViewModel>();
            foreach (var wetter in _wettermodel.Vorschau10Tage.Take(7))
            {
                taeglichListe.Add(new TaeglichesWetterViewModel()
                {
                    Conditions = wetter.Conditions,
                    Date = wetter.Time.ToString("%d"),
                    DayOfWeek = wetter.Time.ToString("dddd"),
                    TemperatureHigh = wetter.TemperatureHigh.Value,
                    TemperatureLow = wetter.TemperatureLow.Value
                });
            }
            VorherageWoche = taeglichListe;
        }

        #endregion Public Methods

        #region Private Fields

        private WetterModel _wettermodel;
        private string _lastUpdate;
        private IEnumerable<TaeglichesWetterViewModel> _vorherageWoche;
        private StundlichesWetterViewModel _aktuellesWetter;
        private UhrModel _uhrmodel;

        #endregion Private Fields

        #region Private Methods

        private void UpdateStuendlichesWetter()
        {
            var stundlichListe = new List<StundlichesWetterViewModel>();
            foreach (var wetter in _wettermodel.VorschauStuendlich.Skip(2))
            {
                stundlichListe.Add(new StundlichesWetterViewModel
                {
                    Time = int.Parse(wetter.Time.ToString("HH")),
                    Temperature = wetter.Temperature.Value,
                    Rainfall = wetter.Rainfall.Value,
                    Snowfall = wetter.Snowfall.Value,
                    Conditions = wetter.Conditions
                });
            }
            VorhersageStuendlich = stundlichListe;
        }

        private void WetterModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_wettermodel.Vorschau10Tage))
            {
                Update10TageWetter();
                updateTimestamp();
            }
            if (e.PropertyName == nameof(_wettermodel.VorschauStuendlich))
            {
                UpdateAktuellesWetter();
                UpdateStuendlichesWetter();
                updateTimestamp();
            }
        }

        #endregion Private Methods
    }
}