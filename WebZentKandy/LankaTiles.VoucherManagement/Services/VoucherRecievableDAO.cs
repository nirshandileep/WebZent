using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.VoucherManagement
{
    public class VoucherRecievableDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add Voucher

        public bool AddVoucher(VoucherRecievable voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_InsVoucher);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, voucher.CustomerID);
                db.AddInParameter(cmd, "@mPaymentAmount", DbType.Currency, voucher.PaymentAmount);
                db.AddInParameter(cmd, "@dtPaymentDate", DbType.DateTime, voucher.PaymentDate);
                db.AddInParameter(cmd, "@iPaymentTypeId", DbType.Int32, (int)voucher.PaymentTypeId);

                if (voucher.PaymentTypeId == Structures.PaymentTypes.CASH)
                {
                    db.AddInParameter(cmd, "@sChequeNo", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                    db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, DBNull.Value);
                }
                else if (voucher.PaymentTypeId == Structures.PaymentTypes.CHEQUE)
                {
                    db.AddInParameter(cmd, "@sChequeNo", DbType.String, voucher.ChequeNo);
                    db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, voucher.ChequeDate);
                    db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);
                    db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, DBNull.Value);
                }
                else if (voucher.PaymentTypeId == Structures.PaymentTypes.CREDIT_CARD)
                {
                    db.AddInParameter(cmd, "@sChequeNo", DbType.String, voucher.ChequeNo);
                    db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);
                    db.AddInParameter(cmd, "@sCardType", DbType.String, voucher.CardType);
                    db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, voucher.CardCommisionRate);
                }

                if (voucher.Comment == string.Empty)
                {
                    db.AddInParameter(cmd, "@sComment", DbType.String, DBNull.Value);
                }
                else 
                {
                    db.AddInParameter(cmd, "@sComment", DbType.String, voucher.Comment);
                }

                db.AddInParameter(cmd, "@iCreatedBy", DbType.Int32, voucher.CreatedBy);

                db.AddOutParameter(cmd, "@inewPaymentID", DbType.Int64, 8);
                db.AddOutParameter(cmd, "@snewPaymentCode", DbType.String, 20);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    Int64 newVoucherID = 0;
                    Int64.TryParse(db.GetParameterValue(cmd, "@inewPaymentID").ToString().Trim(), out newVoucherID);

                    if (newVoucherID > 0)
                    {
                        voucher.PaymentID = newVoucherID;
                        voucher.PaymentCode = db.GetParameterValue(cmd, "@snewPaymentCode").ToString().Trim();

                        if (this.UpdateVoucherDetails(voucher, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
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

        #region Update Voucher

        public bool UpdateVoucher(VoucherRecievable voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_InsVoucher);

                db.AddInParameter(cmd, "@mPaymentAmount", DbType.Currency, voucher.PaymentAmount);
                db.AddInParameter(cmd, "@dtPaymentDate", DbType.DateTime, voucher.PaymentDate);
                db.AddInParameter(cmd, "@iPaymentTypeId", DbType.Int32, (int)voucher.PaymentTypeId);
                switch (voucher.PaymentTypeId)
                {
                    case Structures.PaymentTypes.CASH:
                        db.AddInParameter(cmd, "@sChequeNo", DbType.String, DBNull.Value);//null
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);//null
                        db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);//null
                        db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, DBNull.Value);
                        break;
                    case Structures.PaymentTypes.CHEQUE:
                        db.AddInParameter(cmd, "@sChequeNo", DbType.String, voucher.ChequeNo);
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, voucher.ChequeDate);
                        db.AddInParameter(cmd, "@sCardType", DbType.String, DBNull.Value);//null
                        db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, DBNull.Value);//null
                        break;
                    case Structures.PaymentTypes.CREDIT_CARD:
                        db.AddInParameter(cmd, "@sChequeNo", DbType.String,voucher.ChequeNo);
                        db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, DBNull.Value);//null
                        db.AddInParameter(cmd, "@sCardType", DbType.String, voucher.CardType);
                        db.AddInParameter(cmd, "@dCardCommisionRate", DbType.Decimal, voucher.CardCommisionRate);
                        break;
                    default:
                        break;
                }

                db.AddInParameter(cmd, "@sComment", DbType.String, voucher.Comment);
                db.AddInParameter(cmd, "@iCreatedBy", DbType.Int32, voucher.CreatedBy);
                db.AddInParameter(cmd, "@inewPaymentID", DbType.Int64, voucher.PaymentID);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    Int64 newVoucherID = 0;
                    Int64.TryParse(db.GetParameterValue(cmd, "@inewPaymentID").ToString().Trim(), out newVoucherID);

                    if (newVoucherID > 0)
                    {
                        voucher.PaymentID = newVoucherID;
                        voucher.PaymentCode = db.GetParameterValue(cmd, "@snewPaymentID").ToString().Trim();

                        if (this.UpdateVoucherDetails(voucher, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
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

        #region Update Voucher Details

        private bool UpdateVoucherDetails(VoucherRecievable voucher, Database db, DbTransaction pTransaction)
        {
            bool result = false;
            try
            {
                DbCommand insCmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_Insert_Voucher_Details);

                db.AddInParameter(insCmd, "@iPaymentID", DbType.Int64, voucher.PaymentID);
                db.AddInParameter(insCmd, "@iInvoiceId", DbType.Int32, "InvoiceId", DataRowVersion.Current);
                db.AddInParameter(insCmd, "@mAmount", DbType.Currency, "Amount", DataRowVersion.Current);
                db.AddInParameter(insCmd, "@iCreatedBy", DbType.Int32, voucher.CreatedBy);

                //DbCommand updCmd = db.GetStoredProcCommand(Constant.SP_Voucher_Insert_Voucher_Details);

                //db.AddInParameter(updCmd, "@iPaymentDetailID", DbType.Int64, "PaymentDetailID",DataRowVersion.Current);
                //db.AddInParameter(updCmd, "@iInvoiceId", DbType.Int32, "InvoiceId", DataRowVersion.Current);
                //db.AddInParameter(updCmd, "@mAmount", DbType.Currency, "Amount", DataRowVersion.Current);
                //db.AddInParameter(updCmd, "@iModifiedBy", DbType.Currency, "ModifiedBy", DataRowVersion.Current);

                int rowcount = 0;
                rowcount = db.UpdateDataSet(voucher.DsPaymentDetails, voucher.DsPaymentDetails.Tables[0].TableName, insCmd, null, null, pTransaction);

                result = true;

            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Voucher By ID

        public bool GetVoucherByID(VoucherRecievable voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_GetByID);

                db.AddInParameter(cmd, "@iPaymentID", DbType.Int64, voucher.PaymentID);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        voucher.PaymentCode     = reader["PaymentCode"].ToString();
                        voucher.CustomerID      = Convert.ToInt32(reader["CustomerID"].ToString());
                        voucher.PaymentAmount   = reader["PaymentAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PaymentAmount"].ToString()) :
                                                    Convert.ToDecimal("0");
                        voucher.PaymentDate     = reader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaymentDate"].ToString()) :
                                                    DateTime.MinValue;

                        voucher.PaymentTypeId   = (Structures.PaymentTypes)Convert.ToInt32(reader["PaymentTypeId"].ToString());
                        voucher.ChequeNo        = reader["ChequeNo"].ToString();
                        voucher.ChequeDate      = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"].ToString()) :
                                                    DateTime.MinValue;
                        voucher.Comment         = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : "";

                        string cardtype         = reader["CardType"] != DBNull.Value ? reader["CardType"].ToString().Trim() : Structures.CardTypes.NONE.ToString();

                        switch (cardtype)
                        {
                            case "NONE":
                                voucher.CardType = Structures.CardTypes.NONE;
                                break;
                            case "MASTER":
                                voucher.CardType = Structures.CardTypes.MASTER;
                                break;
                            case "VISA":
                                voucher.CardType = Structures.CardTypes.VISA;
                                break;
                            case "AMERICAN_EXPRESS":
                                voucher.CardType= Structures.CardTypes.AMERICAN_EXPRESS;
                                break;
                            default:
                                break;
                        }

                        result = true;
                    }
                }

                //if (result)
                //{
                    this.GetVoucherDetailsByID(voucher);
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

        #region Get Voucher Details By ID

        public DataSet GetVoucherDetailsByID(VoucherRecievable voucher)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_GetVoucherDetailsByID);

                db.AddInParameter(cmd, "@iPaymentID", DbType.Int32, voucher.PaymentID);

                voucher.DsPaymentDetails = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }

        #endregion

        #region Get Receivable invoice details by customer id

        public DataSet GetReceivableInvoiceDetailsByCustomerID(Int32 customerID)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_GetReceivableInvoicesByCustomerID);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, customerID);

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

        #region Select all vouchers

        public DataSet SelectAllVouchers()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Receivable_SearchVouchers);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Get Sales By Items for reporting

        public DataSet GetPaymentsReveivedForReporting(VoucherRecievableSearch voucherSearch)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Voucher_Report_Payments_ReceivedByDateRange);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, voucherSearch.FromDate.Value);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, voucherSearch.ToDate.Value);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetPaymentsReveivedForReporting(VoucherRecievableSearch voucherSearch)");
                return null;
                throw ex;
            }

        }

        #endregion
    }
}
