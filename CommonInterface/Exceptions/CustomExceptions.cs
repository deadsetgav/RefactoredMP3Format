using Id3LibTagAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CustomExceptions
    {
    }
    [Serializable]
    public class InvalidFolderNameFormatException : Exception
    {
        public InvalidFolderNameFormatException(string foldername) 
            : base(string.Format("Unknown folder name format: {0}", foldername))
        {
            //standard exception
        }
    }

    [Serializable]
    public class SettingNotValidException : Exception
    {
        public SettingNotValidException(string settingName, string value)
            : base(string.Format("{0} not valid: {1}", settingName,value))
        {
            //standard exception behavior
        }
    }

    [Serializable]
    public class DataNotFoundException: Exception
    {
        public DataNotFoundException(string message) 
        : base(message)
        {

        }
    }

    [Serializable]
    public class AlbumNotFoundException : DataNotFoundException 
    {
        public AlbumNotFoundException(string album,string artist) 
            : base (string.Format("Album {0} not found for Artist {1}",album,artist))
        {
 
        } 
    }
    [Serializable]
    public class ArtistNotFoundException :DataNotFoundException
    {
        public ArtistNotFoundException(string artist) 
            : base (string.Format ("Artist {0} not found", artist))
        {

        }
    }

    [Serializable]
    public class SignificantTagMismatchException : Exception
    {
        public SignificantTagMismatchException(IMp3 mp3) 
            : base (string.Format ("Tags dont match the rest of the artist/album : {0}, {1} - {2}",mp3.Artist, mp3.Album, mp3.Title))
        {

        }
    }
}
