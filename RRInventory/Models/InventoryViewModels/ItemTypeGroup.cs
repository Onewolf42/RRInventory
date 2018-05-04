using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RRInventory.Models.InventoryViewModels
{
    public class ItemTypeGroup
    {
        [DataType(DataType.Text)]
        public Item.ItemTypes? ItemType { get; set; }

        public int ItemCount { get; set; }
    }
}

