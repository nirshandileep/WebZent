using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.InvoiceManagement.Business_Entities;
using LankaTiles.Common;

namespace LankaTiles.InvoiceManagement
{
    [Serializable]
    public class Invoice
    {
        #region Private Variables

        private int         _InvoiceId;
        private string      _InvoiceNo;      
        private DateTime    _Date;
        private decimal     _GrandTotal;
        private int         _BranchId;
        private int?        _CustomerID;
        private string      _PaymentType;
        private decimal     _PaidAmount;//09/10/2011
        private decimal     _DueAmount;
        private bool        _IsPaid;
        private bool?       _I_in;
        private int         _CreatedUser;
        private DateTime    _CreatedDate;
        private int?        _ModifiedUser;
        private DateTime?   _ModifiedDate;
        private string      _TransferNote;
        private string      _ChequeNumber;
        private DateTime    _ChequeDate;
        private string      _Remarks;
        private bool        _IsIssued;
        private decimal     _CustDebitTotal;
        private LankaTiles.Common.Structures.InvoiceStatus _Status;
        
        private LankaTiles.Common.Structures.CardTypes     _CardType;
        private decimal     _CardComisionRate;
        private decimal     _BankComision;
        private decimal     _Banked_Ammount;
        private long?       _GRNId;

        private DataSet         _DsInvoiceDetails;
        private InvoiceDetails  _InvoiceDetails;
        private bool        _IsVoucherPaymentMade;
        private int         _CreditOption;

        #endregion

        #region Public Members

        public int InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public decimal GrandTotal
        {
            get { return _GrandTotal; }
            set { _GrandTotal = value; }
        }

        public int BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        /// <summary>
        /// Nullable
        /// </summary>
        public int? CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public string PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }

        /// <summary>
        /// Currently Not saved in the database
        /// PaidAmount = GrandTotal - DueAmount
        /// </summary>
        public decimal PaidAmount
        {
            get { return _PaidAmount; }
            set { _PaidAmount = value; }
        }

        public decimal DueAmount
        {
            get { return _DueAmount; }
            set { _DueAmount = value; }
        }

        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        public string ChequeNumber
        {
            get { return _ChequeNumber; }
            set { _ChequeNumber = value; }
        }

        public DateTime ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        /// <summary>
        /// Nullable
        /// </summary>
        public bool? I_in
        {
            get { return _I_in; }
            set { _I_in = value; }
        }

