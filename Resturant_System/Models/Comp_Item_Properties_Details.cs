using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Component_Properties_Details")]
    public class Comp_Item_Properties_Details
    {
        [Key]
        [Column(Order = 1)]
        public int ItemID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PropID { get; set; }
        public string Value { get; set; }
        public string Extra { get; set; }// nullable

    }

    public class Component_Properties_DetailsDBContext : DbContext
    {
        public DbSet<Comp_Item_Properties_Details> Component_Properties_Detailses { get; set; }
    }
}