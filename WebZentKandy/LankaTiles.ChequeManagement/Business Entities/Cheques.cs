using System;
using System.Collections.Generic;
using System.Text;
using LankaTiles.Common;

namespace LankaTiles.ChequeManagement
{
    [Serializable]
    public class Cheques
    {
        #region Private properties

        private Int32 _ChqId;
        private Int32 _ChqBookId;
        private Decimal _Amount;
        private String _Comment;
        private Structures.ChqStatusId _ChqStatusId;
        private DateTime _WrittenDate;
        private DateTime _ChqDate;
        private Int32 _WrittenBy;
        private String _WrittenByName;
        private Int32 _ModifiedBy;
        private String _ModifiedByName;
        private DateTime _ModifiedDate;
        private Int64 _ChequeNo;
        private String _Bank;
        private String _BankBranch;

        #endregion

        #region public propertues

        public Int32 ChqId
        {
            get { return _ChqId; }
            set { _ChqId = value; }
        }

        public Int32 ChqBookId
        {
            get { return _ChqBookId; }
            set { _ChqBookId = value; }
        }

        public Decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public Structures.ChqStatusId ChqStatusId
        {
            get { return _ChqStatusId; }
            set { _ChqStatusId = value; }
        }

        public DateTime WrittenDate
        {
            get { return _WrittenDate; }
            set { _WrittenDate = value; }
        }

        public DateTime ChqDate
        {
            get { return _ChqDate; }
            set { _ChqDate = value; }
        }

        public Int32 WrittenBy
        {
            get { return _WrittenBy; }
            set { _WrittenBy = value; }
        }

        public String WrittenByName
        {
            get { return _WrittenByName; }
            set { _WrittenByName = value; }
        }

        public Int32 ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public String ModifiedByName
        {
            get { return _ModifiedByName; }
            set { _ModifiedByName = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public Int64 ChequeNo
        {
            get { return _ChequeNo; }
            set { _ChequeNo = value; }
        }

        public String Bank
        {
            get { return _Bank; }
            set { _Bank = value; }
        }

        public String BankBranch
        {
            get { return _BankBranch; }
            set { _BankBranch = value; }
        }

        #endregion

        #region Methods 

        public void GetChequeById()
        {
            try
            {

                new ChequesDAO().GetChequeById(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion



    }
}
