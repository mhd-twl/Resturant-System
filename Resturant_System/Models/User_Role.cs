using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("User_Role")]
    public class User_Role
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public User_Role() { }
        public User_Role(string name , string description)
        {
            this.Name = name;
            this.Description = description;
        }


    }
    public class User_RoleDBContext : DbContext
    {
        public DbSet<User_Role> user_role { get; set; }
    }

}