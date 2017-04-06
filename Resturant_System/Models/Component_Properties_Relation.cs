using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Component_Properties_Relation")]
    public class Component_Properties_Relation
    {
        [Key]
        [Column(Order = 1)]
        public int CompID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PropID { get; set; }
    }
    public class Component_Properties_RelationDBContext : DbContext
    {
        public DbSet<Component_Properties_Relation> Component_Properties_Relations { get; set; }
    }
}