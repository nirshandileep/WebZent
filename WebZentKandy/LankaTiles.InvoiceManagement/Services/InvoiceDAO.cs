using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;
using LankaTiles.ItemsManagement;


namespace LankaTiles.InvoiceManagement
{
    [Serializable]
    public class InvoiceDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add Invoice

        public bool AddInvoice(Invoice invoice)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Add);

                if (CheckInvoiceNumberForExistence(invoice.InvoiceNo.Trim()))
                {
                    if (invoice.I_in.Value == true)
                    {
                        invoice.InvoiceNo = this.GetNextCodeForHdnInvoice(invoice);
                    }
                    else
                    {
                        invoice.InvoiceNo = this.GetNextCodeForInvoice(invoice);
                    }
                }

                db.AddInParameter(cmd, "@sInvoiceNo", DbType.String, invoice.InvoiceNo.Trim());
                db.AddInParameter(cmd, "@mGrandTotal", DbType.Currency, invoice.GrandTotal);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, invoice.BranchId);
                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, invoice.CustomerID);
                db.AddInParameter(cmd, "@sPaymentType", DbType.String, invoice.PaymentType);
                db.AddInParameter(cmd, "@mDueAmount", DbType.Currency, invoice.DueAmount);
                db.AddInParameter(cmd, "@mCustDebitAmount", DbType.Currency, invoice.CustDebitTotal);

                db.AddInParameter(cmd, "@biGRNId", DbType.Int64, invoice.GRNId);

                db.AddInParameter(cmd, "@bIsPaid", DbType.Boolean, invoice.IsPaid);
                db.AddInParameter(cmd, "@bI_in", DbType.Boolean, invoice.I_in);
                db.AddInParameter(cmd, "@iCreatedUser", DbType.Int32, invoice.CreatedUser);
                db.AddInParameter(cmd, "@sTransferNote", DbType.String, invoice.TransferNote);
                if (invoice.PaymentType == "2")
                {
                    db.AddInParameter(cmd, "@sChequeNumber", DbType.String, invoice.ChequeNumber);
                    if (invoice.CreatedDate != DateTime.MinValue)
                    {
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, invoice.CreatedDate);
                    }
                    else
                    {
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                    }
                }
                else
                {
                    db.AddInParameter(cmd, "@sChequeNumber", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                }

                if (invoice.PaymentType == "3")
                {
                    db.AddInParameter(cmd, "@sCardType", DbType.String, invoice.CardType);
                    db.AddInParameter(cmd, "@mBankComision", DbType.Currency, invoice.BankComision);
                    db.AddInParameter(cmd, "@mBanked_Ammount", DbType.Currency, invoice.Banked_Ammount);
                    db.AddInParameter(cmd, "@dCardComisionRate", DbType.Decimal, invoice.CardComisionRate);
                }
                else
                {
                    db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@mBankComision", DbType.Currency, DBNull.Value);
                    db.AddInParameter(cmd, "@mBanked_Ammount", DbType.Currency, invoice.GrandTotal);
                    db.AddInParameter(cmd, "@dCardComisionRate", DbType.Decimal, DBNull.Value);
                }
                db.AddInParameter(cmd, "@sRemarks", DbType.String, invoice.Remarks);
                db.AddInParameter(cmd, "@dDate", DbType.Date, invoice.Date);

                db.AddOutParameter(cmd, "@iInvoiceId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    Int32 newInvoiceID = Convert.ToInt32(db.GetParameterValue(cmd, "@iInvoiceId"));

                    if (newInvoiceID > 0)
                    {
                        invoice.InvoiceId = newInvoiceID;
                        if (this.UpdateInvoiceDetails(invoice, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
                    }
                }
            }
            catch (DataException de)
            {
                transaction.Rollback();
                result = false;
                throw de;
            }
            catch (System.Exception ex )
            {
                transaction.Rollback();
                result = false;
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Update Invoice

        public bool UpdateInvoice(Invoice invoice)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Update);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(cmd, "@mGrandTotal", DbType.Currency, invoice.GrandTotal);// 03/09/2011 - Nirshan
                db.AddInParameter(cmd, "@mDueAmount", DbType.Currency, invoice.DueAmount);
                db.AddInParameter(cmd, "@bIsPaid", DbType.Boolean, invoice.IsPaid);
                db.AddInParameter(cmd, "@iModifiedUser", DbType.Int32, invoice.ModifiedUser);

                if (invoice.PaymentType != "1")
                {
                    db.AddInParameter(cmd, "@sChequeNumber", DbType.String, invoice.ChequeNumber);
                    if (invoice.CreatedDate != DateTime.MinValue)
                    {
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, invoice.CreatedDate);
                    }
                    else
                    {
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                    }
                }
                else
                {
                    db.AddInParameter(cmd, "@sChequeNumber", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                }

                if (invoice.PaymentType == "3")
                {
                    db.AddInParameter(cmd, "@sCardType", DbType.String, invoice.CardType);
                    db.AddInParameter(cmd, "@mBankComision", DbType.Currency, invoice.BankComision);
                    db.AddInParameter(cmd, "@mBanked_Ammount", DbType.Currency, invoice.Banked_Ammount);
                    db.AddInParameter(cmd, "@dCardComisionRate", DbType.Decimal, invoice.CardComisionRate);
                }
                else
                {
                    db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@mBankComision", DbType.Currency, DBNull.Value);
                    db.AddInParameter(cmd, "@mBanked_Ammount", DbType.Currency, invoice.GrandTotal);
                    db.AddInParameter(cmd, "@dCardComisionRate", DbType.Decimal, DBNull.Value);
                }

                db.AddInParameter(cmd, "@sRemarks", DbType.String, invoice.Remarks);
                db.AddInParameter(cmd, "@iStatus", DbType.Int32, (int)invoice.Status);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    if (this.UpdateInvoiceDetails(invoice, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }

                }

            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                result = false;
                throw ex;
            }

            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Update Invoice Details

        private bool UpdateInvoiceDetails(Invoice invoice, Database db, DbTransaction transaction)
        {
            bool result = true;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Add_InvoiceDetails);
                db.AddInParameter(insCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@biQuantity", DbType.Int64, "Quantity", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(insCommand, "@mPrice", DbType.Currency, "Price", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@tiGroupID", DbType.Int16, "GroupID", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@mTotalPrice", DbType.Currency, "TotalPrice", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@dDiscountPerUnit", DbType.Currency, "DiscountPerUnit", DataRowVersion.Current);

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Update_InvoiceDetails);
                db.AddInParameter(updCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@biQuantity", DbType.Int64, "Quantity", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(updCommand, "@mPrice", DbType.Currency, "Price", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@tiGroupID", DbType.Int16, "GroupID", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mTotalPrice", DbType.Currency, "TotalPrice", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Delete_InvoiceDetails);
                db.AddInParameter(delCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(delCommand, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(delCommand, "@tiGroupID", DbType.Int16, "GroupID", DataRowVersion.Current);

                int rowcount = db.UpdateDataSet(invoice.DsInvoiceDetails, invoice.DsInvoiceDetails.Tables[0].TableName, insCommand, updCommand, delCommand,
                                transaction);

                //Commented this because there can be instances where the line items are not edited
                //if (rowcount > 0)
                //{
                //    result = true;
                //}
                //else
                //{
                //    result = false;
                //}

            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Invoice By ID

        public bool GetInvoiceByInvoiceID(Invoice invoice)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetInvoiceByID);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                
                IDataReader reader = db.ExecuteReader(cmd);

                if(reader !=null)
                {
                    while(reader.Read())
                    {
                        invoice.InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                        invoice.InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? Convert.ToString(reader["InvoiceNo"].ToString()) : string.Empty;
                        invoice.Date = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"].ToString()) : DateTime.MinValue;
                        invoice.GrandTotal = reader["GrandTotal"] != DBNull.Value ? Convert.ToDecimal(reader["GrandTotal"].ToString()) 
                                             :Convert.ToDecimal("0");
                        invoice.BranchId = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"].ToString()) : 0;
                        invoice.IsIssued = reader["IsIssued"] != DBNull.Value ? (reader["IsIssued"].ToString() == "1" ? true : false) : false;

                        if (reader["CustomerID"] != DBNull.Value)
                        {
                            invoice.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        }
                        else
                        {
                            invoice.CustomerID = null;
                        }
                       
                        invoice.PaymentType = reader["PaymentType"] != DBNull.Value ? Convert.ToString(reader["PaymentType"].ToString()) 
                                                : string.Empty;
                        invoice.DueAmount = reader["DueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["DueAmount"].ToString())
                                                : Convert.ToDecimal("0");

                        invoice.CustDebitTotal = reader["CustomerDebitUsed"] != DBNull.Value ? Convert.ToDecimal(reader["CustomerDebitUsed"].ToString()) : Convert.ToDecimal("0");

                        invoice.IsPaid = reader["IsPaid"] != DBNull.Value ? Convert.ToBoolean(reader["IsPaid"].ToString()) : false;
                        invoice.Status = (Structures.InvoiceStatus)(reader["InvoiceStatusID"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceStatusID"].ToString()) : 1);
                        if(reader["I_in"] != DBNull.Value)
                        {
                            invoice.I_in = Convert.ToBoolean(reader["I_in"].ToString());
                        }
                        else
                        {
                            invoice.I_in = null;
                        }
                        invoice.CreatedUser = reader["CreatedUser"] != DBNull.Value ? Convert.ToInt32(reader["CreatedUser"].ToString()) : 0;
                        invoice.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) 
                                               : DateTime.MinValue;

                        if(reader["ModifiedUser"] != DBNull.Value )
                        {
                            invoice.ModifiedUser = Convert.ToInt32(reader["ModifiedUser"].ToString());
                        }
                        else
                        {
                            invoice.ModifiedUser = null;
                        }

                        if(reader["ModifiedDate"] != DBNull.Value )
                        {
                            invoice.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                        }
                        else
                        {
                            invoice.ModifiedDate = null;
                        }

                        if (reader["TransferNote"] != DBNull.Value)
                        {
                            invoice.TransferNote = Convert.ToString(reader["TransferNote"].ToString());
                        }
                        else
                        {
                            invoice.TransferNote= string.Empty;
                        }

                        invoice.ChequeNumber = reader["ChequeNumber"] != DBNull.Value ? Convert.ToString(reader["ChequeNumber"].ToString()) 
                                                : string.Empty;

                        invoice.ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"])
                                                : DateTime.MinValue;

                        invoice.Remarks = reader["Remarks"] != DBNull.Value ? Convert.ToString(reader["Remarks"].ToString())
                                                : string.Empty;

                        invoice.Banked_Ammount = reader["Banked_Ammount"] != DBNull.Value ? Convert.ToDecimal(reader["Banked_Ammount"].ToString()) : Convert.ToDecimal("0");

                        if (reader["CardType"] != DBNull.Value)
                        {
                            if (reader["CardType"].ToString() == Structures.CardTypes.AMERICAN_EXPRESS.ToString())
                            {
                                invoice.CardType = Structures.CardTypes.AMERICAN_EXPRESS;
                            }
                            else if (reader["CardType"].ToString() == Structures.CardTypes.MASTER.ToString())   
                            {
                                invoice.CardType = Structures.CardTypes.MASTER;
                            }
                            else if (reader["CardType"].ToString() == Structures.CardTypes.VISA.ToString())
                            {
                                invoice.CardType = Structures.CardTypes.VISA;
                            }
                        }

                        if (reader["GRNId"] != DBNull.Value)
                        {
                            invoice.GRNId = Convert.ToInt64(reader["GRNId"].ToString().Trim() == "" ? "0" : reader["GRNId"].ToString().Trim());
                        }
                        else
                        {
                            invoice.GRNId = null;
                        }

                        //09/10/2011
                        invoice.PaidAmount = invoice.GrandTotal - (invoice.DueAmount + invoice.CustDebitTotal);
                        invoice.DsInvoiceDetails = this.GetInvoiceDetailsByInvoiceID(invoice);

                        //2012/07/11
                        invoice.IsVoucherPaymentMade = reader["IsVoucherPaymentMade"] != DBNull.Value ? Convert.ToBoolean(reader["IsVoucherPaymentMade"].ToString()) : false;

                        result = true;
                    }
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

        #region Get Invoice By Number

        public bool GetInvoiceByInvoiceNumber(Invoice invoice)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetInvoiceByNumber);

                db.AddInParameter(cmd, "@sInvoiceNo", DbType.String, invoice.InvoiceNo);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        invoice.InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                        invoice.InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? Convert.ToString(reader["InvoiceNo"].ToString()) : string.Empty;
                        invoice.Date = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"].ToString()) : DateTime.MinValue;
                        invoice.GrandTotal = reader["GrandTotal"] != DBNull.Value ? Convert.ToDecimal(reader["GrandTotal"].ToString())
                                             : Convert.ToDecimal("0");
                        invoice.BranchId = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"].ToString()) : 0;
                        invoice.IsIssued = reader["IsIssued"] != DBNull.Value ? (reader["IsIssued"].ToString() == "1" ? true : false) : false;
                        if (reader["CustomerID"] != DBNull.Value)
                        {
                            invoice.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        }
                        else
                        {
                            invoice.CustomerID = null;
                        }

                        invoice.PaymentType = reader["PaymentType"] != DBNull.Value ? Convert.ToString(reader["PaymentType"].ToString())
                                                : string.Empty;
                        invoice.DueAmount = reader["DueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["DueAmount"].ToString())
                                                : Convert.ToDecimal("0");
                        invoice.CustDebitTotal = reader["CustomerDebitUsed"] != DBNull.Value ? Convert.ToDecimal(reader["CustomerDebitUsed"].ToString()) : Convert.ToDecimal("0");
                        invoice.IsPaid = reader["IsPaid"] != DBNull.Value ? Convert.ToBoolean(reader["IsPaid"].ToString()) : false;
                        
                        invoice.Status = (Structures.InvoiceStatus)(reader["InvoiceStatusID"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceStatusID"].ToString()) : 1);
                        
                        if (reader["I_in"] != DBNull.Value)
                        {
                            invoice.I_in = Convert.ToBoolean(reader["I_in"].ToString());
                        }
                        else
                        {
                            invoice.I_in = null;
                        }
                        invoice.CreatedUser = reader["CreatedUser"] != DBNull.Value ? Convert.ToInt32(reader["CreatedUser"].ToString()) : 0;
                        invoice.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString())
                                               : DateTime.MinValue;

                        if (reader["ModifiedUser"] != DBNull.Value)
                        {
                            invoice.ModifiedUser = Convert.ToInt32(reader["ModifiedUser"].ToString());
                        }
                        else
                        {
                            invoice.ModifiedUser = null;
                        }

                        if (reader["ModifiedDate"] != DBNull.Value)
                        {
                            invoice.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                        }
                        else
                        {
                            invoice.ModifiedDate = null;
                        }

                        if (reader["TransferNote"] != DBNull.Value)
                        {
                            invoice.TransferNote = Convert.ToString(reader["TransferNote"].ToString());
                        }
                        else
                        {
                            invoice.TransferNote = string.Empty;
                        }

                        invoice.ChequeNumber = reader["ChequeNumber"] != DBNull.Value ? reader["ChequeNumber"].ToString() : string.Empty;

                        invoice.ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"])
                                                : DateTime.MinValue;

                        invoice.Remarks = reader["Remarks"] != DBNull.Value ? Convert.ToString(reader["Remarks"].ToString())
                                                : string.Empty;
                        
                        invoice.Banked_Ammount = reader["Banked_Ammount"] != DBNull.Value ? Convert.ToDecimal(reader["Banked_Ammount"].ToString()) : Convert.ToDecimal("0");
                        //invoice.CardType = (Structures.CardTypes)(reader["CardType"] != DBNull.Value ? reader["CardType"].ToString() : "2");

                        if (reader["CardType"] != DBNull.Value)
                        {
                            if (reader["CardType"].ToString() == Structures.CardTypes.AMERICAN_EXPRESS.ToString())
                            {
                                invoice.CardType = Structures.CardTypes.AMERICAN_EXPRESS;
                            }
                            else if (reader["CardType"].ToString() == Structures.CardTypes.MASTER.ToString())
                            {
                                invoice.CardType = Structures.CardTypes.MASTER;
                            }
                            else if (reader["CardType"].ToString() == Structures.CardTypes.VISA.ToString())
                            {
                                invoice.CardType = Structures.CardTypes.VISA;
                            }
                        }
                        

                        //09/10/2011
                        invoice.PaidAmount = invoice.GrandTotal - invoice.DueAmount;

                        invoice.IsVoucherPaymentMade = reader["IsVoucherPaymentMade"] != DBNull.Value ? (reader["IsVoucherPaymentMade"].ToString() == "1" ? true : false) : false;

                        invoice.DsInvoiceDetails = this.GetInvoiceDetailsByInvoiceID(invoice);

                        result = true;
                    }
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

        #region Get All Invoices

        public DataSet GetAllInvoices()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetAll);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                 throw ex;
            }

            return ds;

        }

        #endregion

        #region Get Invoice Details By Invoice ID

        public DataSet GetInvoiceDetailsByInvoiceID(Invoice invoice)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Get_InvoiceDetails_By_ID);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Invoice Details By Invoice ID For Sales Returns

        public DataSet GetInvoiceDetailsByInvoiceIDForReturns(Invoice invoice)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Get_InvoiceDetails_For_Returns_By_ID);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Next Code For Hidden Invoice

        public string GetNextCodeForHdnInvoice(Invoice invoice)
        {
            string strCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetNextCodeForHidden);

                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, invoice.BranchId);


                strCode = (string) db.ExecuteScalar(cmd);
            }
            catch (System.Exception ex)
            {
                strCode = string.Empty;
                throw ex;
            }
            return strCode;

        }

        #endregion

        #region Get Next Code For Invoice

        public string GetNextCodeForInvoice(Invoice invoice)
        {
            string strCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetNextCodeForInvoice);

                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, invoice.BranchId);

                strCode = (string)db.ExecuteScalar(cmd);
            }
            catch (System.Exception ex)
            {
                strCode = string.Empty;
                throw ex;
            }
            return strCode;

        }

        #endregion

        #region Search Invoice

        public DataSet SearchInvoice(InvoiceSearch invoiceSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Search);

                db.AddInParameter(cmd, "@sInvoiceNo", DbType.String, invoiceSearch.InvoiceNo);
                db.AddInParameter(cmd, "@sFromDate", DbType.String, invoiceSearch.FromDate);
                db.AddInParameter(cmd, "@sToDate", DbType.String, invoiceSearch.ToDate);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, invoiceSearch.BranchId);
                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, invoiceSearch.CustomerID);
                db.AddInParameter(cmd, "@iDueOption", DbType.Int32, invoiceSearch.DueOption);
                db.AddInParameter(cmd, "@iIsPaid", DbType.Int32, invoiceSearch.IsPaid);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Invoice ByRole

        public DataSet GetInvoiceByRole(Int16 roleId)
        {
            DataSet ds = null;
            DataSet temp = new DataSet();
            try
            {
                ds = this.GetAllInvoices();

                DataView dv = new DataView(ds.Tables[0]);
                dv.Sort = "I_in";

                if (roleId > 2)
                {
                    dv.RowFilter = "I_in = 0";                    
                }
                else
                {
                    dv.RowFilter = "I_in = 0 OR I_in = 1";
                }

                temp.Merge(dv.ToTable());
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return temp;

        }

        #endregion

        #region Get All Paid Partially Delivered Invoices

        public DataSet GetAllPaidPartiallyDeliveredInvoices()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetPaidPartiallyDeliveredInvoices);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Paid Partially Delivered Invoices by Invoice ID

        public DataSet GetPaidPartiallyDeliveredInvoicesByInvoiceID(Invoice invoice)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetPaidPartiallyDeliveredInvoicesByInvoiceID);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Items To be Issued By InvoiceID

        public DataSet GetItemsTobeIssuedByInvoiceID(Invoice invoice)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetItemsTobeIssuedByInvoiceID);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Item Details To be Issued By InvoiceID and Item Id

        public bool GetItemTobeIssuedDetailsByInvoiceAndItem(Invoice invoice, Item objItem)
        {
            bool istrue = false ;
            DataSet ds = null;
            try
            {
                ds = this.GetItemsTobeIssuedByInvoiceID(invoice);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataView dv = new DataView(ds.Tables[0]);

                    int found = -1;

                    dv.Sort = "Id";
                    found = dv.Find(objItem.ItemId);
                    if (found != -1)
                    {
                        invoice.InvoiceDetails.InvDetailId  = dv.Table.Rows[found]["Id"] != null ? Int64.Parse(dv.Table.Rows[found]["Id"].ToString()) : 0;
                        objItem.ItemDescription             = dv.Table.Rows[found]["ItemDescription"] != null ? dv.Table.Rows[found]["ItemDescription"].ToString() : String.Empty;
                        objItem.QuantityToBeIssued          = dv.Table.Rows[found]["QtyTobeIssued"] != null ? Int32.Parse(dv.Table.Rows[found]["QtyTobeIssued"].ToString()) : 0;
                        objItem.QuantityInHand              = dv.Table.Rows[found]["QuantityInHand"] != null ? Int32.Parse(dv.Table.Rows[found]["QuantityInHand"].ToString()) : 0;
                        istrue = true;
                    }
                }
                else
                {
                    istrue = false;
                }
                
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return istrue;

        }

        #endregion

        #region Check Invoice Number For Existence

        public bool CheckInvoiceNumberForExistence(string invoiceNumber)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Check_InvoiceNumber_IsExists);

                db.AddInParameter(cmd, "@sInvoiceNo", DbType.String, invoiceNumber);
                db.AddOutParameter(cmd, "@bIsExists", DbType.Boolean, 1);
                db.ExecuteNonQuery(cmd);

                result = db.GetParameterValue(cmd, "@bIsExists") != DBNull.Value ? Convert.ToBoolean(db.GetParameterValue(cmd, "@bIsExists")) : false;

            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;

        }

        #endregion

        #region Cancel Invoice

        public bool CancelInvoice(Invoice invoice)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Cancel);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(cmd, "@iInvoiceStatusID", DbType.Int32, (Int32)Structures.InvoiceStatus.Cancelled);
                db.AddInParameter(cmd, "@sRemarks", DbType.String, invoice.Remarks);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    transaction.Commit();
                    result = true;
                }

            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                result = false;
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Get All Cancelable Invoices

        public DataSet GetAllCancelableInvoices()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_GetCancelableInvoices);

                ds = db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion

        #region Get All Invoices for reporting

        /// <summary>
        /// Get all invoice related details in the database for reporting purposes
        /// </summary>
        /// <returns>Dataset with 2 tables</returns>
        public DataSet GetAllInvoicesAndDetailsForReporting()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllInvoiceDetails);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All Invoices for reporting with search criteria

        /// <summary>
        /// Get all invoice related details in the database for reporting purposes
        /// Over load with invoice search by search criteria
        /// </summary>
        /// <returns>Dataset with 2 tables</returns>
        public DataSet GetAllInvoicesAndDetailsForReporting(InvoiceSearch invoiceSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllInvoiceDetailsByDateRange);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Invoice Details By Invoice ID For Reporting

        public DataSet GetInvoiceDetailsByInvoiceIDForReporting(Invoice invoice)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetInvoiceDetailsByInvId);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All invoices for Item ID For Reporting

        public DataSet GetItemWiseInvoicesForReporting(Int64 itemId,DateTime? fromDate, DateTime? toDate)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetItemWiseInvoicesByItemId);

                db.AddInParameter(cmd, "@iItemId", DbType.Int64, itemId);
                db.AddInParameter(cmd, "@dFromDate", DbType.DateTime, fromDate);
                db.AddInParameter(cmd, "@dToDate", DbType.DateTime, toDate);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All Acticve Invoices for reporting NOT USED

        /// <summary>
        /// Get all active invoices
        /// NOT USED
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllActiveInvoicesForReporting()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllActiveInvoices);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All Acticve Invoices for reporting

        /// <summary>
        /// Get all active invoices
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllActiveInvoicesForReporting(InvoiceSearch invoiceSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllActiveInvoicesByDateRange);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All Cancelled Invoices for reporting NOT USED

        /// <summary>
        /// Get all cancelled invoices
        /// NOT USED
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllCancelledInvoicesForReporting()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllCancelledInvoices);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get All Cancelled Invoices for reporting

        /// <summary>
        /// Get all cancelled invoices
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllCancelledInvoicesForReporting(InvoiceSearch invoiceSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllCancelledInvoicesByDateRange);


                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Payment Due Invoices for reporting NOT USED

        /// <summary>
        /// Get all payment due invoices
        /// NOT USED
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllPaymentDueInvoicesForReporting()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllPaymentDueInvoices);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Payment Due Invoices for reporting

        /// <summary>
        /// Get all payment due invoices
        /// </summary>
        /// <returns>Dataset with one table</returns>
        public DataSet GetAllPaymentDueInvoicesForReporting(InvoiceSearch invoiceSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetAllPaymentDueInvoicesByDateRange);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion

        #region Get Sales By Items for reporting

        public DataSet GetItemWiseSalesForReporting(InvoiceSearch invoiceSearch)
        {
            try
            {
                //testing svn

                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetItemSalesByItems);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemWiseSalesForReporting(InvoiceSearch invoiceSearch)");
                return null;
                throw ex;
            }

        }

        #endregion

        #region Get Sales By Items for reporting Sales Rep wise

        public DataSet GetItemWiseSalesForReportingRepWise(InvoiceSearch invoiceSearch)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Invoice_Report_GetItemSalesByItemsRepWise);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, invoiceSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, invoiceSearch.ToDateRep);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemWiseSalesForReportingRepWise(InvoiceSearch invoiceSearch)");
                return null;
                throw ex;
            }

        }

        #endregion

        #region Update invoice status

        public bool UpdateInvoiceStatus(Invoice invoice)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_UpdateStatus);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(cmd, "@iStatus", DbType.Int32, (int)invoice.Status);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (System.Exception ex)
            {
                success = false;
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemWiseSalesForReporting(InvoiceSearch invoiceSearch)");
                throw ex;
            }
            return success;
        }

        #endregion

        #region Update Sales Rep

        /// <summary>
        /// Update Sales Rep name by invoice id
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns>True if success</returns>
        public bool UpdateSalesRep(Invoice invoice)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Invoice_Update_SalesRepByInvId);

                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, invoice.InvoiceId);
                db.AddInParameter(cmd, "@iCreatedUser", DbType.Int32, invoice.CreatedUser);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (System.Exception ex)
            {
                success = false;
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateSalesRep(Invoice invoice)");
                throw ex;
            }
            return success;
        }

        #endregion
    }
}
