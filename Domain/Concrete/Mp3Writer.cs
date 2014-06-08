using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Domain.Concrete;
using System.Text.RegularExpressions;
using CommonInterface;
using TagLibTagAdapter;

namespace Domain.Factory
{
    public class Mp3Writer : IMp3Writer
    {
        protected CopyType _copyType;
        protected bool _fixTag;
        protected DirectoryInfo _outFolder;
        private IAlbumFormatter _formatter;

        public Mp3Writer(IFormatterSettings settings, IAlbumFormatter formatter)
        {
            if (!Directory.Exists(settings.OutputDirectoryPath))
            {
                Directory.CreateDirectory(settings.OutputDirectoryPath);
            }
            _outFolder = new DirectoryInfo(settings.OutputDirectoryPath);
            _copyType = settings.CopyOrMove;
            _fixTag = settings.FixTags;
            _formatter = formatter;
        }
        
        public void WriteAlbums(IArtist artist)
        {
            foreach (IAlbum album in artist.Albums)
            {
                try
                {
                    WriteAlbum(artist, album);
                }
                catch (Exception ex)
                {
                    Log.WriteErrorToLog(string.Format("Failed to write album {0} for artist {1}", album.Title, artist.Name ), ex);
                }             
            }
        }

        private void WriteAlbum(IArtist artist, IAlbum album)
        {
            Log.WriteInfoToLog("Writing: {0} - {1}", artist.Name, album.Title);
            var writingFolder = _formatter.GetFolderToWriteTo(_outFolder.FullName,artist, album);

            foreach (IMp3 track in album.Tracks())
            {
                WriteTrack(writingFolder, track, album);
            }

            CopyAdditionalFiles(writingFolder, album);

            if (_copyType == CopyType.Move) 
            {
                DeleteSourceFiles(album);
            }
        }

        protected void WriteTrack(DirectoryInfo writingFolder, IMp3 track, IAlbum album)
        {
            var destinationFile = Path.Combine(writingFolder.FullName, track.FileName);
            File.Copy(track.FullFilePath, destinationFile,true);

            if (_fixTag)
            {
                var mp3File = Mp3Adapter.GetMp3(destinationFile);
                _formatter.FormatMp3Tags(mp3File, album);
            }

            if (_copyType == CopyType.Move)
            {
                File.Delete(track.FullFilePath);
            }         
        }
 
        private void CopyAdditionalFiles(DirectoryInfo writingFolder, IAlbum album)
        {
            //TODO - get a reader better than this
            var files = album.GetAdditionalFiles;
            foreach (FileInfo file in files)
            {
                var destinationFile = Path.Combine(writingFolder.FullName, file.Name); 
                File.Copy(file.FullName, destinationFile,true);

                if (_copyType == CopyType.Move)
                {
                    File.Delete(file.FullName);
                }
            }
        }

        private void DeleteSourceFiles(IAlbum album)
        {
            Log.WriteDetailToLog("Removing source files {0}", album.Source);
            Directory.Delete(album.Source, true);
        }

    }
}
