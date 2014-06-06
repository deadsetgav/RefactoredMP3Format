using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class Log : ILog
    {
        private static ILog _log;

        public static void SetLog(ILog log)
        {
            _log = log;
        }

        public static ILog GetInstance()
        {
            if (_log != null)
            {
                return _log; 
            } 
            else 
            {
                return new Log(); 
            }
        }
    
        public static void WriteDetailToLog(string message, params string[] details)
        {
            WriteDetailToLog(string.Format(message, details));
        }
        public static void WriteDetailToLog(string info)
        {
            GetInstance().WriteVerbose(info); 
        }
        public static void WriteInfoToLog(string message, params string[] details)
        {
            WriteInfoToLog(string.Format(message, details));
        }
        public static void WriteInfoToLog(string info)
        {
            GetInstance().WriteInfo(info);
        }
        public static void WriteErrorToLog(string info, Exception ex)
        {
            GetInstance().WriteError(info, ex);
        }

        public void WriteVerbose(string info)
        {
            if (_log != null)
            {
                _log.WriteVerbose(info);
            }
        }

        public void WriteInfo(string info)
        {
            if (_log != null)
            {
                _log.WriteInfo(info);
            }
        }

        public void WriteError(string info, Exception ex)
        {
            if (_log != null)
            {
                _log.WriteError(info, ex);
            }
        }
    }
}
