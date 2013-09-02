using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.ChequeManagement
{
    public class ChequesDAO
    {

        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        /// <summary>
        /// Get cheque by cheque id
        /// </summary>
        /// <param name="chqBook"></param>
        /// <returns></returns>
        public bool GetChequeById(Cheques cheque)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Cheque_Get_Cheque_By_ChqID);

                db.AddInParameter(cmd, "@iChqId", DbType.Int32, cheque.ChqId);
                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {

                        cheque.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"].ToString()) : 0;
                        cheque.ChequeNo = reader["ChequeNo"] != DBNull.Value ? Convert.ToInt64(reader["ChequeNo"].ToString()) : 0;
                        cheque.ChqBookId = reader["ChqBookId"] != DBNull.Value ? Convert.ToInt32(reader["ChqBookId"].ToString()) : 0;
                        cheque.ChqDate = reader["ChqDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChqDate"].ToString()) : DateTime.MinValue;
                        cheque.ChqStatusId = reader["ChqStatusId"] != DBNull.Value ? (Structures.ChqStatusId)Convert.ToInt16(reader["ChqStatusId"].ToString()) : Structures.ChqStatusId.Created;
                        cheque.Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : String.Empty;
                        cheque.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"].ToString()) : 0;
                        cheque.ModifiedByName = reader["ModifiedByName"] != DBNull.Value ? reader["ModifiedByName"].ToString() : String.Empty;
                        cheque.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"].ToString()) : DateTime.MinValue;
                        cheque.WrittenBy = reader["WrittenBy"] != DBNull.Value ? Convert.ToInt32(reader["WrittenBy"].ToString()) : 0;
                        cheque.WrittenByName = reader["WrittenByName"] != DBNull.Value ? reader["WrittenByName"].ToString() : String.Empty;
                        cheque.WrittenDate = reader["WrittenDate"] != DBNull.Value ? Convert.ToDateTime(reader["WrittenDate"].ToString()) : DateTime.MinValue;
                        cheque.Bank = reader["BankName"] != DBNull.Value ? reader["BankName"].ToString() : String.Empty;
                        cheque.BankBranch = reader["BankBranch"] != DBNull.Value ? reader["BankBranch"].ToString() : String.Empty;
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool GetChequeById(Cheques cheque)");
                throw ex;
                success = false;
            }
            return success;
        }

        public bool UpdateCheque(Cheques cheque)
        {
            bool success = true;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Cheque_UpdateCheque);

                db.AddInParameter(dbCommand, "@iChqId", DbType.Int32, cheque.ChqId);
                db.AddInParameter(dbCommand, "@mAmount", DbType.Currency, cheque.Amount);
                db.AddInParameter(dbCommand, "@sComment", DbType.String, cheque.Comment);
                db.AddInParameter(dbCommand, "@tiChqStatusId", DbType.Int16, (Int16)cheque.ChqStatusId);
                db.AddInParameter(dbCommand, "@dtWrittenDate", DbType.DateTime, cheque.WrittenDate);
                db.AddInParameter(dbCommand, "@dtChqDate", DbType.DateTime, cheque.ChqDate);
                db.AddInParameter(dbCommand, "@iWrittenBy", DbType.Int32, cheque.WrittenBy);
                db.AddInParameter(dbCommand, "@iModifiedBy", DbType.Int32, cheque.ModifiedBy);


                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    transaction.Commit();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                transaction.Rollback();
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateCheque(Cheques cheque)");
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

        public bool UpdateChequeStatus(Cheques cheque)
        {
            bool success = true;
            try
            {

                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Cheque_UpdateChequeStatus);

                db.AddInParameter(dbCommand, "@iChqId", DbType.Int32, cheque.ChqId);
                db.AddInParameter(dbCommand, "@tiChqStatusId", DbType.Int16, cheque.ChqStatusId);
                
                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                transaction.Rollback();
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateChequeStatus(Cheques cheque)");
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


        #region Get All Cheques by issue date range

        public DataSet GetAllChequesByDateRangeForReporting(DateTime fromDate, DateTime toDate)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Cheque_Report_GetAllChequesByDateRange);

                db.AddInParameter(dbCommand, "@dFromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@dToDate", DbType.DateTime, toDate);

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ds = null;
                throw ex;
            }
            return ds;

        }

        #endregion
    }
}
