using Domain.Concrete;
using Domain.HelperClasses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Facade
{
    public abstract class FacadeBase
    {
        public const string VERSION = "v0.5";
        protected ISettings settings { get; set; }

        public FacadeBase(ISettings setting)
        {
            settings = setting;
        }
        
        public ILog ProcessLog
        {
            get { return Log.GetInstance(); }
            set { Log.SetLog(value); }
        }

        public abstract void Process();
   

        public IMusicCollection ReadSourceDirectory()
        {
            var fileReader = new CollectionFileReader();
            return fileReader.ReadCollection(settings.SourceDirectoryPath);
        }

    }
}
