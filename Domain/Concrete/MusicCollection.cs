using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Domain.Concrete
{
    public class MusicCollection : IMusicCollection
    {

        protected string _filePath;
        protected Dictionary<string, IArtist> _artistCollection;

        public MusicCollection(string collectionFilePath)
        {
            this._filePath = collectionFilePath;
            this._artistCollection = new Dictionary<string, IArtist>();
        }
        
        public string CollectionFilePath
        {
            get { return _filePath; }
        }

        public IArtist[] Artists
        {
            get { return _artistCollection.Values.ToArray(); }
        }

        public void ScanItemsToCollection()
        {
            var sourceFolder = new DirectoryInfo(_filePath);
            Log.WriteDetailToLog("Reading source folder: {0}", _filePath);
            foreach (var subFolder in sourceFolder.GetDirectories())
            {
                try
                {
                    var artist = MusicDirectoryReader.GetArtistsAlbumsFromDirectory(subFolder.FullName);
                    AddArtistToCollection(artist);
                }
                catch (Exception ex)
                {
                    Log.WriteErrorToLog(string.Format("Failed to scan source folder {0}",subFolder.FullName), ex);
                }
            }
            Log.WriteDetailToLog("Finished reading from source folder");
        }

        private void AddArtistToCollection(IArtist artist)
        {
            var artistKey = artist.Name.ToLower().Trim();
            if (_artistCollection.ContainsKey(artistKey))
            {
                _artistCollection[artistKey].AddAlbums(artist.Albums.ToArray());
            }
            else
            {
                _artistCollection.Add(artistKey, artist);
            }
        }
    }
}
