using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Resturant_System.Models
{
    [Table("Comp_Item")]
    public class Comp_Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Component")]
        public int CompId { get; set; }
        public string Extra { get; set; }
     

    }

    public class Comp_ItemDBContext : DbContext
    {
        public DbSet<Comp_Item> Comp_Item { get; set; }
    }
}