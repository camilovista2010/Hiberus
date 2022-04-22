using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.DataAccessLayer.DataContext;
using Hiberus.Model.Models.HiberusEntity;

namespace Hiberus.DataAccessLayer.Dal
{
    public class TransactionDal : BaseDal<Transaction>, ITransactionDal
    {
        public TransactionDal(HiberusDbContext appDbContext) : base(appDbContext)
        {
        }

        public bool AddRangeTransaction(ICollection<Transaction> transactions)
        {
            this.Context.Transactions.AddRange(transactions);
            return this.Context.SaveChanges() > 0 ?  true : false ;
        }

        public ICollection<Transaction> GetTransaction()
        {
            return this.Context.Transactions.ToList();
        }

        public ICollection<Transaction> GetTransactionBySku(string sku)
        { 
            return this.Context.Transactions.Where(c => c.Sku.Equals(sku)).ToList();
        }

        public bool RemoveAllTransaction()
        {
            this.Context.Transactions.RemoveRange(this.Context.Transactions);
            return this.Context.SaveChanges() > 0 ? true : false;
        }
    }
}
