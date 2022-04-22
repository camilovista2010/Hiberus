
using AutoMapper;
using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.Model.Models.Exceptions;
using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Services.ExternalServices;
using Hiberus.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hiberus.Services.Services
{
    public class TransactionService : BaseServices, ITransactionService
    {
        public readonly ITransactionDal TransactionDal;

        public TransactionService(ILogger<BaseServices> logger, IExceptionHandlerService exceptionHandler, IMapper mapper, IQuietApi quietApi , ITransactionDal transactionDal) : base(logger, exceptionHandler, mapper , quietApi)
        {
            TransactionDal = transactionDal;
        }

        public ICollection<Transaction> GetTransaction()
        {
            ICollection<Transaction> transactionsReturn = new List<Transaction>();
            var transactionsApiItems = QuietApi.GetTransaction().Result;
            if (transactionsApiItems.Count == 0)
            {
                transactionsReturn = TransactionDal.GetTransaction();
                if (transactionsReturn == null)
                {
                    throw new BusinessException(BusinessException.RESOURCE_NOT_FOUND, string.Format("Not found Transactions"));
                }
            }
            else
            {
                TransactionDal.RemoveAllTransaction();
                TransactionDal.AddRangeTransaction(transactionsApiItems);
                transactionsReturn = transactionsApiItems;
            }
            return transactionsReturn;

        }

        public ICollection<Transaction> GetTransactionBySku(string sku)
        {
            ICollection<Transaction> transactionsReturn = new List<Transaction>();
            var transactionsApiItems = QuietApi.GetTransaction().Result;
            if (transactionsApiItems.Count == 0)
            {
                transactionsReturn = TransactionDal.GetTransactionBySku(sku);
                if (transactionsReturn == null)
                {
                    throw new BusinessException(BusinessException.RESOURCE_NOT_FOUND, string.Format("Not found Transactions"));
                }
            }
            else
            {
                TransactionDal.RemoveAllTransaction();
                TransactionDal.AddRangeTransaction(transactionsApiItems);
                transactionsReturn = transactionsApiItems.Where(x => x.Sku.Equals(sku)).ToList();
            }
            return transactionsReturn;
        }
    }
}
