using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.VoucherManagement
{
    public class VoucherRecievableSearch
    {
        private DateTime ? _FromDate;
        private DateTime ? _ToDate;

        /// <summary>
        /// Search From Date
        /// </summary>
        public DateTime ? FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        /// <summary>
        /// Search To Date
        /// </summary>
        public DateTime ? ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
    }
}
