using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.GRNManagement
{
    public class GRNSearchParameters
    {
        #region Private Variables

        private string _POCode;       
        private int? _SalesReturnID;
        private string _SuplierInvNo;
        private string _FromDate;
        private string _ToDate;

        #endregion

        #region Public Properties

        public string POCode
        {
            get { return _POCode; }
            set { _POCode = value; }
        }

        public int? SalesReturnID
        {
            get { return _SalesReturnID; }
            set { _SalesReturnID = value; }
        }

        public string SuplierInvNo
        {
            get { return _SuplierInvNo; }
            set { _SuplierInvNo = value; }
        }

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        #endregion

        #region Search

        public DataSet Search()
        {
            try
            {
                return (new GRNDAO()).GRNSearch(this);
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
