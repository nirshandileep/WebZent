using System;
using System.Text;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.VoucherManagement
{
    [Serializable]
    public class VoucherRecievable
    {
        private Int64 paymentID;
        private String paymentCode;
        private Int32 customerID;
        private String customerName;
        private Decimal paymentAmount;
        private DateTime paymentDate;
        private Structures.PaymentTypes paymentTypeId;
        private Int32 createdBy;
        private DateTime createdDate;
        private Int32 modifiedBy;
        private DateTime modifiedDate;
        private string chequeNo;
        private DateTime chequeDate;
        private Structures.CardTypes cardType;
        private decimal cardCommisionRate;
        private string comment;
        private DataSet dsPaymentDetails;

        #region Public Properties

        public Int64 PaymentID
        {
            get
            {
                return paymentID;
            }
            set
            {
                paymentID = value;
            }
        }

        public String PaymentCode
        {
            get
            {
                return paymentCode;
            }
            set
            {
                paymentCode = value;
            }
        }

        public Int32 CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
        
        public String CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }
        
        public Decimal PaymentAmount
        {
            get
            {
                return paymentAmount;
            }
            set
            {
                paymentAmount = value;
            }
        }
        
        public DateTime PaymentDate
        {
            get
            {
                return paymentDate;
            }
            set
            {
                paymentDate = value;
            }
        }
        
        public Structures.PaymentTypes PaymentTypeId
        {
            get
            {
                return paymentTypeId;
            }
            set
            {
                paymentTypeId = value;
            }
        }
        
        public Int32 CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public Int32 ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }

        public string ChequeNo
        {
            get
            {
                return chequeNo;
            }
            set
            {
                chequeNo = value;
            }
        }

        public DateTime ChequeDate
        {
            get
            {
                return chequeDate;
            }
            set
            {
                chequeDate = value;
            }
        }

        public Structures.CardTypes CardType
        {
            get
            {
                return cardType;
            }
            set
            {
                cardType = value;
            }
        }

        public decimal CardCommisionRate
        {
            get
            {
                return cardCommisionRate;
            }
            set
            {
                cardCommisionRate = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }

        public DataSet DsPaymentDetails
        {
            get
            {
                return dsPaymentDetails;
            }
            set
            {
                dsPaymentDetails = value;
            }
        }

        #endregion

        #region Public Methods

        public bool Save()
        {
            try
            {
                if (this.paymentID > 0)
                {
                    //todo
                }
                else
                {
                    this.SetBankCommissionsByCardType();
                    new VoucherRecievableDAO().AddVoucher(this);
                }
            }
            catch (System.Exception ex)
            {   
                throw ex;
                return false;
            }
            return true;
        }

        public bool GetVoucherByID()
        {
            try
            {
                new VoucherRecievableDAO().GetVoucherByID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
            return true;
        }

        #endregion

        private void SetBankCommissionsByCardType()
        {
            try
            {
                if (this.cardType == Structures.CardTypes.AMERICAN_EXPRESS)
                {
                    this.cardCommisionRate = Decimal.Parse(Constant.Bank_Reduction_Amex.Trim());
                }
                else if (this.cardType == Structures.CardTypes.MASTER)
                {
                    this.cardCommisionRate = Decimal.Parse(Constant.Bank_Reduction_Master.Trim());
                }
                else if (this.cardType == Structures.CardTypes.VISA)
                {
                    this.cardCommisionRate = Decimal.Parse(Constant.Bank_Reduction_Visa.Trim());
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
