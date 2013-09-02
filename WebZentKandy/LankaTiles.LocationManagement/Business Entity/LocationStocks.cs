using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.LocationManagement
{
    [Serializable]
    public class LocationStocks
    {

        #region Private Properties

        private int branchId;
        private int itemId;
        private Int64 quantityInHand;
        private Int64? freezedQty;

        #endregion

        #region Public Members

        public int BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public Int64 QuantityInHand
        {
            get { return quantityInHand; }
            set { quantityInHand = value; }
        }

        public Int64? FreezedQty
        {
            get { return freezedQty; }
            set { freezedQty = value; }
        }

        #endregion
    }
}
