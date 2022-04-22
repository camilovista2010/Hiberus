
namespace Hiberus.Model.Models.HiberusEntity
{ 
    public class Rate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string From { get; set; }
        public string To { get; set; }
        public decimal rate { get; set; }
    }
}
