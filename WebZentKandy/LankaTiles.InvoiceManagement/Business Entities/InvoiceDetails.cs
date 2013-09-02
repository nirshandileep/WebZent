using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.InvoiceManagement.Business_Entities
{
    [Serializable]
    public class InvoiceDetails
    {
        private Int64 _InvDetailId;
        private Int32 _ItemId;
        private Int64 _Quantity;
        private Int32 _InvoiceId;
        private Decimal _Price;
        private Int16 _GroupID;
        private Decimal _TotalPrice;
        private Int64 _IssuedQTY;

        public Int64 InvDetailId
        {
            get { return _InvDetailId; }
            set { _InvDetailId = value; }
        }

        private Int32 ItemId
        {
            get
            {
                return _ItemId;
            }
            set
            {
                _ItemId = value;
            }
        }

        public Int64 Quantity
        {
            get 
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }

        public Int32 InvoiceId
        {
            get 
            {
                return _InvoiceId;
            }
            set
            {
                _InvoiceId = value;
            }
        }

        public Decimal Price
        {
            get 
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }

        public Int16 GroupID
        {
            get 
            {
                return _GroupID;
            }
            set
            {
                _GroupID = value;
            }
        }

        public Decimal TotalPrice
        {
            get 
            {
                return _TotalPrice;
            }
            set 
            {
                _TotalPrice = value;
            }
        }

        public Int64 IssuedQTY
        {
            get 
            {
                return _IssuedQTY;
            }
            set
            {
                _IssuedQTY = value;
            
            }
        }

        public InvoiceDetails()
        {

        }

    }
}