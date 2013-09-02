using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using LankaTiles.Common;

namespace LankaTiles.ItemsManagement
{
    public class GroupsDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Add Groups

        public bool AddGroups(Group group)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Groups_Insert);

                db.AddInParameter(dbCommand, "@sGroupCode", DbType.String, group.GroupCode);
                db.AddInParameter(dbCommand, "@sGroupName", DbType.String, group.GroupName);
                db.AddInParameter(dbCommand, "@sDescription", DbType.String, group.Description);
                db.AddInParameter(dbCommand, "@tiItemCount", DbType.Int16, group.ItemCount);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, group.IsActive);

                db.AddOutParameter(dbCommand, "@tiGroupId", DbType.Int16, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int16 newGroupId = Convert.ToInt16(db.GetParameterValue(dbCommand, "@tiGroupId"));
                    if (newGroupId > 0)
                    {
                        group.GroupId = newGroupId;
                        if (this.UpdateGroupItems(group, db, transaction))
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

        #region Update Groups

        public bool UpdateGroups(Group group)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Groups_Update);

                db.AddInParameter(dbCommand, "@tiGroupId", DbType.Int16, group.GroupId);
                db.AddInParameter(dbCommand, "@sGroupName", DbType.String, group.GroupName);
                db.AddInParameter(dbCommand, "@sDescription", DbType.String, group.Description);
                db.AddInParameter(dbCommand, "@tiItemCount", DbType.Int16, group.ItemCount);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, group.IsActive);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdateGroupItems(group, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
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

        #region Update Group Items

        private bool UpdateGroupItems(Group group, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_Groups_InsGroupItems);
                db.AddInParameter(insCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@tiIGroupId", DbType.Int16, group.GroupId);

                DbCommand DelCommand = db.GetStoredProcCommand(Constant.SP_BrandCategories_DelBrandCategories);
                db.AddInParameter(DelCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(DelCommand, "@tiIGroupId", DbType.Int16, group.GroupId);

                db.UpdateDataSet(group.GroupItems, group.GroupItems.Tables[0].TableName, insCommand, null, DelCommand, transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Delete Group

        public bool DeleteGroup(Group group)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_Delete);

                db.AddInParameter(cmd, "@tiGroupId", DbType.Int16, group.GroupId);

                db.ExecuteNonQuery(cmd);
                result = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
                result = false;
            }

            return result;
        }

        #endregion

        #region Get Group By ID

        public bool GetGroupByID(Group group)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_GetByID);

                db.AddInParameter(cmd, "@tiGroupId", DbType.Int16, group.GroupId);

                DataSet ds = db.ExecuteDataSet(cmd);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    group.GroupId = ds.Tables[0].Rows[0]["GroupId"] != DBNull.Value ?
                                         Convert.ToInt16(ds.Tables[0].Rows[0]["GroupId"].ToString()) : Convert.ToInt16("0");

                    group.GroupCode = ds.Tables[0].Rows[0]["GroupCode"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["GroupCode"].ToString()) : string.Empty;

                    group.GroupName = ds.Tables[0].Rows[0]["GroupName"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["GroupName"].ToString()) : string.Empty;

                    group.Description = ds.Tables[0].Rows[0]["Description"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["Description"].ToString()) : string.Empty;

                    group.ItemCount = ds.Tables[0].Rows[0]["ItemCount"] != DBNull.Value ?
                                         Convert.ToInt16(ds.Tables[0].Rows[0]["ItemCount"].ToString()) : Convert.ToInt16("0");

                    group.IsActive = ds.Tables[0].Rows[0]["IsActive"] != DBNull.Value ?
                                         Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]) : false;

                }

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    group.Cost = ds.Tables[1].Rows[0]["TotCost"] != DBNull.Value ?
                                 Convert.ToDecimal(ds.Tables[1].Rows[0]["TotCost"].ToString()) : Convert.ToDecimal("0");

                    group.SellingPrice = ds.Tables[1].Rows[0]["TotSellingPrice"] != DBNull.Value ?
                                         Convert.ToDecimal(ds.Tables[1].Rows[0]["TotSellingPrice"].ToString()) : Convert.ToDecimal("0");

                    group.MinSellingPrice = ds.Tables[1].Rows[0]["TotMinSellingPrice"] != DBNull.Value ?
                                            Convert.ToDecimal(ds.Tables[1].Rows[0]["TotMinSellingPrice"].ToString()) : Convert.ToDecimal("0");

                }
                group.GroupItems = this.GetItemsByGroupId(group);

                group.QuantityInHand = this.CalculateQIH(group);

                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
                result = false;
            }

            return result;
        }

        #endregion

        #region Get Items By GroupId

        public DataSet GetItemsByGroupId(Group group)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_SelItemsByGroupID);

                db.AddInParameter(cmd, "@tiGroupId", DbType.Int16, group.GroupId);

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

        #region Get Items By Group Code

        public DataSet GetItemsByGroupCode(Group group)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_SelItemsByGroupCode);

                db.AddInParameter(cmd, "@sGroupCode", DbType.String, group.GroupCode);

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

        #region Get Next Groups Code

        public string GetNextGroupsCode()
        {
            string nextPOCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_GetNextCode);

                nextPOCode = (string)db.ExecuteScalar(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return nextPOCode;
        }

        #endregion

        #region Get All Groups

        public DataSet GetAllGroups()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_GetAll);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Group By code

        public bool GetGroupByCode(Group group)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Groups_GetByCode);

                db.AddInParameter(cmd, "@sGroupCode", DbType.String, group.GroupCode);

                DataSet ds = db.ExecuteDataSet(cmd);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    group.GroupId = ds.Tables[0].Rows[0]["GroupId"] != DBNull.Value ?
                                         Convert.ToInt16(ds.Tables[0].Rows[0]["GroupId"].ToString()) : Convert.ToInt16("0");

                    group.GroupCode = ds.Tables[0].Rows[0]["GroupCode"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["GroupCode"].ToString()) : string.Empty;

                    group.GroupName = ds.Tables[0].Rows[0]["GroupName"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["GroupName"].ToString()) : string.Empty;

                    group.Description = ds.Tables[0].Rows[0]["Description"] != DBNull.Value ?
                                         Convert.ToString(ds.Tables[0].Rows[0]["Description"].ToString()) : string.Empty;

                    group.ItemCount = ds.Tables[0].Rows[0]["ItemCount"] != DBNull.Value ?
                                         Convert.ToInt16(ds.Tables[0].Rows[0]["ItemCount"].ToString()) : Convert.ToInt16("0");

                    group.IsActive = ds.Tables[0].Rows[0]["IsActive"] != DBNull.Value ?
                                         Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]) : false;

                }

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    group.Cost = ds.Tables[1].Rows[0]["TotCost"] != DBNull.Value ?
                                 Convert.ToDecimal(ds.Tables[1].Rows[0]["TotCost"].ToString()) : Convert.ToDecimal("0");

                    group.SellingPrice = ds.Tables[1].Rows[0]["TotSellingPrice"] != DBNull.Value ?
                                         Convert.ToDecimal(ds.Tables[1].Rows[0]["TotSellingPrice"].ToString()) : Convert.ToDecimal("0");

                    group.MinSellingPrice = ds.Tables[1].Rows[0]["TotMinSellingPrice"] != DBNull.Value ?
                                            Convert.ToDecimal(ds.Tables[1].Rows[0]["TotMinSellingPrice"].ToString()) : Convert.ToDecimal("0");

                }
                group.GroupItems = this.GetItemsByGroupCode(group);

                group.QuantityInHand = this.CalculateQIH(group);

                result = true;


            }
            catch (System.Exception ex)
            {
                throw ex;
                result = false;
            }

            return result;
        }

        #endregion

        #region Calculate QIH

        private Int64 CalculateQIH(Group group)
        {
            try
            {

                Int64 minQTY = 0;

                DataSet ds = group.GroupItems;

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    Int64 rowQTY =0;
                    foreach (DataRow row in group.GroupItems.Tables[0].Rows)
                    {
                        rowQTY = row["BillableQty"] != DBNull.Value ? Convert.ToInt64(row["BillableQty"].ToString()) :
                                      Convert.ToInt64("0");

                        if (rowQTY == 0)
                        {
                            minQTY = 0;
                            return minQTY;
                        }
                        else if (rowQTY > 0 && ( (rowQTY < minQTY) || minQTY ==0 ))
                        {
                            minQTY = rowQTY;
                        }
                        
                    }
                }

                return minQTY;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
