using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.SupplierManagement
{
    [Serializable]
    public class Supplier
    {
        #region Private Variables

        private int _SupId;
        private string _Sup_Code;
        private string _SupplierName;
        private string _SupplierPhone;
        private string _SupplierContact;
        private string _SupplierAddress;
        private bool _IsActive;
        private int _CreatedUser;
        private DateTime _CreatedDate;
        private int _ModifiedBy;
        private DateTime _ModifiedDate;
        private decimal _creditAmmount;

        #endregion

        #region Public Properties

        public int SupId
        {
            get { return _SupId; }
            set { _SupId = value; }
        }

        public string Sup_Code
        {
            get { return _Sup_Code; }
            set { _Sup_Code = value; }
        }

        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }

        public string SupplierPhone
        {
            get { return _SupplierPhone; }
            set { _SupplierPhone = value; }
        }

        public string SupplierContact
        {
            get { return _SupplierContact; }
            set { _SupplierContact = value; }
        }

        public string SupplierAddress
        {
            get { return _SupplierAddress; }
            set { _SupplierAddress = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public int CreatedUser
        {
            get { return _CreatedUser; }
            set { _CreatedUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public decimal CreditAmmount
        {
            get { return _creditAmmount; }
            set { _creditAmmount = value; }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool result = false;
            try
            {
                if (this.SupId > 0)
                {
                    result = (new SupplierDAO()).UpdateSuppliers(this);
                }
                else
                {
                    result = (new SupplierDAO()).AddSuppliers(this);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Delete

        public bool Delete()
        {
            bool result = false;
            try
            {
                result = (new SupplierDAO()).DeleteSuppliers(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region GetSupplierByID

        public bool GetSupplierByID()
        {
            bool result = false;
            try
            {
                result = (new SupplierDAO()).GetSupplierByID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

    }
}
