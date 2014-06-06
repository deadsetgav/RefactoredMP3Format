using Domain.Interfaces;
using Id3LibTagAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool StringsCloselyMatch(string first, string second) 
        {
            return (DamerauLevenshtein.Distance_noCase(first, second) <= 2);
        }
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


    public class DamerauLevenshtein
    {
        public static int Distance_noCase(string source, string target)
        {
            return Distance(source.ToLower().Trim(), target.ToLower().Trim());
        }
        
        public static int Distance(string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    return 0;
                }
                else
                {
                    return target.Length;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            var score = new int[source.Length + 2, target.Length + 2];

            var INF = source.Length + target.Length;
            score[0, 0] = INF;
            for (var i = 0; i <= source.Length; i++) { score[i + 1, 1] = i; score[i + 1, 0] = INF; }
            for (var j = 0; j <= target.Length; j++) { score[1, j + 1] = j; score[0, j + 1] = INF; }

            var sd = new SortedDictionary<char, int>();
            foreach (var letter in (source + target))
            {
                if (!sd.ContainsKey(letter))
                    sd.Add(letter, 0);
            }

            for (var i = 1; i <= source.Length; i++)
            {
                var DB = 0;
                for (var j = 1; j <= target.Length; j++)
                {
                    var i1 = sd[target[j - 1]];
                    var j1 = DB;

                    if (source[i - 1] == target[j - 1])
                    {
                        score[i + 1, j + 1] = score[i, j];
                        DB = j;
                    }
                    else
                    {
                        score[i + 1, j + 1] = Math.Min(score[i, j], Math.Min(score[i + 1, j], score[i, j + 1])) + 1;
                    }

                    score[i + 1, j + 1] = Math.Min(score[i + 1, j + 1], score[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }

                sd[source[i - 1]] = i;
            }

            return score[source.Length + 1, target.Length + 1];
        }
    }
}
