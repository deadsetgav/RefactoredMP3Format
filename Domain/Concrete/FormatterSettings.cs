using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Domain.Exceptions;

namespace Domain
{
    public class FormatterSettings : IFormatterSettings
    {

        private string _sourcePath;
        private string _outPath;

        public FormatterSettings()
        {
                       
        }

        public string SourceDirectoryPath 
        {
            get { return _sourcePath; }  
            set { _sourcePath = CheckValidSourcePath(value); }
        }
        public string OutputDirectoryPath
        {
            get { return _outPath;}
            set { _outPath = CheckValidOutPath(value); }
        }
        public bool FixTags { get; set; }
        public FormatStyle Format { get; set; }    
        public CopyType CopyOrMove { get; set; }
       
        private string CheckValidSourcePath(string value)
        {
            if(value != String.Empty && Directory.Exists(value))
            {
                return  value;
            }
            else
            {
                throw new SettingNotValidException("SourceDirectoryPath", value);
            }
        }
        private string CheckValidOutPath(string path)
        {
            if (path == string.Empty)
            {
                throw new SettingNotValidException("OutputDirectoryPath", path);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
     
   

    }

 
}
