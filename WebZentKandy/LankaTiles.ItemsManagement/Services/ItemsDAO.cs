using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;
using System.Data;

namespace LankaTiles.ItemsManagement
{
    public class ItemsDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Get Next Item Code

        public string GetNextItemCode()
        {
            string nextCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Item_GetNextItemCode);

                nextCode = (string)db.ExecuteScalar(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public string GetNextItemCode()");
                throw ex;
            }
            return nextCode;

        }

        #endregion

        #region Delete Item

        public bool DeleteItem(Item item)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Delete);

                db.AddInParameter(dbCommand, "@iItemId", System.Data.DbType.Int32, item.ItemId);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    success = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool DeleteItem()");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Get All Items

        public DataSet GetAllItems()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_GetAll);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllItems()");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get Items By ID

        public bool GetItemsByID(Item item)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_GetByID);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, item.ItemId);

                IDataReader reader = db.ExecuteReader(dbCommand);

                while (reader.Read())
                {
                    item.ItemId = reader["ItemId"] != DBNull.Value ? Convert.ToInt32(reader["ItemId"].ToString()) : 0;
                    item.ItemCode = reader["ItemCode"].ToString();
                    item.ItemDescription = reader["ItemDescription"].ToString();
                    item.Cost = reader["Cost"] != DBNull.Value ? Convert.ToDecimal(reader["Cost"].ToString().Trim()) : (decimal)0.00;
                    item.SellingPrice = reader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["SellingPrice"].ToString().Trim()) : (decimal)0.00;
                    item.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                    item.CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"].ToString()) : 0;
                    item.UpdatedUser = reader["UpdatedUser"] != DBNull.Value ? Convert.ToInt32(reader["UpdatedUser"].ToString()) : 0;
                    item.UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"].ToString()) : DateTime.MinValue;
                    item.QuantityInHand = reader["QuantityInHand"] != DBNull.Value ? Convert.ToInt64(reader["QuantityInHand"].ToString()) : 0;
                    item.FreezedQty = reader["FreezedQty"] != DBNull.Value ? Convert.ToInt64(reader["FreezedQty"].ToString()) : 0;
                    item.ROL = reader["ROL"] != DBNull.Value ? Convert.ToInt32(reader["ROL"].ToString()) : 0;
                    item.BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"].ToString()) : 0;
                    item.MinSellingPrice = reader["MinSellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["MinSellingPrice"].ToString()) : (decimal)0.00;
                    item.InvoicedQty = reader["InvoicedQty"] != DBNull.Value ? Convert.ToInt32(reader["InvoicedQty"].ToString()) : 0;
                    item.BrandName = reader["BrandName"].ToString();
                    item.CategoryName = reader["CategoryType"].ToString();
                    item.IType.TypeId = reader["TypeId"] != DBNull.Value ? Convert.ToInt32(reader["TypeId"].ToString()) : 0;
                    item.IType.TypeName = reader["TypeName"] != DBNull.Value ? reader["TypeName"].ToString().Trim() : string.Empty;
                    success = true;
                }


            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemsByID(Item item)");
                throw ex;
            }
            return success;
        }

        #endregion

        #region Get Items By Item Code

        public bool GetItemsByItemCode(Item item)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_GetByItemCode);

                db.AddInParameter(dbCommand, "@sItemCode", DbType.String, item.ItemCode);

                IDataReader reader = db.ExecuteReader(dbCommand);

                while (reader.Read())
                {
                    item.ItemId = reader["ItemId"] != DBNull.Value ? Convert.ToInt32(reader["ItemId"].ToString()) : 0;
                    item.ItemCode = reader["ItemCode"].ToString();
                    item.ItemDescription = reader["ItemDescription"].ToString();
                    item.Cost = reader["Cost"] != DBNull.Value ? Convert.ToDecimal(reader["Cost"].ToString().Trim()) : (decimal)0.00;
                    item.SellingPrice = reader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["SellingPrice"].ToString().Trim()) : (decimal)0.00;
                    item.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                    item.CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"].ToString()) : 0;
                    item.UpdatedUser = reader["UpdatedUser"] != DBNull.Value ? Convert.ToInt32(reader["UpdatedUser"].ToString()) : 0;
                    item.UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"].ToString()) : DateTime.MinValue;
                    item.QuantityInHand = reader["QuantityInHand"] != DBNull.Value ? Convert.ToInt64(reader["QuantityInHand"].ToString()) : 0;
                    item.FreezedQty = reader["FreezedQty"] != DBNull.Value ? Convert.ToInt64(reader["FreezedQty"].ToString()) : 0;
                    item.ROL = reader["ROL"] != DBNull.Value ? Convert.ToInt32(reader["ROL"].ToString()) : 0;
                    item.BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"].ToString()) : 0;
                    item.BrandName = reader["BrandName"].ToString();
                    item.MinSellingPrice = reader["MinSellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["MinSellingPrice"].ToString()) : (decimal)0.00;
                    item.InvoicedQty = reader["InvoicedQty"] != DBNull.Value ? Convert.ToInt32(reader["InvoicedQty"].ToString()) : 0;
                    item.CategoryName = reader["CategoryType"].ToString();
                    item.IType.TypeId = reader["TypeId"] != DBNull.Value ? Convert.ToInt32(reader["TypeId"].ToString()) : 0;
                    item.IType.TypeName = reader["TypeName"] != DBNull.Value ? reader["TypeName"].ToString().Trim() : string.Empty;
                    success = true;
                }


            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemsByID(Item item)");
                throw ex;
            }
            return success;
        }

        #endregion

        #region Search Items

        public DataSet SearchItems(ItemsSearch itemsSearch)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Search);

                db.AddInParameter(dbCommand, "@sItemCode", DbType.String, itemsSearch.ItemCode);
                db.AddInParameter(dbCommand, "@sItemDescription", DbType.String, itemsSearch.ItemDescription);
                db.AddInParameter(dbCommand, "@mSellingPrice", DbType.Currency, itemsSearch.SellingPrice);
                db.AddInParameter(dbCommand, "@iOption", DbType.Int32, itemsSearch.Option);
                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, itemsSearch.SupId);
                db.AddInParameter(dbCommand, "@iCategoryId", DbType.Int32, itemsSearch.CategoryId);
                db.AddInParameter(dbCommand, "@biQuantityInHand", DbType.Int64, itemsSearch.QuantityInHand);
                db.AddInParameter(dbCommand, "@iROL", DbType.Int32, itemsSearch.ROL);
                db.AddInParameter(dbCommand, "@iBranchId", DbType.Int32, itemsSearch.BranchID);
                db.AddInParameter(dbCommand, "@iBrandId", DbType.Int32, itemsSearch.BrandID);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet SearchItems(Item item)");
                return null;
                throw ex;
            }

        }

        #endregion

        #region Add Items

        public bool AddItems(Item item)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Insert);

                db.AddInParameter(dbCommand, "@sItemCode", DbType.String, item.ItemCode);
                db.AddInParameter(dbCommand, "@sItemDescription", DbType.String, item.ItemDescription);
                db.AddInParameter(dbCommand, "@mCost", DbType.Currency, item.Cost);
                db.AddInParameter(dbCommand, "@mSellingPrice", DbType.Currency, item.SellingPrice);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, item.IsActive);
                db.AddInParameter(dbCommand, "@iCategoryId", DbType.Int32, item.CategoryId);
                db.AddInParameter(dbCommand, "@iUpdatedUser", DbType.Int32, item.UpdatedUser);
                db.AddInParameter(dbCommand, "@biQuantityInHand", DbType.Int64, item.QuantityInHand);
                db.AddInParameter(dbCommand, "@biFreezedQty", DbType.Int64, item.FreezedQty);
                db.AddInParameter(dbCommand, "@iROL", DbType.Int32, item.ROL);
                db.AddInParameter(dbCommand, "@iBrandId", DbType.Int32, item.BrandId);
                db.AddInParameter(dbCommand, "@mMinSellingPrice", DbType.Currency, item.MinSellingPrice);
                db.AddInParameter(dbCommand, "@iInvoicedQty", DbType.Int32, item.InvoicedQty);

                if (item.IType.TypeId == -1)
                {
                    db.AddInParameter(dbCommand, "@iTypeId", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iTypeId", DbType.Int32, item.IType.TypeId);
                }

                db.AddOutParameter(dbCommand, "@iItemId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    int newItemID = (int)db.GetParameterValue(dbCommand, "@iItemId");

                    if (newItemID > 0)
                    {
                        success = true;
                        item.ItemId = newItemID;
                    }
                }

                if (success)
                {
                    ItemHistory itemHistory = new ItemHistory(item);
                    itemHistory.HistoryTypeId = Structures.ItemHistoryTypes.CreateItem;
                    AddItemsHistory(itemHistory, null);
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

        #region Update Items

        public bool UpdateItems(Item item)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Update);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, item.ItemId);
                db.AddInParameter(dbCommand, "@sItemDescription", DbType.String, item.ItemDescription);
                db.AddInParameter(dbCommand, "@mCost", DbType.Currency, item.Cost);
                db.AddInParameter(dbCommand, "@mSellingPrice", DbType.Currency, item.SellingPrice);
                db.AddInParameter(dbCommand, "@bIsActive", DbType.Boolean, item.IsActive);
                db.AddInParameter(dbCommand, "@iCategoryId", DbType.Int32, item.CategoryId);
                db.AddInParameter(dbCommand, "@iUpdatedUser", DbType.Int32, item.UpdatedUser);
                db.AddInParameter(dbCommand, "@biQuantityInHand", DbType.Int64, item.QuantityInHand);
                db.AddInParameter(dbCommand, "@biFreezedQty", DbType.Int64, item.FreezedQty);
                db.AddInParameter(dbCommand, "@iROL", DbType.Int32, item.ROL);
                db.AddInParameter(dbCommand, "@iBrandId", DbType.Int32, item.BrandId);
                db.AddInParameter(dbCommand, "@mMinSellingPrice", DbType.Currency, item.MinSellingPrice);
                db.AddInParameter(dbCommand, "@iInvoicedQty", DbType.Int32, item.InvoicedQty);

                if (item.IType.TypeId == -1)
                {
                    db.AddInParameter(dbCommand, "@iTypeId", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iTypeId", DbType.Int32, item.IType.TypeId);
                }

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    success = true;
                }

                if (success)
                {
                    ItemHistory itemHistory = new ItemHistory(item);
                    itemHistory.HistoryTypeId = Structures.ItemHistoryTypes.ItemUpdated;
                    AddItemsHistory(itemHistory, null);
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateItems(Item item)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Update Items In Bulk

        public bool UpdateItemsInBulk(DataSet dsItems)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                //todo:Insert,Delete if required!

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Items_UpdateAllById);
                db.AddInParameter(updCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@biQuantityInHand", DbType.String, "QuantityInHand", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@bIsActive", DbType.Boolean, "IsActive", DataRowVersion.Current);

                db.UpdateDataSet(dsItems, dsItems.Tables[0].TableName, null, updCommand, null, transaction);
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

        #region Get Items By Item ID and Branch ID

        public bool GetItemsByItemIDAndBranchID(Item item, Int32 branchID)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_GetByItemIDAndBranchID);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, item.ItemId);
                db.AddInParameter(dbCommand, "@iBranchID", DbType.Int32, branchID);

                IDataReader reader = db.ExecuteReader(dbCommand);

                while (reader.Read())
                {
                    item.ItemId = reader["ItemId"] != DBNull.Value ? Convert.ToInt32(reader["ItemId"].ToString()) : 0;
                    item.ItemCode = reader["ItemCode"].ToString();
                    item.ItemDescription = reader["ItemDescription"].ToString();
                    item.Cost = reader["Cost"] != DBNull.Value ? Convert.ToDecimal(reader["Cost"].ToString().Trim()) : (decimal)0.00;
                    item.SellingPrice = reader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["SellingPrice"].ToString().Trim()) : (decimal)0.00;
                    item.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                    item.CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"].ToString()) : 0;
                    item.UpdatedUser = reader["UpdatedUser"] != DBNull.Value ? Convert.ToInt32(reader["UpdatedUser"].ToString()) : 0;
                    item.UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"].ToString()) : DateTime.MinValue;
                    item.QuantityInHand = reader["QuantityInHand"] != DBNull.Value ? Convert.ToInt64(reader["QuantityInHand"].ToString()) : 0;
                    item.FreezedQty = reader["FreezedQty"] != DBNull.Value ? Convert.ToInt64(reader["FreezedQty"].ToString()) : 0;
                    item.ROL = reader["ROL"] != DBNull.Value ? Convert.ToInt32(reader["ROL"].ToString()) : 0;
                    item.BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"].ToString()) : 0;
                    item.MinSellingPrice = reader["MinSellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["MinSellingPrice"].ToString()) : (decimal)0.00;
                    item.InvoicedQty = reader["InvoicedQty"] != DBNull.Value ? Convert.ToInt32(reader["InvoicedQty"].ToString()) : 0;
                    item.BrandName = reader["BrandName"].ToString();
                    item.CategoryName = reader["CategoryType"].ToString();

                    success = true;
                }


            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemsByID(Item item)");
                throw ex;
            }
            return success;
        }

        #endregion

        ///
        /// Item stock adjustment methods
        ///
        #region Add Adjustment

        public bool AddItemsAdjustment(ItemStockAdjustment itemAdj)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Adjustment_InsItems);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, itemAdj.ItemId);
                db.AddInParameter(dbCommand, "@sDescription", DbType.String, itemAdj.Description);
                db.AddInParameter(dbCommand, "@iQty", DbType.Int32, itemAdj.Qty);
                db.AddInParameter(dbCommand, "@iCreatedBy", DbType.Int32, itemAdj.UserId);
                db.AddOutParameter(dbCommand, "@iId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    int newID = (int)db.GetParameterValue(dbCommand, "@iId");

                    if (newID > 0)
                    {
                        success = true;
                        itemAdj.Id = newID;
                    }
                }

                if (success)
                {
                    Item item = new Item();
                    item.ItemId = itemAdj.ItemId;
                    this.GetItemsByID(item);

                    ItemHistory itemHistory = new ItemHistory(item);
                    itemHistory.HistoryTypeId = Structures.ItemHistoryTypes.StockAdjustment;
                    itemHistory.UserId = itemAdj.UserId;
                    itemHistory.Qty = itemAdj.Qty;
                    itemHistory.RefNo = itemAdj.Id.ToString();

                    AddItemsHistory(itemHistory, null);
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItemsAdjustment(ItemStockAdjustment itemAdj)");
                throw ex;
            }
            return success;

        }

        #endregion

        #region Get Adjustments By Item

        public DataSet GetItemsAdjustmentsByItem(Item item)
        {
            DataSet dsAdjustments;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Items_Adjustment_SelItemsByItemId);

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, item.ItemId);
                dsAdjustments = db.ExecuteDataSet(dbCommand);
                
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItemsAdjustment(ItemStockAdjustment itemAdj)");
                throw ex;
            }
            return dsAdjustments;

        }

        #endregion

        
        ///
        /// Item stock adjustment methods
        ///
        #region Add History

        public bool AddItemsHistory(ItemHistory itemAdj, DbTransaction transaction)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand("SP_Items_InsItemsHistory");

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, itemAdj.ItemId);
                db.AddInParameter(dbCommand, "@sDescription", DbType.String, itemAdj.Description);
                db.AddInParameter(dbCommand, "@iQty", DbType.Int32, itemAdj.Qty);
                db.AddInParameter(dbCommand, "@mCost", DbType.Currency, itemAdj.Cost);
                db.AddInParameter(dbCommand, "@mSellingPrice", DbType.Currency, itemAdj.SellingPrice);
                db.AddInParameter(dbCommand, "@iUserId", DbType.Int32, itemAdj.UserId);
                db.AddInParameter(dbCommand, "@sRefNo", DbType.String, itemAdj.RefNo);
                db.AddInParameter(dbCommand, "@iHistoryTypeId", DbType.Int32, (int)itemAdj.HistoryTypeId);
                //db.AddInParameter(dbCommand, "@iCreatedBy", DbType.Int32, itemAdj.UserId);
//                db.AddOutParameter(dbCommand, "@iHistoryId", DbType.Int64, 16);

                int rowCount = 0;

                if (transaction == null)
                {
                    rowCount = db.ExecuteNonQuery(dbCommand);
                }
                else
                {
                    rowCount = db.ExecuteNonQuery(dbCommand, transaction);
                }

                if (rowCount > 0)
                {
                    //int newID = (int)db.GetParameterValue(dbCommand, "@iHistoryId");

                    //if (newID > 0)
                    //{
                        success = true;
                        //itemAdj.HistoryId = newID;
                    //}
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddItemsHistory(ItemHistory itemAdj)");
                throw ex;
            }
            return success;

        }

        public DataSet GetItemHistoryByItemId(int itemId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand("SP_Items_SelItemsHistory_ByItemId");

                db.AddInParameter(dbCommand, "@iItemId", DbType.Int32, itemId);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
