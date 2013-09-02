namespace LankaTiles.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data;
    using System.Collections;
    using LankaTiles.UserManagement;

    public class LankaTilesExceptions
    {

        #region Log Error and exception messages to database

        public static String WriteEventLogs(System.Exception ex, String databaseConnection, String userName)
        {
            try
            {
                String[] formatMessge = FormatMessage(ex.ToString());
                Database db = DatabaseFactory.CreateDatabase(databaseConnection);

                String functionName = String.Empty;
                String className = String.Empty;

                GetMethodName(ex, out className, out functionName);

                //For Insert Event data.
                string sqlEventLog = "uspInsertLogMessages";
                DbCommand dbCommandInsertEventLog = db.GetStoredProcCommand(sqlEventLog);

                db.AddInParameter(dbCommandInsertEventLog, "@vcUser", DbType.String, userName);
                db.AddInParameter(dbCommandInsertEventLog, "@vcClassName", DbType.String, className);
                db.AddInParameter(dbCommandInsertEventLog, "@vcFunctionName", DbType.String, functionName);
                db.AddInParameter(dbCommandInsertEventLog, "@vcMessage1", DbType.String, formatMessge[0]);
                db.AddInParameter(dbCommandInsertEventLog, "@vcMessage2", DbType.String, formatMessge[1]);
                db.AddInParameter(dbCommandInsertEventLog, "@vcMessage3", DbType.String, formatMessge[2]);
                db.AddInParameter(dbCommandInsertEventLog, "@vcMessage4", DbType.String, formatMessge[3]);
                db.AddOutParameter(dbCommandInsertEventLog, "@nLogId", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCommandInsertEventLog);

                return db.GetParameterValue(dbCommandInsertEventLog, "@nLogId").ToString();
            }
            catch (System.Exception exc)
            {
                (new LogMessages()).LogTransaction((new User()), exc.ToString());
                return String.Empty;
            }
            finally
            {
                //If this component pass the exception ,need to log the messge by mail or write on the txt file.
            }
        }

        #endregion Log Error and exception messages to database

        #region Format Message

        private static String[] FormatMessage(String message)
        {
            try
            {
                if (message.Length > 12000)
                {
                    String[] errorMessage =  { message.Substring(0, 4000), message.Substring(3999, 8000), message.Substring(7999, 12000), message.Substring(11999, message.Length - 12000) };
                    return errorMessage;
                }
                else if (message.Length > 8000)
                {
                    String[] errorMessage =  { message.Substring(0, 4000), message.Substring(3999, 8000), message.Substring(7999, message.Length - 8000), string.Empty };
                    return errorMessage;
                }
                else if (message.Length > 4000)
                {
                    String[] errorMessage =  { message.Substring(0, 4000), message.Substring(3999, message.Length - 4000), string.Empty, string.Empty };
                    return errorMessage;
                }
                else
                {
                    String[] errorMessage =  { message.Substring(0, message.Length), string.Empty, string.Empty, string.Empty };
                    return errorMessage;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        #endregion

        #region Get Method Name

        private static void GetMethodName(System.Exception ex, out String className, out String functionName)
        {
            functionName = String.Empty;
            className = String.Empty;
            foreach (DictionaryEntry de in ex.Data)
            {
                if (de.Key.ToString() == "BusinessLayerException")
                {
                    String[] value = de.Value.ToString().Split('|');
                    className = value[0].ToString();
                    functionName = value[1].ToString();
                    break;
                }
                if (de.Key.ToString() == "UILayerException")
                {
                    String[] value = de.Value.ToString().Split('|');
                    className = value[0].ToString();
                    functionName = value[1].ToString();
                    break;
                }
            }


        }

        #endregion

    }
}
