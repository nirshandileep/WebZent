using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.VoucherManagement
{
    [Serializable]
    public class Voucher
    {
        #region Private Variables

        private Int32 _VoucherID;
        private string _VoucherCode;
        private Int32 ?_ChqId;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private string _ChequeNumber;
        private DateTime? _ChequeDate;
        private string _Bank;
        private string _BankBranch;
        private string _Description;
        private decimal _TotalAmount;
        private Int32 _VoucherTypeID;
        private Int32 _PaymentTypeID;
        private Int32? _SupplierId;
        private Int32? _CustomerID;
        private DateTime? _PaymentDate;
        private Int32? _AccountID;
        private Int32? _BranchId;

        private DataSet _DsVoucherDetails;

        #endregion

        #region Public Properties

        public Int32 VoucherID
        {
            get { return _VoucherID; }
            set { _VoucherID = value; }
        }

        public string VoucherCode
        {
            get { return _VoucherCode; }
            set { _VoucherCode = value; }
        }

        public Int32 ?ChqId
        {
            get { return _ChqId; }
            set { _ChqId = value; }
        }

        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public string ChequeNumber
        {
            get { return _ChequeNumber; }
            set { _ChequeNumber = value; }
        }

        public DateTime? ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }

        public string Bank
        {
            get { return _Bank; }
            set { _Bank = value; }
        }

        public string BankBranch
        {
            get { return _BankBranch; }
            set { _BankBranch = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        public Int32 VoucherTypeID
        {
            get { return _VoucherTypeID; }
            set { _VoucherTypeID = value; }
        }

        public Int32 PaymentTypeID
        {
            get { return _PaymentTypeID; }
            set { _PaymentTypeID = value; }
        }

        public Int32? SupplierId
        {
            get { return _SupplierId; }
            set { _SupplierId = value; }
        }

        public Int32? CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        /// <summary>
        /// Added on request by Shafia on 05/11/2011
        /// </summary>
        public DateTime? PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

        public DataSet DsVoucherDetails
        {
            get 
            {
                if (_DsVoucherDetails == null)
                {
                    _DsVoucherDetails = this.GetVoucherDetailsByID();
                }
                return _DsVoucherDetails; 
            }
            set { _DsVoucherDetails = value; }
        }

        public Int32? AccountID
        {
            get 
            {
                return _AccountID;
            }
            set
            {
                _AccountID = value;
            }
        }

        /// <summary>
        /// The branch the expence was incured
        /// </summary>
        public Int32? BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        #endregion

        #region Add

        public bool Add()
        {
            bool result = false;
            try
            {
                result = (new VoucherDAO()).AddVoucher(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Voucher By Code

        public bool GetVoucherByCode()
        {
            bool result = false;
            try
            {
                result = (new VoucherDAO()).GetVoucherByCode(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Voucher By ID

        public bool GetVoucherByID()
        {
            bool result = false;
            try
            {
                result = (new VoucherDAO()).GetVoucherByID(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Voucher Details By ID

        public DataSet GetVoucherDetailsByID()
        {
            DataSet ds = null;
            try
            {
                ds = (new VoucherDAO()).GetVoucherDetailsByID(this);
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
