using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class ItemsSearch
    {
        #region Private Variables

        private string _ItemCode;
        private string _ItemDescription;
        private decimal _SellingPrice;
        private int _CategoryId;
        private int _Option;
        private Int64 _QuantityInHand;
        private int _ROL;
        private int _BrandID;
        private int _Branch;
        private int _SupId;

        #endregion

        #region Public Properties

        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        public decimal SellingPrice
        {
            get { return _SellingPrice; }
            set { _SellingPrice = value; }
        }
        
        public int Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        public int SupId
        {
            get { return _SupId; }
            set { _SupId = value; }
        }

        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public Int64 QuantityInHand
        {
            get { return _QuantityInHand; }
            set { _QuantityInHand = value; }
        }
        
        public int ROL
        {
            get { return _ROL; }
            set { _ROL = value; }
        }

        public int BranchID
        {
            get { return _Branch; }
            set { _Branch = value; }
        }
       
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        
        #endregion

        #region Search

        public DataSet Search()
        {
            try
            {
                return (new ItemsDAO()).SearchItems(this);
            }
            catch (System.Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion
    }
}
