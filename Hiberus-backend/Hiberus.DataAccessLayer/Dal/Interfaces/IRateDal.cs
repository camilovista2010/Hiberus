using Hiberus.Model.Models.HiberusEntity; 

namespace Hiberus.DataAccessLayer.Dal.Interfaces
{
    public interface IRateDal : IBaseDal<Rate>
    {
        public ICollection<Rate> GetRates();
        public bool AddRangeRates(ICollection<Rate> rates); 
        public bool RemoveAllRates();
    }
}
