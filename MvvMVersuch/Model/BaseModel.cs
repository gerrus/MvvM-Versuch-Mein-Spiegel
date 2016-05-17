using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;

namespace MvvMVersuch.Model
{
    public abstract class BaseModel : ObservableObject
    {
        public abstract TimeSpan Interval { get; }

        public bool Ready { get; protected set; }

        public abstract Task Update();

        internal async void TimerTick(object sender, object e)
        {
            try
            {
                await Update();
            }
            catch
            {
                // If we had any mean to handle it, we would do it here.
            }
        }
    }
}