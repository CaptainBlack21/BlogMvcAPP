using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcAPP.Models
{
    public class Blogİntializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            List<Catagory> Katagoriler = new List<Catagory>()
            {
                 new Catagory() {KatagoriAdi="C#"},
                 new Catagory() {KatagoriAdi="ömer"},
                 new Catagory() {KatagoriAdi="berat"},
                 new Catagory() {KatagoriAdi="esila"},
                 new Catagory() {KatagoriAdi="zeynep"},
            };
            foreach (var item in Katagoriler)
            {
                context.Katagoriler.Add(item);
            }
            context.SaveChanges();


            List<Blog> bloglar = new List<Blog>()
            {
                new Blog() {Baslik="C# DeleGates Hakkında",Aciklama="C# DeleGates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-10),AnaSayfa=true,Onay=false,İcerik="C# DeleGates Hakkında C# DeleGates Hakkında",Resim="1.jpg",CatagoryId=1},
                new Blog() {Baslik="C# DeleGates Hakkında1",Aciklama="C# DeleGates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-9),AnaSayfa=false,Onay=true,İcerik="C# DeleGates Hakkında C# DeleGates Hakkında",Resim="2.jpg",CatagoryId=2},
                new Blog() {Baslik="C# DeleGates Hakkında2",Aciklama="C# DeleGates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-7),AnaSayfa=true,Onay=true,İcerik="C# DeleGates Hakkında C# DeleGates Hakkında",Resim="1.jpg",CatagoryId=3},
                new Blog() {Baslik="C# DeleGates Hakkında3",Aciklama="C# DeleGates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-6),AnaSayfa=false,Onay=false,İcerik="C# DeleGates Hakkında C# DeleGates Hakkında",Resim="2.jpg",CatagoryId=1},
                new Blog() {Baslik="C# DeleGates Hakkında4",Aciklama="C# DeleGates Hakkında",EklenmeTarihi=DateTime.Now.AddDays(-3),AnaSayfa=true,Onay=true,İcerik="C# DeleGates Hakkında C# DeleGates Hakkında",Resim="3.jpg",CatagoryId=1},

            };
            foreach (var item in bloglar)
            {
                context.Bloglar.Add(item);
            }
            context.SaveChanges();


            base.Seed(context);
        }
    }
}