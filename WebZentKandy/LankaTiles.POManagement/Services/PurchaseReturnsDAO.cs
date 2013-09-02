using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.POManagement
{
    public class PurchaseReturnsDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Get PO By ID

        public bool GetPRByPRId(PurchaseReturns pr)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PR_GetByPRID);

                db.AddInParameter(cmd, "@iPRId", DbType.Int32, pr.PRId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        pr.PRId = reader["PRId"] != DBNull.Value ? Convert.ToInt32(reader["PRId"].ToString()) : 0;
                        pr.PRCode = reader["PRCode"].ToString();
                        pr.TotalReturns = reader["TotalReturns"] != DBNull.Value ? Convert.ToDecimal(reader["TotalReturns"].ToString()) : Convert.ToDecimal("0");
                        pr.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;
                        pr.CreatedUser = reader["CreatedUser"].ToString();
                        pr.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) : DateTime.MinValue;
                        pr.Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : String.Empty;
                        pr.GRNId = reader["GRNId"] != DBNull.Value ? Convert.ToInt64(reader["GRNId"].ToString()) : 0;
                        pr.ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"].ToString()) : DateTime.MinValue;
                        pr.IsReimbursed = reader["IsReimbursed"] != DBNull.Value ? Convert.ToBoolean(reader["IsReimbursed"].ToString()) : false;
                        if (reader["ModifiedBy"].ToString() == string.Empty)
                        {
                            pr.ModifiedBy = null;
                            pr.ModifiedDate = null;
                        }
                        else
                        {
                            pr.ModifiedBy = (int?)reader["ModifiedBy"];
                            pr.ModifiedDate = (DateTime?)reader["ModifiedDate"];
                        }
    
                        pr.ModifiedUser = reader["ModifiedUser"].ToString();

                        pr.POCode = reader["POCode"].ToString();
                        pr.SuplierInvNo = reader["SuplierInvNo"].ToString();

                        pr.SupplierName = reader["SupplierName"].ToString();

                        result = true;
                    }
                    pr.DsReturnDetails = this.GetPRItemsByPRID(pr);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get PR Items By PR ID

        public DataSet GetPRItemsByPRID(PurchaseReturns PR)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PR_GetPRItemsByPRID);

                db.AddInParameter(cmd, "@iPRId", DbType.Int32, PR.PRId);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Add Purchase Returns

        public bool AddPR(PurchaseReturns pr)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PR_Insert);

                db.AddInParameter(dbCommand, "@iGRNId", DbType.Int64, pr.GRNId);
                db.AddInParameter(dbCommand, "@iCreatedBy", DbType.Int32, pr.CreatedBy);
                db.AddInParameter(dbCommand, "@mTotalReturns", DbType.Currency, pr.TotalReturns);
                db.AddInParameter(dbCommand, "@sComment", DbType.String, pr.Comment);
                db.AddInParameter(dbCommand, "@dReturnDate", DbType.Date, pr.ReturnDate);

                db.AddOutParameter(dbCommand, "@iPRId", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@sPRCode", DbType.String, 10);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int32 newPRID = (Int32)db.GetParameterValue(dbCommand, "@iPRId");
                    string prCode = (string)db.GetParameterValue(dbCommand, "@sPRCode");
                    if (newPRID > 0)
                    {
                        pr.PRId = newPRID;
                        pr.PRCode = prCode;

                        if (this.UpdatePRItems(pr, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddPR(PurchaseReturns pr)");
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

        #region Update PR

        public bool UpdatePR(PurchaseReturns pr)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_PR_Update);

                db.AddInParameter(dbCommand, "@iPRId", DbType.Int32, pr.PRId);
                db.AddInParameter(dbCommand, "@iModifiedBy", DbType.Int32, pr.ModifiedBy);
                db.AddInParameter(dbCommand, "@mTotalReturns", DbType.Currency, pr.TotalReturns);
                db.AddInParameter(dbCommand, "@sComment", DbType.String, pr.Comment);
                db.AddInParameter(dbCommand, "@dReturnDate", DbType.Date, pr.ReturnDate);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdatePRItems(pr, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }
                }
            }

            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdatePR(PurchaseReturns pr)");
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

        #region Update PR Items

        private bool UpdatePRItems(PurchaseReturns pr, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_PR_InsPRItems);
                db.AddInParameter(insCommand, "@iPRId", DbType.Int32, pr.PRId);
                db.AddInParameter(insCommand, "@iGRNDetailsId", DbType.Int64, "GRNDetailsId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@iQty", DbType.Int32, "Qty", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@mUnitCost", DbType.Decimal, "UnitCost", DataRowVersion.Current);

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_PR_UpdPRItems);
                db.AddInParameter(updCommand, "@iPRDetailId", DbType.Int32, "PRDetailId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mUnitCost", DbType.Currency, "UnitCost", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iQty", DbType.Int32, "Qty", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_PR_DelPRItems);
                db.AddInParameter(delCommand, "@biPRDetailId", DbType.Int64, "PRDetailId", DataRowVersion.Current);

                db.UpdateDataSet(pr.DsReturnDetails, pr.DsReturnDetails.Tables[0].TableName, insCommand, updCommand, delCommand, transaction);
                result = true;
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "private bool UpdatePRItems(PurchaseReturns pr, Database db, DbTransaction transaction)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region PR details by grn details id

        public DataSet GetPRItemsByGRNDetailID(Int64 ID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PR_GetPRItemsByGRNDetailID);

                db.AddInParameter(cmd, "@iGRNDetailId", DbType.Int64, ID);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {

                throw ex;
                return null;
            }
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemsToReturnByGRNId(Int64 grnId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PR_GetItemsToReturnByGRNId);

                db.AddInParameter(cmd, "@biGRNId", DbType.Int64, grnId);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search purchase details by Purchase returns search criteria
        /// </summary>
        /// <param name="prSearch"></param>
        /// <returns></returns>
        public DataSet SearchPurchaseReturns(PurchaseReturnsSearch prSearch)
        {
            try
            {
                try
                {
                    Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                    DbCommand cmd = db.GetStoredProcCommand(Constant.SP_PR_SelPurchaseReturns);

                    db.AddInParameter(cmd, "@sSupInvNo", DbType.String, prSearch.SupInvNo);
                    db.AddInParameter(cmd, "@sPRCode", DbType.String, prSearch.PRCode);
                    db.AddInParameter(cmd, "@siIssuedStatus", DbType.Int16, (int)prSearch.IssuedStatus);
                    if (prSearch.FromDate.HasValue)
                    {
                        db.AddInParameter(cmd, "@dFromDate", DbType.DateTime, prSearch.FromDate);
                    }
                    else
                    {
                        db.AddInParameter(cmd, "@dFromDate", DbType.DateTime, DBNull.Value);
                    }
                    if (prSearch.ToDate.HasValue)
                    {
                        db.AddInParameter(cmd, "@dToDate", DbType.DateTime, prSearch.ToDate);
                    }
                    else
                    {
                        db.AddInParameter(cmd, "@dToDate", DbType.DateTime, DBNull.Value);
                    }
                   
                    return db.ExecuteDataSet(cmd);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
