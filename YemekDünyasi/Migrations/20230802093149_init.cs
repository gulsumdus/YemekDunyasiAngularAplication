using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekDünyasi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantTable_CategoryTable_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTable_RestaurantTable_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderTable_UsersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTable_CategoryTable_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTable_RestaurantTable_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemTable",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemTable", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItemTable_OrderTable_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemTable_ProductTable_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSepetTable",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSepetTable", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_UserSepetTable_ProductTable_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSepetTable_UsersTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryTable",
                columns: new[] { "Id", "KategoriResimUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://avatarfiles.alphacoders.com/324/324948.jpg", "Döner" },
                    { 2, "https://avatarfiles.alphacoders.com/155/thumb-155431.jpg", "Burger" },
                    { 3, "https://avatarfiles.alphacoders.com/977/thumb-97720.jpg", "Tavuk" },
                    { 4, "https://avatarfiles.alphacoders.com/153/153723.jpg", "Pasta&Tatlı" },
                    { 5, "https://avatarfiles.alphacoders.com/321/321978.jpg", "Tost&Sandviç" },
                    { 6, "https://i4.hurimg.com/i/hurriyet/75/750x422/628670424e3fe02cd0cd9919.jpg", "Sokak Lezzetleri" },
                    { 7, "https://cdn.yemek.com/mncrop/940/625/uploads/2016/05/adana-kebap-one-cikan.jpg", "Kebap" },
                    { 8, "https://avatarfiles.alphacoders.com/341/341145.jpg", "Pizza" },
                    { 9, "https://images.deliveryhero.io/image/fd-tr/LH/vrjp-hero.jpg", "Pide" },
                    { 10, "https://www.elizinn.com.tr/wp-content/uploads/2022/11/kh1-600x597.jpg", "Kahvaltı" },
                    { 11, "https://cdn.yemek.com/mncrop/940/625/uploads/2020/04/lahmacun-yeni-one-cikan.jpg", "Lahmacun" },
                    { 12, "https://cdn.yemek.com/mncrop/940/625/uploads/2014/10/tas-kebabi-yemekcom.jpg", "Ev yemekleri" },
                    { 13, "https://cdn.yemek.com/mncrop/940/625/uploads/2015/07/kaffeform.jpg", "Kahve" },
                    { 14, "https://cdn.yemek.com/mncrop/940/625/uploads/2020/03/vejetaryen-carpaccio-editor.jpg", "Vejetaryen" },
                    { 15, "https://cdn.yemek.com/mncrop/940/625/uploads/2016/02/inanclarina-gore-mezhep-aliskanliklari.jpg", "Dünya Mutfağı" },
                    { 16, "https://cdn.yemek.com/mnresize/1250/833/uploads/2022/03/ev-koftesi-yemekcom.jpg", "Köfte" },
                    { 17, "https://cdn.yemek.com/mnresize/1250/833/uploads/2022/09/10-dakikada-peynirli-borek-onecikan.jpg", "Börek" },
                    { 18, "https://cdn.yemek.com/mncrop/940/625/uploads/2019/03/etsiz-cig-kofte-yeni.jpg", "Çiğ Köfte" },
                    { 19, "https://cdn.yemek.com/mncrop/620/388/uploads/2021/07/coban-salata-one-cikan.jpg", "Salata" },
                    { 20, "https://cdn.yemek.com/mncrop/620/388/uploads/2020/08/limonlu-soguk-cay-sunum.jpg", "İçecek" },
                    { 21, "https://cdn.yemek.com/mncrop/620/388/uploads/2016/11/balik-pisirmek-koku-shutter-2022.jpg", "Deniz Ürünleri" },
                    { 22, "https://cdn.yemek.com/mncrop/620/388/uploads/2015/04/olmeden-yenmesi-gereken-dondurmalar.jpg", "Dondurma" }
                });

            migrationBuilder.InsertData(
                table: "RestaurantTable",
                columns: new[] { "Id", "Address", "CategoryId", "Name", "RestResimUrl", "TelNo" },
                values: new object[,]
                {
                    { 111, "Batıkent", null, "Ala kebap", "https://www.alaiskembe.com/wp-content/uploads/2021/11/alaiskembe-halkali-min.jpg", "08505678990" },
                    { 112, "Kızılay", null, "Lezzet Sofrası", "https://media-cdn.tripadvisor.com/media/photo-s/17/72/ef/b8/kapadokya-lezzet-sofrasi.jpg", "03123456789" },
                    { 113, "Çankaya", null, "Pizza Evi", "https://img.freepik.com/premium-vector/pizza-house-logo-template-flat-design-style_180868-2435.jpg", "03127894561" },
                    { 114, "Bahçelievler", null, "Burger Land", "https://family-images-y24bv7yxalct4.azureedge.net/families/7625/7625_homescreenlogo_1024x1024.jpg", "03125678321" },
                    { 115, "Gölbaşı", null, "Nefis Deniz Mahsülleri", "https://i.nefisyemektarifleri.com/2016/12/26/balik-firinda-izgara-tadinda.jpeg", "03123450987" },
                    { 116, "Etimesgut", null, "Tostçudayım", "https://www.ismailtoparli.com/dosyalar/ismailtoparli/yukle/sektorler/tostcu/tostcu7.jpg", "03127895678" },
                    { 117, "Tunalı Hilmi", null, "Mehmet Efendi", "https://upload.wikimedia.org/wikipedia/tr/d/d0/Kurukahveci_Mehmet_Efendi_Logo.jpeg", "03125678901" },
                    { 118, "Ankara Üniversitesi", null, "Fırıncı Temel", "https://d1hzl1rkxaqvcd.cloudfront.net/contest_entries/2119929/_300px/50e9dbe9b169fee1ac2182abaac93b19.jpg", "03123456789" },
                    { 119, "Yıldırım Beyazıt Üniversitesi", null, "Menemen Kahvaltı Salonu", "https://d1hzl1rkxaqvcd.cloudfront.net/contest_entries/1867941/_300px/a7eada0a1e9113bf555fcf4e5545990c.jpg", "03127894567" },
                    { 120, "Cebeci", null, "Lahmacun Dükkanı", "https://st2.depositphotos.com/1007499/5212/v/450/depositphotos_52120603-stock-illustration-turkish-food-lahmacun-stamp.jpg", "03125678912" },
                    { 121, "Ulus", null, "Yasemin Pastanesi", "https://i.pinimg.com/736x/46/a0/53/46a053c1ddec57f6c41b9560fcc0f04a.jpg", "08974452214" },
                    { 122, "Çankaya", null, "Vefa Bozacısı", "https://yuzyillikhikayeler.com/wp-content/uploads/2022/08/vefa-bozacisi-logo.jpg", "04563457788" },
                    { 123, "Batıkent", null, "Adıyaman çiğköfte", "https://www.ramazanaltug.com.tr/assets/images/adyamancigkoftekrmzkare01-1270x953.jpg", "02224567767" },
                    { 124, "Yenimahalle", null, "Starbucks", "https://e1.pxfuel.com/desktop-wallpaper/23/400/desktop-wallpaper-starbucks-coffee-logo-starbucks-logo-thumbnail.jpg", "05785554475" },
                    { 125, "Pursaklar", null, "Lezzet Döner", "https://cdn1.tasarlatasarlat.com/desings/e0/28/795/d900d160f7456bc1ece8e711b.jpg", "02345556787" },
                    { 126, "Ostim", null, "Dünya Döner", "https://www.dunyadoner.com.tr/Upload/File/2020/2/6/dunyadonerlogo1.png", "04665678799" },
                    { 127, "Batıkent", null, "Paşa Döner", "https://seeklogo.com/images/P/pasa-doner-logo-B8F59C9E94-seeklogo.com.png", "02556547766" },
                    { 128, "Batıkent", null, "Burger King", "https://upload.wikimedia.org/wikipedia/commons/thumb/8/85/Burger_King_logo_(1999).svg/512px-Burger_King_logo_(1999).svg.png", "08987543355" }
                });

            migrationBuilder.InsertData(
                table: "UsersTable",
                columns: new[] { "Id", "Adress", "Email", "Name", "Password", "Surname", "TelNo", "UserResimUrl" },
                values: new object[,]
                {
                    { 1, "Batıkent", "Durayse@gmail.com", "Ayşe", "jhngbvffh", "Duran", "05434346578", "https://avatarfiles.alphacoders.com/349/349625.png" },
                    { 2, "Ümitköy", "Durayse@gmail.com", "Ai", "jhngbvffh", "Kaşcı", "05434346578", "https://avatarfiles.alphacoders.com/348/348381.jpg" }
                });

            migrationBuilder.InsertData(
                table: "UsersTable",
                columns: new[] { "Id", "Adress", "Email", "Name", "Password", "Surname", "TelNo", "UserResimUrl" },
                values: new object[,]
                {
                    { 3, "Çamlıca", "ahmet1987@gmail.com", "Ahmet", "jhngbvffh", "Satır", "05434346579", "https://avatarfiles.alphacoders.com/349/349379.png" },
                    { 4, "Sıhiyye", "12345@gmail.com", "Kemal", "jhngbvffh", "Kuru", "05434346574", "https://avatarfiles.alphacoders.com/893/89303.gif" },
                    { 5, "Ulus", "sk1234@gmail.com", "Selvi", "jhngbvffh", "Kara", "05434346573", "https://avatarfiles.alphacoders.com/896/89615.png" },
                    { 6, "Etimesgut", "2Mel@gmail.com", "Melih", "jhngbvffh", "Mutlu", "05434346575", "https://avatarfiles.alphacoders.com/349/thumb-349931.png" },
                    { 7, "Bahçelievler", "taskadir@gmail.com", "Kadir", "jhngbvffh", "Taş", "05434346570", "https://avatarfiles.alphacoders.com/350/350226.jpg" },
                    { 8, "Emek", "elifs@gmail.com", "Elif", "jhngbvffh", "Salkım", "05434346572", "https://avatarfiles.alphacoders.com/543/54363.jpg" },
                    { 9, "Mamak", "daylin@gmail.com", "aylin", "jhngbvffh", "Durmuş", "05434346571", "https://avatarfiles.alphacoders.com/613/61339.jpg" },
                    { 10, "Batımerkez", "kalden@gmail.com", "Deniz", "jhngbvffh", "Kaleci", "05434346576", "https://avatarfiles.alphacoders.com/350/350631.jpeg" }
                });

            migrationBuilder.InsertData(
                table: "OrderTable",
                columns: new[] { "Id", "OrderDate", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 2, 12, 31, 49, 657, DateTimeKind.Local).AddTicks(8823), null, 1 },
                    { 2, new DateTime(2023, 8, 2, 12, 31, 49, 657, DateTimeKind.Local).AddTicks(8824), null, 2 },
                    { 3, new DateTime(2023, 8, 2, 12, 31, 49, 657, DateTimeKind.Local).AddTicks(8825), null, 3 },
                    { 4, new DateTime(2023, 8, 2, 12, 31, 49, 657, DateTimeKind.Local).AddTicks(8826), null, 4 },
                    { 5, new DateTime(2023, 8, 2, 12, 31, 49, 657, DateTimeKind.Local).AddTicks(8826), null, 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductTable",
                columns: new[] { "Id", "CategoryId", "Info", "Name", "Price", "RestaurantId", "UrunResimUrl" },
                values: new object[,]
                {
                    { 1, 1, "1 Porsiyon(100 gr Döner Eti)", "İskender", 250m, 111, "https://www.diyetkolik.com/site_media/media/2019/11/01/iskender-kebap.jpg" },
                    { 2, 7, "1 Porsion (200gr)", "Kuzu Şiş", 230m, 111, "https://i4.hurimg.com/i/hurriyet/75/750x422/60dc62dec9de3d3820d4722e.jpg" },
                    { 3, 3, "1 Porsion (200gr)", "Tavuk Şiş", 120m, 111, "https://cdn.tarifikolay.com/public/tt/uploads/2022/kuzu-sis_2204-730x548.jpg" },
                    { 4, 7, "1 Porsion (200gr)", "Beyti", 155m, 111, "https://i2.milimaj.com/i/milliyet/75/0x410/5ed105ce5542820f10d423b6.jpg" },
                    { 5, 7, "1 Porsion (100gr Kıyma)", "Alinazik", 160m, 111, "https://menu.seyridem.com/upload/11389856076552836344.jpeg" },
                    { 6, 13, "Süt, çikolata sosu ve kar haline getirilmiş buzun krema ve üzerine çikolata sosu ile süslenerek sunulması", "Chocolate Cream Frappuccino", 80m, 124, "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-58_720x720.png" },
                    { 7, 13, "Baharatlı Siyah Çay Özlü Latte) Baharatlar ve siyah çay karışımının, su ile inceltilmesi ardından buharla ısıtılmış kadifemsi süt ile buluşması", "Chai Tea Latte", 69m, 124, "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-40_720x720.png" },
                    { 8, 13, "(Karamel Şuruplu Sıcak Çikolata) Buharla ısıtılmış süt, karamel şurubu ve tarifi Starbucksa özel çikolata sosunun buluşmasıyla ortaya çıkan tatlı lezzet", "Signature Caramel Hot Chocolate", 55m, 124, "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-62_720x720.png" },
                    { 9, 4, "Pandispanya ve kahveli kremanın beyaz çikolata ile buluştuğu fındık parçacıklarıyla kaplı pasta", "Kahveli Pasta", 45m, 124, "https://api.sbux.retter.io/3e898s82a/CALL/Image/get/SBUX-104_720x720.png" },
                    { 10, 2, "Whopper® eti, büyük boy susamlı sandviç ekmeği, salatalık turşusu, ketçap, mayonez, doğranmış marul, domates ve soğandan oluşan bir Burger King® klasiği. Enfes patates kızartması ve içeceğiyle birlikte Whopper® Menü keyfini istediğin gibi yaşa!", "Whopper® Menü", 150m, 128, "https://www.burgerking.com.tr/cmsfiles/products/whopper-menu.png?v=273" },
                    { 11, 3, "Bir Burger King Klasiği, 6’lı ya da 9 parça çıtır BK King Nuggets®", "BK King Nuggets", 60m, 128, "https://www.burgerking.com.tr/cmsfiles/products/bk-king-nuggets-1.png?v=273" },
                    { 12, 2, "Hamburger eti, küçük boy susamlı sandviç ekmeği, salatalık turşusu, hardal ve ketçaptan oluşan lezzet. Enfes patates kızartması ve içeceğiyle birlikte Hamburger Menü keyfini istediğin gibi yaşa!", "Hamburger Menü", 135m, 128, "https://www.burgerking.com.tr/cmsfiles/products/hamburger-menu.png?v=273" },
                    { 13, 8, "Pizza sosu, mozzarella peyniri, salam, mantar", "Çiftlik Evi", 135m, 112, "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/ciftlik-evi_638138698051152285.jpg" },
                    { 14, 8, "Pizza sosu, mozzarella peyniri, salam, sucuk, sosis, yeşilbiber, mantar, mısır", "Karışık", 169m, 112, "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/karisik_638138711642553787.jpg" },
                    { 15, 8, "Pizza sosu, mozzarella peyniri, pepperoni, sucuk, sosis, salam, yeşilbiber, mantar, siyah zeytin, mısır, domates, kekik, susam", "Superboll Pizza", 161m, 112, "https://www.pizzahut.com.tr/CMSFiles/Product/LittleImage/superboll-pizza.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTable_ProductId",
                table: "OrderItemTable",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_RestaurantId",
                table: "OrderTable",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_UserId",
                table: "OrderTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTable_CategoryId",
                table: "ProductTable",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTable_RestaurantId",
                table: "ProductTable",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTable_CategoryId",
                table: "RestaurantTable",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSepetTable_ProductId",
                table: "UserSepetTable",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemTable");

            migrationBuilder.DropTable(
                name: "UserSepetTable");

            migrationBuilder.DropTable(
                name: "OrderTable");

            migrationBuilder.DropTable(
                name: "ProductTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "RestaurantTable");

            migrationBuilder.DropTable(
                name: "CategoryTable");
        }
    }
}
