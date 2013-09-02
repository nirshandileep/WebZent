using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.CustomerManagement
{
    [Serializable]
    public class CustomerSearch
    {
        #region Private Variables

        private int _CustId;
        private string _CustomerCode;
        private string _Cus_Name;
        private int _IsActiveOption;
        private int _IsCreditCustomerOption;
        private DateTime _FromDate;
        private DateTime _ToDate;

        #endregion

        #region Public Properties

        public int CustId
        {
            get { return _CustId; }
            set { _CustId = value; }
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

        /// <summary>
        /// -1 > All
        /// 1 > Active
        /// 0 > Inactive
        /// </summary>
        public int IsActiveOption
        {
            get { return _IsActiveOption; }
            set { _IsActiveOption = value; }
        }

        /// <summary>
        /// -1 > All
        /// 1 > Credit Customers
        /// 0 > Non Credit Customers
        /// </summary>
        public int IsCreditCustomerOption
        {
            get { return _IsCreditCustomerOption; }
            set { _IsCreditCustomerOption = value; }
        }


        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion

        #region Search

        public DataSet Search()
        {
            DataSet ds = null;
            try
            {
                ds = (new CustomerDAO()).SearchCustomers(this);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion
    }
}
