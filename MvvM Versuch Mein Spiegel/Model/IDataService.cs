using System.Threading.Tasks;

namespace MvvM_Versuch_Mein_Spiegel.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}