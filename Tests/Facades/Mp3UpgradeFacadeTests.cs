using CommonInterface;
using DataRepository;
using Domain;
using Domain.Concrete;
using Domain.Facade;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Facades
{
    [TestClass]
    public class Mp3UpgradeFacadeTests
    {
        private FormatterSettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
            var outDir = new DirectoryInfo(_settings.OutputDirectoryPath);
        }

        [TestMethod]
        public void TestCreateUpgradeFacade()
        {
            var upgrageFacade = new Mp3UpgradeFacade(_settings);
        }

        [TestMethod]
        public void TestUpgrader_CleanseTrack_FixesTrack()
        {
            var dbTrack = GetTestDbTrack01();
            var rippedTrack = GetTestRippedTrack01();

            var upgrader = new Upgrader();
            upgrader.CleanseTrack(rippedTrack, dbTrack);

            Assert.AreEqual("1",rippedTrack.Track);
            Assert.AreEqual("Roads To Judah", rippedTrack.Album);
            Assert.AreEqual("Deafheaven", rippedTrack.Artist);
            Assert.AreEqual("Deafheaven", rippedTrack.AlbumArtist);
            Assert.AreEqual("2011", rippedTrack.Year);
            Assert.AreEqual(320, rippedTrack.BitRate);
            Assert.AreEqual("track1.mp3", rippedTrack.FileName);
           
        }

        [TestMethod]
        public void TestUpgrader_CleanseTrack_DoesNotFixWrongTrack()
        {
            Exception result = null;
            
            try
            {
                var dbTrack = GetTestDbTrack01();
                var rippedTrack = GetTestRippedTrack02();

                var upgrader = new Upgrader();
                upgrader.CleanseTrack(rippedTrack, dbTrack);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            Assert.IsInstanceOfType(result, typeof(TrackMismatchException));

        }

        [TestMethod]
        public void TestUpgrader_CleanseTrack_FixesWithSlightMismatch()
        {
            var dbTrack = GetTestDbTrack02();
            var rippedTrack = GetTestRippedTrack02();

            var upgrader = new Upgrader();
            upgrader.CleanseTrack(rippedTrack, dbTrack);

            AssertTracksMatch(dbTrack, rippedTrack);
        }


        [TestMethod]
        public void TestUpgrader_CleanseAlbum()
        {

            var ripped = TestAlbumFileReader.GetImperfectDeafheavenAlbum();
            var db = TestAlbumFileReader.GetPerfectDeafheavenAlbum();

            var upgrader = new Upgrader();
            var result = upgrader.UpgradeAlbum(ripped, db);

            Assert.AreEqual(UpgradeResult.NoErrors, result);

            var rippedList = new List<IMp3>();
            var dbList = new List<IMp3>();

            rippedList.AddRange(ripped.Tracks());
            dbList.AddRange(db.Tracks());

            for (int i = 0; i < 4; i++)
            {
                AssertTracksMatch(dbList[i], rippedList[i]);
            }

        }

        private void AssertTracksMatch( IMp3 dbTrack,IMp3 rippedTrack)
        {
            Assert.AreEqual(dbTrack.Track, rippedTrack.Track);
            Assert.AreEqual(dbTrack.Album, rippedTrack.Album);
            Assert.AreEqual(dbTrack.Artist, rippedTrack.Artist);
            Assert.AreEqual(dbTrack.AlbumArtist, rippedTrack.AlbumArtist);
            Assert.AreEqual(dbTrack.Year, rippedTrack.Year);
            Assert.AreEqual(320, rippedTrack.BitRate);
            Assert.AreEqual(dbTrack.FileName, rippedTrack.FileName);
        }



        private TestMp3 GetTestRippedTrack01()
        {
            return new TestMp3
            {
                Track = "01",
                Title = "Violent",
                Album = "Roads To Judah",
                Artist = "Deafheaven",
                AlbumArtist = "",
                Year = "2010",
                BitRate = 320,
                FileName = "rippedFromCD01.mp3",
                FullFilePath = @"c:\testfolder\track1.mp3"
            };
        }

        private TestMp3 GetTestRippedTrack02()
        {
            return new TestMp3
            {
                Track = "02",
                Title = "language games",
                Album = "roads to judah",
                Artist = "deafheaven",
                AlbumArtist = "",
                Year = "2010",
                BitRate = 320,
                FileName = "rippedFromCD02.mp3",
                FullFilePath = @"c:\testfolder\track2.mp3"
            };
        }

        private TestMp3 GetTestDbTrack01()
        {
            return new TestMp3
            {
                Track = "1",
                Title = "Violent",
                Album = "Roads To Judah",
                Artist = "Deafheaven",
                AlbumArtist = "Deafheaven",
                Year = "2011",
                BitRate = 128,
                FileName = "track1.mp3",
                FullFilePath = @"c:\music\track1.mp3"
            };
        }

        private TestMp3 GetTestDbTrack02()
        {
            return new TestMp3
            {
                Track = "2",
                Title = "Language Games",
                Album = "Roads To Judah",
                Artist = "Deafheaven",
                AlbumArtist = "Deafheaven",
                Year = "2011",
                BitRate = 128,
                FileName = "track2.mp3",
                FullFilePath = @"c:\music\track2.mp3"
            };
        }
        

    }
}
