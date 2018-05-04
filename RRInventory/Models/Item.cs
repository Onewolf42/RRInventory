using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRInventory.Models
{
    public class Item
    {
        public enum ItemTypes
        {
            Locomotive, PassengerCar, RollingStock, Structure
        }

        public Guid ID { get; set; }
        public Guid AccountID { get; set; }
        public ItemTypes Type { get; set; }
        public string Description { get; set; }
        public string Railroad { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public Guid ImageID { get; set; }

        public Account Account { get; set; }
        public Image Image { get; set; }
    }
}
