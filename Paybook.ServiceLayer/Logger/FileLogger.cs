using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Paybook.ServiceLayer.Services;

namespace Paybook.ServiceLayer.Logger
{
    public class FileLogger : ILogger
    {
        private readonly object lockStreamWriter = new object();
        private readonly IDateTimeProvider _dateTimeProvider;

        public FileLogger(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }


        public string GetMethodName()
        {
            StackFrame stackFrame = new StackFrame();
            MethodBase methodBase = stackFrame.GetMethod();

            //MethodBase m = MethodBase.GetCurrentMethod();
            return $"{methodBase.ReflectedType.Name}.{methodBase.Name}";
        }

        public void Info(string message)
        {
            string exceptionType = LoggerExceptionType.Information.ToString().ToUpper();
            Uri currentUri = HttpContext.Current.Request.Url;
            string messageFormat = $"Ezzy: {_dateTimeProvider.Now:dd.MM.yyyy hh:mm:ss tt} \t{exceptionType} \t{currentUri} \t{message}";

            WriteMessageToFile(messageFormat);
        }

        public void Error(string methodName, Exception ex)
        {
            InputModel inputModel = new InputModel()
            {
                HostIP = HttpContext.Current.Request.UserHostAddress,
                Url = HttpContext.Current.Request.Url.ToString(),
                FileName = "", // " | Page : " + _HttpRequest.GetHttpRequestCurrent().PhysicalApplicationPath;

                MethodName = methodName,
                LineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7).Replace("line", "").Replace(" ", ""),
                Type = ex.GetType().ToString(),
                ExceptionType = LoggerExceptionType.Error.ToString(),

                Message = ex.Message.ToString(),
                StackTrace = ex.StackTrace.ToString(),
            };

            WriteMessageToFile(inputModel.LoggerMessageFormat(_dateTimeProvider.Now));
        }

        private void WriteMessageToFile(string message)
        {
            lock (lockStreamWriter)
            {
                string path = GetFilePath();
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);

                    sw.Flush();
                    sw.Close();
                }
            }
        }


        private string GetFilePath()
        {
            string directoryPath = CreateDirectory($"~/_Documents/Logs/{_dateTimeProvider.Now:yyyyMM}");
            string filePath = $"{directoryPath}/Log_{_dateTimeProvider.Now:yyyyMMdd}.txt";   //Text File Name

            if (File.Exists(filePath))            
                return filePath;            

            File.Create(filePath).Dispose();
            return filePath;
        }

        private string CreateDirectory(string directory)
        {
            directory = HttpContext.Current.Server.MapPath(directory);

            if (Directory.Exists(directory))            
                return directory;            

            Directory.CreateDirectory(directory);
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
        public string Project { get; set; } = "Ezzy";
        public string Url { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
        public string LineNumber { get; set; }
        public string Type { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string LoggerMessageFormat(DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("-----------------Exception occured on {0}-----------------", dateTime.ToString("dd.MM.yyyy hh:mm:ss tt"));
            sb.AppendLine();

            if (!string.IsNullOrEmpty(HostIP))
                sb.Append("Host IP : " + HostIP);
            if (!string.IsNullOrEmpty(Project))
                sb.Append(" | Project : " + Project);
            if (!string.IsNullOrEmpty(Url))
                sb.Append(" | Url : " + Url);
            if (!string.IsNullOrEmpty(FileName))
                sb.Append(" | File Name : " + FileName);

            sb.AppendLine();

            if (!string.IsNullOrEmpty(MethodName))
                sb.Append("Method Name : " + MethodName);
            if (!string.IsNullOrEmpty(LineNumber))
                sb.Append(" | Line# : " + LineNumber);
            if (!string.IsNullOrEmpty(Type))
                sb.Append(" | Type : " + Type);
            if (!string.IsNullOrEmpty(ExceptionType))
                sb.Append(" | Exception Type : " + ExceptionType);

            sb.AppendLine();

            if (!string.IsNullOrEmpty(Message))
                sb.AppendLine("Message : " + Message);
            if (!string.IsNullOrEmpty(StackTrace))
                sb.AppendLine(StackTrace);

            return sb.ToString();
        }
    }
}
