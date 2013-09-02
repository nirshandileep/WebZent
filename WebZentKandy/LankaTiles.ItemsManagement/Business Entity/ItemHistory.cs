using System;
using System.Collections.Generic;
using System.Text;
using LankaTiles.Common;

namespace LankaTiles.ItemsManagement
{
    public class ItemHistory
    {
        private Int64 historyId;

        public Int64 HistoryId
        {
            get { return historyId; }
            set { historyId = value; }
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
        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private decimal sellingPrice;

        public decimal SellingPrice
        {
            get { return sellingPrice; }
            set { sellingPrice = value; }
        }
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string refNo;

        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }
        private Structures.ItemHistoryTypes historyTypeId;

        public Structures.ItemHistoryTypes HistoryTypeId
        {
            get { return historyTypeId; }
            set { historyTypeId = value; }
        }
        private int qtyInStock;

        public int QtyInStock
        {
            get { return qtyInStock; }
            set { qtyInStock = value; }
        }
        private string itemDescription;

        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public ItemHistory()
        {
 
        }

        public ItemHistory(Item item)
        {
            this.RefNo = item.ItemCode;
            this.SellingPrice = item.SellingPrice;
            this.Cost = item.Cost;
            this.ItemDescription = item.ItemDescription;
            this.ItemId = item.ItemId;
            this.QtyInStock = Int32.Parse(item.QuantityInHand.ToString());
            this.UserId = item.UpdatedUser;
        }
    }
}
