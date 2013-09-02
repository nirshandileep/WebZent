namespace LankaTiles.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using LankaTiles.Common;
    using System.Data.Common;

    [Serializable]
    public class User
    {
        #region Private members

        private Int32 userId;
        private String salutation;
        private String firstName;
        private String lastName;
        private String userName;
        private String password;
        private bool isActive;
        private String _class;
        private Int16  userRoleID;
        private Int32 branchID;
        private Int32 searchOption;
          
        #endregion

        #region public properties

        /// <summary>
        /// Password of the User
        /// </summary>
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Last Name part of the user
        /// </summary>
        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /// <summary>
        /// First name part of the user
        /// </summary>
        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// Salutation ,Mr., Mrs., Miss.,
        /// </summary>
        public String Salutation
        {
            get { return salutation; }
            set { salutation = value; }
        }

        /// <summary>
        /// UserId from database
        /// </summary>
        public Int32 UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// UserName unique to each user
        /// </summary>
        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// Active inactive status
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// User role assigned to the user
        /// </summary>
        public Int16 UserRoleID
        {
            get { return userRoleID; }
            set { userRoleID = value; }
        }

        /// <summary>
        /// Users branch 
        /// </summary>
        public Int32 BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }

        public Int32 SearchOption
        {
            get { return searchOption; }
            set { searchOption = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Public default constructor
        /// </summary>
        public User()
        {
            this.userId = 0;
            this.userName = String.Empty;
            this.userRoleID = 0;
            this.isActive = true;
            this.password = String.Empty;
            this.BranchID = 0;
        }

        #endregion

        #region Public Methods

        #region Save

        public bool Save()
        {
            if (this.UserId > 0)
            {
                return (new UsersDAO()).UpdateUser(this);
            }
            else
            {
                return (new UsersDAO()).AddUser(this);
            }
        }

        #endregion
                               
        #region Delete 

        public bool Delete()
        {
            try
            {
                return (new UsersDAO()).DeleteUser(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public void DeleteUser(Int64 userid)");
                return false;
                throw ex;
            }
       }
        #endregion

        #region Get User By ID

       public bool GetUserByID()
        {
            try
            {
                return (new UsersDAO()).GetUserByID(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public void DeleteUser(Int64 userid)");
                return false;
                throw ex;
            }
        }
        #endregion

        #region Search User

        public DataSet SearchUsers()
        {
            try
            {
                return (new UsersDAO()).SearchUser(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public DataSet SearchUsers(string _firstname, string _lastname, string _class,string _username, bool _status)");
                return null;
                throw ex;
            }
        }

        #endregion
                
        #region Authenticate login

        public bool CheckLogin()
        {
            bool temp = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_CheckUserLogin);

                db.AddInParameter(dbCommand, "@vcUserName", DbType.String, this.UserName);
                db.AddInParameter(dbCommand, "@vcPassword", DbType.String, this.Password);

                IDataReader reader = db.ExecuteReader(dbCommand);

                if (reader.Read())
                {
                    this.UserId = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : Int32.Parse("0");
                    this.UserName = reader["UserName"] != DBNull.Value ? Convert.ToString(reader["UserName"]) : string.Empty;
                    this.FirstName = reader["FirstName"] != DBNull.Value ? Convert.ToString(reader["FirstName"]) : string.Empty;
                    this.LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : string.Empty;
                    this.Password = reader["Password"] != DBNull.Value ? Convert.ToString(reader["Password"]) : string.Empty;
                    this.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false;
                    this.UserRoleID = reader["RoleId"] != DBNull.Value ? short.Parse(reader["RoleId"].ToString()) : short.Parse("0");
                    this.BranchID = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"]) : Int32.MinValue;
                    temp = this.IsActive;
                }



            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public User CheckLogin()");
                throw ex;
            }
            this.AddLogEntry(temp);
            return temp;
        }

        #endregion

        #region Enter entry log

        public void AddLogEntry(bool trueFalse)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_InsLoginLog);

                db.AddInParameter(dbCommand, "@vcUserName", DbType.String, this.UserName);
                db.AddInParameter(dbCommand, "@vcPassword", DbType.String, this.Password);
                db.AddInParameter(dbCommand, "@bLogin", DbType.Boolean, trueFalse);
                db.ExecuteNonQuery(dbCommand);
 
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", this.GetType().ToString() + Constant.Error_Seperator + "public User AddLogEntry()");
                throw ex;
            }
        }

        #endregion

        #endregion

    }
}