using Domain.Concrete;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HelperClasses
{
    public class CollectionFileReader
    {

        public IMusicCollection ReadCollection(string sourceDirectoryPath)
        {
            var sourceFilePath = sourceDirectoryPath;
            var collection = new MusicCollection(sourceFilePath);
            collection.ScanItemsToCollection();
            return collection;
        }
        
    }
}
