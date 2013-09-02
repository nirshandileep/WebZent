using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;
using System.Data;

namespace LankaTiles.CustomerManagement
{
    public class CustomerDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add Customer

        public bool AddCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Insert);

                db.AddInParameter(cmd, "@sCustomerCode", DbType.String, customer.CustomerCode);
                db.AddInParameter(cmd, "@sCus_Name", DbType.String, customer.Cus_Name);
                db.AddInParameter(cmd, "@sCus_Address", DbType.String, customer.Cus_Address);
                db.AddInParameter(cmd, "@sCus_Tel", DbType.String, customer.Cus_Tel);
                db.AddInParameter(cmd, "@sCus_Contact", DbType.String, customer.Cus_Contact);
                db.AddInParameter(cmd, "@bIsActive", DbType.Boolean, customer.IsActive);
                db.AddInParameter(cmd, "@bIsCreditCustomer", DbType.Boolean, customer.IsCreditCustomer);

                db.AddOutParameter(cmd, "@iCustomerID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    Int32 newCustomerID = Convert.ToInt32(db.GetParameterValue(cmd, "@iCustomerID"));

                    if (newCustomerID > 0)
                    {
                        customer.CustomerID = newCustomerID;

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

        #region Update Customer

        public bool UpdateCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Update);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, customer.CustomerID);
                db.AddInParameter(cmd, "@sCustomerCode", DbType.String, customer.CustomerCode);
                db.AddInParameter(cmd, "@sCus_Name", DbType.String, customer.Cus_Name);
                db.AddInParameter(cmd, "@sCus_Address", DbType.String, customer.Cus_Address);
                db.AddInParameter(cmd, "@sCus_Tel", DbType.String, customer.Cus_Tel);
                db.AddInParameter(cmd, "@sCus_Contact", DbType.String, customer.Cus_Contact);
                db.AddInParameter(cmd, "@bIsActive", DbType.Boolean, customer.IsActive);
                db.AddInParameter(cmd, "@bIsCreditCustomer", DbType.Boolean, customer.IsCreditCustomer);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = true;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
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

        #region Delete Customer

        public bool DeleteCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Delete);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, customer.CustomerID);
               
                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = true;
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

        #region Get Next Customer Code

        public string GetNextCustomerCode()
        {
            string strCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_GetNextCode);

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

        #region Get All Customers

        public DataSet GetAllCustomers()
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_GetAll);

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

        #region Get Customers By ID

        public bool GetCustomerByID(Customer customer)
        {
            bool rslt = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Get_ByID);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, customer.CustomerID);
                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        customer.CustomerID     = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"].ToString()) : 0;
                        customer.CustomerCode   = reader["CustomerCode"] != DBNull.Value ? Convert.ToString(reader["CustomerCode"].ToString()) : string.Empty;
                        customer.Cus_Name       = reader["Cus_Name"] != DBNull.Value ? Convert.ToString(reader["Cus_Name"].ToString()) : string.Empty;
                        customer.Cus_Address    = reader["Cus_Address"] != DBNull.Value ? Convert.ToString(reader["Cus_Address"].ToString()) : string.Empty;
                        customer.Cus_Tel        = reader["Cus_Tel"] != DBNull.Value ? Convert.ToString(reader["Cus_Tel"].ToString()) : string.Empty;
                        customer.Cus_Contact    = reader["Cus_Contact"] != DBNull.Value ? Convert.ToString(reader["Cus_Contact"].ToString()) : string.Empty;
                        customer.IsActive       = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                        customer.IsCreditCustomer = reader["IsCreditCustomer"] != DBNull.Value ? Convert.ToBoolean(reader["IsCreditCustomer"].ToString()) : false;
                        customer.Cus_CreditTotal = reader["Cus_CreditTotal"] != DBNull.Value ? Decimal.Parse(reader["Cus_CreditTotal"].ToString()) : 0;

                        customer.GRNIds         = reader["GRNIds"] != DBNull.Value ? reader["GRNIds"].ToString() : string.Empty;

                        rslt = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                rslt = false;
                throw ex;
            }
            return rslt;

        }

        #endregion

        #region Search Customers

        public DataSet SearchCustomers(CustomerSearch customerSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Search);

                db.AddInParameter(cmd, "@sCustomerCode", DbType.String, customerSearch.CustomerCode);
                db.AddInParameter(cmd, "@sCus_Name", DbType.String, customerSearch.Cus_Name);
                db.AddInParameter(cmd, "@iIsActiveOption", DbType.Int32, customerSearch.IsActiveOption);
                db.AddInParameter(cmd, "@iIsCreditCustomerOption", DbType.Int32, customerSearch.IsCreditCustomerOption);

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

        #region Update Customers From Grid
        
        public bool UpdateAllCustomers(DataSet dsCustomers)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Customers_UpdateAll_ByID);
                db.AddInParameter(updCommand, "@iCustomerID", DbType.Int32, "CustomerID", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mCus_CreditTotal", DbType.Currency, "Cus_CreditTotal", DataRowVersion.Current);

                db.UpdateDataSet(dsCustomers, dsCustomers.Tables[0].TableName, null, updCommand, null, transaction);
                result = true;
                transaction.Commit();

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

        public DataSet GetTransactionHistoryByCustomer(CustomerSearch custSearch)
        {
            DataSet ds;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Customers_Report_TransactionsAll);

                db.AddInParameter(cmd, "@iCustomerID", DbType.Int32, custSearch.CustId);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;
        }


    }
}
