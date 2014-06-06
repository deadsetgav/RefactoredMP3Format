using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Id3LibTagAdapter;
using Domain.Concrete;

namespace Tests
{

    public class LogFile : ILog
    {
        private StreamWriter _log;
        public LogFile(string path)
        {
            _log = new StreamWriter(path, true);
            _log.AutoFlush = true;
        }
        public void Close()
        {
            _log.Flush();
            _log.Close();
            _log.Dispose();
        }
        public void WriteVerbose(string info)
        {
            _log.WriteLine(info);
        }

        public void WriteInfo(string info)
        {
            _log.WriteLine(info);
        }

        public void WriteError(string info, Exception ex)
        {
            _log.WriteLine(string.Format("{0}:{1}", info, ex.Message));
        }
    }

    public class TestAlbumFileReader:IAlbumFileReader
    {
        public TestAlbumFileReader()
        {
            _albumTracks = new List<IMp3>();
            _additionalFiles = new List<FileInfo>();
        }
        private List<IMp3> _albumTracks;
        private List<FileInfo> _additionalFiles;
        public List<IMp3> GetAlbumTracks()
        {
            return _albumTracks;
        }

        public FileInfo[] GetAdditionalFiles()
        {
            return _additionalFiles.ToArray(); 
        }

        public string Source
        {
            get;set;
        }

        public static IAlbumFileReader GetFNMAlbumFileReader()
        {
            var reader = new TestAlbumFileReader();
            reader._albumTracks.Add(new TestMp3() { Track = "1", Title = "Collision", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "2", Title = "Stripsearch", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album of the Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "3", Title = "Last Cup Of Sorrow", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "4", Title = "Naked Infront of the Computer", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "5", Title = "Helpless", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "6", Title = "Mouth To Mouth", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "7", Title = "Ashes to Ashes", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album of the Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "8", Title = "She Loves Me Not", Year = "1997", BitRate = 128, Artist = "Faith no More", Album = "album of the year" });
            reader._albumTracks.Add(new TestMp3() { Track = "9", Title = "Got That Feeling", Year = "1997", BitRate = 192, Artist = "Faith no More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "10", Title = "Paths of Glory", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "11", Title = "Home Sick Home", Year = "1997", BitRate = 320, Artist = "faith no more", Album = "Album Of The Year" });
            reader._albumTracks.Add(new TestMp3() { Track = "12", Title = "Pristina", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = "Album Of The Year" });

            return reader;
        }

        public static IAlbumFileReader GetFNMAngelFileReader()
        {
            var artist = "Faith No More";
            var album = "Angel Dust";
            var year = "1992";

            var reader = new TestAlbumFileReader();
            reader._albumTracks.Add(new TestMp3() { Track = "1", Title = "Land Of Sunshine", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "2", Title = "Caffeine", Year = year, BitRate = 192, Artist = "Faith No More", Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "3", Title = "Midlife Crisis", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "4", Title = "RV", Year = "1997", BitRate = 192, Artist = "Faith No More", Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "5", Title = "Smaller And Smaller", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "6", Title = "Everything's Ruined", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "7", Title = "Malpractice", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "8", Title = "Kindergarten", Year = year, BitRate = 128, Artist = "Faith no More", Album = "angel dust", AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "9", Title = "Be Aggressive", Year = year, BitRate = 192, Artist = "Faith no More", Album = "angel dust", AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "10", Title = "A Small Victory", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "11", Title = "Crack Hitler", Year = "1991", BitRate = 320, Artist = "faith no more", Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "12", Title = "Jizzlobber", Year = year, BitRate = 192, Artist = artist, Album = "Angel dust", AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "13", Title = "Midnight Cowboy", Year = year, BitRate = 192, Artist = artist, Album = album, AlbumArtist = artist });
            reader._albumTracks.Add(new TestMp3() { Track = "14", Title = "Easy", Year = "1993", BitRate = 192, Artist = "FAITH NO MORE", Album = album, AlbumArtist = artist });

            return reader;
        }

        public static IArtist GetFNM()
        {
            var aoyReader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var albumOfYear = new Album("Album of the Year", "Faith No More", aoyReader);
            var adReader = TestAlbumFileReader.GetFNMAngelFileReader();
            var angelDust = new Album("Angel Dust", "Faith No More", adReader);

            var artist = new Artist("faith no more");
            artist.AddAlbum(albumOfYear);
            artist.AddAlbum(angelDust);

            return artist;
        }
    
    }
  

    public class TestMp3 : IMp3
    {

        public TestMp3()
        {
            Saved = false;
        }

        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Track { get; set; }
        public string Year { get; set; }
        public int BitRate { get; set; }
        public bool Saved { get; set; }
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public void Save()
        {
            Saved = true;
        }
    }

    public class TestMusicCollection : IMusicCollection
    {

        private List<IArtist> _artistList;

        public TestMusicCollection() 
        {
            _artistList = new List<IArtist>();
        }

        public string CollectionFilePath
        {
            get;
            set;
        }

        public IArtist[] Artists
        {
            get { return _artistList.ToArray(); }
            set { _artistList = value.ToList(); }
        }

        public void ScanItemsToCollection()
        {
            throw new NotImplementedException();
        }

        private void PopulateWithTestValues()
        {
            var reader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var fnm = new Artist("Faith No More");
            var albumOfTheYear = new Album("Album Of The Year", "Faith no More",reader);      
            _artistList.Add(fnm);
            
        }
    }

    public class TestAbumFormatter : AlbumFormatterBase
    {
        public TestAbumFormatter(IFormatterSettings settings)
        {
        }

        public bool TestTitleStartsWithTrackNumber(string title)
        {
            return TitleStartsWithTrackNumber(title);
        }
        public string TestTrimTrackFromTitle(string title)
        {
            return TrimTrackFromTitle(title);
        }

        public override DirectoryInfo GetFolderToWriteTo(string writeFolder, IArtist artist, IAlbum album)
        {
            throw new NotImplementedException();
        }

        public override void FormatMp3Tags(IMp3 track, IAlbum album)
        {
            throw new NotImplementedException();
        }
    }

    public class TestLogObject : ILog
    {
        private StringBuilder _myLog;

        public TestLogObject()
        {
            _myLog = new StringBuilder();
        }

        public void WriteVerbose(string info)
        {
            _myLog.AppendLine(info);
        }
        public void WriteInfo(string info)
        {
            _myLog.AppendLine(info);
        }

        public void WriteError(string info, Exception ex)
        {
            _myLog.AppendLine(info);
            _myLog.AppendLine(ex.Message);
        }

        public string SeeLogDetails()
        {
            return _myLog.ToString();
        }
    }

}
