using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCAjaxExample.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public float price { get; set; }
        public string photo { get; set; }
    }

    public class Sale
    {
        public int id { get; set; }
        public int SalesID { get; set; }
        [ForeignKey("prod")]
        public int prodID { get; set; }
        public Product prod { get; set; }
        public float qty { get; set; }
        public float price { get; set; }
    }

    public class Inventory
    {
        public int id { get; set; }
        [ForeignKey("p")]
        public int prodID { get; set; }
        public Product p { get; set; }
        public float qty { get; set; }

    }

    public class EcomContext: DbContext
    {
        public DbSet<Product> product { get; set; }
        public DbSet<Sale> sale { get; set; }
        public DbSet<Inventory> inv { get; set; }
    }


}