using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace YemekDünyasi.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestResimUrl { get; set; } // Resim parametresi
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TelNo { get; set; }
       

       
        [JsonIgnore]// bu özellik birbirine bağlı olan tabloların hepsinin gelmesini engeller.
        public ICollection<Order>? Orders { get; set; }
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
