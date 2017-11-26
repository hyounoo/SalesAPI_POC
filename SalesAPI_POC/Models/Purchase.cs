using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAPI_POC.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}