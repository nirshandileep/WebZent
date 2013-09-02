using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.POManagement
{
    public class POSearchParameters
    {
        #region Private Variables

        private string _POCode;
        private int _SupId;
        private decimal _POAmount;
        private int _DueAmountOption;
        private int _TotRcvdOption;
        private string _FromDate;
        private string _ToDate;
        private DateTime? _FromDateRep;
        private DateTime? _ToDateRep;

        #endregion

        #region Public Variables

        public string POCode
        {
            get { return _POCode; }
            set { _POCode = value; }
        }

        public int SupId
        {
            get { return _SupId; }
            set { _SupId = value; }
        }

        public decimal POAmount
        {
            get { return _POAmount; }
            set { _POAmount = value; }
        }

        /// <summary>
        /// 1 -> Has Due Payments, 
        /// 0 -> No Due, 
        /// -1 -> All
        /// </summary>
        public int DueAmountOption
        {
            get { return _DueAmountOption; }
            set { _DueAmountOption = value; }
        }

        /// <summary>
        /// 1 -> Totaly Received, 
        /// 0 -> Partialy Received, 
        /// -1 -> All
        /// </summary>
        public int TotRcvdOption
        {
            get { return _TotRcvdOption; }
            set { _TotRcvdOption = value; }
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

        public DateTime? FromDateRep
        {
            get { return _FromDateRep; }
            set { _FromDateRep = value; }
        }

        public DateTime? ToDateRep
        {
            get { return _ToDateRep; }
            set { _ToDateRep = value; }
        }

        #endregion

        #region Search

        /// <summary>
        /// Search purchase orders witht he details provided in the PO Parameters object
        /// </summary>
        /// <returns>Dataset with search results</returns>
        public DataSet Search()
        {
            try
            {
                return (new PODAO()).SearchPO(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Report Methods

        /// <summary>
        /// Get details of purchased items by items
        /// </summary>
        /// <returns>Dataset with search results</returns>
        public DataSet SearchPOItemsForReporting()
        {
            try
            {
                return (new PODAO()).GetItemwisePODetailsForReporting(this);
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
