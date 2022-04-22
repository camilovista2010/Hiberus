using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Model.ModelsDto;

namespace Hiberus.Services.Interfaces
{
    public interface ITransactionService
    { 
        public ICollection<Transaction> GetTransaction();
        public ICollection<Transaction> GetTransactionBySku(string sku);
    }
}
