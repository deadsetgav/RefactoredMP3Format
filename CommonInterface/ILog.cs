using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILog
    {
        void WriteVerbose(string info);
        void WriteInfo(string info);
        void WriteError(string info, Exception ex);
    }
}
