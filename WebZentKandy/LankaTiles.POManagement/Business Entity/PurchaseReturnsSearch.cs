using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.POManagement
{
    public class PurchaseReturnsSearch
    {
        #region Private properties

        private string pRCode;
        private string supInvNo;
        private DateTime? fromDate;
        private DateTime? toDate;
        private LankaTiles.Common.Structures.PRRecievedStatus issuedStatus;

        #endregion

        #region Public properties

        public string PRCode
        {
            get { return pRCode; }
            set { pRCode = value; }
        }

        public string SupInvNo
        {
            get { return supInvNo; }
            set { supInvNo = value; }
        }

        public DateTime? FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        public DateTime? ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }

        public LankaTiles.Common.Structures.PRRecievedStatus IssuedStatus
        {
            get { return issuedStatus; }
            set { issuedStatus = value; }
        }

        #endregion

        #region Methods

        public DataSet Search()
        {
            try
            {
                return new PurchaseReturnsDAO().SearchPurchaseReturns(this);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #endregion

    }
}
