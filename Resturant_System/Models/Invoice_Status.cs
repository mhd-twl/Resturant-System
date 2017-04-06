using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Invoice_Status")]
    public class Invoice_Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Invoice_Status() { }
        public Invoice_Status(string name)
        {
            this.Name = name;
        }
        public Invoice_Status( string name , string description) {
            this.Name = name;
            this.Description = description; 
        }

    }
    public class Invoice_StatusDBContext : DbContext
    {
        public DbSet<Invoice_Status> Invoice_Status { get; set; }
    }
}