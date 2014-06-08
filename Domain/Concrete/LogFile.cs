using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class LogFile : ILog
    {
        private StreamWriter _log;
        public LogFile(string path)
        {
            _log = new StreamWriter(path, true);
            _log.AutoFlush = true;
        }
        public void Close()
        {
            _log.Flush();
            _log.Close();
            _log.Dispose();
        }
        public void WriteVerbose(string info)
        {
            _log.WriteLine(info);
        }

        public void WriteInfo(string info)
        {
            _log.WriteLine(info);
        }

        public void WriteError(string info, Exception ex)
        {
            _log.WriteLine(string.Format("{0}:{1}", info, ex.Message));
        }
    }
}
