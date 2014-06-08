using CommonInterface;
using Domain;
using Domain.Concrete;
using Domain.Facade;
using Id3LibTagAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class Mp3StandardiseFacadeTests
    {
        private FormatterSettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
            //var outDir = new DirectoryInfo(_settings.OutputDirectoryPath);
        }


        [TestMethod]
        public void TestStandardiseFacade()
        {

            var artist = TestAlbumFileReader.GetFNM();
            var facade = new Mp3StandardiseFacade(_settings);

            facade.StandardiseArtist(artist);

            var albumOfTheYear = artist.Albums[0];
            foreach(IMp3 mp3 in albumOfTheYear.Tracks())
            {
                Assert.AreEqual("Album Of The Year", mp3.Album);
                Assert.AreEqual("Faith No More", mp3.Artist);
                Assert.AreEqual("Faith No More", mp3.AlbumArtist);
                Assert.AreEqual("1997", mp3.Year);
            }

            var angelDust = artist.Albums[1];
            foreach(IMp3 mp3 in angelDust.Tracks())
            {
                Assert.AreEqual("Angel Dust", mp3.Album);
                Assert.AreEqual("Faith No More", mp3.Artist);
                Assert.AreEqual("Faith No More", mp3.AlbumArtist);
                Assert.AreEqual("1992", mp3.Year);
            }
           

        }

        [TestMethod] 
        [Ignore]
        public void TestLiveData()
        {
            _settings.SourceDirectoryPath = "L:\\Music";
            var facade = new Mp3StandardiseFacade(_settings);

            var log = new LogFile("l:\\log.txt");
            facade.ProcessLog = log;

            facade.Process();

        }
    }
}
