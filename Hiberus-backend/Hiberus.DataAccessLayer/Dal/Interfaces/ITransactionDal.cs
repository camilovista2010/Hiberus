using Hiberus.Model.Models.HiberusEntity;

namespace Hiberus.DataAccessLayer.Dal.Interfaces
{
    public interface ITransactionDal : IBaseDal<Transaction>
    { 
        public ICollection<Transaction> GetTransaction();
        public ICollection<Transaction> GetTransactionBySku(string sku); 
        public bool AddRangeTransaction(ICollection<Transaction> transactions);
        public bool RemoveAllTransaction();
    }
}
