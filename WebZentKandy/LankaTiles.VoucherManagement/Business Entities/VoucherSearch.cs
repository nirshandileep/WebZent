using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.VoucherManagement
{
    [Serializable]
    public class VoucherSearch
    {
        #region Private Variables

        private string _VoucherCode;
        private String _ChequeNumberFrom;
        private String _ChequeNumberTo;
        private DateTime _ChequeDateFrom;
        private DateTime _ChequeDateTo;
        private DateTime? _FromDate;
        private DateTime? _ToDate;

        #endregion

        #region Public Properties

        public string VoucherCode
        {
            get { return _VoucherCode; }
            set { _VoucherCode = value; }
        }

        public String ChequeNumberFrom
        {
            get { return _ChequeNumberFrom; }
            set { _ChequeNumberFrom = value; }
        }

        public String ChequeNumberTo
        {
            get { return _ChequeNumberTo; }
            set { _ChequeNumberTo = value; }
        }

        public DateTime ChequeDateFrom
        {
            get { return _ChequeDateFrom; }
            set { _ChequeDateFrom = value; }
        }

        public DateTime ChequeDateTo
        {
            get { return _ChequeDateTo; }
            set { _ChequeDateTo = value; }
        }

        public DateTime? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime? ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion
    }
}
