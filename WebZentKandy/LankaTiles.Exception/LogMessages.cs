namespace LankaTiles.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.IO;
    using LankaTiles.Common;
    using LankaTiles.UserManagement;

    public class LogMessages
    {
        private User LogUser;
        public void LogTransaction(User userTemp, String logMessage)
        {
            // todo:
            // needs to write to the log file
            if (Constant.LogExecutionTimes == "1")
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;
                
                try
                {
                    string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-mm-yy") + "LogExecutionTimes.txt";
                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                    {
                        File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                    }
                    using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                    {
                        w.WriteLine("\r\nLog Entry : ");
                        w.WriteLine("{0}", DateTime.Now.ToString());
                        string err = logMessage;
                        w.WriteLine(err);
                        w.WriteLine("__________________________");
                        w.Flush();
                        w.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    this.LogExecutionTime(ex.Message);
                }
            }
        }

        public void LogExecutionTime(string methodName)
        {
            if (Constant.LogExecutionTimes == "1")
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;
                //String logMessage = String.Format("Page : {0}\r\nMothod : {1}\r\nStart Time : {2}\r\nEnd Time : {3}\r\nTime Spent : {4}", sRet, methodName, StartTime, EndTime, TotalTime.Milliseconds);

                try
                {
                    string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-mm-yy") + "LogExecutionTimes.txt";
                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                    {
                        File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                    }
                    using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                    {
                        w.WriteLine("\r\nLog Entry : ");
                        w.WriteLine("{0}", DateTime.Now.ToString());
                        string err = "Todo: this should be descriptive";
                        w.WriteLine(err);
                        w.WriteLine("__________________________");
                        w.Flush();
                        w.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    this.LogExecutionTime(ex.Message);
                }
            }
        }
    }
}
