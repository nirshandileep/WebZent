using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.InvoiceManagement
{
    [Serializable]
    public class GetPass
    {
        #region Private Variables

        private Int64       _GPId;
        private string      _GPCode;
        private Int32       _InvoiceId;
        private string      _InvoiceNo;
        private Decimal     _InvoiceAmmount;
        private Int32       _CreatedBy;
        private DateTime    _CreatedDate;
        private Int32       _ModifiedBy;
        private DateTime    _ModifiedDate;

        private DataSet     _DsGatePassDetails;

        #endregion

        #region Public Members

        public Int64 GPId
        {
            get { return _GPId; }
            set { _GPId = value; }
        }

        public string GPCode
        {
            get { return _GPCode; }
            set { _GPCode = value; }
        }

        public Int32 InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        public Decimal InvoiceAmmount
        {
            get { return _InvoiceAmmount; }
            set { _InvoiceAmmount = value; }
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

        public Int32 ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public DataSet DsGatePassDetails
        {
            get 
            {
                if (_DsGatePassDetails == null)
                {
                    _DsGatePassDetails = new DataSet();
                    _DsGatePassDetails = (new GetPassDAO()).GetGetPassDetailsByID(this);
                }
                return _DsGatePassDetails; 
            }
            set { _DsGatePassDetails = value; }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool result = false;
            try
            {
                if (this.GPId > 0)
                {
                    result = (new GetPassDAO()).UpdateGetPass(this);
                }
                else
                {
                    result = (new GetPassDAO()).AddGetPass(this);
                }
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Get Pass ByID

        public bool GetGetPassByID()
        {
            bool result = false;
            try
            {
                result = (new GetPassDAO()).GetGetPassByID(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Get Pass By Code

        public bool GetGetPassByCode()
        {
            bool result = false;
            try
            {
                result = (new GetPassDAO()).GetGetPassByCode(this);
            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Get Pass Details By ID

        public DataSet GetGetPassDetailsByID()
        {
            DataSet ds = null;
            try
            {
                ds = (new GetPassDAO()).GetGetPassDetailsByID(this);
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
