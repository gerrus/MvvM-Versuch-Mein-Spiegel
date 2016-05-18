using System;
using System.Threading.Tasks;

namespace MvvMVersuch.Model.Uhr
{
    public class UhrModel : BaseModel
    {
        #region Public Properties

        public DateTime AktuelleZeit
        {
            get { return aktuelleZeit; }
            set { Set(ref aktuelleZeit, value); }
        }

        public override TimeSpan Interval => TimeSpan.FromSeconds(1);

        #endregion Public Properties

        #region Public Methods

        public override async Task Update()
        {
            AktuelleZeit = DateTime.Now;
        }

        #endregion Public Methods

        #region Private Fields

        private DateTime aktuelleZeit;

        #endregion Private Fields
    }
}