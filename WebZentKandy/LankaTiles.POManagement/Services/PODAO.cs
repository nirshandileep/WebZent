using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;
using System.Data;

namespace LankaTiles.POManagement
{
    public class PODAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Add PO

        public bool AddPO(PO po)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PO_Insert);

                db.AddInParameter(dbCommand, "@sPOCode", DbType.String, po.POCode);
                db.AddInParameter(dbCommand, "@mPOAmount", DbType.Currency, po.POAmount);
                db.AddInParameter(dbCommand, "@mBalanceAmount", DbType.Currency, po.BalanceAmount);
                db.AddInParameter(dbCommand, "@iPOCreatedUser", DbType.Int32, po.POCreatedUser);
                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, po.SupId);
                db.AddInParameter(dbCommand, "@dtPODate", DbType.DateTime, po.PODate);

                db.AddInParameter(dbCommand, "@iRequestedBy", DbType.Int32, po.RequestedBy.Value);
                db.AddInParameter(dbCommand, "@sPOComment", DbType.String, po.POComment);

                db.AddOutParameter(dbCommand, "@iPOId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int32 newPOID = (Int32)db.GetParameterValue(dbCommand, "@iPOId");
                    if (newPOID > 0)
                    {
                        po.POId = newPOID;
                        if (this.UpdatePOItems(po, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddPO(PO po)");
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

        #region Update PO

        public bool UpdatePO(PO po)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PO_Update);

                db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, po.POId);
                db.AddInParameter(dbCommand, "@mPOAmount", DbType.Currency, po.POAmount);
                db.AddInParameter(dbCommand, "@iSupId", DbType.Int32, po.SupId);
                db.AddInParameter(dbCommand, "@mBalanceAmount", DbType.Currency, po.BalanceAmount);
                db.AddInParameter(dbCommand, "@iPOLastModifiedBy", DbType.Int32, po.POLastModifiedBy);
                db.AddInParameter(dbCommand, "@iRequestedBy", DbType.Int32, po.RequestedBy);
                db.AddInParameter(dbCommand, "@sPOComment", DbType.String, po.POComment);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdatePOItems(po, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }
                }
            }

            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddPO(PO po)");
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

        #region Update PO Status

        public bool UpdatePOStatus(PO po)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PO_UpdateStatus);

                db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, po.POId);
                db.AddInParameter(dbCommand, "@iPOStatus", DbType.Int16, (Int16)po.POStatus);
                db.AddInParameter(dbCommand, "@sPOComment", DbType.String, po.POComment);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    result = true;
                }
            }

            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdatePOStatus(PO po)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Update PO Items

        private bool UpdatePOItems(PO po, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_PO_InsPOItems);
                db.AddInParameter(insCommand, "@iPOId", DbType.Int32, po.POId);
                db.AddInParameter(insCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@mPOItemCost", DbType.Currency, "POItemCost", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@iQty", DbType.Int32, "Qty", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@mLineCost", DbType.Currency, "LineCost", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@dDiscountPerUnit", DbType.Decimal, "DiscountPerUnit", DataRowVersion.Current);

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_PO_UpdPOItems);
                db.AddInParameter(updCommand, "@iPOId", DbType.Int32, po.POId);
                db.AddInParameter(updCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mPOItemCost", DbType.Currency, "POItemCost", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iQty", DbType.Int32, "Qty", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mLineCost", DbType.Currency, "LineCost", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@dDiscountPerUnit", DbType.Decimal, "DiscountPerUnit", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@biPOItemId", DbType.Int64, "POItemId", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_PO_DelPOItems);
                db.AddInParameter(delCommand, "@biPOItemId", DbType.Int64, "POItemId", DataRowVersion.Current);

                db.UpdateDataSet(po.DsPOItems, po.DsPOItems.Tables[0].TableName, insCommand, updCommand, delCommand, transaction);
                result = true;
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get PO By ID

        public bool GetPOByID(PO po)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_GetByID);

                db.AddInParameter(cmd, "@iPOId", DbType.Int32, po.POId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        po.POId = reader["POId"] != DBNull.Value ? Convert.ToInt32(reader["POId"].ToString()) : 0;
                        po.POCode = reader["POCode"].ToString();
                        po.POAmount = reader["POAmount"] != DBNull.Value ? Convert.ToDecimal(reader["POAmount"].ToString())
                                      : Convert.ToDecimal("0");
                        po.BalanceAmount = reader["BalanceAmount"] != DBNull.Value ? Convert.ToDecimal(reader["BalanceAmount"].ToString())
                                           : Convert.ToDecimal("0");
                        po.POCreatedUser = reader["POCreatedUser"] != DBNull.Value ? Convert.ToInt32(reader["POCreatedUser"].ToString())
                                           : Convert.ToInt32("0");
                        po.POCreatedDate = reader["POCreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["POCreatedDate"].ToString())
                                           : DateTime.MinValue;
                        po.SupId = reader["SupId"] != DBNull.Value ? Convert.ToInt32(reader["SupId"].ToString())
                                   : Convert.ToInt32("0");
                        po.POLastModifiedBy = reader["POLastModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["POLastModifiedBy"].ToString())
                                              : Convert.ToInt32("0");
                        po.POLastModifiedDate = reader["POLastModifiedDate"] != DBNull.Value ?
                                                Convert.ToDateTime(reader["POLastModifiedDate"].ToString()) : DateTime.MinValue;
                        po.IsReceived = reader["IsReceived"] != DBNull.Value ?
                                         Convert.ToBoolean(reader["IsReceived"].ToString()) : false;
                        po.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : String.Empty;

                        if (reader["RequestedBy"] != DBNull.Value)
                        {
                            po.RequestedBy = Convert.ToInt32(reader["RequestedBy"].ToString());
                        }
                        else
                        {
                            po.RequestedBy = null;
                        }
                        
                        if (reader["PODate"]!=DBNull.Value)
                        {
                            po.PODate = Convert.ToDateTime(reader["PODate"].ToString());
                        }
                        else
                        {
                            po.PODate = null;
                        }

                        po.POStatus = (Structures.POStatus)(reader["POStatus"] != DBNull.Value ? Convert.ToInt16(reader["POStatus"].ToString()) : Convert.ToInt16("1"));
                        po.POComment = reader["POComment"] != DBNull.Value ? reader["POComment"].ToString() : String.Empty;

                        result = true;
                    }
                    po.DsPOItems = this.GetPOItemsByPOID(po);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get PO Items By PO ID

        public DataSet GetPOItemsByPOID(PO po)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_GetPOItemsByPOID);

                db.AddInParameter(cmd, "@iPOId", DbType.Int32, po.POId);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get All PO

        public DataSet GetAllPO()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_GetAll);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Next PO Code

        public string GetNextPOCode()
        {
            string nextPOCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_GetNextCode);

                nextPOCode = (string)db.ExecuteScalar(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return nextPOCode;
        }

        #endregion

        #region Search PO

        public DataSet SearchPO(POSearchParameters poSearchParameters)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_SearchPO);

                db.AddInParameter(cmd, "@sPOCode", DbType.String, poSearchParameters.POCode);
                db.AddInParameter(cmd, "@iSupId", DbType.Int32, poSearchParameters.SupId);
                db.AddInParameter(cmd, "@mPOAmount", DbType.Currency, poSearchParameters.POAmount);
                db.AddInParameter(cmd, "@iDueAmountOption", DbType.Int32, poSearchParameters.DueAmountOption);
                db.AddInParameter(cmd, "@iTotRcvdOption", DbType.Int32, poSearchParameters.TotRcvdOption);
                db.AddInParameter(cmd, "@sFromDate", DbType.String, poSearchParameters.FromDate);
                db.AddInParameter(cmd, "@sToDate", DbType.String, poSearchParameters.ToDate);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get All Partialy Received PO

        public DataSet GetAllPartialyReceivedPO()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_SelPartialyReceivedPO);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get All Partialy Received PO Items By POID

        public DataSet GetAllPartialyReceivedPOItemsByPOID(PO po)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_SelPartialyReceivedPOItemsByPOID);

                db.AddInParameter(cmd, "@iPOId", DbType.Int32, po.POId);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get All Payable POs

        public DataSet GetAllPayablePOs()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_GetAllPayable);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Purchase Orders for PO Report

        /// <summary>
        /// Get PO Details for reporting
        /// </summary>
        /// <param name="poSearchParameters">Po search Parameters class object</param>
        /// <returns>DataSet</returns>
        public DataSet GetPurchaseOrdersForReporting(POSearchParameters poSearchParameters)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PO_Report_SearchPO);

                db.AddInParameter(cmd, "@iDueAmountOption", DbType.Int32, poSearchParameters.DueAmountOption);
                db.AddInParameter(cmd, "@iTotRcvdOption", DbType.Int32, poSearchParameters.TotRcvdOption);
                db.AddInParameter(cmd, "@sFromDate", DbType.String, poSearchParameters.FromDate);
                db.AddInParameter(cmd, "@sToDate", DbType.String, poSearchParameters.ToDate);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get PO By Items for reporting

        public DataSet GetItemwisePODetailsForReporting(POSearchParameters poSearch)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PO_Report_GetPODetailsByItems);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, poSearch.FromDateRep);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, poSearch.ToDateRep);

                return db.ExecuteDataSet(dbCommand);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetItemwisePODetailsForReporting(POSearchParameters poSearch)");
                return null;
                throw ex;
            }

        }

        #endregion
    }
}
