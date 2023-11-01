using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string KategoriResimUrl { get; set; } // Resim parametresi
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Restaurant>? Restaurants { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products{ get; set; }
    }
}

