using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Id3LibTagAdapter;
using System.Collections;

namespace Domain.Concrete
{
    public class Album : IAlbum
    {
        private string _title;
        private string _artistName;
        private string _releaseYear;
        private List<IMp3> _lazyTrackList;
        private IAlbumFileReader _reader;
        
        public Album(string title, string artistName, IAlbumFileReader Reader)
        {
            _title = title;
            _artistName = artistName;
            _reader = Reader;
            _releaseYear = string.Empty;
        }
        
        public string Title
        {
            get { return _title; }
        }

        public string ArtistName
        {
            get { return _artistName; }
        }

        public string Source
        {
            get { return _reader.Source; }
        }

        public string ReleaseYear
        {
            get
            {
                if (string.IsNullOrEmpty(_releaseYear))
                {
                    _releaseYear = GetAlbumYearFromTracks();
                }
                return _releaseYear;
            }

            set
            {
                _releaseYear = value;
            }
        }
       
        public IMp3[] Tracks()
        {
            if (_lazyTrackList == null)
            {
                _lazyTrackList = _reader.GetAlbumTracks();
            }

            return _lazyTrackList.ToArray();
        }

        public FileInfo[] GetAdditionalFiles
        {
            get { return _reader.GetAdditionalFiles(); }
        }

        public string GetAlbumYearFromTracks()
        {
            return AlbumReader.GetMostCommonYearFromTracks(this);
        }

        public int GetAverageBitRateFromTracks()
        {
            return AlbumReader.GetMostCommonBitRateFromTracks(this);
        }

    }
}
