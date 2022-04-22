

using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.DataAccessLayer.DataContext;
using Hiberus.Model.Models.HiberusEntity;

namespace Hiberus.DataAccessLayer.Dal
{
    public class RateDal : BaseDal<Rate>, IRateDal
    {
        public RateDal(HiberusDbContext appDbContext) : base(appDbContext)
        {
        }

        public bool AddRangeRates(ICollection<Rate> rates)
        {
            this.Context.Rates.AddRange(rates);
            return this.Context.SaveChanges() > 0 ? true : false;
        }

        public ICollection<Rate> GetRates()
        {
            return this.Context.Rates.ToList();
        }

        public bool RemoveAllRates()
        {
            this.Context.Rates.RemoveRange(this.Context.Rates);
            return this.Context.SaveChanges() > 0 ? true : false;
        }
    }
}
