using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Id3LibTagAdapter;

namespace Tests
{
    [TestClass]
    public class TagAdapterTests
    {

        private IFormatterSettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
        }

        [TestMethod]
        public void TestAdapter()
        {
            var path = _settings.SourceDirectoryPath;
            var filename = Path.Combine(path, "Ride", "Going Blank Again", "01 - Leave Them All Behind.mp3");

            var mp3 = Mp3Adapter.GetMp3(filename);

            Assert.AreEqual("Going Blank Again", mp3.Album);
            Assert.AreEqual("Ride", mp3.Artist);
            Assert.AreEqual("Ride", mp3.AlbumArtist);
            Assert.AreEqual("1992", mp3.Year);

            Assert.AreEqual(320, mp3.BitRate);
          
        }

        [TestMethod]
        public void TestForUTF16_Fails()
        {
            var sourcePath = GetFailsFolder();
            var artistPath = Path.Combine(sourcePath, "Minsk");
            var artistDir = new DirectoryInfo(artistPath);

            var albums = artistDir.GetDirectories();
            foreach(var album in albums)
            {
                var albumFailed = false;
                var failList = new StringBuilder();

                var tracks = album.GetFiles("*.mp3");
                foreach (var track in tracks)
                {
                    var trackRead = CanReadTrack(track.FullName);
                    if (trackRead == false)
                    {
                        failList.AppendLine(track.Name);
                        albumFailed = true;
                    }
                }
                Assert.IsFalse(albumFailed, string.Format("failed to read track: {0}", failList.ToString()));
            }
            
            


        }

        private bool CanReadTrack(string filename) 
        {
            try
            {
                var mp3 = Mp3Adapter.GetMp3(filename);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        [TestMethod]
        public void TestGetFailsFolder()
        {
            var path = GetFailsFolder();
            Assert.IsTrue(Directory.Exists(path));
        }

        private string GetFailsFolder()
        {
            var path = _settings.SourceDirectoryPath;

            path = path.Remove(path.LastIndexOf("\\")+1);
            path =  path + "Fails";
            return path;
        }

       
    }
}
