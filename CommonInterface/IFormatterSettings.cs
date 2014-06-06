using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISettings
    {
        string SourceDirectoryPath { get; }
        string OutputDirectoryPath { get; }
    }

    public interface IFormatterSettings : ISettings
    {
        bool FixTags { get; }
        FormatStyle Format { get; }
        CopyType CopyOrMove { get; }
    }

    public enum CopyType
    {
        Move,
        Copy
    }

    public enum FormatStyle
    {
        Gav,
        Pete
    }

}
