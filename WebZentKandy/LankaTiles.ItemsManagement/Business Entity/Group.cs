using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.Common;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class Group
    {
        #region Private Variables

        private Int16 _GroupId;
        private string _GroupCode;
        private string _GroupName;
        private string _Description;
        private Int16 _ItemCount;
        private bool _IsActive;
        private decimal _Cost;
        private decimal _SellingPrice;
        private decimal _MinSellingPrice;
        private Int64 _QuantityInHand;

        private DataSet _GroupItems;

        #endregion

        #region Public Properties

        public Int16 GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }

        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public Int16 ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }

        public Int64 QuantityInHand
        {
            get { return _QuantityInHand; }
            set { _QuantityInHand = value; }
        }

        public DataSet GroupItems
        {
            get
            {
                if (_GroupItems == null)
                {
                    _GroupItems = new DataSet();
                    _GroupItems = (new GroupsDAO()).GetItemsByGroupId(this);
                }
                return _GroupItems;
            }
            set { _GroupItems = value; }
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

        public decimal MinSellingPrice
        {
            get { return _MinSellingPrice; }
            set { _MinSellingPrice = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        #endregion

        #region Save

        public bool Save()
        {
            try
            {
                if (this.GroupId > 0)
                {
                    return (new GroupsDAO()).UpdateGroups(this);
                }
                else
                {
                    return (new GroupsDAO()).AddGroups(this);
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool Save()");
                throw ex;
            }
        }

        #endregion

        #region Get Group By ID

        public bool GetGroupByID()
        {
            try
            {
                if (this.GroupId == 0)
                {
                    this.GroupCode = (new GroupsDAO()).GetNextGroupsCode();
                    return false;
                }
                else
                {
                    return (new GroupsDAO()).GetGroupByID(this);
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetGroupByID()");
                throw ex;
            }
        }

        #endregion

        #region Get Group By Code

        public bool GetGroupByCode()
        {
            try
            {
               return (new GroupsDAO()).GetGroupByCode(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetGroupByID()");
                throw ex;
            }
        }

        #endregion

        #region Get Items By GroupId

        public DataSet GetItemsByGroupId()
        {
            try
            {
                return (new GroupsDAO()).GetItemsByGroupId(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemsByGroupId()");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get Items By Group Code

        public DataSet GetItemsByGroupCode()
        {
            try
            {
                return (new GroupsDAO()).GetItemsByGroupCode(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemsByGroupId()");
                return null;
                throw ex;
            }
        }

        #endregion
    }
}
