﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class PriceLevelInventoryItem
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }

        public int PriceLevelId { get; set; }
        public virtual PriceLevel PriceLevel { get; set; }

        public short? Type { get; set; }
        public decimal? CustomPrice { get; set; }
        public decimal? CustomPricePercent { get; set; }
    }
}
