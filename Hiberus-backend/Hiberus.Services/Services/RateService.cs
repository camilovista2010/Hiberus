
using AutoMapper;
using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.Model.Models.Exceptions;
using Hiberus.Model.Models.HiberusEntity;
using Hiberus.Services.ExternalServices;
using Hiberus.Services.Interfaces;
using Hiberus.Services.Utils;
using Microsoft.Extensions.Logging;

namespace Hiberus.Services.Services
{
    public class RateService : BaseServices, IRateService
    {

        public readonly IRateDal RateDal;

        public RateService(ILogger<BaseServices> logger, IExceptionHandlerService exceptionHandler, IMapper mapper , IQuietApi quietApi , IRateDal rateDal) : base(logger, exceptionHandler, mapper , quietApi)
        {
            RateDal = rateDal;
        }

        public ICollection<Rate> GetRates()
        {
            ICollection<Rate> ratesReturn = new List<Rate>();
            var RateApiItems = CalculateRateMissing.CalculateRate(QuietApi.GetRate().Result);
            if (RateApiItems.Count == 0)
            {
                ratesReturn = RateDal.GetRates();
                if (ratesReturn == null)
                {
                    throw new BusinessException(BusinessException.RESOURCE_NOT_FOUND, string.Format("Not found Rates"));
                }
            }
            else
            {
                RateDal.RemoveAllRates();
                RateDal.AddRangeRates(RateApiItems);
                ratesReturn = RateApiItems;
            }            
            return ratesReturn;
        }

    }
}
