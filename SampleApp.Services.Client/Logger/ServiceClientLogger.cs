using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace SampleApp.Services.Client.Logger
{
    public class ServiceClientLogger
    {
        #region Member variables and Properties

        private const int LogExceptionLevel = 5;
        private const int ObjectSerilizerRecursionLimit = 3;
        private readonly string NewLine = Environment.NewLine;

        //private ILogger logger;

        //private ILogger Logger
        //{
        //    get
        //    {
        //        if (logger == null)
        //            logger = LogManager.GetLogger("DM.Services.Client");

        //        return logger;
        //    }
        //}

        #endregion

        #region Public Methods

        public void LogError(Exception ex)
        {
            var sb = new StringBuilder();

            short level = 0;

            while (ex != null)
            {
                sb.Append(ex.Message);
                ex = ex.InnerException;
                if (level++ > LogExceptionLevel)
                    break;
            }
            WriteToLog(sb.ToString(), LogType.Error);
        }

        public void LogMethoInfo()
        {
            try
            {
                LogMethoInfoImpl(new object[] {});
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message + ex.StackTrace, LogType.LoggerError);
            }
        }

        public void LogMethoInfo(object parameterValue)
        {
            try
            {
                LogMethoInfoImpl(new[] {parameterValue});
            }
            catch (Exception ex)
            {
                //WriteToLog(ex.Message + ex.StackTrace, LogType.LoggerError);
            }
        }

        public void LogMethoInfo(object[] parameterValues)
        {
            try
            {
                LogMethoInfoImpl(parameterValues);
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message + ex.StackTrace, LogType.LoggerError);
            }
        }

        public void LogReturnValueInfo(object returnValue)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat("Returns:{0}", GetObjectString(returnValue));
                WriteToLog(sb.ToString(), LogType.Info);
            }
            catch (Exception ex)
            {
                // WriteToLog(ex.Message + ex.StackTrace, LogType.LoggerError);
            }
        }

        #endregion

        #region  Private Methods

        // Keep it private and seperatement because it is using stackTrace.GetFrame(2).GetMethod();
        private void LogMethoInfoImpl(object[] parameterValues)
        {
            var stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
            ParameterInfo[] parameters = methodBase.GetParameters();

            var sb = new StringBuilder();

            sb.AppendFormat("Method:{0}(", methodBase.Name);

            for (int i = 0; i < parameterValues.Length; i++)
            {
                sb.AppendFormat("{0}={1}{2}", parameters[i].Name, GetObjectString(parameterValues[i]),
                                i == parameterValues.Length - 1 ? "" : NewLine);
            }
            sb.Append(")");
            WriteToLog(sb.ToString(), LogType.Info);
        }

        private string GetObjectString(object paraemeterValue)
        {
            if (paraemeterValue == null)
                return "null";

            if (IsBasicType(paraemeterValue.GetType()))
                return paraemeterValue.ToString();

            var serializer = new JavaScriptSerializer();

            // Todo: recurstion limit is not working
            //serializer.RecursionLimit = ObjectSerilizerRecursionLimit;

            return serializer.Serialize(paraemeterValue);
        }

        //public bool IsCollection(Type T)
        //{
        //    return typeof(ICollection).IsAssignableFrom(T) || typeof(ICollection<>).IsAssignableFrom(T);
        //}

        //private string GetObjectPropertyString(object parameter1)
        //{
        //    return parameter1.ToString();
        //}

        private bool IsBasicType(Type type)
        {
            // TODO: Handle enum type and may be some more basic types
            return type.IsPrimitive || type == typeof (string) || type == typeof (DateTime);
        }

        private void WriteToLog(string logInfoString, LogType logType)
        {
            switch (logType)
            {
                case LogType.Info:
                    //Logger.Info(logInfoString);
                    break;
                case LogType.Debug:
                    //Logger.Debug(logInfoString);
                    break;
                case LogType.Error:
                    //Logger.Error(logInfoString);
                    break;
                case LogType.LoggerError:
                    //Logger.Error("Logger threw an error");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("logType");
            }

            // todo: need to remove
            Console.WriteLine(logInfoString);
        }

        #endregion

        #region ToDecide

        // TODO: temporaty- need to revmoe
        public enum LogType
        {
            Info = 0,
            Debug = 1,
            Error = 2,
            LoggerError = 3
        }

        #endregion
    }
}