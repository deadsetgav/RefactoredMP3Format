using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3Lib;
using Id3Lib;
using Id3Lib.Frames;
using CommonInterface;

namespace Id3LibTagAdapter
{
      
    public class OldMp3Adapter : IMp3
    {
        // This class uses the id3Lib class for the reading / writing of Mp3 tags
        // 
        // This class acts as an adapter so no other part of the application
        // needs to know about the 3rd party class library.
        
        private static IMp3 GetMp3(string path)
        {
            return new OldMp3Adapter(path);
        }

        private TagHandler _handler;
        private Mp3File _mp3;

        public string Track 
        {
            get { return _handler.Track; }
            set { _handler.Track = value; }
        }
        public string Title
        {
            get { return _handler.Title; }
            set { _handler.Title = value; }
        }
        public string Album 
        {
            get { return _handler.Album; }
            set { _handler.Album = value; } 
        } 
        public string Artist
        {
            get { return _handler.Artist; }
            set { _handler.Artist = value; }
        } 
        public string AlbumArtist
        {
            get { return _handler.AlbumArtist;  }           
            set  {  _handler.AlbumArtist = value; }                
        }
        public string Year
        {
            get { return _handler.Year; }
            set { _handler.Year = value; }
        }
        public int BitRate
        {
            get { return GetBitRate(); }
        }
        private OldMp3Adapter(string filepath)
        {
            _mp3 = new Mp3File(filepath);
            _handler = _mp3.TagHandler;
        }
        
        public void Save()
        {
            _mp3.Update();
        }
        public string FullFilePath
        {
            get { return _mp3.FileName; }
        }
        public string FileName
        {
            get { return _mp3.FileName.Substring(_mp3.FileName.LastIndexOf("\\") +1 ); }
        }

        private int GetBitRate()
        {

            if (_mp3.Audio.BitRateMp3.HasValue)
            {
                var bitrate = _mp3.Audio.BitRateMp3.Value;
                var kbs = Convert.ToInt32(bitrate / 1000);
                return kbs;
            }
            else
            {
                return 0;
            }         
        }
    }


}
