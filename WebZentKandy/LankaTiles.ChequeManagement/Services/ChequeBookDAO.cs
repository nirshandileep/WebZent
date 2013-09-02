using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.ChequeManagement
{
    public class ChequeBookDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        /// <summary>
        /// Creates the cheque book and saves the cheques in tha dataset dsCheques property
        /// </summary>
        /// <param name="chqBook"></param>
        /// <returns></returns>
        public bool Create(ChequeBook chqBook)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Cheque_Insert);

                db.AddInParameter(dbCommand, "@iNoOfCheques", DbType.Int32, chqBook.NoOfCheques);
                db.AddInParameter(dbCommand, "@biFirstChqNo", DbType.Int64, chqBook.FirstChqNo);
                db.AddInParameter(dbCommand, "@biLastChqNo", DbType.Int64, chqBook.LastChqNo);
                db.AddInParameter(dbCommand, "@iCreatedBy", DbType.Int32, chqBook.CreatedBy);
                db.AddInParameter(dbCommand, "@sBankName", DbType.String, chqBook.BankName);
                db.AddInParameter(dbCommand, "@sBankBranch", DbType.String, chqBook.BankBranch);

                db.AddOutParameter(dbCommand, "@iChqBookId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int32 newChqBookId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@iChqBookId"));
                    if (newChqBookId > 0)
                    {
                        chqBook.ChqBookId = newChqBookId;
                        if (this.UpdateCheques(chqBook, db, transaction))
                        {
                            transaction.Commit();
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                transaction.Rollback();
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool Create(ChequeBook chqBook)");
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return success;
        }


        /// <summary>
        /// Updates the entire cheque book and updated the cheques if status changed
        /// </summary>
        /// <param name="chqBook"></param>
        /// <returns></returns>
        public bool Update(ChequeBook chqBook)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Cheque_Update);

                db.AddInParameter(dbCommand, "@iChqBookId", DbType.Int32, chqBook.ChqBookId);
                db.AddInParameter(dbCommand, "@iNoOfCheques", DbType.Int32, chqBook.NoOfCheques);
                db.AddInParameter(dbCommand, "@biFirstChqNo", DbType.Int64, chqBook.FirstChqNo);
                db.AddInParameter(dbCommand, "@biLastChqNo", DbType.Int64, chqBook.LastChqNo);
                db.AddInParameter(dbCommand, "@iModifiedBy", DbType.Int32, chqBook.ModifiedBy);
                db.AddInParameter(dbCommand, "@sBankName", DbType.String, chqBook.BankName);
                db.AddInParameter(dbCommand, "@sBankBranch", DbType.String, chqBook.BankBranch);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdateCheques(chqBook, db, transaction))
                    {
                        transaction.Commit();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                transaction.Rollback();
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool Update(ChequeBook chqBook)");
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// Updates cheque details, update individual cheques
        /// </summary>
        /// <param name="chqBook"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private bool UpdateCheques(ChequeBook chqBook, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_Cheque_InsertDetails);
                db.AddInParameter(insCommand, "@iChqBookId", DbType.Int32, chqBook.ChqBookId);
                db.AddInParameter(insCommand, "@tiChqStatusId", DbType.Int16, "ChqStatusId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@biChequeNo", DbType.Int64, "ChequeNo", DataRowVersion.Current);

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Cheque_UpdateDetails);
                db.AddInParameter(updCommand, "@iChqId", DbType.Int32, "ChqId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mAmount", DbType.Currency, "Amount", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@sComment", DbType.String, "Comment", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@tiChqStatusId", DbType.Int16, "ChqStatusId", DataRowVersion.Current);
                //db.AddInParameter(updCommand, "@dtWrittenDate", DbType.DateTime, "WrittenDate", DataRowVersion.Current);
                //db.AddInParameter(updCommand, "@dtChqDate", DbType.DateTime, "ChqDate", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iWrittenBy", DbType.Int32, "WrittenBy", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iModifiedBy", DbType.Int32, "ModifiedBy", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_Cheque_DeleteDetails);
                db.AddInParameter(delCommand, "@iChqId", DbType.Int32, "ChqId", DataRowVersion.Current);

                db.UpdateDataSet(chqBook.DsCheques, chqBook.DsCheques.Tables[0].TableName, insCommand, updCommand, delCommand, transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Updates cheque details, update individual cheques
        /// </summary>
        /// <param name="chqBook"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool UpdateCheques(ChequeBook chqBook)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
             DbTransaction transaction;
            connection = db.CreateConnection();
            connection.Open();
            transaction = connection.BeginTransaction();

            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_Cheque_InsertDetails);
                db.AddInParameter(insCommand, "@iChqBookId", DbType.Int32, chqBook.ChqBookId);
                db.AddInParameter(insCommand, "@tiChqStatusId", DbType.Int16, "ChqStatusId", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@biChequeNo", DbType.Int64, "ChequeNo", DataRowVersion.Current);

                DbCommand updCommand = db.GetStoredProcCommand(Constant.SP_Cheque_UpdateDetails);
                db.AddInParameter(updCommand, "@iChqId", DbType.Int32, "ChqId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@mAmount", DbType.Currency, "Amount", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@sComment", DbType.String, "Comment", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@tiChqStatusId", DbType.Int16, "ChqStatusId", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iWrittenBy", DbType.Int32, "WrittenBy", DataRowVersion.Current);
                db.AddInParameter(updCommand, "@iModifiedBy", DbType.Int32, "ModifiedBy", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_Cheque_DeleteDetails);
                db.AddInParameter(delCommand, "@iChqId", DbType.Int32, "ChqId", DataRowVersion.Current);

                int rowcount = 0;
                rowcount = db.UpdateDataSet(chqBook.DsCheques, chqBook.DsCheques.Tables[0].TableName, insCommand, updCommand, delCommand, transaction);
                if (rowcount > 0)
                {
                    result = true;
                    transaction.Commit();

                }
                else
                {
                    result = false;
                    transaction.Rollback();
                }

            }
            catch (System.Exception ex)
            {
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

        /// <summary>
        /// Get cheque book by cheque book id
        /// </summary>
        /// <param name="chqBook"></param>
        /// <returns></returns>
        public bool GetChqBookById(ChequeBook chqBook)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Get_Book_By_ID);

                db.AddInParameter(cmd, "@iChqBookId", DbType.Int32, chqBook.ChqBookId);
                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {

                        chqBook.NoOfCheques = reader["NoOfCheques"] != DBNull.Value ? Convert.ToInt32(reader["NoOfCheques"].ToString()) : 0;
                        chqBook.FirstChqNo = reader["FirstChqNo"] != DBNull.Value ? Convert.ToInt64(reader["FirstChqNo"].ToString()) : 0;
                        chqBook.LastChqNo = reader["LastChqNo"] != DBNull.Value ? Convert.ToInt64(reader["LastChqNo"].ToString()) : 0;
                        chqBook.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;
                        chqBook.CreatedByName = reader["CreatedByName"] != DBNull.Value ? reader["CreatedByName"].ToString() : string.Empty;
                        chqBook.CreatedDate = reader["CreatedDate"] != DBNull.Value ? DateTime.Parse(reader["CreatedDate"].ToString()) : DateTime.MinValue;
                        chqBook.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"].ToString()) : 0;
                        chqBook.ModifiedByName = reader["ModifiedByName"] != DBNull.Value ? reader["ModifiedByName"].ToString() : string.Empty;

                        if (reader["ModifiedDate"] == DBNull.Value)
                        {
                            chqBook.ModifiedDate = null;
                        }
                        else
                        {
                            chqBook.ModifiedDate = DateTime.Parse(reader["ModifiedDate"].ToString());
                        }

                        chqBook.BankName = reader["BankName"].ToString();
                        chqBook.BankBranch = reader["BankBranch"].ToString();

                        chqBook.DsCheques = this.GetChequeBookDetailsById(chqBook);
                    }
                }

                if (chqBook.DsCheques == null)
                {
                    chqBook.DsCheques = this.GetChequeBookDetailsById(chqBook);
                }

                success = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "private bool GetChqBookById(ChequeBook chqBook)");
                throw ex;
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Get cheque book details (cheques) by book id
        /// </summary>
        /// <param name="chkBook"></param>
        /// <returns></returns>
        public DataSet GetChequeBookDetailsById(ChequeBook chkBook)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Get_Details_By_ID);

                db.AddInParameter(cmd, "@iChqBookId", DbType.Int32, chkBook.ChqBookId);

                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "private DataSet GetChequeBookDetailsById(Int32 chqBookId)");
                throw ex;
                return null;
            }
        }

        /// <summary>
        /// Check if the generating cheque numbers already exist in the database
        /// </summary>
        /// <param name="chkBook"></param>
        /// <returns></returns>
        public bool ValidateCheckNumbersByRange(ChequeBook chkBook)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Is_ChqNumbers_Already_Exists);

                db.AddInParameter(cmd, "@biFirstChqNo", DbType.Int64, chkBook.FirstChqNo);
                db.AddInParameter(cmd, "@biLastChqNo", DbType.Int64, chkBook.LastChqNo);
                db.AddOutParameter(cmd, "@iResult", DbType.Int64, 4);

                db.ExecuteNonQuery(cmd);

                Int32 result = Convert.ToInt32(db.GetParameterValue(cmd, "@iResult"));

                if (result == 1)//If numbers already exist
                {
                    success = false;
                }
                else
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool ValidateCheckNumbersByRange(ChequeBook chkBook)");
                throw ex;
            }

            return success;
        }

        public DataSet GetAllChequeBooks()
        {
            DataSet dsCheques;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Get_Book_All);

                dsCheques = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllCheques()");
                throw ex;
            }
            return dsCheques;
        }

        public DataSet GetAllAvailableCheques()
        {
            DataSet dsCheques;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Get_All_Available_Cheques);

                dsCheques = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllCheques()");
                throw ex;
            }
            return dsCheques;
        }

    }
}
