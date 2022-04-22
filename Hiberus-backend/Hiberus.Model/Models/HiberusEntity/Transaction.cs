
namespace Hiberus.Model.Models.HiberusEntity
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
