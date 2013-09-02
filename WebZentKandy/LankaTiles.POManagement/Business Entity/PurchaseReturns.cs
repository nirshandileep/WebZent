using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.POManagement
{
    [Serializable]
    public class PurchaseReturns
    {
    
        #region Private properties

        private Int32 pRId;
        private String pRCode;
        private Int64 gRNId;
        private Int32 createdBy;
        private String createdUser;
        private DateTime createdDate;
        private Int32? modifiedBy;
        private String modifiedUser;
        private DateTime? modifiedDate;
        private Boolean isReimbursed;
        private decimal totalReturns;
        private string comment;
        private DateTime returnDate;
        private DataSet dsReturnDetails;
        private string suplierInvNo;
        private string pOCode;
        private string supplierName;

        #endregion

        #region Public properties

        public String PRCode
        {
            get { return pRCode; }
            set { pRCode = value; }
        }

        public Int64 GRNId
        {
            get { return gRNId; }
            set { gRNId = value; }
        }

        public Int32 CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public String CreatedUser
        {
            get { return createdUser; }
            set { createdUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public Int32? ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        public String ModifiedUser
        {
            get { return modifiedUser; }
            set { modifiedUser = value; }
        }

        public DateTime? ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; }
        }

        public Boolean IsReimbursed
        {
            get { return isReimbursed; }
            set { isReimbursed = value; }
        }
 
        public decimal TotalReturns
        {
            get { return totalReturns; }
            set { totalReturns = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
 
        public Int32 PRId
        {
            get { return pRId; }
            set { pRId = value; }
        }

        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        public DataSet DsReturnDetails
        {
            get { return dsReturnDetails; }
            set { dsReturnDetails = value; }
        }

        public string POCode
        {
            get { return pOCode; }
            set { pOCode = value; }
        }

        public string SuplierInvNo
        {
            get { return suplierInvNo; }
            set { suplierInvNo = value; }
        }

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        #endregion

        #region Methods

        public bool GetPurchaseReturnsByPRNId()
        {
            bool success = true;

            try
            {
               return (new PurchaseReturnsDAO()).GetPRByPRId(this);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return success;
        }

        /// <summary>
        /// Save purchase return
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            bool success = true;
            try
            {
                if (this.PRId>0)
                {
                    //todo:update return
                }
                else
                {
                    return new PurchaseReturnsDAO().AddPR(this);
                }

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return success;
        }

        #endregion
    }
}
