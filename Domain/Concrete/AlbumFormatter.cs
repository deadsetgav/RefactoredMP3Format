using CommonInterface;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class AlbumFormatFactory
    {
        public static IAlbumFormatter GetAlbumFormatter(FormatStyle style)
        {
            switch (style)
            {
                case FormatStyle.Gav: return new GavAlbumFormatter();

                case FormatStyle.Pete:  return new PeteAlbumFormatter();

                default: throw new ApplicationException(string.Format("Unknown album format style: {0}",style));        
  
            }
        }
    }
    
    public abstract class AlbumFormatterBase :IAlbumFormatter
    {
        protected AlbumFormatterBase()
        {
        }
       
        public abstract DirectoryInfo GetFolderToWriteTo(string writeFolder, IArtist artist, IAlbum album);
        
        public abstract void FormatMp3Tags(IMp3 track, IAlbum album);

        protected static bool TitleStartsWithTrackNumber(string title)
        {
            var reg = new Regex("^([0-9]*)(\\s)-(\\s)");
            var match = reg.Match(title);
            return match.Success;
        }

        protected static string TrimTrackFromTitle(string title)
        {
            var reg = new Regex("^([0-9]*)(\\s)-(\\s)");
            var match = reg.Match(title);
            return title.Remove(match.Index, match.Length);
        }
    }

    public class GavAlbumFormatter :AlbumFormatterBase
    {
        
        public override DirectoryInfo GetFolderToWriteTo(string writeFolder, IArtist artist, IAlbum album)
        {
            var writePath = Path.Combine(writeFolder, artist.Name, album.Title);
            if (!Directory.Exists(writePath))
            {
                Log.WriteDetailToLog("Creating folder: {0}", writePath);
                Directory.CreateDirectory(writePath);
            }
            return new DirectoryInfo(writePath);
        }

        public override void FormatMp3Tags(IMp3 mp3, IAlbum album)
        {
            if (TitleStartsWithTrackNumber(mp3.Title))
            {
                mp3.Title = TrimTrackFromTitle(mp3.Title);
            }
            if (mp3.AlbumArtist != mp3.Artist)
            {
                mp3.AlbumArtist = mp3.Artist;
            }
            if (mp3.Track.StartsWith("0"))
            {
                mp3.Track = mp3.Track.TrimStart('0');
            }
            if (string.IsNullOrEmpty(mp3.Year))
            {
                mp3.Year = album.ReleaseYear;
            }
            mp3.Save();

        }
        
    }

    public class PeteAlbumFormatter : AlbumFormatterBase
    {

        public override DirectoryInfo GetFolderToWriteTo(string writeFolder, IArtist artist, IAlbum album)
        {
            if (album.ReleaseYear != string.Empty)
            {
                return CreateArtistYearAlbumFormat(writeFolder,artist, album);
            }
            else
            {
                return CreateArtistAlbumFormat(writeFolder,artist, album);
            }
        }

        private DirectoryInfo CreateArtistYearAlbumFormat(string writeFolder, IArtist artist, IAlbum album)
        {
            var albumFolderName = string.Format("{0} - {1} - {2}", artist.Name, album.ReleaseYear.ToString(), album.Title);
            var fullPath = Path.Combine(writeFolder, albumFolderName);
            return CreateDirectory(fullPath);
        }

        private DirectoryInfo CreateArtistAlbumFormat(string writeFolder, IArtist artist, IAlbum album)
        {
            var albumFolderName = string.Format("{0} - {1}", artist.Name, album.Title);
            var fullPath = Path.Combine(writeFolder, albumFolderName);
            return CreateDirectory(fullPath);
        }

        private DirectoryInfo CreateDirectory(string writePath)
        {
            if (!Directory.Exists(writePath))
            {
                Log.WriteDetailToLog("Creating folder: {0}", writePath);
                Directory.CreateDirectory(writePath);
            }
            return new DirectoryInfo(writePath);
        }

        public override void FormatMp3Tags(IMp3 mp3, IAlbum album)
        {
            if (mp3.AlbumArtist != mp3.Artist)
            {
                mp3.AlbumArtist = mp3.Artist;
            }
            if (!TitleStartsWithTrackNumber(mp3.Title))
            {
                mp3.Title = string.Format("{0} - {1}", mp3.Track, mp3.Title);
            }
            if (string.IsNullOrEmpty(mp3.Year))
            {
                mp3.Year = album.ReleaseYear;
            }

            mp3.Save();

        }
      
    }

}
