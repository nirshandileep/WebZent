using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class ItemTransfer
    {
        #region Private Variables

        private int _TransferId;
        private int _InvoiceId;
        private int _BranchFrom;
        private int _BranchTo;
        private int _TransferQty;
        private int _TransferBy;
        private DateTime _TransferDate;
        private int _ReceivedBy;
        private DateTime _ReceivedDate;
        private DataSet _dsTransferInvoiceItems;

        #endregion

        #region Public Properties

        public int TransferId
        {
            get { return _TransferId; }
            set { _TransferId = value; }
        }

        public int InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        public int BranchFrom
        {
            get { return _BranchFrom; }
            set { _BranchFrom = value; }
        }

        public int BranchTo
        {
            get { return _BranchTo; }
            set { _BranchTo = value; }
        }

        public int TransferQty
        {
            get { return _TransferQty; }
            set { _TransferQty = value; }
        }

        public int TransferBy
        {
            get { return _TransferBy; }
            set { _TransferBy = value; }
        }

        public DateTime TransferDate
        {
            get { return _TransferDate; }
            set { _TransferDate = value; }
        }

        public int ReceivedBy
        {
            get { return _ReceivedBy; }
            set { _ReceivedBy = value; }
        }

        public DateTime ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }

        public DataSet DsTransferInvoiceItems
        {
            get
            {
                if (_dsTransferInvoiceItems == null)
                {
                    _dsTransferInvoiceItems = new DataSet();
                    _dsTransferInvoiceItems = (new ItemTransferDAO()).GetItemTransferInvoiceByTransferID(this);
                }
                return _dsTransferInvoiceItems;
            }
            set { _dsTransferInvoiceItems = value; }
        }

        #endregion

        #region Constructor

        public ItemTransfer()
        {
            this._InvoiceId = 0;
        }

        #endregion

        #region Add

        public bool Add()
        {
            try
            {
                return (new ItemTransferDAO()).AddItemTransfer(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get Item Transfer By TransferID

        public bool GetItemTransferByTransferID()
        {
            try
            {
                return (new ItemTransferDAO()).GetItemTransferByTransferID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get Item Transfer Invoice By TransferID

        public DataSet GetItemTransferInvoiceByTransferID()
        {
            try
            {
                return (new ItemTransferDAO()).GetItemTransferInvoiceByTransferID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Update Item Transfer

        public bool UpdateItemTransfer()
        {
            try
            {
                return (new ItemTransferDAO()).UpdateItemTransfer(this);
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
