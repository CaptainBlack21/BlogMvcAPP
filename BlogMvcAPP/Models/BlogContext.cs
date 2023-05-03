using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace BlogMvcAPP.Models
{
    public class BlogContext:DbContext
    {
        public BlogContext():base("blogdb")
        {
            Database.SetInitializer(new Blogİntializer());
        }
        public DbSet<Blog> Bloglar { get; set; }
        public DbSet<Catagory> Katagoriler { get; set; }
    }
}