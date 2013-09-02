using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;

namespace LankaTiles.UserManagement
{
    public class UsersDAO
    {
        #region Add User

        public bool AddUser(User user)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_Insert);

                db.AddInParameter(cmd, "@sUserName", DbType.String, user.UserName);
                db.AddInParameter(cmd, "@sFirstName", DbType.String, user.FirstName);
                db.AddInParameter(cmd, "@sLastName", DbType.String, user.LastName);
                db.AddInParameter(cmd, "@sPassword", DbType.String, user.Password);
                db.AddInParameter(cmd, "@tiRoleId", DbType.Int16, user.UserRoleID);
                db.AddInParameter(cmd, "@bIsActive", DbType.Boolean, user.IsActive);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, user.BranchID);

                db.AddOutParameter(cmd, "@iUserId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    int newUserID = (int)db.GetParameterValue(cmd, "@iUserId");
                    if (newUserID > 0)
                    {
                        user.UserId = newUserID;
                        result = true;
                         
                    }

                }
                              
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser(User user)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Update User

        public bool UpdateUser(User user)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_Update);

                db.AddInParameter(cmd, "@iUserId", DbType.Int32, user.UserId);
                db.AddInParameter(cmd, "@sFirstName", DbType.String, user.FirstName);
                db.AddInParameter(cmd, "@sLastName", DbType.String, user.LastName);
                db.AddInParameter(cmd, "@sPassword", DbType.String, user.Password);
                db.AddInParameter(cmd, "@tiRoleId", DbType.Int16, user.UserRoleID);
                db.AddInParameter(cmd, "@bIsActive", DbType.Boolean, user.IsActive);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, user.BranchID);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                   result = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser(User user)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Delete User

        public bool DeleteUser(User user)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_Delete);

                db.AddInParameter(cmd, "@iUserId", DbType.Int32, user.UserId);
                
                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = true;
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser(User user)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Check UserName Exists

        public bool IsUserNameExists(string userName)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_CheckUserNameExists);

                db.AddInParameter(cmd, "@sUserName", DbType.String, userName);

                db.AddOutParameter(cmd, "@bIsExists", DbType.Boolean, 1);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = (bool)db.GetParameterValue(cmd, "@bIsExists");
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get All Users

        public DataSet GetAllUsers()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_GetAll);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser()");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get User By ID

        public bool GetUserByID(User user)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_GetUserById);

                db.AddInParameter(cmd, "@iUserId", DbType.Int32, user.UserId);
                
                IDataReader reader = db.ExecuteReader(cmd);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        user.UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"].ToString()) : 0;
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.UserRoleID = reader["RoleId"] != DBNull.Value ? short.Parse(reader["RoleId"].ToString()) : short.Parse("0");
                        user.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"].ToString()) : false;
                        user.BranchID = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"].ToString()) : 0;

                        result = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser()");
                throw ex;
            }

            return result;
        }

        #endregion

        #region Search User

        public DataSet SearchUser(User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_Search);

                db.AddInParameter(cmd, "@sUserName", DbType.String, user.UserName);
                db.AddInParameter(cmd, "@sFirstName", DbType.String, user.FirstName);
                db.AddInParameter(cmd, "@sLastName", DbType.String, user.LastName);
                db.AddInParameter(cmd, "@tiRoleId", DbType.Int16, user.UserRoleID);
                db.AddInParameter(cmd, "@iSearchOption", DbType.Int32, user.SearchOption);
                db.AddInParameter(cmd, "@iBranchId", DbType.Int32, user.BranchID);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser(User user)");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get All User Roles

        public DataSet GetAllUserRoles()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_User_GetAllUserRoles);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddUser()");
                return null;
                throw ex;
            }
        }

        #endregion
    }
}
