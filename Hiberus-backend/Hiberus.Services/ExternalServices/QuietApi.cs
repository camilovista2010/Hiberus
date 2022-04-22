using Hiberus.Model.Models.HiberusEntity;
using Newtonsoft.Json;
using RestSharp;

namespace Hiberus.Services.ExternalServices
{
    public class QuietApi : IQuietApi
    {
        private readonly string API_URL = Environment.GetEnvironmentVariable("API_URL") ?? "";

        private RestClient Client { get; set; }
        public QuietApi()
        {
            Client = new RestClient(API_URL);
        } 

        public async Task<IList<Rate>> GetRate()
        {
            IList<Rate> ratesItem = new List<Rate>(); 
            var request = new RestRequest("/rates.json", Method.Get);
            RestResponse response = await Client.ExecuteGetAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseItems = JsonConvert.DeserializeObject<List<Rate>>(response.Content);
                ratesItem = responseItems;
            }

            return ratesItem;
        }

        public async Task<IList<Transaction>> GetTransaction()
        { 

            IList<Transaction> transactionItem = new List<Transaction>();
            var request = new RestRequest("/transactions.json", Method.Get);
            RestResponse response = await Client.ExecuteGetAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseItems = JsonConvert.DeserializeObject<List<Transaction>>(response.Content);
                transactionItem = responseItems;
            }  
            return transactionItem;

        }
    }
}
