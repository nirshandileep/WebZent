using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using LankaTiles.Common;

namespace LankaTiles.GRNManagement
{
    // has grn service methods
    public class GRNDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Add GRN

        public bool AddGRN(GRN grn)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_GRN_Insert);

                if (grn.POId.HasValue)
                {
                    db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, grn.POId);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, DBNull.Value);
                }

                if (grn.InvId.HasValue)
                {
                    db.AddInParameter(dbCommand, "@iInvId", DbType.Int32, grn.InvId);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iInvId", DbType.Int32, DBNull.Value);
                }

                if (grn.SalesReturnID.HasValue)
                {
                    db.AddInParameter(dbCommand, "@iSalesReturnID", DbType.Int32, grn.SalesReturnID);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iSalesReturnID", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(dbCommand, "@iRec_By", DbType.Int32, grn.Rec_By);
                db.AddInParameter(dbCommand, "@sSuplierInvNo", DbType.String, grn.SuplierInvNo);

                db.AddInParameter(dbCommand, "@mReceivedTotal", DbType.Decimal, grn.TotalAmount);
                db.AddInParameter(dbCommand, "@sCreditNote", DbType.String, grn.CreditNote);

                db.AddInParameter(dbCommand, "@dtRec_Date", DbType.DateTime, grn.Rec_Date);

                db.AddOutParameter(dbCommand, "@iGRNId", DbType.Int64, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int64 newGRNID = Convert.ToInt64(db.GetParameterValue(dbCommand, "@iGRNId"));
                    if (newGRNID > 0)
                    {
                        grn.GRNId = newGRNID;
                        if (this.UpdateGRNItems(grn, db, transaction))
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
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddGRN(GRN grn)");
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

        #region Update GRN
        //this is the update 
        public bool UpdateGRN(GRN grn)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_GRN_Update);

                db.AddInParameter(dbCommand, "@iGRNId", DbType.Int64, grn.GRNId);

                if (grn.POId.HasValue)
                {
                    db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, grn.POId);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iPOId", DbType.Int32, DBNull.Value);
                }

                if (grn.SalesReturnID.HasValue)
                {
                    db.AddInParameter(dbCommand, "@iSalesReturnID", DbType.Int32, grn.SalesReturnID);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@iSalesReturnID", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(dbCommand, "@iRec_By", DbType.Int32, grn.Rec_By);
                db.AddInParameter(dbCommand, "@sSuplierInvNo", DbType.String, grn.SuplierInvNo);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdateGRNItems(grn, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }

                }


            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddGRN(GRN grn)");
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

        #region Update GRN Items (Details)

        private bool UpdateGRNItems(GRN grn, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_GRN_InsertDetails);
                db.AddInParameter(insCommand, "@iId", DbType.Int64, "Id",DataRowVersion.Current);
                db.AddInParameter(insCommand, "@biGRNId", DbType.Int64, grn.GRNId);
                db.AddInParameter(insCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@iReceivedQty", DbType.Int32, "ReceivedQty", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@mItemValue", DbType.Currency, "ItemValue", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_GRN_DeleteDetails);
                //db.AddInParameter(insCommand, "@iId", DbType.Int64, "Id");not yet done
                db.AddInParameter(delCommand, "@biGRNId", DbType.Int64, grn.GRNId);
                db.AddInParameter(delCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);

                db.UpdateDataSet(grn.GRNItems, grn.GRNItems.Tables[0].TableName, insCommand, null, delCommand, transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get GRN By ID

        public bool GetGRNByID(GRN grn)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GRN_Get_By_ID);

                db.AddInParameter(cmd, "@biGRNId", DbType.Int64, grn.GRNId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        grn.GRNId = reader["GRNId"] != DBNull.Value ? Convert.ToInt64(reader["GRNId"].ToString()) : 0;
                        if (reader["POId"] == DBNull.Value || reader["POId"].ToString() == String.Empty)
                        {
                            grn.POId = null;
                        }
                        else
                        {
                            grn.POId = Convert.ToInt32(reader["POId"].ToString());
                        }

                        if (reader["SalesReturnID"] == DBNull.Value || reader["SalesReturnID"].ToString() == String.Empty)
                        {
                            grn.SalesReturnID = null;
                        }
                        else
                        {
                            grn.SalesReturnID = reader["SalesReturnID"] != DBNull.Value ? Convert.ToInt32(reader["SalesReturnID"].ToString()) : 0;
                        }

                        if (reader["InvoiceId"] == DBNull.Value || reader["InvoiceId"].ToString() == String.Empty)
                        {
                            grn.InvId = null;
                        }
                        else
                        {
                            grn.InvId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                            grn.GRNInvoice.InvoiceId = grn.InvId.Value;
                            grn.GRNInvoice.GetInvoiceByInvoiceID();
                        }
                        
                        grn.Rec_Date = reader["Rec_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Rec_Date"].ToString()) : DateTime.MinValue;
                        grn.Rec_By = reader["Rec_By"] != DBNull.Value ? Convert.ToInt32(reader["Rec_By"].ToString()) : 0;
                        grn.TotalAmount = reader["ReceivedTotal"] != DBNull.Value ? Convert.ToDecimal(reader["ReceivedTotal"].ToString()) : 0;
                        grn.SuplierInvNo = reader["SuplierInvNo"].ToString();
                        grn.CreditNote = reader["CreditNote"].ToString();

                        grn.GRNItems = this.GetGRNDetailsByGRNID(grn);

                        result = true;

                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetGRNByID(GRN grn)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get GRN By ID and customer id

        public bool GetGRNByIDAndCustId(GRN grn,Int32 customerId)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GRN_Get_By_IDAndCustId);

                db.AddInParameter(cmd, "@biGRNId", DbType.Int64, grn.GRNId);
                db.AddInParameter(cmd, "@iCustId", DbType.Int32, customerId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        grn.GRNId = reader["GRNId"] != DBNull.Value ? Convert.ToInt64(reader["GRNId"].ToString()) : 0;
                        //if (reader["POId"] == DBNull.Value || reader["POId"].ToString() == String.Empty)
                        //{
                        //    grn.POId = null;
                        //}
                        //else
                        //{
                        //    grn.POId = Convert.ToInt32(reader["POId"].ToString());
                        //}

                        //if (reader["SalesReturnID"] == DBNull.Value || reader["SalesReturnID"].ToString() == String.Empty)
                        //{
                        //    grn.SalesReturnID = null;
                        //}
                        //else
                        //{
                        //    grn.SalesReturnID = reader["SalesReturnID"] != DBNull.Value ? Convert.ToInt32(reader["SalesReturnID"].ToString()) : 0;
                        //}

                        //if (reader["InvoiceId"] == DBNull.Value || reader["InvoiceId"].ToString() == String.Empty)
                        //{
                        //    grn.InvId = null;
                        //}
                        //else
                        //{
                        //    grn.InvId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                        //    grn.GRNInvoice.InvoiceId = grn.InvId.Value;
                        //    grn.GRNInvoice.GetInvoiceByInvoiceID();
                        //}

                        grn.Rec_Date = reader["Rec_Date"] != DBNull.Value ? Convert.ToDateTime(reader["Rec_Date"].ToString()) : DateTime.MinValue;
                        grn.Rec_By = reader["Rec_By"] != DBNull.Value ? Convert.ToInt32(reader["Rec_By"].ToString()) : 0;
                        grn.TotalAmount = reader["ReceivedTotal"] != DBNull.Value ? Convert.ToDecimal(reader["ReceivedTotal"].ToString()) : 0;
                        //grn.SuplierInvNo = reader["SuplierInvNo"].ToString();
                        grn.TotalPaid = reader["SupplierPaidAmmount"] != DBNull.Value ? Convert.ToDecimal(reader["SupplierPaidAmmount"].ToString()) : 0;

                        //grn.GRNItems = this.GetGRNDetailsByGRNID(grn);

                        result = true;

                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetGRNByID(GRN grn)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get GRN Items By GRN ID

        public DataSet GetGRNDetailsByGRNID(GRN grn)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GRN_Get_Details_By_ID);

                db.AddInParameter(cmd, "@biGRNId", DbType.Int64, grn.GRNId);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetGRNByID(GRN grn)");
                throw ex;
                return null;
            }
        }

        #endregion

        #region GRN Search

        public DataSet GRNSearch(GRNSearchParameters grnSearchParameters)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GRN_Search);

                db.AddInParameter(cmd, "@sPOCode", DbType.String, grnSearchParameters.POCode);
                db.AddInParameter(cmd, "@iSalesReturnID", DbType.Int32, grnSearchParameters.SalesReturnID.HasValue ? grnSearchParameters.SalesReturnID : 0);
                db.AddInParameter(cmd, "@sSuplierInvNo", DbType.String, grnSearchParameters.SuplierInvNo);
                db.AddInParameter(cmd, "@sFromDate", DbType.String, grnSearchParameters.FromDate);
                db.AddInParameter(cmd, "@sToDate", DbType.String, grnSearchParameters.ToDate);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GRNSearch(GRNSearchParameters grnSearchParameters)");
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get GRN By ID

        public bool IsSupplierInvNoExist(GRN grn)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GRN_Check_SupInvNo_Exist);

                db.AddInParameter(cmd, "@sSuplierInvNo", DbType.String, grn.SuplierInvNo);
                db.AddOutParameter(cmd, "@iResult", DbType.Int16, 4);

                db.ExecuteReader(cmd);
                int output = Int32.Parse(db.GetParameterValue(cmd, "@iResult").ToString());
                if (output > 0)
                {
                    result = true;
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool IsSupplierInvNoExist(GRN grn)");
                throw ex;
            }
            return result;
        }

        #endregion
    }
}
