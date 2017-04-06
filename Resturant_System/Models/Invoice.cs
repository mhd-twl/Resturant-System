using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resturant_System.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Open Date")]
        public DateTime Open_Date { get; set; }
        [Display(Name = "Closed Date")]
        public DateTime Close_Date { get; set; }
        [Display(Name = "Invoice Status")]
        public int Invoice_Status { get; set; }
        [Display(Name = "Table")]
        public int Table_Id { get; set; }

        public string Extra { get; set; }


        public Invoice() { }
        public Invoice(DateTime openDate, DateTime closeDate, int invoiceStatus, int tableId)
        {
            this.Open_Date = openDate;
            this.Close_Date = closeDate;
            this.Invoice_Status = invoiceStatus;
            this.Table_Id = tableId;
        }
        public Invoice(DateTime openDate , DateTime closeDate , int invoiceStatus, int tableId,string extra) 
        {
            this.Open_Date = openDate;
            this.Close_Date = closeDate;
            this.Invoice_Status = invoiceStatus;
            this.Table_Id = tableId;
            this.Extra = extra;
        }
        

    }


    public class InvoiceDBContext : DbContext
    {
        public DbSet<Invoice> Invoice { get; set; }
    }
}