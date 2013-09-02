using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;

namespace LankaTiles.SupplierManagement
{
    public class SupplierDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add Suppliers

        public bool AddSuppliers(Supplier supplier)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_Insert);

                db.AddInParameter(dbCommand, "@sSup_Code", DbType.String, supplier.Sup_Code);
                db.AddInParameter(dbCommand, "@sSupplierName", DbType.String, supplier.SupplierName);
                db.AddInParameter(dbCommand, "@sSupplierPhone", DbType.String, supplier.SupplierPhone);
                db.AddInParameter(dbCommand, "@sSupplierContact", DbType.String, supplier.SupplierContact);
                db.AddInParameter(dbCommand, "@sSupplierAddress", DbType.String, supplier.SupplierAddress);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, supplier.IsActive);
                db.AddInParameter(dbCommand, "@iCreatedUser", DbType.Int32, supplier.CreatedUser);
                db.AddInParameter(dbCommand, "@iModifiedBy", DbType.Int32, supplier.ModifiedBy);

                db.AddOutParameter(dbCommand, "@iSupId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    int newSupplierID = (int)db.GetParameterValue(dbCommand, "@iSupId");

                    if (newSupplierID > 0)
                    {
                        success = true;
                        supplier.SupId = newSupplierID;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItems(Item item)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Update Suppliers

        public bool UpdateSuppliers(Supplier supplier)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_Update);

                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, supplier.SupId);
                db.AddInParameter(dbCommand, "@sSupplierName", DbType.String, supplier.SupplierName);
                db.AddInParameter(dbCommand, "@sSupplierPhone", DbType.String, supplier.SupplierPhone);
                db.AddInParameter(dbCommand, "@sSupplierContact", DbType.String, supplier.SupplierContact);
                db.AddInParameter(dbCommand, "@sSupplierAddress", DbType.String, supplier.SupplierAddress);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, supplier.IsActive);
                db.AddInParameter(dbCommand, "@iModifiedBy", DbType.Int32, supplier.ModifiedBy);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    success = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItems(Item item)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Delete Suppliers

        public bool DeleteSuppliers(Supplier supplier)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_Delete);

                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, supplier.SupId);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    success = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItems(Item item)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Get Supplier By ID

        public bool GetSupplierByID(Supplier supplier)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_GetByID);

                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, supplier.SupId);

                IDataReader reader = db.ExecuteReader(dbCommand);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        supplier.SupId = reader["SupId"] != DBNull.Value ? Convert.ToInt32(reader["SupId"].ToString()) : 0;
                        supplier.Sup_Code = reader["Sup_Code"].ToString();
                        supplier.SupplierName = reader["SupplierName"].ToString();
                        supplier.SupplierPhone = reader["SupplierPhone"].ToString();
                        supplier.SupplierContact = reader["SupplierContact"].ToString();
                        supplier.SupplierAddress = reader["SupplierAddress"].ToString();
                        supplier.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                        supplier.CreatedUser = reader["CreatedUser"] != DBNull.Value ? Convert.ToInt32(reader["CreatedUser"].ToString()) : 0;
                        supplier.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) : DateTime.Now;
                        supplier.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"].ToString()) : 0;
                        supplier.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"].ToString()) : DateTime.Now;
                        supplier.CreditAmmount = reader["CreditAmmount"] != DBNull.Value ? Convert.ToDecimal(reader["CreditAmmount"].ToString()) : 0;
                        success = true;
                    }
                }


                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    success = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItems(Item item)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region GetAllSuppliers

        public DataSet GetAllSuppliers()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_GetAll);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Next Supplier Code

        public string GetNextSupplierCode()
        {
            string newSupCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_GetNextCode);

                newSupCode = (string)db.ExecuteScalar(dbCommand);
            }
            catch (System.Exception ex)
            {
                throw ex;

            }

            return newSupCode;
        }

        #endregion

        #region Search Suppliers

        public DataSet SearchSuppliers(SupplierSearch supplierSearch)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Supplier_Search);

                db.AddInParameter(dbCommand, "@sSup_Code", DbType.String, supplierSearch.Sup_Code);
                db.AddInParameter(dbCommand, "@sSupplierName", DbType.String, supplierSearch.SupplierName);
                db.AddInParameter(dbCommand, "@iOption", DbType.Int32, supplierSearch.Option);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItems(Item item)");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Update Suppliers From Grid

        public bool UpdateAllSuppliers(DataSet dsSuppliers)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Suppliers_UpdateAll_ByID);
                db.AddInParameter(updCommand, "@iSupplierID", DbType.Int32, "SupId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mSup_CreditTotal", DbType.Currency, "CreditAmmount", DataRowVersion.Current);

                db.UpdateDataSet(dsSuppliers, dsSuppliers.Tables[0].TableName, null, updCommand, null, transaction);
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

        /// <summary>
        /// Return supplier related expenses by date range and supplier id
        /// </summary>
        /// <param name="searchCriteria">Set FromDate, ToDate and SuppleirId</param>
        /// <returns></returns>
        public DataSet SelectSupplierRelatedExpences(SupplierSearch searchCriteria)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Suppliers_Report_SuppliersTransctionsById);

                db.AddInParameter(dbCommand, "@nSupplierId", DbType.String, searchCriteria.Sup_Id);
                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, searchCriteria.FromDate);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, searchCriteria.ToDate);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet SelectSupplierRelatedExpences(SupplierSearch searchCriteria)");
                return null;
                throw ex;
            }
        }
    }
}
