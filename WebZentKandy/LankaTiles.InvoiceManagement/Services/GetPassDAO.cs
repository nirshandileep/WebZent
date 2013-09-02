using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data;

namespace LankaTiles.InvoiceManagement
{
    public class GetPassDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add GetPass

        public bool AddGetPass(GetPass getPass)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_Add);

                db.AddInParameter(cmd, "@sGPCode", DbType.String, getPass.GPCode);
                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, getPass.InvoiceId);
                db.AddInParameter(cmd, "@iCreatedBy", DbType.Int32, getPass.CreatedBy);

                db.AddOutParameter(cmd, "@biGPId", DbType.Int64, 8);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    Int32 newGatePassID = Convert.ToInt32(db.GetParameterValue(cmd, "@biGPId"));

                    if (newGatePassID > 0)
                    {
                        getPass.GPId = newGatePassID;
                        if (this.UpdateGetPassDetails(getPass, db, transaction))
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

        #region Update GetPass

        public bool UpdateGetPass(GetPass getPass)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);

                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_Update);

                db.AddInParameter(cmd, "@biGPId", DbType.Int64, getPass.GPId);
                db.AddInParameter(cmd, "@iModifiedBy", DbType.Int32, getPass.ModifiedBy);

                if (db.ExecuteNonQuery(cmd, transaction) > 0)
                {
                    if (this.UpdateGetPassDetails(getPass, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }
                }

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

        #region Update GetPass Details

        private bool UpdateGetPassDetails(GetPass getPass, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_GetPas_Add_Details);
                db.AddInParameter(insCommand, "@biGPId", DbType.Int64, getPass.GPId);
                db.AddInParameter(insCommand, "@biInvDetID", DbType.Int64, "InvDetID", DataRowVersion.Current);
                db.AddInParameter(insCommand, "@biQty", DbType.Int64, "Qty", DataRowVersion.Current);

                DbCommand delCommand = db.GetStoredProcCommand(Constant.SP_GetPass_Delete_GetPassDetails);
                db.AddInParameter(delCommand, "@biGPId", DbType.Int64, getPass.GPId);
                db.AddInParameter(delCommand, "@biInvDetID", DbType.Int64, "InvDetID", DataRowVersion.Current);
                db.AddInParameter(delCommand, "@biQty", DbType.Int64, "Qty", DataRowVersion.Current);

                db.UpdateDataSet(getPass.DsGatePassDetails, getPass.DsGatePassDetails.Tables[0].TableName, insCommand, null, delCommand,
                                transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Next GetPass Code

        public string GetNextGetPassCode()
        {
            string strCode = string.Empty;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_GetNextCode);

                strCode = (string)db.ExecuteScalar(cmd);
            }
            catch (System.Exception ex)
            {
                strCode = string.Empty;
                throw ex;
            }
            return strCode;

        }

        #endregion

        #region Get GetPass By Code

        public bool GetGetPassByCode(GetPass getPass)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_GetByCode);

                db.AddInParameter(cmd, "@sGPCode", DbType.String, getPass.GPCode);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        getPass.GPId = reader["GPId"] != DBNull.Value ? Convert.ToInt64(reader["GPId"].ToString()) : 0;
                        getPass.GPCode = reader["GPCode"] != DBNull.Value ? Convert.ToString(reader["GPCode"].ToString()) : string.Empty;
                        getPass.InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                        getPass.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;
                        getPass.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) : DateTime.MinValue;
                        getPass.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"].ToString()) : 0;
                        getPass.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"].ToString()) : DateTime.MinValue;

                        getPass.DsGatePassDetails = this.GetGetPassDetailsByID(getPass);

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

        #region Get GetPass By ID

        public bool GetGetPassByID(GetPass getPass)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_Get_ByID);

                db.AddInParameter(cmd, "@biGPId", DbType.Int64, getPass.GPId);

                IDataReader reader = db.ExecuteReader(cmd);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        getPass.GPId = reader["GPId"] != DBNull.Value ? Convert.ToInt64(reader["GPId"].ToString()) : 0;
                        getPass.GPCode = reader["GPCode"] != DBNull.Value ? Convert.ToString(reader["GPCode"].ToString()) : string.Empty;
                        getPass.InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"].ToString()) : 0;
                        getPass.InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? Convert.ToString(reader["InvoiceNo"].ToString()) : string.Empty;
                        getPass.InvoiceAmmount = reader["GrandTotal"] != DBNull.Value ? Convert.ToDecimal(reader["GrandTotal"].ToString()) : 0;
                        
                        getPass.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"].ToString()) : 0;
                        getPass.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"].ToString()) : DateTime.MinValue;
                        getPass.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"].ToString()) : 0;
                        getPass.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"].ToString()) : DateTime.MinValue;

                        getPass.DsGatePassDetails = this.GetGetPassDetailsByID(getPass);

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

        #region Get GetPass Details By ID

        public DataSet GetGetPassDetailsByID(GetPass getPass)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_Get_DetailsByID);

                db.AddInParameter(cmd, "@biGPId", DbType.Int64, getPass.GPId);

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

        #region GetPass Search

        public DataSet GetPassSearch(GetPassSearch getPassSearch)
        {
            DataSet ds = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_GetPass_Search);

                db.AddInParameter(cmd, "@sGPCode", DbType.String, getPassSearch.GPCode);
                db.AddInParameter(cmd, "@iInvoiceId", DbType.Int32, getPassSearch.InvoiceId);
                db.AddInParameter(cmd, "@iCreatedBy", DbType.Int32, getPassSearch.CreatedBy);
                db.AddInParameter(cmd, "@sCreatedDateFrom", DbType.String, getPassSearch.CreatedDateFrom);
                db.AddInParameter(cmd, "@sCreatedDateTo", DbType.String, getPassSearch.CreatedDateTo);

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
    }
}
