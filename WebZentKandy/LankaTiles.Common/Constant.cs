namespace LankaTiles.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Configuration.Assemblies;

    public class Constant
    {
        public static readonly string Database_Connection_Name = "LankaTiles";
        public static readonly char Error_Seperator = '|';
        public static readonly string Error_System = "An error has occurred.Please inform the administrator.";
        public static readonly string Error_Code = "Please include error code {0} and the steps that you took just prior to this error message appearing.";
        public static readonly string CONST_SiteAddress = System.Configuration.ConfigurationSettings.AppSettings["App_URL"];
        public static readonly string LogExecutionTimes = System.Configuration.ConfigurationSettings.AppSettings["LogExecutionTimes"];
        public static readonly string MainStoreId = System.Configuration.ConfigurationSettings.AppSettings["MainStoreId"];
        public static readonly string MaximumDiscountAllowed_Invoice = System.Configuration.ConfigurationSettings.AppSettings["MaximumDiscountAllowed"];
        public static readonly string MaximumDiscountAllowed_PO = System.Configuration.ConfigurationSettings.AppSettings["MaximumDiscountAllowedPO"];

        public static readonly string Bank_Reduction_Master = System.Configuration.ConfigurationSettings.AppSettings["SurChargeMaster"];
        public static readonly string Bank_Reduction_Visa = System.Configuration.ConfigurationSettings.AppSettings["SurChargeVisa"];
        public static readonly string Bank_Reduction_Amex = System.Configuration.ConfigurationSettings.AppSettings["SurChargeAmex"];

        public static readonly string Increment_Seed = System.Configuration.ConfigurationSettings.AppSettings["IncrementSeed"];
        public static readonly string SalesReport_Manager_MinBackDate_Date = System.Configuration.ConfigurationSettings.AppSettings["Sales_Manager_Min_BackDate"];

        //Amount that can be invoiced for cash customer
        public static readonly string Invoice_Max_Amount_Without_Customer = System.Configuration.ConfigurationSettings.AppSettings["MaxInvAmountWithoutCustomer"];
        public static readonly string Invoice_Omitted_Customers_Above_Limit = System.Configuration.ConfigurationSettings.AppSettings["InvCustomerIds"];

        #region User Management
        public static readonly string SP_SiteSurvey_InsertUser = "uspInsertUsers";
        public static readonly string SP_User_GetUserName = "uspGetUsername";

        public static readonly string SP_User_Insert = "SP_Users_InsUsers";
//        public static readonly string SP_User_Insert = "SP_Users_InsUsers";
        public static readonly string SP_User_InsLoginLog = "SP_User_InsLoginLog";

        public static readonly string SP_User_Update = "SP_Users_UpdUsers";
        public static readonly string SP_User_Search = "SP_Users_SelUser";
        public static readonly string SP_User_GetUserById = "SP_Users_SelByID";
        public static readonly string SP_User_Delete = "SP_Users_DelUser";
        public static readonly string SP_User_GetAll = "SP_Users_SelAllUsers";
        public static readonly string SP_User_CheckUserNameExists = "SP_Users_ChkUserNameExists";
        public static readonly string SP_User_GetAllUserRoles = "SP_UserRoles_SelAllUserRoles";
                
        public static readonly string SP_User_CheckUserLogin = "uspCheckUserLogin";


        public static readonly string MSG_User_UserNameExists = "Username exists please enter another Username.";
        public static readonly string MSG_User_PasswordMisMatch = "The passwords does not match.";
        public static readonly string MSG_User_SuccessfullySaved = "The changes were successfully saved.";
        public static readonly string MSG_User_InvalidUserNameOrPassword = "Invalid Username or password";

        public static readonly string MSG_Save_SavedSeccessfully = "The Information is saved successfully";
        public static readonly string MSG_Save_NotSavedSeccessfully = "The Information was not saved !";

        #endregion

        #region Data Formats
        public static readonly string Format_Date = "dd-MMM-yyyy";
        #endregion

        #region Items Management

        public static readonly string SP_Item_GetNextItemCode = "SP_Item_GetNextItemCode";
        public static readonly string SP_Items_Delete = "SP_Items_DelItems";
        public static readonly string SP_Items_Insert = "SP_Items_InsItems";
        public static readonly string SP_Items_GetAll = "SP_Items_SelAllItems";
        public static readonly string SP_Items_GetByID = "SP_Items_SelByID";
        public static readonly string SP_Items_GetByItemCode = "SP_Items_SelByItemCode";
        public static readonly string SP_Items_Search = "SP_Items_SelItems";
        public static readonly string SP_Items_Update = "SP_Items_UpdItems";

        public static readonly string SP_Items_GetByItemIDAndBranchID = "SP_Items_SelByItemIDAndBranchID";
        public static readonly string SP_Items_UpdateAllById = "SP_Items_UpdateAllItemsById";

        #endregion

        public static readonly string SP_Type_Insert = "SP_Type_InsTypes";
        public static readonly string SP_Type_GetAll = "SP_Type_SelAllTypes";

        /// <summary>
        /// Stock Adjustment Sp's
        /// </summary>

        public static readonly string SP_Items_Adjustment_InsItems = "SP_Items_Adjustment_InsItems";
        public static readonly string SP_Items_Adjustment_SelItemsByItemId = "SP_Items_Adjustment_SelAllByItemId";

        #region 

        #endregion


        #region Location Management

        public static readonly string SP_Location_InsertLocation = "SP_Branch_InsBranchs";
        public static readonly string SP_Location_IsLocationCodeExists = "SP_Branchs_ChkIsExistsBranchCode";
        public static readonly string SP_Location_SearchLocations = "SP_Branch_SelBranches";
        public static readonly string SP_Location_GetAllLocations = "SP_Branch_SelAllBranches";
        public static readonly string SP_Location_Delete = "SP_Branch_DelBranch";
        public static readonly string SP_Location_Update = "SP_Branch_UpdBranchs";
        public static readonly string SP_Location_GetLocationByID = "SP_Branch_SelByID";

        public static readonly string SP_Location_GetLocationStocksByBranch = "SP_BranchStocks_SelBranchStocksByBranch";
        public static readonly string SP_Location_GetLocationStocksByBranchAndItem = "SP_BranchStocks_SelByBranchAndItem";
        public static readonly string SP_Location_GetLocationStocksByItem = "SP_BranchStocks_SelByItem";

        public static readonly string MSG_Location_CodeExists = "Location Code Already Exists";
        public static readonly string MSG_Save_LocationWithSameDetailsExists = "A location with exact details exists.";
        public static readonly string MSG_Save_LocationUpdateSuccess = "Location details were successfully updated.";
        public static readonly string MSG_Save_LocationUpdateNotSuccess = "Location details were not updated successfully.";
        
        #endregion

        #region Category Related

        public static readonly string Error_Category_Exists = "The Category Type Currently Exists";
        
        public static readonly string SP_Categories_ChkIsExists = "SP_Categories_ChkIsExists";
        public static readonly string SP_Categories_Delete = "SP_Categories_DelCategories";
        public static readonly string SP_Categories_Insert = "SP_Categories_InsCategories";
        public static readonly string SP_Categories_GetAll = "SP_Categories_SelAllCategories";
        public static readonly string SP_Categories_GetByID = "SP_Categories_SelByID";
        public static readonly string SP_Categories_Search = "SP_Categories_SelCategories";
        public static readonly string SP_Categories_Update = "SP_Categories_UpdCategories";

        public static readonly string SP_Categories_Get_BrandsByCategoryID = "SP_Categories_SelBrandsByCategoryID";

        #endregion

        #region Brand related

        public static readonly string SP_Brands_Insert = "SP_Brands_InsBrands";
        public static readonly string SP_Brands_Delete = "SP_Brands_DelBrands";
        public static readonly string SP_Brands_GetAll = "SP_Brands_SelAllBrands";
        public static readonly string SP_Brands_Search = "SP_Brands_SelBrands";
        public static readonly string SP_Brands_GetByID = "SP_Brands_SelByID";
        public static readonly string SP_Brands_Update = "SP_Brands_UpdBrands";
        public static readonly string SP_Brands_SelCategoriesByBrandID = "SP_Brands_SelCategoriesByBrandID";

        public static readonly string SP_BrandCategories_InsBrandCategories = "SP_BrandCategories_InsBrandCategories";
        public static readonly string SP_BrandCategories_DelBrandCategories = "SP_BrandCategories_DelBrandCategories";
        
        #endregion

        #region PO related

        public static readonly string SP_PO_GetNextCode = "SP_PO_GetNextCode";
        public static readonly string SP_PO_Insert = "SP_PO_InsPO";
        public static readonly string SP_PO_GetAll = "SP_PO_SelAllPO";
        public static readonly string SP_PO_GetByID = "SP_PO_SelPOByID";
        public static readonly string SP_PO_Update = "SP_PO_UpdPO";
        public static readonly string SP_PO_UpdateStatus = "SP_PO_UpdPOStatus";
        public static readonly string SP_PO_GetPOItemsByPOID = "SP_PO_SelPOItemsByPOID";
        public static readonly string SP_PO_SearchPO = "SP_PO_SearchPO";
        public static readonly string SP_PO_SelPartialyReceivedPO = "SP_PO_SelPartialyReceivedPO";
        public static readonly string SP_PO_SelPartialyReceivedPOItemsByPOID = "SP_PO_SelPartialyReceivedPOItemsByPOID";
        public static readonly string SP_PO_GetAllPayable = "SP_PO_SelAllPayable";

        public static readonly string SP_PO_InsPOItems = "SP_PO_InsPOItems";
        public static readonly string SP_PO_UpdPOItems = "SP_PO_UpdPOItems";
        public static readonly string SP_PO_DelPOItems = "SP_PO_DelPOItems";

        public static readonly string SP_PO_Report_GetPODetailsByItems = "SP_PO_Report_GetPODetailsByItems";
        public static readonly string SP_PO_Report_SearchPO = "SP_PO_Report_SearchPO";

        public static readonly string MSG_PO_ItemAlreadyExistInPO = "Item {0} is currently added to this PO";

        #endregion

        #region Purchase Returns related

        public static readonly string SP_PR_Insert = "SP_PR_InsPR";
        public static readonly string SP_PR_GetByPRID = "SP_PR_SelPRByID";
        public static readonly string SP_PR_Update = "SP_PR_UpdPR";
        public static readonly string SP_PR_GetPRItemsByPRID = "SP_PR_SelPRItemsByPRID";
        public static readonly string SP_PR_GetPRItemsByGRNDetailID = "SP_PR_SelPRItemsByGRNDetailID";
        public static readonly string SP_PR_GetItemsToReturnByGRNId = "SP_PR_SelReturnItemsByGRNId";
        public static readonly string SP_PR_SelPurchaseReturns = "SP_PR_SelPurchaseReturns";//todo

        public static readonly string SP_PR_InsPRItems = "SP_PR_InsPRItems";
        public static readonly string SP_PR_UpdPRItems = "SP_PR_UpdPRItems";
        public static readonly string SP_PR_DelPRItems = "SP_PR_DelPRItems";

        #endregion

        #region Supplier related

        public static readonly string SP_Supplier_Delete = "SP_Suppliers_DelSuppliers";
        public static readonly string SP_Supplier_GetNextCode = "SP_Suppliers_GetNextCode";
        public static readonly string SP_Supplier_Insert = "SP_Suppliers_InsSuppliers";
        public static readonly string SP_Supplier_GetAll = "SP_Suppliers_SelAllSuppliers";
        public static readonly string SP_Supplier_GetByID = "SP_Suppliers_SelByID";
        public static readonly string SP_Supplier_Search = "SP_Suppliers_SelSuppliers";
        public static readonly string SP_Supplier_Update = "SP_Suppliers_UpdSuppliers";
        public static readonly string SP_Suppliers_UpdateAll_ByID = "SP_Suppliers_BulkUpdSuppliers";

        public static readonly string SP_Suppliers_Report_SuppliersTransctionsById = "SP_Suppliers_Report_SuppliersTransctionsById";

        #endregion

        #region GRN related

        public static readonly string SP_GRN_Insert = "SP_GRN_InsGRN";
        public static readonly string SP_GRN_Update = "SP_GRN_UpdGRN";
        public static readonly string SP_GRN_InsertDetails = "SP_GRN_InsGRNDetails";
        public static readonly string SP_GRN_DeleteDetails = "SP_GRN_DelGRNDetails";
        public static readonly string SP_GRN_Get_Details_By_ID = "SP_GRN_SelGRNDetailsByID";
        public static readonly string SP_GRN_Get_By_ID = "SP_GRN_SelGRNByID";
        public static readonly string SP_GRN_Search = "SP_GRN_SelGRN";

        public static readonly string SP_GRN_Check_SupInvNo_Exist = "SP_GRN_CheckSupInvNoExist";
        public static readonly string SP_GRN_Get_By_IDAndCustId = "SP_GRN_SelGRNByIDAndCust";

        #endregion

        #region Groups related

        public static readonly string SP_Groups_Insert = "SP_Groups_InsGroups";
        public static readonly string SP_Groups_Delete = "SP_Groups_DelGroups";
        public static readonly string SP_Groups_GetAll = "SP_Groups_SelAll";
        public static readonly string SP_Groups_GetByID = "SP_Groups_SelByID";
        public static readonly string SP_Groups_Update = "SP_Groups_UpdGroups";
        public static readonly string SP_Groups_SelItemsByGroupID = "SP_Groups_SelGroupItemsByGroupID";
        public static readonly string SP_Groups_GetNextCode = "SP_Groups_GetNextCode";
        public static readonly string SP_Groups_GetByCode = "SP_Groups_SelByCode";
        public static readonly string SP_Groups_SelItemsByGroupCode = "SP_Groups_SelGroupItemsByGroupCode";
        
        public static readonly string SP_Groups_InsGroupItems = "SP_Groups_InsGroupItems";
        public static readonly string SP_Groups_DelGroupItems = "SP_Groups_DelGroupItems";

        #endregion

        #region Item Transfer related

        public static readonly string SP_Item_Transfer_Insert = "SP_ItemTransfer_InsItemTransfer";
        public static readonly string SP_Item_Transfer_Update = "SP_ItemTransfer_UpdItemTransfer";
        public static readonly string SP_Item_Transfer_Get_By_ID = "SP_ItemTransfer_SelTransferByID";
        public static readonly string SP_Item_Transfer_Get_All = "SP_ItemTransfer_SelAll";

        public static readonly string SP_Item_Transfer_Invoice_Insert = "SP_ItemTransfer_InsItemTransferInvoice";
        public static readonly string SP_Item_Transfer_Invoice_Get_By_TransferID = "SP_ItemTransfer_SelItemTransferInvoiceByTransferID";

        public static readonly string MSG_Item_Transfer_InActive = "Item {0} is not Active!";
        #endregion

        #region Invoice related

        public static readonly string SP_Invoice_Delete_InvoiceDetails = "SP_Invoice_DelInvoiceDetails";
        public static readonly string SP_Invoice_GetNextCodeForHidden = "SP_Invoice_GetNextCodeForHdnInvoice";
        public static readonly string SP_Invoice_GetNextCodeForInvoice = "SP_Invoice_GetNextCodeForInvoice";
        public static readonly string SP_Invoice_Add_InvoiceDetails = "SP_Invoice_InsInvoiceDetails";
        public static readonly string SP_Invoice_Add = "SP_Invoice_InsInvoiceHeader";
        public static readonly string SP_Invoice_GetAll = "SP_Invoice_SelAllInvoices";
        public static readonly string SP_Invoice_Get_InvoiceDetails_By_ID = "SP_Invoice_SelInvoiceDetailsByInvoiceID";
        public static readonly string SP_Invoice_GetInvoiceByID = "SP_Invoice_SelInvoicesByInvoiceID";
        public static readonly string SP_Invoice_GetInvoiceByNumber = "SP_Invoice_SelInvoicesByInvoiceNumber";
        public static readonly string SP_Invoice_Update_InvoiceDetails = "SP_Invoice_UpdInvoiceDetails";
        public static readonly string SP_Invoice_Update = "SP_Invoice_UpdInvoiceHeader";
        public static readonly string SP_Invoice_Search = "SP_Invoices_SelInvoices";
        public static readonly string SP_Invoice_UpdateStatus = "SP_Invoice_UpdInvoiceStatus";
        public static readonly string SP_Invoice_Update_SalesRepByInvId = "SP_Invoice_Update_SalesRepByInvId";

        public static readonly string SP_Invoice_Check_InvoiceNumber_IsExists = "SP_Invoice_ChkInvNumberIsExists";
        public static readonly string SP_Invoice_Get_InvoiceDetails_For_Returns_By_ID = "SP_Invoice_SelInvoiceDetailsForReturnsByInvoiceID";

        public static readonly string SP_Invoice_GetPaidPartiallyDeliveredInvoices = "SP_Invoice_SelPaidPartiallyDeliveredInvoices";
        public static readonly string SP_Invoice_GetPaidPartiallyDeliveredInvoicesByInvoiceID
                                        = "SP_Invoice_SelPaidPartiallyDeliveredInvoicesByInvoiceID";
        public static readonly string SP_Invoice_GetItemsTobeIssuedByInvoiceID = "SP_Invoice_SelItemsTobeIssuedByInvoiceID";

        #region Invoice Report section
        
        public static readonly string SP_Invoice_Report_GetAllInvoiceDetails = "SP_Invoice_Report_GetAllInvoiceDetails";
        public static readonly string SP_Invoice_Report_GetAllInvoiceDetailsByDateRange = "SP_Invoice_Report_GetAllInvoiceDetailsByDateRange";/**/

        public static readonly string SP_Invoice_Report_GetInvoiceDetailsByInvId = "SP_Invoice_Report_GetInvoiceDetailsByInvId";
        
        public static readonly string SP_Invoice_Report_GetAllActiveInvoices = "SP_Invoice_Report_GetAllActiveInvoices";
        public static readonly string SP_Invoice_Report_GetAllActiveInvoicesByDateRange = "SP_Invoice_Report_GetAllActiveInvoicesByDateRange";/**/

        public static readonly string SP_Invoice_Report_GetAllCancelledInvoices = "SP_Invoice_Report_GetAllCancelledInvoices";
        public static readonly string SP_Invoice_Report_GetAllCancelledInvoicesByDateRange = "SP_Invoice_Report_GetAllCancelledInvoicesByDateRange";/**/

        public static readonly string SP_Invoice_Report_GetAllPaymentDueInvoices = "SP_Invoice_Report_GetAllPaymentDueInvoices";
        public static readonly string SP_Invoice_Report_GetAllPaymentDueInvoicesByDateRange = "SP_Invoice_Report_GetAllPaymentDueInvoicesByDateRange";/**/

        public static readonly string SP_Invoice_Report_GetItemSalesByItems = "SP_Invoice_Report_GetItemSalesByItems";
        public static readonly string SP_Invoice_Report_GetItemSalesByItemsRepWise = "SP_Invoice_Report_GetItemSalesByItemsRepWise";//new rep wise
        public static readonly string SP_Invoice_Report_GetItemWiseInvoicesByItemId = "SP_Invoice_Report_GetItemWiseInvoicesByItemId";

        #endregion

        public static readonly string MSG_Invoice_Transfer_Details = "Item Code{0}; Branch {1}; Quantity {2}; /n";

        public static readonly string SP_Invoice_GetCancelableInvoices = "SP_Invoice_SelCancelableInvoices";
        public static readonly string SP_Invoice_Cancel = "SP_Invoice_Cancel";

        public static readonly string URL_Navigate_From_AddInvoice = "AddInvoice.aspx?ItemId={0}&FromURL=ProductSearch.aspx";
        public static readonly string URL_Navigate_From_POAdd = "POAdd.aspx?ItemId={0}&FromURL=ProductSearch.aspx";
        public static readonly string URL_Navigate_From_ItemTransfer = "ItemTransfer.aspx?ItemId={0}&FromURL=ProductSearch.aspx";
        public static readonly string URL_Navigate_From_GroupItemAdd = "GroupItemAdd.aspx?ItemId={0}&FromURL=ProductSearch.aspx";
        #endregion

        #region GetPass related

        public static readonly string SP_GetPass_Delete_GetPassDetails = "SP_GetPass_DelGetPassDetails";
        public static readonly string SP_GetPass_GetNextCode = "SP_GetPass_GetNextCode";
        public static readonly string SP_GetPass_Add = "SP_GetPass_InsGetPass";
        public static readonly string SP_GetPas_Add_Details = "SP_GetPass_InsGetPassDetails";
        public static readonly string SP_GetPass_GetByCode = "SP_GetPass_SelByCode";
        public static readonly string SP_GetPass_Get_DetailsByID = "SP_GetPass_SelDetailsByID";
        public static readonly string SP_GetPass_Get_ByID = "SP_GetPass_SelByID";
        public static readonly string SP_GetPass_Update = "SP_GetPass_UpdGetPass";

        public static readonly string SP_GetPass_Search = "SP_GetPass_SelGetPass";

        /// <summary>
        /// Cannot issue quantity {0} from Main Store, available quantity is {1}. Please transfer {2} and proceed!
        /// </summary>
        public static readonly string MSG_Item_InsufficientInMainStore = "Cannot issue quantity {0} from Main Store, available quantity in Main Store is {1}. /n Please transfer {2} and proceed!";

        #endregion

        #region Customer related

        public static readonly string SP_Customers_GetNextCode = "SP_Customers_GetNextCode";
        public static readonly string SP_Customers_GetAll = "SP_Customers_SelAllCustomers";
        public static readonly string SP_Customers_Get_ByID = "SP_Customers_SelByID";
        public static readonly string SP_Customers_Insert = "SP_Customers_InsCustomers";
        public static readonly string SP_Customers_Update = "SP_Customers_UpdCustomers";
        public static readonly string SP_Customers_Delete = "SP_Customers_DelCustomers";

        public static readonly string SP_Customers_Search = "SP_Customers_SelCustomers";
        public static readonly string SP_Customers_UpdateAll_ByID = "SP_Customers_BulkUpdCustomers";

        public static readonly string SP_Customers_Report_TransactionsAll = "SP_Customers_Report_TransactionsAll";

        #endregion

        #region Voucher related

        public static readonly string SP_Voucher_GetAll = "SP_Voucher_GetAll";
        public static readonly string SP_Voucher_GetByCode = "SP_Voucher_GetByCode";
        public static readonly string SP_Voucher_GetByID = "SP_Voucher_GetByID";
        public static readonly string SP_Voucher_GetNextCode = "SP_Voucher_GetNextCode";
        public static readonly string SP_Voucher_InsVoucher = "SP_Voucher_InsVoucher";
        public static readonly string SP_Voucher_Insert_Voucher_Details = "SP_Voucher_InsVoucherDetails";
        public static readonly string SP_Voucher_Get_Voucher_Details_By_VoucherID = "SP_Voucher_SelVoucherDetailsByVoucherID";
        public static readonly string SP_Voucher_SearchVouchers = "SP_Voucher_SelVouchers";
        
        public static readonly string SP_Voucher_GetAll_AccountTypes = "SP_Voucher_GetAll_AccountTypes";
        public static readonly string SP_Voucher_InsAccountType = "SP_Voucher_InsAccountType";

        public static readonly string SP_Voucher_Receivable_InsVoucher = "SP_Voucher_Receivable_InsVoucher";
        public static readonly string SP_Voucher_Receivable_Insert_Voucher_Details = "SP_Voucher_Receivable_InsVoucherDetails";
        public static readonly string SP_Voucher_Receivable_GetByID = "SP_Voucher_Receivable_GetByID";
        public static readonly string SP_Voucher_Receivable_GetVoucherDetailsByID = "SP_Voucher_Receivable_SelVoucherDetailsByVoucherID";
        public static readonly string SP_Voucher_Receivable_GetReceivableInvoicesByCustomerID = "SP_Voucher_Receivable_GetReceivableInvoicesByCustomerID";
        public static readonly string SP_Voucher_Receivable_SearchVouchers = "SP_Voucher_Receivable_SelVouchers";

        public static readonly string SP_Voucher_Report_Payments_ReceivedByDateRange = "SP_Voucher_Report_Payments_ReceivedByDateRange";
        public static readonly string SP_Voucher_Report_ExpencesByDateRange = "SP_Voucher_Report_ExpencesByDateRange";
        public static readonly string SP_Voucher_Report_DayBookByDateRange = "SP_Voucher_Report_DayBook";

        #endregion

        #region Cheque management

        public static readonly string SP_Cheque_Insert = "SP_Cheque_InsChqBook";
        public static readonly string SP_Cheque_Update = "SP_Cheque_UpdChqBook";
        public static readonly string SP_Cheque_InsertDetails = "SP_Cheque_InsChqDetails";
        public static readonly string SP_Cheque_UpdateDetails = "SP_Cheque_UpdChqDetails";
        public static readonly string SP_Cheque_DeleteDetails = "SP_Cheque_DelChqDetails";
        public static readonly string SP_Cheque_Get_Details_By_ID = "SP_Cheque_SelDetailsByBookID";
        public static readonly string SP_Cheque_Get_Book_By_ID = "SP_Cheque_SelChqBookByID";

        public static readonly string SP_Cheque_Get_Book_All = "SP_Cheque_SelAllChqBooks";
        public static readonly string SP_Cheque_Is_ChqNumbers_Already_Exists = "SP_Cheque_Is_ChqNumbers_Already_Exists";

        public static readonly string SP_Cheque_Get_Cheque_By_ChqID = "SP_Cheque_SelChequeByChqID";
        public static readonly string SP_Cheque_UpdateCheque = "SP_Cheque_UpdCheque";
        public static readonly string SP_Cheque_UpdateChequeStatus = "SP_Cheque_UpdChequeStatus";

        public static readonly string SP_Cheque_Get_All_Available_Cheques = "SP_Cheque_SelAllAvailableCheques";
        public static readonly string SP_Cheque_Report_GetAllChequesByDateRange = "SP_Cheque_Report_GetAllChequesByDateRange";

        #endregion

        #region Filter expressions

        public static readonly string FE_ItemSearch_From_ItemTransfers = "[QuantityInHand] > '0'";

        #endregion
    }
}
