using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.ItemsManagement
{
    public class ItemTransferDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Add Item Transfer

        public bool AddItemTransfer(ItemTransfer itemTransfer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Insert);


                db.AddInParameter(dbCommand, "@iInvoiceId", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCommand, "@iBranchFrom", DbType.Int32, itemTransfer.BranchFrom);
                db.AddInParameter(dbCommand, "@iBranchTo", DbType.Int32, itemTransfer.BranchTo);
                db.AddInParameter(dbCommand, "@iTransferQty", DbType.Int32, itemTransfer.TransferQty);
                db.AddInParameter(dbCommand, "@iTransferBy", DbType.Int32, itemTransfer.TransferBy);

                db.AddOutParameter(dbCommand, "@iTransferId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int32 newTransferID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@iTransferId"));
                    if (newTransferID > 0)
                    {
                        itemTransfer.TransferId = newTransferID;
                        if (this.AddItemTransferInvoice(itemTransfer, db, transaction))
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

        #region Add Item Transfer Invoice

        private bool AddItemTransferInvoice(ItemTransfer itemTransfer, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Invoice_Insert);

                db.AddInParameter(insCommand, "@iTransferId", DbType.Int32, itemTransfer.TransferId);
                db.AddInParameter(insCommand, "@iItemId", DbType.Int32, "ItemId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@iQuantity", DbType.Int32, "Quantity", DataRowVersion.Current);

                db.AddInParameter(insCommand, "@iBranchFrom", DbType.Int32, itemTransfer.BranchFrom);
                db.AddInParameter(insCommand, "@iBranchTo", DbType.Int32, itemTransfer.BranchTo);

                db.UpdateDataSet(itemTransfer.DsTransferInvoiceItems, itemTransfer.DsTransferInvoiceItems.Tables[0].TableName, insCommand,
                                 null, null, transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Update Item Transfer

        public bool UpdateItemTransfer(ItemTransfer itemTransfer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Update);

                db.AddInParameter(dbCommand, "@iTransferId", DbType.Int32, itemTransfer.InvoiceId);
                db.AddInParameter(dbCommand, "@iReceivedBy", DbType.Int32, itemTransfer.ReceivedBy);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.AddItemTransferInvoice(itemTransfer, db, transaction))
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

        #region Get Item Transfer By TransferID

        public bool GetItemTransferByTransferID(ItemTransfer ItemTransfer)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Get_By_ID);

                db.AddInParameter(cmd, "@iTransferId", DbType.Int32, ItemTransfer.TransferId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        ItemTransfer.TransferId = reader["TransferId"] != DBNull.Value ? Convert.ToInt32(reader["TransferId"].ToString()) : 0;
                        ItemTransfer.BranchFrom = reader["BranchFrom"] != DBNull.Value ? Convert.ToInt32(reader["BranchFrom"].ToString()) : 0;
                        ItemTransfer.BranchTo = reader["BranchTo"] != DBNull.Value ? Convert.ToInt32(reader["BranchTo"].ToString()) : 0;
                        ItemTransfer.TransferQty = reader["TransferQty"] != DBNull.Value ? Convert.ToInt32(reader["TransferQty"].ToString()) : 0;
                        ItemTransfer.TransferBy = reader["TransferBy"] != DBNull.Value ? Convert.ToInt32(reader["TransferBy"].ToString()) : 0;
                        ItemTransfer.TransferDate = reader["TransferDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransferDate"].ToString()) : DateTime.MinValue;
                        ItemTransfer.ReceivedBy = reader["ReceivedBy"] != DBNull.Value ? Convert.ToInt32(reader["ReceivedBy"].ToString()) : 0;
                        ItemTransfer.ReceivedDate = reader["ReceivedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReceivedDate"].ToString()) : DateTime.MinValue;
                        ItemTransfer.InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;

                        ItemTransfer.DsTransferInvoiceItems = this.GetItemTransferInvoiceByTransferID(ItemTransfer);

                        result = true;

                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Item Transfer Invoice By TransferID

        public DataSet GetItemTransferInvoiceByTransferID(ItemTransfer ItemTransfer)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Invoice_Get_By_TransferID);

                db.AddInParameter(cmd, "@iTransferId", DbType.Int32, ItemTransfer.TransferId);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get All Item Transfer

        public DataSet GetAllItemTransfer()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Item_Transfer_Get_All);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
