using Hiberus.Model.Models.HiberusEntity;

namespace Hiberus.Services.ExternalServices
{
    public interface IQuietApi
    {
        public Task<IList<Rate>> GetRate();

        public Task<IList<Transaction>> GetTransaction();
    }
}
