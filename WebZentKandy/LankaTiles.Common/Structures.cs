using System;
using System.Collections.Generic;
using System.Text;

namespace LankaTiles.Common
{
    public class Structures
    {
        /// <summary>
        /// Keep this synced with the tblUserRoles table in the database
        /// </summary>
        public enum UserRoles
        {
            SuperAdmin      = 1,
            Administrator   = 2,
            Manager         = 3,
            InventoryUser   = 4,
            Cashier         = 5,
            Accountant      = 6,
            AdminAssistance = 7,
        }

        /// <summary>
        /// Keep this as a reference
        /// </summary>
        public enum IsActive
        {
            Active      = 1,
            Inactive    = 0,
        }

        /// <summary>
        /// Voucher Voucher Types
        /// </summary>
        public enum VoucherTypes
        {
            OTHER = 0,
            PURCHASE_ORDERS = 1,
            SUPPLIERS = 2,
            CUSTOMERS = 3
        }

        /// <summary>
        /// Voucher Payment Types
        /// </summary>
        public enum PaymentTypes
        {
            CASH = 1,
            CHEQUE = 2,
            CREDIT_CARD = 3
        }

        /// <summary>
        /// Invoice Status
        /// </summary>
        public enum InvoiceStatus
        {
            Created = 1,
            Pending = 2,
            Printed = 3,
            Delivered = 4,
            Cancelled = 5
        }

        /// <summary>
        /// Card Types
        /// </summary>
        public enum CardTypes
        {
            NONE = -1,
            MASTER = 1,
            VISA = 2,
            AMERICAN_EXPRESS = 3
        }

        /// <summary>
        /// Item Search Grid Column Captions
        /// </summary>
        public enum ItemSearchGridColumnNames
        {
            Cost,
            ItemCode,
            ItemDescription,
            SellingPrice,
            MinSellingPrice,
            ROL,
            TypeName,
            CategoryType,
            BrandName,
            QuantityInHand,
            InvoicedQty,
            IsActive,
            TotalValue,
            TrueQIH,
            TrueStockValue,
        }

        /// <summary>
        /// Purchase Order Add Grid Column Captions
        /// </summary>
        public enum PurchaseOrderGridColumnNames
        {
            ItemCode        = 0,
            ItemDescription = 1,
            BrandName       = 2,
            POItemCost      = 3,
            DiscountPerUnit = 4,
            Qty             = 5,
            LineCost        = 6,
        }

        /// <summary>
        /// Purchase Order Status
        /// </summary>
        public enum POStatus
        {
            Active = 1,
            Cancel = 2,
        }

        /// <summary>
        /// Purchase Return recieved status
        /// </summary>
        public enum PRRecievedStatus
        {
            All = 0,
            TotallyIssued = 1,
            PartiallyIssued = 2,
        }

        /// <summary>
        /// The status list for an individual cheque
        /// </summary>
        public enum ChqStatusId
        {
            Created = 1,
            Issued = 2,
            Cancelled = 3,
            Lost = 4,
        }

        /// <summary>
        /// Different item history types
        /// </summary>
        public enum ItemHistoryTypes
        {
            CreateItem      = 1,
            ItemUpdated     = 2,
            GrnPO           = 3,
            GrnInvoice      = 4,
            InvoiceCreated  = 5,
            InvoiceUpdate   = 6,
            InvoiceCancel   = 7,
            StockAdjustment = 8,
            ItemTransfersToShowrooms = 9,
            PurchaseReturns =10,
            IssueGatePass = 11,
            BulkUpdate     = 12,
        }
    }
}
