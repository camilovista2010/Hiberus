using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Model.ModelsDto;

namespace Hiberus.Services.Interfaces
{
    public interface IRateService
    {
        public ICollection<Rate> GetRates();
    }
}
