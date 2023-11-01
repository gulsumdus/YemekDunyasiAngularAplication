using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string UrunResimUrl { get; set; } // Resim parametresi
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Info { get; set; }
     
        public int RestaurantId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }
        [JsonIgnore]
        public ICollection<UserSepet>? UserCarts { get; set; }
        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}

