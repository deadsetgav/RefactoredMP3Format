using Domain.Interfaces;
using Id3LibTagAdapter;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class DBArtist : IArtist
    {
        private List<IAlbum> _albums;

        public DBArtist(string artistName)
        {
            this.Name = artistName;
        }

        public string Name { get; private set; }
    
        public List<IAlbum> Albums 
        {
            get { return _albums; }
        }

        public void AddAlbum(IAlbum album)
        {
            _albums.Add(album);
        }

        public void AddAlbums(IAlbum[] albums)
        {
            _albums.AddRange(albums);
        }
    }

    public class DBAlbum : IAlbum
    {
       
        public string Title {get;set;}
        public string ArtistName {get;set;}
        public string ReleaseYear {get;set;}
        public string Source{get;set;}
        public int BitRate { get; set; }
 
        private IMp3[] _tracks;
        public IMp3[] Tracks()
        {
            return _tracks;
        }
        public void SetTracks(IMp3[] mp3s) 
        {
            _tracks = mp3s;
        }
        public System.IO.FileInfo[] GetAdditionalFiles
        {
            get {  return new List<FileInfo>().ToArray();   }          
        }

        public string GetAlbumYearFromTracks()
        {
            return ReleaseYear;
        }

        public int GetAverageBitRateFromTracks()
        {
            return BitRate;
        }
      
    
    }
    
    public class DBTrack : IMp3
    {
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Track { get; set; }
        public string Year { get; set; }
        public int BitRate { get; set; }
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public void Save()
        {
            //do nothing
        }

    }
}
