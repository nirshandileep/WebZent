using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.InvoiceManagement;

namespace LankaTiles.GRNManagement
{
    [Serializable]
    public class GRN
    {
        #region Private Variables

        private Int64 _GRNId;
        private int? _POId;
        private int? _SalesReturnID;
        private DateTime _Rec_Date;
        private int _Rec_By;
        private string _SuplierInvNo;
        private DataSet _GRNItems;
        private int? _InvId;
        private decimal _TotalAmount;
        private string _CreditNote;
        private decimal _TotalPaid;
        private int _CustId;

        private Invoice _GRNInvoice;
        #endregion

        #region Public Variables

        public Int64 GRNId
        {
            get { return _GRNId; }
            set { _GRNId = value; }
        }

        public int? POId
        {
            get { return _POId; }
            set { _POId = value; }
        }

        public int? SalesReturnID
        {
            get { return _SalesReturnID; }
            set { _SalesReturnID = value; }
        }

        public DateTime Rec_Date
        {
            get { return _Rec_Date; }
            set { _Rec_Date = value; }
        }

        public int Rec_By
        {
            get { return _Rec_By; }
            set { _Rec_By = value; }
        }

        public string SuplierInvNo
        {
            get { return _SuplierInvNo; }
            set { _SuplierInvNo = value; }
        }

        public DataSet GRNItems
        {
            get
            {
                if (_GRNItems == null)
                {
                    _GRNItems = new DataSet();
                    _GRNItems = (new GRNDAO()).GetGRNDetailsByGRNID(this);
                }
                return _GRNItems;
            }
            set { _GRNItems = value; }
        }

        public Invoice GRNInvoice
        {
            get
            {
                if (_GRNInvoice == null)
                {
                    _GRNInvoice = new Invoice();
                }
                return _GRNInvoice; 
            }
            set { _GRNInvoice = value; }
        }

        public int? InvId
        {
            get { return _InvId; }
            set { _InvId = value; }
        }

        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        public decimal TotalPaid
        {
            get { return _TotalPaid; }
            set { _TotalPaid = value; }
        }

        public int CustId
        {
            get { return _CustId; }
            set { _CustId = value; }
        }

        public string CreditNote
        {
            get { return _CreditNote; }
            set { _CreditNote = value; }
        }

        #endregion

        #region Add

        public bool Save()
        {
            try
            {
                if (this.GRNId > 0)
                {
                    return (new GRNDAO()).UpdateGRN(this);
                }
                else
                {
                    return (new GRNDAO()).AddGRN(this);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get GRN By ID

        public bool GetGRNByID()
        {
            try
            {
                return (new GRNDAO()).GetGRNByID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion
    }
}
