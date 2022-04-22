using Hiberus.Model.Models.HiberusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiberus.Services.Utils
{
    public static class CalculateRateMissing
    {
        public static IList<Rate> CalculateRate(IList<Rate> rates)
        {
            var moneyList = rates.Select(x => x.From).Concat(rates.Select( x=> x.To)).Distinct();
            IList<Rate> ratesMisssing = new List<Rate>();
            foreach (var itemFrom in moneyList)
            {
                foreach (var itemTo in moneyList.Where( x => !x.Equals(itemFrom)))
                {
                    var RateFilter = rates.Where(x => x.From.Equals(itemFrom) && x.To.Equals(itemTo));
                    if (!RateFilter.Any())
                    {
                        ratesMisssing.Add(new Rate() { From = itemFrom , To = itemTo , rate = - 1 });
                    }
                }
            }

            int limitRateValue = rates.Concat(ratesMisssing).Count();

            while (rates.Count() < limitRateValue)
            {
                foreach (var item in ratesMisssing)
                { 
                    ValueRateRef(item.From, item.To, ref rates );
                }

                ratesMisssing = ratesMisssing.Where(x => !rates.Select(x => x.From + x.To).Contains(x.From + x.To)).ToList();
            }
           

            return rates;
        }


        private static decimal ValueRateRef(string from , string to , ref IList<Rate> rates )
        {  
            if (from == null || to == null) return 0;

            if (!rates.Where(x => x.From.Equals(from)).Any()  || !rates.Where(x => x.To.Equals(to)).Any()) return 0;
           
            IList<string> idMoneys  = new List<string>();
            IList<Rate> ratesMoney = rates.Where(x => x.From.Equals(from)).ToList();
            Rate? nextMoney = ratesMoney.First();
            int indexRateNetMoney = ratesMoney.Count() > 1 ? ratesMoney.Count() - 1 : 1;
            decimal rateValue = rateValue = Math.Round(1/ nextMoney.rate, 2);
            idMoneys.Add(nextMoney.From + nextMoney.To);

            while (to != nextMoney.From)
            {
                nextMoney = rates.Where(x => !idMoneys.Contains(x.From + x.To)).FirstOrDefault(x => x.From.Equals(nextMoney.To));
                if (nextMoney == null)
                {
                    if (rates.Where(x => x.From.Equals(from)).Count() > 1)
                    {
                        nextMoney = rates.Where(x => x.From.Equals(from)).ToList()[indexRateNetMoney];
                        rateValue = rateValue = Math.Round(1 / nextMoney.rate, 2);
                        indexRateNetMoney--;
                    }
                    else
                    {
                        rateValue = -1;
                        nextMoney = new Rate() { From = to };
                    }
                    
                }
                else
                {
                    rateValue = Math.Round(rateValue / nextMoney.rate, 2);
                    idMoneys.Add(nextMoney.From + nextMoney.To); 
                }
                
            }

            if (rateValue != -1)
            {
                rates.Add(new Rate { From = from, To = to, rate = rateValue });
            }

            return rateValue;
        }

 

         
    }
}
