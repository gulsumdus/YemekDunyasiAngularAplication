using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class UserSepet
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
