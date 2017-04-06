using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Resturant_System.Models
{
    [Table("Component")]
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }

    }

    public class ComponentDBContext : DbContext
    {
        public DbSet<Component> Components { get; set; }
    }
}