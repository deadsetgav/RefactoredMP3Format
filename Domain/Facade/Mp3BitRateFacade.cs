using DataRepository;
using Domain.Concrete;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Facade
{
    public class Mp3BitRateFacade : FacadeBase
    {
        private IFormatterSettings _settings;
       

        public Mp3BitRateFacade(IFormatterSettings settings):base (settings)
        {
            _settings = settings;
        }

        public override void Process()
        {
            Log.WriteInfoToLog("Starting Process()...");

            var dataAccess = new DataAccess("Albums");
            var collection = ReadSourceDirectory();
            
            var dbWriter = new MusicCollectionDatabaseWriter(collection, dataAccess);
            dbWriter.WriteCollectionToDatabase();

            Log.WriteInfoToLog("Finished Process()...");
        }
        
       
    }
}
