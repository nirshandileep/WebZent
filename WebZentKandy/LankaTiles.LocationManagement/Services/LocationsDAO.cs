using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;

namespace LankaTiles.LocationManagement
{
    public class LocationsDAO
    {
        #region Add Location

        public bool AddLocation(Location objLocation)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_InsertLocation);

                db.AddInParameter(dbCommand, "@sBranchCode", DbType.String, objLocation.BranchCode);
                db.AddInParameter(dbCommand, "@sBranchName", DbType.String, objLocation.BranchName);
                db.AddInParameter(dbCommand, "@sAddress1", DbType.String, objLocation.Address1);
                db.AddInParameter(dbCommand, "@sAddress2", DbType.String, objLocation.Address2);
                db.AddInParameter(dbCommand, "@sTelephone", DbType.String, objLocation.Telephone);
                db.AddInParameter(dbCommand, "@sContactName", DbType.String, objLocation.ContactName);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, objLocation.IsActive);
                db.AddInParameter(dbCommand, "@sInvCode", DbType.String, objLocation.InvPrefix);

                db.AddOutParameter(dbCommand, "@iBranchId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    int newLocID = (int)db.GetParameterValue(dbCommand, "@iBranchId");
                    if (newLocID > 0)
                    {
                        objLocation.BranchId = newLocID;
                        result = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public bool InsertLocation()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Update Location

        public bool UpdateLocation(Location objLocation)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_Update);

                db.AddInParameter(dbCommand, "@iBranchId", DbType.Int32, objLocation.BranchId);
                db.AddInParameter(dbCommand, "@sBranchCode", DbType.String, objLocation.BranchCode);
                db.AddInParameter(dbCommand, "@sBranchName", DbType.String, objLocation.BranchName);
                db.AddInParameter(dbCommand, "@sAddress1", DbType.String, objLocation.Address1);
                db.AddInParameter(dbCommand, "@sAddress2", DbType.String, objLocation.Address2);
                db.AddInParameter(dbCommand, "@sTelephone", DbType.String, objLocation.Telephone);
                db.AddInParameter(dbCommand, "@sContactName", DbType.String, objLocation.ContactName);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, objLocation.IsActive);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    result = true;
                    
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public bool InsertLocation()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Delete Location

        public bool DeleteLocation(Location objLocation)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_Delete);

                db.AddInParameter(dbCommand, "@iBranchId", DbType.Int32, objLocation.BranchId);
                                
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    result = true;
                    
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public bool InsertLocation()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region GetAllBranches
        public DataSet GetAllBranches()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_GetAllLocations);

                return db.ExecuteDataSet(dbCommand);
            }
            catch(System.Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region Get Location By ID
        public bool GetLocationByID(Location objLocation)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_GetLocationByID);

                db.AddInParameter(dbCommand, "@iBranchId", DbType.Int32, objLocation.BranchId);

                IDataReader reader = db.ExecuteReader(dbCommand);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        objLocation.BranchId = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"].ToString()) : 0;
                        objLocation.BranchCode = reader["BranchCode"].ToString();
                        objLocation.BranchName = reader["BranchName"].ToString();
                        objLocation.Address1 = reader["Address1"].ToString();
                        objLocation.Address2 = reader["Address2"].ToString();
                        objLocation.Telephone = reader["Telephone"].ToString();
                        objLocation.ContactName = reader["ContactName"].ToString();
                        objLocation.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                        objLocation.InvPrefix = reader["InvPrefix"] != DBNull.Value ? reader["InvPrefix"].ToString().ToString() : string.Empty;

                        result = true;
                    }
                }

            }
            catch(System.Exception ex)
            {
                throw ex;
            }
            return result;

        }

        #endregion

        #region Is Branch Code Exists

        public bool IsBranchCodeExists(string branchCode)
        {
            bool isExists = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_IsLocationCodeExists);

                db.AddInParameter(dbCommand, "@sBranchCode", DbType.String, branchCode);
                db.AddOutParameter(dbCommand, "@bIsExists", DbType.Boolean, 1);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    isExists = (bool)db.GetParameterValue(dbCommand, "@bIsExists");
                }
                
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", "User" + Constant.Error_Seperator + "public bool IsBranchCodeExists(Location objLocation)");
                throw ex;
            }
            return isExists;
        }

        #endregion

        #region Search Location

        public DataSet SearchLocation(Location objLocation)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_SearchLocations);

                db.AddInParameter(dbCommand, "@sBranchCode", DbType.String, objLocation.BranchCode);
                db.AddInParameter(dbCommand, "@sBranchName", DbType.String, objLocation.BranchName);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, objLocation.IsActive);

                return db.ExecuteDataSet(dbCommand);
                
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Location Stocks By Branch

        public DataSet GetLocationStocksByBranch(LocationStocks locationStocks)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_GetLocationStocksByBranch);

                db.AddInParameter(dbCommand, "@iBranchID", DbType.Int32, locationStocks.BranchId);
                
                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Location Stocks By Branch And Item

        public DataSet GetLocationStocksByBranchAndItem(LocationStocks locationStocks)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_GetLocationStocksByBranchAndItem);

                db.AddInParameter(dbCommand, "@iBranchID", DbType.Int32, locationStocks.BranchId);
                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, locationStocks.ItemId);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Location Stocks By Item

        public DataSet GetLocationStocksByItem(LocationStocks locationStocks)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Location_GetLocationStocksByItem);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, locationStocks.ItemId);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion
    }
}
