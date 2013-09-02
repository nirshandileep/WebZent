using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.SupplierManagement
{
    public class SupplierSearch
    {
        #region Private Variables

        private int _Sup_Id;
        private string _Sup_Code;
        private string _SupplierName;
        private int _Option;
        private DateTime _FromDate;
        private DateTime _ToDate;

        #endregion

        #region Public Properties

        public int Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        public int Sup_Id
        {
            get { return _Sup_Id; }
            set { _Sup_Id = value; }
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
            try
            {
                return (new SupplierDAO()).SearchSuppliers(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion


    }
}
