using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcAPP.Models
{
    public class Catagory
    {
        public int Id { get; set; }
        public string KatagoriAdi { get; set; }

        public List<Blog> Bloglar { get; set; }
    }
}