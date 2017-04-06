using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Reserved_Table")]
    public class Reserved_Table
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Table")]
        public int Table_Id { get; set; }
        [Display(Name = "Table Status")]
        public bool  Table_Status { get; set; }
        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        public string Extra { get; set; }

        public Reserved_Table() { }
        public Reserved_Table(int tableID, bool tableStatus, DateTime strtdate)
        {
            this.Table_Id = tableID;
            this.Table_Status = tableStatus;
            this.Start_Date = strtdate;
        }
        public Reserved_Table(int tableID, bool tableStatus, DateTime strtdate, string extra)
        {
            this.Table_Id = tableID;
            this.Table_Status = tableStatus;
            this.Start_Date = strtdate;
            this.Extra = extra; 
        }
        
        

    }

    public class Reserved_TableDBContext : DbContext
    {
        public DbSet<Reserved_Table> Reserved_Table { get; set; }
    }
}