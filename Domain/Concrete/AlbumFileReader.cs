using Domain.Interfaces;
using TagLibTagAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterface;
using System.Text.RegularExpressions;

namespace Domain.Concrete
{
    public class AlbumFileReader : IAlbumFileReader
    {
        private DirectoryInfo _source;

        public AlbumFileReader(string path)
        {
            _source = new DirectoryInfo(path);
        }

        public List<IMp3> GetAlbumTracks()
        {
            var fileList = MusicDirectoryReader.GetMusicFilesFromFolder(_source.FullName);
            var trackList = new List<IMp3>();
            foreach (var file in fileList)
            {
                var track = Mp3Adapter.GetMp3(file.FullName);
                trackList.Add(track);
            }

            return trackList;
        }

        public FileInfo[] GetAdditionalFiles()
        {
            var fileList = MusicDirectoryReader.GetPictureFilesFromFolder(_source.FullName);
            return fileList.ToArray();
        }

        public string Source
        {
            get { return _source.FullName; }
        }
    }

    public class AlbumReader
    {
        public static string GetMostCommonAlbumTitleFromTracks(IAlbum album)
        {
            var albumQuery = album.Tracks().Select(f => f.Album);
            return GetCommonString(albumQuery);
        }

        public static string GetMostCommonAlbumArtistFromTracks(IAlbum album)
        {
            var albumArtistQuery = album.Tracks().Select(f => f.AlbumArtist);
            return GetCommonString(albumArtistQuery);
        }

        public static string GetMostCommonYearFromTracks(IAlbum album)
        {
            var yearsQuery = album.Tracks().Select(f => f.Year);
            return GetCommonString(yearsQuery);
        }

        public static string GetMostCommonArtistNameFromTracks(IAlbum album)
        {
            var artistQuery = album.Tracks().Select(f => f.Artist);
            return GetCommonString(artistQuery);
        }

        public static int GetMostCommonBitRateFromTracks(IAlbum album)
        {
            var bitRate = album.Tracks().Select(f => f.BitRate);
            var query = bitRate.GroupBy(item => item).
                OrderByDescending(g => g.Count()).
                Select(g => g.Key).First();

            return Convert.ToInt32(query);
        }

        private static string GetCommonString(IEnumerable<string> searchlist)
        {
            var query = searchlist.GroupBy(item => item).
                    OrderByDescending(g => g.Count()).
                    Select(g => g.Key).First();

            return query.ToString();
        }

        //public static bool StringsCloselyMatch(string first, string second) 
        //{
        //    return (DamerauLevenshtein.Distance_IgnoreCase(first, second) <= 2);
        //}
    }

    public class ArtistReader
    {
        public static string GetMostCommonArtistNameFromAlbums(IArtist artist)
        {
            var tracks = new List<IMp3>();
            foreach (IAlbum album in artist.Albums)
            {
                tracks.AddRange(album.Tracks());
            }

            var artistName = tracks.Select(f => f.Artist);
            var query = artistName.GroupBy(item => item).
                    OrderByDescending(g => g.Count()).
                    Select(g => g.Key).First();

            return query.ToString();
        }
    }

    
}
