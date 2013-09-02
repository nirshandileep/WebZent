using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.CustomerManagement
{
    [Serializable]
    public class Customer
    {
        #region Private Variables

        private Int32 _CustomerID;
        private string _CustomerCode;
        private string _Cus_Name;
        private string _Cus_Address;
        private string _Cus_Tel;
        private string _Cus_Contact;
        private bool _IsActive;
        private bool _IsCreditCustomer;
        private decimal _Cus_CreditTotal;
        private string _GRNIds;
             

        #endregion

        #region Public Members

        public Int32 CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        public string Cus_Name
        {
            get { return _Cus_Name; }
            set { _Cus_Name = value; }
        }

        public string Cus_Address
        {
            get { return _Cus_Address; }
            set { _Cus_Address = value; }
        }

        public string Cus_Tel
        {
            get { return _Cus_Tel; }
            set { _Cus_Tel = value; }
        }

        public string Cus_Contact
        {
            get { return _Cus_Contact; }
            set { _Cus_Contact = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public bool IsCreditCustomer
        {
            get { return _IsCreditCustomer; }
            set { _IsCreditCustomer = value; }
        }

        public decimal Cus_CreditTotal
        {
            get { return _Cus_CreditTotal; }
            set { _Cus_CreditTotal = value; }
        }

        public string GRNIds
        {
            get { return _GRNIds; }
            set { _GRNIds = value; }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool result = false;
            try
            {
                if (this.CustomerID > 0)
                {
                    result = (new CustomerDAO()).UpdateCustomer(this);
                }
                else
                {
                    result = (new CustomerDAO()).AddCustomer(this);
                }
            }
            catch (System.Exception ex)
            {
                result = false;
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
                if (this.CustomerID > 0)
                {
                    result = (new CustomerDAO()).DeleteCustomer(this);
                }
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Customer By ID

        public bool GetCustomerByID()
        {
            bool result = false;
            try
            {
                if (this.CustomerID > 0)
                {
                    result = (new CustomerDAO()).GetCustomerByID(this);
                }
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion
    }
}
