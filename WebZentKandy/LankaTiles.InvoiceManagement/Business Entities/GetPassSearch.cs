using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.InvoiceManagement
{
    [Serializable]
    public class GetPassSearch
    {
        #region Private Variables

        private string _GPCode;
        private int _InvoiceId;
        private int _CreatedBy;
        private string _CreatedDateFrom;
        private string _CreatedDateTo;

        #endregion

        #region Public Members

        public string GPCode
        {
            get { return _GPCode; }
            set { _GPCode = value; }
        }

        public int InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public string CreatedDateFrom
        {
            get { return _CreatedDateFrom; }
            set { _CreatedDateFrom = value; }
        }

        public string CreatedDateTo
        {
            get { return _CreatedDateTo; }
            set { _CreatedDateTo = value; }
        }

        #endregion
    }
}
