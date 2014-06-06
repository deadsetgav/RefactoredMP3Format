using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Domain.Interfaces;
using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Concrete
{
    public class MusicDirectoryReader
    {

        private static string[] _musicFileSearchPattern = { "*.mp3", "*.wma","*.flac" };
        private static string[] _albumArtSearchPattern = { "*.jpg", "*.bmp", "*.png", "*.jpeg"};

        public static IArtist GetArtistsAlbumsFromDirectory(string filePath)
        {
            var folder = new DirectoryInfo(filePath);
            Log.WriteDetailToLog("Reading: {0}", folder.Name);
            
            if (HasSubFolders(folder) && !FolderContainsMusicFiles(filePath))
            {
                return ReadFromSubFolders(folder);
            }
            else if (FolderContainsMusicFiles(filePath)) 
            {
                return ReadFromFlatFolder(folder);
            }

            Log.WriteInfoToLog("Skipping folder, don't know how to parse album/artist folder names: {0}", filePath);
            return null;
        }

        private static Boolean HasSubFolders(DirectoryInfo folder)
        {
            return (folder.GetDirectories().Count() > 0);
        }
        
        public static Boolean FolderContainsMusicFiles(string folderPath)
        {
            var directory = new DirectoryInfo(folderPath);
            foreach (string pattern in _musicFileSearchPattern)
            {
                if (directory.GetFiles(pattern, SearchOption.TopDirectoryOnly).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static List<FileInfo> GetMusicFilesFromFolder(string folderPath)
        {
            return GetFilesFromFolder(folderPath, _musicFileSearchPattern);
        }
        
        public static List<FileInfo> GetPictureFilesFromFolder(string folderPath)
        {
            return GetFilesFromFolder(folderPath, _albumArtSearchPattern);
        }
        
        private static List<FileInfo> GetFilesFromFolder(string folderPath, string[] searchPatterns)
        {
            var directory = new DirectoryInfo(folderPath);
            var fileList = new List<FileInfo>();
            foreach (string pattern in searchPatterns)
            {
                var files = directory.GetFiles(pattern, SearchOption.AllDirectories);
                fileList.AddRange(files);
            }
            return fileList;
        }
     
        private static IArtist ReadFromSubFolders(DirectoryInfo artistFolder)
        {
            var artist = new Artist(artistFolder.Name);
            foreach (var albumFolder in artistFolder.GetDirectories())
            {
                if (FolderContainsMusicFiles(albumFolder.FullName))
                {
                    Log.WriteDetailToLog("Reading: {0}",albumFolder.Name);
                    var album =  AlbumFactory.GetAlbum(albumFolder.Name, artist.Name, albumFolder.FullName);
                    artist.AddAlbum(album);
                }
            }  
            return artist;
        }

        private static IArtist ReadFromFlatFolder(DirectoryInfo artistAlbumFolder)
        {
            if (artistAlbumFolder.Name.Contains("-"))
            {
                var split = artistAlbumFolder.Name.Split('-');
                if (split.Count() >= 3)
                {
                    return GetArtistAlbumWithYear(split,artistAlbumFolder);
                }
                else if (split.Count() == 2)
                {
                    return GetArtistAlbum(split,artistAlbumFolder);
                } 
            }
           
            // if we haven't resolved the artist/album info by now...
            throw new InvalidFolderNameFormatException(artistAlbumFolder.FullName);
           
        }

        private static IArtist GetArtistAlbumWithYear(string[] split, DirectoryInfo folder)
        {
            var artist = new Artist(split[0].Trim());
            var year = RemoveBrackets(split[1]);
            var albumName = split[2].Trim();
            for (int i = 3; i < split.Length; i++)
            {
                albumName += " - " + split[i].Trim();
            }
            var album = AlbumFactory.GetAlbum(albumName,artist.Name,folder.FullName );
            album.ReleaseYear = year;
            artist.AddAlbum (album);

            return artist;
        }
              
        private static IArtist GetArtistAlbum(string[] split, DirectoryInfo folder)
        {
            var artist = new Artist(split[0].Trim());
            var album = AlbumFactory.GetAlbum(split[1].Trim(), artist.Name, folder.FullName); 
            artist.AddAlbum (album);
            
            return artist;
        }

        private static int GetYearFromAlbumTitle(string albumTitle)
        {
            Regex reg = new Regex("[(][0-9]{4}[)]");
            Match m = reg.Match(albumTitle);
            if (m.Length > 0)
            {
                var year = RemoveBrackets(m.Value);
                return Convert.ToInt16(year);
            }
            return 0;
        }

        private static string StripYearFromAlbumTitle(string albumTitle)
        {
            Regex reg = new Regex("[(][0-9]{4}[)]");
            Match m = reg.Match(albumTitle);
            if (m.Length > 0)
            {
                albumTitle = albumTitle.Remove(m.Index, m.Length).Trim();
            }
            return albumTitle;
        }
       
        private static string RemoveBrackets(string title)
        {
            title = title.Replace("(", "").Replace(")", "");
            title = title.Replace("[", "").Replace("]", "");
            return title.Trim();
        }
    }

     
}
