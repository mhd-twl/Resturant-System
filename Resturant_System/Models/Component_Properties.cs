using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Component_Properties")]
    public class Component_Properties
    {
         [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Component_PropertiesDBContext : DbContext
    {
        public DbSet<Component_Properties> Component_Propertieses { get; set; }
    }
}