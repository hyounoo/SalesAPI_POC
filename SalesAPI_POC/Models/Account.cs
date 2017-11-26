using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAPI_POC.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}