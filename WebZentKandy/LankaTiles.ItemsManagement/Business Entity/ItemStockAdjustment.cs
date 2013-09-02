using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.ItemsManagement
{
    public class ItemStockAdjustment
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int itemId;

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

    }
}
