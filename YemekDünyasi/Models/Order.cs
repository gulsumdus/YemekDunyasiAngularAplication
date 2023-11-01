using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
