using System;
using System.Collections.Generic;

namespace MvvMVersuch.Model.Wetter
{
    public abstract class WetterModel : BaseModel
    {
        #region Private Fields

        private short _mondgroesse;
        private DateTime _mondaufgang;
        private DateTime _monduntergang;
        private DateTime _sonnenaufgang;
        private DateTime _sonnenuntergang;

        private IEnumerable<WetterDetails> _vorschau10Tage;
        private IEnumerable<WetterDetails> _vorschauStuendlich;

        #endregion Private Fields

        #region Public Properties

        public DateTime Mondaufgang
        {
            get { return _mondaufgang; }
            set { Set(ref _mondaufgang, value); }
        }

        public short Mondgroesse
        {
            get { return _mondgroesse; }
            set { Set(ref _mondgroesse, value); }
        }

        public DateTime Monduntergang
        {
            get { return _monduntergang; }
            set { Set(ref _monduntergang, value); }
        }

        public DateTime Sonnenaufgang
        {
            get { return _sonnenaufgang; }
            set { Set(ref _sonnenaufgang, value); }
        }

        public DateTime Sonnenuntergang
        {
            get { return _sonnenuntergang; }
            set { Set(ref _sonnenuntergang, value); }
        }

        public IEnumerable<WetterDetails> Vorschau10Tage
        {
            get { return _vorschau10Tage; }
            set { Set(ref _vorschau10Tage, value); }
        }

        public IEnumerable<WetterDetails> VorschauStuendlich
        {
            get { return _vorschauStuendlich; }
            set { Set(ref _vorschauStuendlich, value); }
        }

        #endregion Public Properties
    }
}