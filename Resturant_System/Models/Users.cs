using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Phone")]
        public int Phone { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Role ")]
        public int Role_id { get; set; }
        
        public string Extra { get; set; }

        public Users() { }
        public Users(string name , string imag , string username , string passwd , int  phone  , string address , bool isActive , int roleId )
        { 
            this.Name = name ;
            this.Image = imag;
            this.Username = username;
            this.Password = passwd;
            this.Phone = phone;
            this.Address = address;
            this.Active = isActive;
            this.Role_id = roleId;
            
        
        }
    }
    public class UsersDBContext : DbContext
    {
        public DbSet<Users> users { get; set; }
    }
}