        public int CreatedUser
        {
            get { return _CreatedUser; }
            set { _CreatedUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        /// <summary>
        /// Nullable
        /// </summary>
        public int? ModifiedUser
        {
            get { return _ModifiedUser; }
            set { _ModifiedUser = value; }
        }

        /// <summary>
        /// Nullable
        /// </summary>
        public DateTime? ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public string TransferNote
        {
            get { return _TransferNote; }
            set { _TransferNote = value; }
        }

        public bool IsIssued
        {
            get { return _IsIssued; }
            set { _IsIssued = value; }
        }

        public decimal CustDebitTotal
        {
            get { return _CustDebitTotal; }
            set { _CustDebitTotal = value; }
        }

        public LankaTiles.Common.Structures.InvoiceStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// Card type
        /// Visa, Master, Amex
        /// </summary>
        public LankaTiles.Common.Structures.CardTypes CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }

        /// <summary>
        /// Commision rate for the card type
        /// </summary>
        public decimal CardComisionRate
        {
            get { return _CardComisionRate; }
            set { _CardComisionRate = value; }
        }

        /// <summary>
        /// Commision taken by the bank
        /// </summary>
        public decimal BankComision
        {
            get { return _BankComision; }
            set { _BankComision = value; }
        }

        /// <summary>
        /// Banked total after bank commision reduced
        /// </summary>
        public decimal Banked_Ammount
        {
            get { return _Banked_Ammount; }
            set { _Banked_Ammount = value; }
        }

        /// <summary>
        /// GRN Id in cases of sales returns claim back
        /// </summary>
        public long? GRNId
        {
            get { return _GRNId; }
            set { _GRNId = value; }
        }

        public DataSet DsInvoiceDetails
        {
            get 
            {
                if (_DsInvoiceDetails == null)
                {
                    _DsInvoiceDetails = new DataSet();
                    _DsInvoiceDetails = (new InvoiceDAO()).GetInvoiceDetailsByInvoiceID(this);
                }
                return _DsInvoiceDetails; 
            }
            set { _DsInvoiceDetails = value; }
        }

        public InvoiceDetails InvoiceDetails
        {
            get 
            {
                if (_InvoiceDetails == null)
                {
                    _InvoiceDetails = new InvoiceDetails();
                }
                return _InvoiceDetails;
            }
            set { _InvoiceDetails = value; }
        }

        /// <summary>
        /// Set to true if receivable voucher has been raised for this invoice
        /// </summary>
        public bool IsVoucherPaymentMade
        {
            get 
            {
                return _IsVoucherPaymentMade;
            }
            set 
            {
                _IsVoucherPaymentMade = value;
            }
        }

        public int CreditOption
        {
            get { return _CreditOption; }
            set { _CreditOption = value; }
        }
        
        #endregion

        #region Save

        public bool Save()
        {
            bool result = false;
            try
            {
                this.CalculateTotals();
                if (this.InvoiceId > 0)
                {
                    result = (new InvoiceDAO()).UpdateInvoice(this);
                }
                else
                {
                    this.Status = Structures.InvoiceStatus.Created;
                    result = (new InvoiceDAO()).AddInvoice(this);
                }
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        private void CalculateTotals()
        {
            try
            {
                if (this.PaymentType == "3")//Card
                {
                    decimal comRate;
                    switch (this.CardType)
                    {
                        case Structures.CardTypes.MASTER:
                            comRate = Convert.ToDecimal(Constant.Bank_Reduction_Master);
                            break;
                        case Structures.CardTypes.VISA:
                            comRate = Convert.ToDecimal(Constant.Bank_Reduction_Visa);
                            break;
                        case Structures.CardTypes.AMERICAN_EXPRESS:
                            comRate = Convert.ToDecimal(Constant.Bank_Reduction_Amex);
                            break;
                        default:
                            comRate = 0;
                            break;
                    }

                    this.CardComisionRate = comRate;
                    this.BankComision = this.PaidAmount * (comRate / 100);
                    this.Banked_Ammount = this.PaidAmount - this.BankComision;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Get Invoice By InvoiceID

        public bool GetInvoiceByInvoiceID()
        {
            bool result = false;
            try
            {
                result = (new InvoiceDAO()).GetInvoiceByInvoiceID(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Invoice By Invoice Number

        public bool GetInvoiceByInvoiceNumber()
        {
            bool result = false;
            try
            {
                result = (new InvoiceDAO()).GetInvoiceByInvoiceNumber(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Invoice Details By InvoiceID

        public DataSet GetInvoiceDetailsByInvoiceID()
        {
            DataSet ds = null;
            try
            {
                ds = (new InvoiceDAO()).GetInvoiceDetailsByInvoiceID(this);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion

        #region Get Next Code For Invoice

        public string GetNextCodeForInvoice()
        {
            string str = string.Empty;
            try
            {
                str = (new InvoiceDAO()).GetNextCodeForInvoice(this);
            }
            catch (System.Exception ex)
            {
                str = string.Empty;
                throw ex;
            }
            return str;
        }

        #endregion

        #region Get Next Code For Hidden Invoice

        public string GetNextCodeForHdnInvoice()
        {
            string str = string.Empty;
            try
            {
                str = (new InvoiceDAO()).GetNextCodeForHdnInvoice(this);
            }
            catch (System.Exception ex)
            {
                str = string.Empty;
                throw ex;
            }
            return str;
        }

        #endregion

        #region Cancel Invoice

        public bool CancelInvoice()
        {
            bool rslt = false;
            try
            {
                rslt = (new InvoiceDAO()).CancelInvoice(this);
            }
            catch (System.Exception ex)
            {
                rslt = false;
                throw ex;
            }
            return rslt;
        }

        #endregion
    }
}
