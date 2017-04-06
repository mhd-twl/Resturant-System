using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Resturant_System.Models
{
    public class UploadObject
    {
        public object datatype  { get; set; }
        public string nameOfTable { get; set; }
        public int idOfElement { get; set; }
        
        public HttpPostedFileBase file { set; get; }

        public UploadObject() { object datatype = new object(); }
        public UploadObject(object datatype) { this.datatype = new object(); this.datatype= datatype; }
        public UploadObject(string tableName, int elementId)
        {
            this.idOfElement = elementId;
            this.nameOfTable = tableName;
        }
        public UploadObject(string tableName, int elementId , object datatype )
        {
            this.idOfElement = elementId;
            this.nameOfTable = tableName;
            this.datatype = datatype;
        }
        
    }
}