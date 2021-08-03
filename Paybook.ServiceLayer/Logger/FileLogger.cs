using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Paybook.ServiceLayer.Logger
{

    public class FileLogger : ILogger
    {

        #region Design Pattern: Singleton (Single Instance)

        private FileLogger() { }
        private static readonly Lazy<ILogger> _Instance = new Lazy<ILogger>(() => new FileLogger());

        public static ILogger Instance
        {
            get
            {
                return _Instance.Value;
            }
        }


        #endregion


        private readonly DateTime _dateTime = DateTime.Now;
        private readonly object lockStreamWriter = new object();

        private InputModel _inputModel;

        public string MethodName
        {
            get
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                return string.Format("{0}.{1}", m.ReflectedType.Name, m.Name);
            }
        }

        public void LogInformation(string message)
        {
            try
            {
                string directory = CreateDirectory($"~/_Documents/Logs/{_dateTime.ToString("yyyyMM")}");
                string path = CreateFile(directory);
                WriteLogMessageToFile(path, $"Ezzy: {_dateTime.ToString("dd.MM.yyyy hh:mm:ss tt")} \t{LoggerExceptionType.Information.ToString().ToUpper()} \t{HttpContext.Current.Request.Url.ToString()} \t{message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LogError(string methodName, Exception ex)
        {
            _inputModel = new InputModel()
            {
                HostIP = HttpContext.Current.Request.UserHostAddress,
                Project = "Ezzy",
                Url = HttpContext.Current.Request.Url.ToString(),
                FileName = "", // " | Page : " + _HttpRequest.GetHttpRequestCurrent().PhysicalApplicationPath;

                MethodName = methodName,
                LineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7).Replace("line", "").Replace(" ", ""),
                Type = ex.GetType().ToString(),
                ExceptionType = LoggerExceptionType.Error.ToString(),

                Message = ex.Message.ToString(),
                //StackTrace = ex.StackTrace.ToString(),
            };


            try
            {
                string directory = CreateDirectory($"~/_Documents/Logs/{_dateTime.ToString("yyyyMM")}");
                string path = CreateFile(directory);
                WriteLogMessageToFile(path, LoggerMessageFormat(_inputModel));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WriteLogMessageToFile(string path, string message)
        {
            lock (lockStreamWriter)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);

                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private string LoggerMessageFormat(InputModel _model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("-----------------Exception occured on {0}-----------------", _dateTime.ToString("dd.MM.yyyy hh:mm:ss tt"));
            sb.AppendLine();

            if (!string.IsNullOrEmpty(_model.HostIP))
                sb.Append("Host IP : " + _model.HostIP);
            if (!string.IsNullOrEmpty(_model.Project))
                sb.Append(" | Project : " + _model.Project);
            if (!string.IsNullOrEmpty(_model.Url))
                sb.Append(" | Url : " + _model.Url);
            if (!string.IsNullOrEmpty(_model.FileName))
                sb.Append(" | File Name : " + _model.FileName);

            sb.AppendLine();

            if (!string.IsNullOrEmpty(_model.MethodName))
                sb.Append("Method Name : " + _model.MethodName);
            if (!string.IsNullOrEmpty(_model.LineNumber))
                sb.Append(" | Line# : " + _model.LineNumber);
            if (!string.IsNullOrEmpty(_model.Type))
                sb.Append(" | Type : " + _model.Type);
            if (!string.IsNullOrEmpty(_model.ExceptionType))
                sb.Append(" | Exception Type : " + _model.ExceptionType);

            sb.AppendLine();

            if (!string.IsNullOrEmpty(_model.Message))
                sb.AppendLine("Message : " + _model.Message);
            if (!string.IsNullOrEmpty(_model.StackTrace))
                sb.AppendLine(_model.StackTrace);

            return sb.ToString();
        }

        private string CreateFile(string path)
        {
            string returnPath = $"{path}/Log_{_dateTime.ToString("yyyyMMdd")}.txt";   //Text File Name
            if (!File.Exists(returnPath))
            {
                File.Create(returnPath).Dispose();
            }

            return returnPath;
        }

        private string CreateDirectory(string directory)
        {
            directory = HttpContext.Current.Server.MapPath(directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

    public enum LoggerExceptionType
    {
        Information = 0, //Please enter your name
        Warning = 1, //Tried 3 times login
        Error = 2, //Exception occured
        Fatal = 3 //Database not connected
    }

    public class InputModel
    {
        public string HostIP { get; set; }
        public string Project { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
        public string LineNumber { get; set; }
        public string Type { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
