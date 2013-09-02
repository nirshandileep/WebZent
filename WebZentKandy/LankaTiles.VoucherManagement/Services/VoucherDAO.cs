using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;
using System.Data;
using LankaTiles.VoucherManagement;

namespace LankaTiles.VoucherManagement
{
    public class VoucherDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Get Next Voucher Code

        public string GetNextVoucherCode()
        {
            string nextVoucherCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_GetNextCode);

                nextVoucherCode = (string)db.ExecuteScalar(cmd);

            }
            catch (System.Exception ex)
            {
                nextVoucherCode = string.Empty;
                throw ex;
            }
            return nextVoucherCode;
        }

        #endregion

        #region Add Voucher

        public bool AddVoucher(Voucher voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_InsVoucher);

                db.AddInParameter(cmd, "@sVoucherCode", DbType.String, voucher.VoucherCode);

                if (voucher.ChqId.HasValue)
                {
                    db.AddInParameter(cmd, "@iChqId", DbType.Int32, voucher.ChqId.Value);
                }
                else
                {
                    db.AddInParameter(cmd, "@iChqId", DbType.Int32, DBNull.Value);
                }
                
                db.AddInParameter(cmd, "@iCreatedBy", DbType.Int32, voucher.CreatedBy);
                db.AddInParameter(cmd, "@sChequeNumber", DbType.String, voucher.ChequeNumber);
                db.AddInParameter(cmd, "@dtChequeDate", DbType.DateTime, voucher.ChequeDate);
                db.AddInParameter(cmd, "@sBank", DbType.String, voucher.Bank);
                db.AddInParameter(cmd, "@sBankBranch", DbType.String, voucher.BankBranch);
                db.AddInParameter(cmd, "@sDescription", DbType.String, voucher.Description);
                db.AddInParameter(cmd, "@mTotalAmount", DbType.Currency, voucher.TotalAmount);
                db.AddInParameter(cmd, "@iVoucherTypeID", DbType.Int32, voucher.VoucherTypeID);
                db.AddInParameter(cmd, "@iPaymentTypeID", DbType.Int32, voucher.PaymentTypeID);
                db.AddInParameter(cmd, "@iSupplierID", DbType.Int32, voucher.SupplierId);
                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, voucher.CustomerID);
                db.AddInParameter(cmd, "@dtPaymentDate", DbType.DateTime, voucher.PaymentDate);//added 05/11/2011
                db.AddInParameter(cmd, "@iAccountID", DbType.Int32, voucher.AccountID);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, voucher.BranchId);//added 18/03/2012

                db.AddOutParameter(cmd, "@inewVoucherID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    Int32 newVoucherID = 0;
                    Int32.TryParse(db.GetParameterValue(cmd, "@inewVoucherID").ToString().Trim(), out newVoucherID);

                    if (newVoucherID > 0)
                    {
                        voucher.VoucherID = newVoucherID;

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

        #region Search Voucher
        public DataSet SearchVoucher(VoucherSearch voucher)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_SearchVouchers);

                if (voucher.ChequeDateFrom == DateTime.MinValue)
                {
                    db.AddInParameter(cmd, "@dtChequeDateFrom", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmd, "@dtChequeDateFrom", DbType.DateTime, voucher.ChequeDateFrom);
                }

                if (voucher.ChequeDateTo == DateTime.MinValue)
                {
                    db.AddInParameter(cmd, "@dtChequeDateTo", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmd, "@dtChequeDateTo", DbType.DateTime, voucher.ChequeDateTo);
                }

                db.AddInParameter(cmd, "@sChequeNumberFrom", DbType.String, voucher.ChequeNumberFrom);
                db.AddInParameter(cmd, "@sChequeNumberTo", DbType.String, voucher.ChequeNumberTo);

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

        #region Update Voucher Details

        private bool UpdateVoucherDetails(Voucher voucher, Database db, DbTransaction pTransaction)
        {
            bool result = false;
            try
            {
                DbCommand insCmd = db.GetStoredProcCommand(Constant.SP_Voucher_Insert_Voucher_Details);

                db.AddInParameter(insCmd, "@sVoucherDetails", DbType.String, "VoucherDetails", DataRowVersion.Current);
                db.AddInParameter(insCmd, "@iVoucherID", DbType.Int32, voucher.VoucherID);
                db.AddInParameter(insCmd, "@iPOId", DbType.Int32, "POId", DataRowVersion.Current);
                db.AddInParameter(insCmd, "@iGRNId", DbType.Int32, "GRNId", DataRowVersion.Current);
                db.AddInParameter(insCmd, "@mAmount", DbType.Currency, "Amount", DataRowVersion.Current);

                int rowcount = 0;
                rowcount = db.UpdateDataSet(voucher.DsVoucherDetails, voucher.DsVoucherDetails.Tables[0].TableName, insCmd, null, null, pTransaction);

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

        public bool GetVoucherByID(Voucher voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_GetByID);

                db.AddInParameter(cmd, "@iVoucherID", DbType.Int32, voucher.VoucherID);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        voucher.VoucherID = reader["VoucherID"] != DBNull.Value ? Convert.ToInt32(reader["VoucherID"].ToString()) : 0;
                                            
                        voucher.VoucherCode = reader["VoucherCode"].ToString();

                        voucher.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;

                        voucher.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) :
                                                DateTime.MinValue;

                        voucher.ChequeNumber = reader["ChequeNumber"].ToString();

                        voucher.ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"].ToString()) :
                                                DateTime.MinValue;

                        voucher.Bank = reader["Bank"].ToString();

                        voucher.BankBranch = reader["BankBranch"].ToString();

                        voucher.Description = reader["Description"].ToString();

                        voucher.TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"].ToString()) :
                                            Convert.ToDecimal("0");
                       
                        voucher.VoucherTypeID = reader["VoucherTypeID"] != DBNull.Value ? Convert.ToInt32(reader["VoucherTypeID"].ToString()) :
                                           Convert.ToInt32("0");
                        voucher.PaymentTypeID = reader["PaymentTypeID"] != DBNull.Value ? Convert.ToInt32(reader["PaymentTypeID"].ToString()) :
                                          Convert.ToInt32("0");

                        voucher.SupplierId = reader["SupplierId"] != DBNull.Value ? Convert.ToInt32(reader["SupplierId"].ToString()) :
                                          Convert.ToInt32("0");

                        voucher.CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"].ToString()) :
                                          Convert.ToInt32("0");

                        //05/11/2011
                        if (reader["VoucherPaymentDate"] != DBNull.Value)
                        {
                            voucher.PaymentDate = Convert.ToDateTime(reader["VoucherPaymentDate"].ToString());
                        }
                        else
                        {
                            voucher.PaymentDate = null;
                        }

                        //14/11/2011
                        if (reader["AccountID"] != DBNull.Value)
                        {
                            voucher.AccountID = Int32.Parse(reader["AccountID"].ToString());
                        }
                        else
                        {
                            voucher.AccountID = null;
                        }

                        //19/03/2012
                        if (reader["BranchId"] != DBNull.Value)
                        {
                            voucher.BranchId = Int32.Parse(reader["BranchId"].ToString());
                        }
                        else
                        {
                            voucher.BranchId = null;
                        }

                        //Todo: load other voucher details

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

        #region Get Voucher By Code

        public bool GetVoucherByCode(Voucher voucher)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_GetByCode);

                db.AddInParameter(cmd, "@sVoucherCode", DbType.String, voucher.VoucherCode);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        voucher.VoucherID = reader["VoucherID"] != DBNull.Value ? Convert.ToInt32(reader["VoucherID"].ToString()) : 0;

                        voucher.VoucherCode = reader["VoucherCode"].ToString();

                        voucher.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;

                        voucher.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) :
                                                DateTime.MinValue;

                        voucher.ChequeNumber = reader["ChequeNumber"].ToString();

                        voucher.ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"].ToString()) :
                                                DateTime.MinValue;

                        voucher.Bank = reader["Bank"].ToString();

                        voucher.BankBranch = reader["BankBranch"].ToString();

                        voucher.Description = reader["Description"].ToString();

                        voucher.TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"].ToString()) :
                                            Convert.ToDecimal("0");

                        voucher.VoucherTypeID = reader["VoucherTypeID"] != DBNull.Value ? Convert.ToInt32(reader["VoucherTypeID"].ToString()) :
                                           Convert.ToInt32("0");
                        voucher.PaymentTypeID = reader["PaymentTypeID"] != DBNull.Value ? Convert.ToInt32(reader["PaymentTypeID"].ToString()) :
                                          Convert.ToInt32("0");

                        voucher.SupplierId = reader["SupplierId"] != DBNull.Value ? Convert.ToInt32(reader["SupplierId"].ToString()) :
                                          Convert.ToInt32("0");

                        voucher.CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"].ToString()) :
                                          Convert.ToInt32("0");

                        //05/11/2011
                        if (reader["VoucherPaymentDate"] != DBNull.Value)
                        {
                            voucher.PaymentDate = Convert.ToDateTime(reader["VoucherPaymentDate"].ToString());
                        }
                        else
                        {
                            voucher.PaymentDate = null;
                        }

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

        #region Get All

        public DataSet GetAll()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_GetAll);

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

        #region Get All Account Types

        public DataSet GetAllAccountTypes()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_GetAll_AccountTypes);

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

        #region Add Account Types

        public bool AddAccountTypes(String accountName)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_InsAccountType);

                db.AddInParameter(cmd, "@sAccountName", DbType.String, accountName);
                db.AddOutParameter(cmd, "@iAccountID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    Int32 AccountID = 0;
                    Int32.TryParse(db.GetParameterValue(cmd, "@iAccountID").ToString().Trim(), out AccountID);

                    if (AccountID > 0)
                    {
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

        #region Get Voucher Details By ID

        public DataSet GetVoucherDetailsByID(Voucher voucher)
         {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Get_Voucher_Details_By_VoucherID);

                db.AddInParameter(cmd, "@iVoucherID", DbType.Int32, voucher.VoucherID);
                
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

        #region Get Expence details for reporting

        public DataSet GetExpencesForReportingByDateRange(VoucherSearch voucher)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Report_ExpencesByDateRange);

                db.AddInParameter(cmd, "@dFromDate", DbType.DateTime, voucher.FromDate.Value);
                db.AddInParameter(cmd, "@dToDate", DbType.DateTime, voucher.ToDate.Value);

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

        #region Get Expence details for reporting

        public DataSet GetDaybookDetailsForReporting(VoucherSearch voucher)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Voucher_Report_DayBookByDateRange);

                db.AddInParameter(cmd, "@dFromDate", DbType.DateTime, voucher.FromDate.Value);
                db.AddInParameter(cmd, "@dToDate", DbType.DateTime, voucher.ToDate.Value);

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
    }
}
