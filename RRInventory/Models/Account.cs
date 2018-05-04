using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRInventory.Models
{
    public class Account
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
