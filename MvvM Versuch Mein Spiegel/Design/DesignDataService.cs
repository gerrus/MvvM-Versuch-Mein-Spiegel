using System.Threading.Tasks;
using MvvM_Versuch_Mein_Spiegel.Model;

namespace MvvM_Versuch_Mein_Spiegel.Design
{
    public class DesignDataService : IDataService
    {
        public Task<DataItem> GetData()
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            return Task.FromResult(item);
        }
    }
}