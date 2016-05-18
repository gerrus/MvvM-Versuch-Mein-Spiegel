using MvvMVersuch.Model;
using Windows.UI.Xaml;

namespace MvvMVersuch.Controllers
{
    public static class TimerController
    {
        public static void RegisterModel(BaseModel model)
        {
            DispatcherTimer timer = new DispatcherTimer { Interval = model.Interval };
            timer.Tick += model.TimerTick;
            timer.Start();
        }
    }
}