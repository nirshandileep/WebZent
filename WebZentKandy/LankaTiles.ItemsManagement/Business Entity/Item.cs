using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.ItemsManagement.Business_Entity;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class Item
    {
        #region Private Variables

        private int         _ItemId;       
        private string      _ItemCode;
        private string      _ItemDescription;
        private decimal     _Cost;
        private decimal     _SellingPrice;
        private bool        _IsActive;
        private int         _CategoryId;
        private int         _UpdatedUser;
        private DateTime    _UpdatedDate;
        private Int64       _QuantityInHand;
        private Int64       _FreezedQty;
        private int         _ROL;
        private int         _BrandId;
        private string      _BrandName;
        private decimal     _MinSellingPrice;
        private int         _InvoicedQty;
        private string      _CategoryName;
        private ITypes      _Type;

        private int _QuantityToBeIssued;
        
        

        #endregion

        #region Public Properties

        public int ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

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

        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; }
        }

        public decimal SellingPrice
        {
            get { return _SellingPrice; }
            set { _SellingPrice = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public int UpdatedUser
        {
            get { return _UpdatedUser; }
            set { _UpdatedUser = value; }
        }

        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        public Int64 QuantityInHand
        {
            get { return _QuantityInHand; }
            set { _QuantityInHand = value; }
        }

        public Int64 FreezedQty
        {
            get { return _FreezedQty; }
            set { _FreezedQty = value; }
        }

        public int ROL
        {
            get { return _ROL; }
            set { _ROL = value; }
        }

        public int BrandId
        {
            get { return _BrandId; }
            set { _BrandId = value; }
        }

        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }

        public decimal MinSellingPrice
        {
            get { return _MinSellingPrice; }
            set { _MinSellingPrice = value; }
        }

        public int InvoicedQty
        {
            get { return _InvoicedQty; }
            set { _InvoicedQty = value; }
        }

        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        public int QuantityToBeIssued
        {
            get { return _QuantityToBeIssued; }
            set { _QuantityToBeIssued = value; }
        }

        public ITypes IType
        {
            get 
            {
                if (_Type == null)
                {
                    _Type = new ITypes();
                }
                return _Type; 
            }
            set { _Type = value; }
        }

        #endregion

        #region Get Next Item Code

        public string GetNextItemCode()
        {
            try
            {
                return (new ItemsDAO()).GetNextItemCode();
            }
            catch (System.Exception ex)
            {
                return string.Empty;
                throw ex;
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            try
            {
                if (this.ItemId > 0)
                {
                    return (new ItemsDAO()).UpdateItems(this);
                }
                else
                {
                    return (new ItemsDAO()).AddItems(this);
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Delete

        public bool Deletee()
        {
            bool result = false;
            try
            {
                if (this.ItemId > 0)
                {
                    result =(new ItemsDAO()).DeleteItem(this);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Item By ID

        public bool GetItemByID()
        {
            try
            {
                return (new ItemsDAO()).GetItemsByID(this);
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Get Item By Item Code

        public bool GetItemsByItemCode()
        {
            try
            {
                return (new ItemsDAO()).GetItemsByItemCode(this);
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Get Item By Item Id And Branch ID

        public bool GetItemsByItemIDAndBranchID(int branchID)
        {
            try
            {
                return (new ItemsDAO()).GetItemsByItemIDAndBranchID(this, branchID);
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion
    }
}
