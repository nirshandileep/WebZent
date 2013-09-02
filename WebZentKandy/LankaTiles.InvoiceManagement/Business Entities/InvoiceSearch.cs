using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.InvoiceManagement
{
    [Serializable]
    public class InvoiceSearch
    {
        #region Private Variables

        private string    _InvoiceNo;
        private string    _FromDate;
        private string    _ToDate;
        private int       _BranchId;
        private int       _CustomerID;
        private int       _DueOption;
        private int       _IsPaid;
        private DateTime? _FromDateRep;
        private DateTime? _ToDateRep;

        #endregion

        #region Public Members

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public int BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        /// <summary>
        /// -1 All,
        /// 1 Has Due, 
        /// 0 No Due
        /// </summary>
        public int DueOption
        {
            get { return _DueOption; }
            set { _DueOption = value; }
        }

        /// <summary>
        /// -1 All,
        /// 1 Is Paid,
        /// 0 Not Paid
        /// </summary>
        public int IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        /// <summary>
        /// Nullable date used for search purposes
        /// From Date/Start Date to search
        /// </summary>
        public DateTime? FromDateRep
        {
            get { return _FromDateRep; }
            set { _FromDateRep = value; }
        }

        /// <summary>
        /// Nullable date used for search purposes
        /// To Date/End Date to search
        /// </summary>
        public DateTime? ToDateRep
        {
            get { return _ToDateRep; }
            set { _ToDateRep = value; }
        }

        #endregion 

        #region Search

        public DataSet Search()
        {
            DataSet ds = null;
            try
            {
                ds = (new InvoiceDAO()).SearchInvoice(this);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion

        #region Search sales report by item

        /// <summary>
        /// Search invoices by items sold
        /// search from date range
        /// </summary>
        /// <returns>Dataset with items sold within given date range</returns>
        public DataSet SearchInvoicedItems()
        {
            DataSet ds = null;
            try
            {
                ds = (new InvoiceDAO()).GetItemWiseSalesForReporting(this);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion

        #region Search sales report by Sales rep

        /// <summary>
        /// Search invoices by items sold
        /// search from date range
        /// </summary>
        /// <returns>Dataset with items sold within given date range</returns>
        public DataSet SearchInvoicedItemsRepWise()
        {
            DataSet ds = null;
            try
            {
                ds = (new InvoiceDAO()).GetItemWiseSalesForReportingRepWise(this);
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
