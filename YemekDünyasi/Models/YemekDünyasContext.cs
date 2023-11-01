using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Models
{
    public class YemekDünyasContext: DbContext
    {
        public YemekDünyasContext(DbContextOptions<YemekDünyasContext>options): base(options)// framework.core dan gelen bir sınıf(DbContext) !!
        {

        }
        public DbSet<User> UsersTable { get; set; }
        public DbSet<Category> CategoryTable { get; set; }
        public DbSet<Order> OrderTable { get; set; }
        public DbSet<OrderItem> OrderItemTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<UserSepet> UserSepetTable { get; set; }
        public DbSet<Restaurant> RestaurantTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSepet>()
                .HasKey(uc => new { uc.UserId, uc.ProductId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            // İlişkileri tanımlama
            modelBuilder.Entity<UserSepet>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserSepets)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserSepet>()
                .HasOne(uc => uc.Product)
                .WithMany(p => p.UserCarts)
                .HasForeignKey(uc => uc.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Restaurant)
                .WithMany(r => r.Products)
                .HasForeignKey(p => p.RestaurantId);


            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId);

            //////////////////////DATA SEEDING////////////////////////

            modelBuilder.Entity<User>()
                .HasData(
                new User() { Id=1, UserResimUrl = "https://avatarfiles.alphacoders.com/349/349625.png", Name ="Ayşe", Surname ="Duran", Password="jhngbvffh", Email="Durayse@gmail.com", Adress="Batıkent", TelNo="05434346578"},
                new User() { Id = 2, UserResimUrl = "https://avatarfiles.alphacoders.com/348/348381.jpg", Name = "Ai", Surname = "Kaşcı", Password = "jhngbvffh", Email = "Durayse@gmail.com", Adress = "Ümitköy", TelNo = "05434346578" },
                new User() { Id = 3, UserResimUrl =  "https://avatarfiles.alphacoders.com/349/349379.png", Name = "Ahmet", Surname = "Satır", Password = "jhngbvffh", Email = "ahmet1987@gmail.com", Adress = "Çamlıca", TelNo = "05434346579" },
                new User() { Id = 4, UserResimUrl = "https://avatarfiles.alphacoders.com/893/89303.gif", Name = "Kemal", Surname = "Kuru", Password = "jhngbvffh", Email = "12345@gmail.com", Adress = "Sıhiyye", TelNo = "05434346574" },
                new User() { Id = 5, UserResimUrl = "https://avatarfiles.alphacoders.com/896/89615.png", Name = "Selvi", Surname = "Kara", Password = "jhngbvffh", Email = "sk1234@gmail.com", Adress = "Ulus", TelNo = "05434346573" },
                new User() { Id = 6, UserResimUrl = "https://avatarfiles.alphacoders.com/349/thumb-349931.png", Name = "Melih", Surname = "Mutlu", Password = "jhngbvffh", Email = "2Mel@gmail.com", Adress = "Etimesgut", TelNo = "05434346575" },
                new User() { Id = 7, UserResimUrl = "https://avatarfiles.alphacoders.com/350/350226.jpg", Name = "Kadir", Surname = "Taş", Password = "jhngbvffh", Email = "taskadir@gmail.com", Adress = "Bahçelievler", TelNo = "05434346570" },
                new User() { Id = 8, UserResimUrl = "https://avatarfiles.alphacoders.com/543/54363.jpg", Name = "Elif", Surname = "Salkım", Password = "jhngbvffh", Email = "elifs@gmail.com", Adress = "Emek", TelNo = "05434346572" },
                new User() { Id = 9, UserResimUrl = "https://avatarfiles.alphacoders.com/613/61339.jpg", Name = "aylin", Surname = "Durmuş", Password = "jhngbvffh", Email = "daylin@gmail.com", Adress = "Mamak", TelNo = "05434346571" },
                new User() { Id = 10, UserResimUrl = "https://avatarfiles.alphacoders.com/350/350631.jpeg", Name = "Deniz", Surname = "Kaleci",Password = "jhngbvffh", Email = "kalden@gmail.com", Adress = "Batımerkez", TelNo = "05434346576"}
                );
            modelBuilder.Entity<Restaurant>()
                .HasData(
                new Restaurant() { Id =111, RestResimUrl = "https://www.alaiskembe.com/wp-content/uploads/2021/11/alaiskembe-halkali-min.jpg", Name = "Ala kebap", Address = "Batıkent", TelNo = "08505678990" },

                new Restaurant() { Id = 112, RestResimUrl = "https://media-cdn.tripadvisor.com/media/photo-s/17/72/ef/b8/kapadokya-lezzet-sofrasi.jpg", Name = "Lezzet Sofrası", Address = "Kızılay", TelNo = "03123456789" },
                new Restaurant() { Id = 113, RestResimUrl = "https://img.freepik.com/premium-vector/pizza-house-logo-template-flat-design-style_180868-2435.jpg", Name = "Pizza Evi", Address = "Çankaya", TelNo = "03127894561" },
                new Restaurant() { Id = 114, RestResimUrl = "https://family-images-y24bv7yxalct4.azureedge.net/families/7625/7625_homescreenlogo_1024x1024.jpg", Name = "Burger Land", Address = "Bahçelievler", TelNo = "03125678321"},
                new Restaurant() { Id = 115, RestResimUrl = "https://i.nefisyemektarifleri.com/2016/12/26/balik-firinda-izgara-tadinda.jpeg", Name = "Nefis Deniz Mahsülleri", Address = "Gölbaşı", TelNo = "03123450987"},
                new Restaurant() { Id = 116, RestResimUrl = "https://www.ismailtoparli.com/dosyalar/ismailtoparli/yukle/sektorler/tostcu/tostcu7.jpg", Name = "Tostçudayım", Address = "Etimesgut", TelNo = "03127895678"},
                new Restaurant() { Id = 117, RestResimUrl = "https://upload.wikimedia.org/wikipedia/tr/d/d0/Kurukahveci_Mehmet_Efendi_Logo.jpeg", Name = "Mehmet Efendi", Address = "Tunalı Hilmi", TelNo = "03125678901" },
                new Restaurant() { Id = 118, RestResimUrl = "https://d1hzl1rkxaqvcd.cloudfront.net/contest_entries/2119929/_300px/50e9dbe9b169fee1ac2182abaac93b19.jpg", Name = "Fırıncı Temel", Address = "Ankara Üniversitesi", TelNo = "03123456789" },
                new Restaurant() { Id = 119, RestResimUrl = "https://d1hzl1rkxaqvcd.cloudfront.net/contest_entries/1867941/_300px/a7eada0a1e9113bf555fcf4e5545990c.jpg", Name = "Menemen Kahvaltı Salonu", Address = "Yıldırım Beyazıt Üniversitesi", TelNo = "03127894567" },
                new Restaurant() { Id = 120, RestResimUrl = "https://st2.depositphotos.com/1007499/5212/v/450/depositphotos_52120603-stock-illustration-turkish-food-lahmacun-stamp.jpg", Name = "Lahmacun Dükkanı", Address = "Cebeci", TelNo = "03125678912"},
                new Restaurant() {Id = 121, RestResimUrl = "https://i.pinimg.com/736x/46/a0/53/46a053c1ddec57f6c41b9560fcc0f04a.jpg", Name = "Yasemin Pastanesi", Address= "Ulus", TelNo= "08974452214" },
                new Restaurant() { Id = 122, RestResimUrl = "https://yuzyillikhikayeler.com/wp-content/uploads/2022/08/vefa-bozacisi-logo.jpg", Name = "Vefa Bozacısı", Address = "Çankaya", TelNo = "04563457788" },
                new Restaurant() { Id = 123, RestResimUrl = "https://www.ramazanaltug.com.tr/assets/images/adyamancigkoftekrmzkare01-1270x953.jpg", Name = "Adıyaman çiğköfte", Address = "Batıkent", TelNo = "02224567767" },
                new Restaurant() { Id = 124, RestResimUrl = "https://e1.pxfuel.com/desktop-wallpaper/23/400/desktop-wallpaper-starbucks-coffee-logo-starbucks-logo-thumbnail.jpg", Name = "Starbucks", Address = "Yenimahalle", TelNo = "05785554475" },
                new Restaurant() { Id = 125, RestResimUrl = "https://cdn1.tasarlatasarlat.com/desings/e0/28/795/d900d160f7456bc1ece8e711b.jpg", Name = "Lezzet Döner", Address = "Pursaklar", TelNo = "02345556787" },
                new Restaurant() { Id = 126, RestResimUrl = "https://www.dunyadoner.com.tr/Upload/File/2020/2/6/dunyadonerlogo1.png", Name = "Dünya Döner", Address = "Ostim", TelNo = "04665678799" },
                new Restaurant() { Id = 127, RestResimUrl = "https://seeklogo.com/images/P/pasa-doner-logo-B8F59C9E94-seeklogo.com.png", Name = "Paşa Döner", Address = "Batıkent", TelNo = "02556547766" },
                new Restaurant() { Id = 128, RestResimUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/85/Burger_King_logo_(1999).svg/512px-Burger_King_logo_(1999).svg.png", Name = "Burger King", Address = "Batıkent", TelNo = "08987543355" }



                );
            modelBuilder.Entity<Category>()
                .HasData(
                new Category() { Id = 1 , KategoriResimUrl = "https://avatarfiles.alphacoders.com/324/324948.jpg",Name = "Döner" },
                new Category() { Id = 2, KategoriResimUrl = "https://avatarfiles.alphacoders.com/155/thumb-155431.jpg", Name = "Burger" },
                new Category() { Id = 3, KategoriResimUrl = "https://avatarfiles.alphacoders.com/977/thumb-97720.jpg", Name = "Tavuk" },
                new Category() { Id = 4, KategoriResimUrl = "https://avatarfiles.alphacoders.com/153/153723.jpg", Name = "Pasta&Tatlı" },
                new Category() { Id = 5, KategoriResimUrl = "https://avatarfiles.alphacoders.com/321/321978.jpg", Name = "Tost&Sandviç" },
                new Category() { Id = 6, KategoriResimUrl = "https://i4.hurimg.com/i/hurriyet/75/750x422/628670424e3fe02cd0cd9919.jpg", Name = "Sokak Lezzetleri" },
                new Category() { Id = 7, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2016/05/adana-kebap-one-cikan.jpg", Name = "Kebap" },
                new Category() { Id = 8, KategoriResimUrl = "https://avatarfiles.alphacoders.com/341/341145.jpg", Name = "Pizza" },
                new Category() { Id = 9, KategoriResimUrl = "https://images.deliveryhero.io/image/fd-tr/LH/vrjp-hero.jpg", Name = "Pide" },
                new Category() { Id = 10,KategoriResimUrl = "https://www.elizinn.com.tr/wp-content/uploads/2022/11/kh1-600x597.jpg", Name = "Kahvaltı" },
                new Category() { Id = 11, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2020/04/lahmacun-yeni-one-cikan.jpg", Name = "Lahmacun" },
                new Category() { Id = 12, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2014/10/tas-kebabi-yemekcom.jpg", Name = "Ev yemekleri" },
                new Category() { Id = 13, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2015/07/kaffeform.jpg", Name = "Kahve" },
                new Category() { Id = 14, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2020/03/vejetaryen-carpaccio-editor.jpg", Name = "Vejetaryen" },
                new Category() { Id = 15, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2016/02/inanclarina-gore-mezhep-aliskanliklari.jpg", Name = "Dünya Mutfağı" },
                new Category() { Id = 16, KategoriResimUrl = "https://cdn.yemek.com/mnresize/1250/833/uploads/2022/03/ev-koftesi-yemekcom.jpg",Name = "Köfte" },
                new Category() { Id = 17, KategoriResimUrl = "https://cdn.yemek.com/mnresize/1250/833/uploads/2022/09/10-dakikada-peynirli-borek-onecikan.jpg",Name = "Börek" },
                new Category() { Id = 18, KategoriResimUrl = "https://cdn.yemek.com/mncrop/940/625/uploads/2019/03/etsiz-cig-kofte-yeni.jpg", Name = "Çiğ Köfte" },
                new Category() { Id = 19, KategoriResimUrl = "https://cdn.yemek.com/mncrop/620/388/uploads/2021/07/coban-salata-one-cikan.jpg", Name = "Salata" },
                new Category() { Id = 20, KategoriResimUrl = "https://cdn.yemek.com/mncrop/620/388/uploads/2020/08/limonlu-soguk-cay-sunum.jpg", Name = "İçecek" },
                new Category() { Id = 21, KategoriResimUrl = "https://cdn.yemek.com/mncrop/620/388/uploads/2016/11/balik-pisirmek-koku-shutter-2022.jpg", Name = "Deniz Ürünleri" },
                new Category() { Id = 22, KategoriResimUrl = "https://cdn.yemek.com/mncrop/620/388/uploads/2015/04/olmeden-yenmesi-gereken-dondurmalar.jpg", Name = "Dondurma" }

                );

            modelBuilder.Entity<Product>()
                .HasData(
                new Product() { Id = 1, UrunResimUrl = "https://www.diyetkolik.com/site_media/media/2019/11/01/iskender-kebap.jpg", Name = "İskender", Price = 250, Info = "1 Porsiyon(100 gr Döner Eti)", RestaurantId = 111, CategoryId = 1 },
                new Product() { Id = 2, UrunResimUrl = "https://i4.hurimg.com/i/hurriyet/75/750x422/60dc62dec9de3d3820d4722e.jpg", Name = "Kuzu Şiş", Price = 230, Info = "1 Porsion (200gr)", RestaurantId = 111, CategoryId = 7 },
                new Product() { Id = 3, UrunResimUrl = "https://cdn.tarifikolay.com/public/tt/uploads/2022/kuzu-sis_2204-730x548.jpg", Name = "Tavuk Şiş", Price = 120, Info = "1 Porsion (200gr)", RestaurantId = 111, CategoryId = 3 },
                new Product() { Id = 4, UrunResimUrl = "https://i2.milimaj.com/i/milliyet/75/0x410/5ed105ce5542820f10d423b6.jpg", Name = "Beyti", Price = 155, Info = "1 Porsion (200gr)", RestaurantId = 111, CategoryId = 7 },
                new Product() { Id = 5, UrunResimUrl = "https://menu.seyridem.com/upload/11389856076552836344.jpeg", Name = "Alinazik", Price = 160, Info = "1 Porsion (100gr Kıyma)", RestaurantId = 111, CategoryId = 7 },
                new Product() { Id = 6, UrunResimUrl = "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-58_720x720.png", Name = "Chocolate Cream Frappuccino", Price = 80, Info = "Süt, çikolata sosu ve kar haline getirilmiş buzun krema ve üzerine çikolata sosu ile süslenerek sunulması", RestaurantId = 124, CategoryId = 13 },
                new Product() { Id = 7, UrunResimUrl = "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-40_720x720.png", Name = "Chai Tea Latte", Price = 69, Info = "Baharatlı Siyah Çay Özlü Latte) Baharatlar ve siyah çay karışımının, su ile inceltilmesi ardından buharla ısıtılmış kadifemsi süt ile buluşması", RestaurantId = 124, CategoryId = 13 },
                new Product() { Id = 8, UrunResimUrl = "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-62_720x720.png", Name = "Signature Caramel Hot Chocolate", Price =55, Info = "(Karamel Şuruplu Sıcak Çikolata) Buharla ısıtılmış süt, karamel şurubu ve tarifi Starbucksa özel çikolata sosunun buluşmasıyla ortaya çıkan tatlı lezzet", RestaurantId = 124, CategoryId =13 },
                new Product() { Id = 9, UrunResimUrl = "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-104_720x720.png", Name = "Kahveli Pasta", Price =45, Info = "Pandispanya ve kahveli kremanın beyaz çikolata ile buluştuğu fındık parçacıklarıyla kaplı pasta", RestaurantId = 124, CategoryId =4 },
                new Product() { Id = 10, UrunResimUrl = "https://www.burgerking.com.tr/cmsfiles/products/whopper-menu.png?v=273", Name = "Whopper® Menü", Price = 150, Info = "Whopper® eti, büyük boy susamlı sandviç ekmeği, salatalık turşusu, ketçap, mayonez, doğranmış marul, domates ve soğandan oluşan bir Burger King® klasiği. Enfes patates kızartması ve içeceğiyle birlikte Whopper® Menü keyfini istediğin gibi yaşa!", RestaurantId =128, CategoryId = 2},
                new Product() { Id = 11, UrunResimUrl = "https://www.burgerking.com.tr/cmsfiles/products/bk-king-nuggets-1.png?v=273", Name = "BK King Nuggets", Price = 60, Info = "Bir Burger King Klasiği, 6’lı ya da 9 parça çıtır BK King Nuggets®", RestaurantId =128, CategoryId =3},
                new Product() { Id = 12, UrunResimUrl = "https://www.burgerking.com.tr/cmsfiles/products/hamburger-menu.png?v=273", Name = "Hamburger Menü", Price = 135, Info = "Hamburger eti, küçük boy susamlı sandviç ekmeği, salatalık turşusu, hardal ve ketçaptan oluşan lezzet. Enfes patates kızartması ve içeceğiyle birlikte Hamburger Menü keyfini istediğin gibi yaşa!", RestaurantId =128, CategoryId =2},
                new Product() { Id = 13, UrunResimUrl = "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/ciftlik-evi_638138698051152285.jpg", Name = "Çiftlik Evi", Price =135, Info = "Pizza sosu, mozzarella peyniri, salam, mantar", RestaurantId =112, CategoryId =8 },
                new Product() { Id = 14, UrunResimUrl = "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/karisik_638138711642553787.jpg", Name = "Karışık", Price = 169, Info = "Pizza sosu, mozzarella peyniri, salam, sucuk, sosis, yeşilbiber, mantar, mısır", RestaurantId = 112, CategoryId =8},
                new Product() { Id = 15, UrunResimUrl = "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/superboll-pizza.jpg", Name = "Superboll Pizza", Price = 161, Info = "Pizza sosu, mozzarella peyniri, pepperoni, sucuk, sosis, salam, yeşilbiber, mantar, siyah zeytin, mısır, domates, kekik, susam", RestaurantId = 112, CategoryId =8 }
                ); ; ;;

            modelBuilder.Entity<Order>()
               .HasData(
                new Order() { Id=1, UserId=1, OrderDate= DateTime.Now},
                new Order() { Id = 2, UserId = 2, OrderDate = DateTime.Now },
                new Order() { Id = 3, UserId = 3 , OrderDate = DateTime.Now },
                new Order() { Id = 4, UserId = 4 , OrderDate = DateTime.Now },
                new Order() { Id = 5, UserId = 5, OrderDate = DateTime.Now }
                );

        }
    }
    
    
}
