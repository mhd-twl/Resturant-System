using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Table")]
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Unit Name")]
        public string Unit_Name { get; set; }
        [Display(Name = "Number of Chairs")]
        public int Number_of_Chairs { get; set; }
    
        public string Extra { get; set; }

        public Table() { }
        public Table(string unit_name)
        {
            this.Unit_Name = unit_name;
        }
        public Table(string unit_name , int numb_of_chairs , string extra) {
            this.Unit_Name = unit_name;
            this.Number_of_Chairs = numb_of_chairs;
            this.Extra = extra;
        }



    }

    public class TableDBContext : DbContext
    {
        public DbSet<Table> Table { get; set; }
    }
}