using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.POManagement
{
    [Serializable]
    public class PO
    {
        #region Private Variables

        private int _POId;
        private string _POCode;
        private decimal _POAmount;
        private decimal _BalanceAmount;
        private DateTime _POCreatedDate;
        private int _POCreatedUser;
        private int _SupId;
        private int _POLastModifiedBy;
        private DateTime _POLastModifiedDate;
        private bool _IsReceived;
        private String _SupplierName;
        private DateTime? _PODate;
        private int? _RequestedBy;
        private LankaTiles.Common.Structures.POStatus _POStatus;
        private string _POComment;

        private DataSet dsPOItems;

        #endregion

        #region Public Properties

        public int POId
        {
            get { return _POId; }
            set { _POId = value; }
        }

        public string POCode
        {
            get { return _POCode; }
            set { _POCode = value; }
        }

        public decimal POAmount
        {
            get { return _POAmount; }
            set { _POAmount = value; }
        }

        public decimal BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
        }

        public DateTime POCreatedDate
        {
            get { return _POCreatedDate; }
            set { _POCreatedDate = value; }
        }

        public int POCreatedUser
        {
            get { return _POCreatedUser; }
            set { _POCreatedUser = value; }
        }

        public int SupId
        {
            get { return _SupId; }
            set { _SupId = value; }
        }

        public int POLastModifiedBy
        {
            get { return _POLastModifiedBy; }
            set { _POLastModifiedBy = value; }
        }

        public DateTime POLastModifiedDate
        {
            get { return _POLastModifiedDate; }
            set { _POLastModifiedDate = value; }
        }

        public bool IsReceived
        {
            get { return _IsReceived; }
            set { _IsReceived = value; }
        }

        public DataSet DsPOItems
        {
            get { return dsPOItems; }
            set { dsPOItems = value; }
        }

        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }

        public DateTime? PODate
        {
            get { return _PODate; }
            set { _PODate = value; }
        }

        public int? RequestedBy
        {
            get { return _RequestedBy; }
            set { _RequestedBy = value; }
        }

        public LankaTiles.Common.Structures.POStatus POStatus
        {
            get { return _POStatus; }
            set { _POStatus = value; }
        }

        public string POComment
        {
            get { return _POComment; }
            set { _POComment = value; }
        }

        #endregion

        #region Constructor

        public PO()
        {
            _POId = 0;
        }

        #endregion

        #region Public Methods

        #region Save

        public bool Save()
        {
            try
            {
                if (this.POId > 0)
                {
                    return (new PODAO()).UpdatePO(this);
                }
                else
                {
                    return (new PODAO()).AddPO(this);
                }
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Get PO By ID

        public bool GetPOByID()
        {
            try
            {
                return (new PODAO()).GetPOByID(this);
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Get PO Items By PO ID

        public DataSet GetPOItemsByPOID()
        {
            try
            {
                return (new PODAO()).GetPOItemsByPOID(this);
            }
            catch (System.Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get All Partialy Received PO

        public DataSet GetAllPartialyReceivedPO()
        {
            try
            {
                return (new PODAO()).GetAllPartialyReceivedPO();
            }
            catch (System.Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get All Partialy Received PO Items By POID

        public DataSet GetAllPartialyReceivedPOItemsByPOID()
        {
            try
            {
                return (new PODAO()).GetAllPartialyReceivedPOItemsByPOID(this);
            }
            catch (System.Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
