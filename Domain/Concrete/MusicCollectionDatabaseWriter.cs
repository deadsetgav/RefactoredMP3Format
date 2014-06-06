using CommonInterface;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class MusicCollectionDatabaseWriter
    {
        private IMusicCollection _collection;
        private IDataAccess _dataAccess;

        public MusicCollectionDatabaseWriter(IMusicCollection collection, IDataAccess dataAccess)
        {
            _collection = collection;
            _dataAccess = dataAccess;
        }

        public void WriteCollectionToDatabase()
        {
            Log.WriteInfoToLog("Starting Database Write");
            ScanItemsIntoCollectionIfEmpty();

            foreach (IArtist artist in _collection.Artists)
            {
                foreach (IAlbum album in artist.Albums)
                {
                    PutAlbumInDatabase(album);
                }
            }
        }
        
        private void ScanItemsIntoCollectionIfEmpty() 
        {
            if (_collection.Artists.Count() == 0)
            {
                _collection.ScanItemsToCollection();
            }
        }

        private void PutAlbumInDatabase(IAlbum album)
        {  
            try
            {
                if (! _dataAccess.AlbumExists(album.Title, album.ArtistName)) 
                { 
                    _dataAccess.SaveAlbum(album);
                }
                
            }
            catch (Exception ex)
            {
                Log.WriteErrorToLog(string.Format("failed to write {0}: {1}",
                    album.ArtistName, album.Title), ex);
            }
        }
      
    }
}
