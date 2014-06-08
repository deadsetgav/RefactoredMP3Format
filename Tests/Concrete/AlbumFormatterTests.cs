using CommonInterface;
using Domain;
using Domain.Concrete;
using Domain.Facade;
using Domain.Factory;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagLibTagAdapter;

namespace Tests
{
    [TestClass]
    public class AlbumFormatterTests
    {

        private IFormatterSettings _settings;

        [TestInitialize]
        public void TestSetup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
        }

        [TestMethod]
        public void TestPeteTitleMatches()
        {
            var testClass = new TestAbumFormatter(FormatterSettingsTests.GetDefaultSettings());
            var title = "01 - This is the track title";
            var result = testClass.TestTitleStartsWithTrackNumber(title);

            Assert.IsTrue(result);

            var newTitle = testClass.TestTrimTrackFromTitle(title);
            Assert.AreEqual("This is the track title", newTitle);

        }

        [TestMethod]
        public void TestPeteTitleNoMatch()
        {
            var testClass = new TestAbumFormatter(FormatterSettingsTests.GetDefaultSettings());
            var title = "99 Ways to Die";
            var result = testClass.TestTitleStartsWithTrackNumber(title);

            Assert.IsFalse(result);

            var newTitle = testClass.TestTrimTrackFromTitle(title);
            Assert.AreEqual(title, newTitle);

        }


        [TestMethod]
        public void TestGetAlbumYearFromTracks_POC_Logic()
        {
            var fileList = MusicDirectoryReader.GetMusicFilesFromFolder("C:\\Users\\Gavin\\Documents\\Visual Studio 2013\\Projects\\RefactoredMP3Format\\Testing\\out\\Ride\\Going Blank Again");
            var trackList = new List<IMp3>();
            foreach (var file in fileList)
            {
                var track = Mp3Adapter.GetMp3(file.FullName);
                trackList.Add(track);

            }

            var years = trackList.Select(f => f.Year);
            var query = years.GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key).First();
            


        }

        [TestMethod]
        public void TestAlbumFormatFactoryForGavStyle()
        {
            var reader = new TestAlbumFileReader();
            var album = new Album("Ride The Lightning", "Metallica", reader);
            var artist = new Artist("Metallica");

            var mp3 = new TestMp3
            {
                Album = "Ride The Lightning",
                AlbumArtist = "",
                Artist = "Metallica",
                Track = "03",
                Title = "03 - For Whom The Bell Tolls",
                Year = "1984"
            };

            var albumFormatter = AlbumFormatFactory.GetAlbumFormatter(FormatStyle.Gav);

            var expectedPath = Path.Combine(_settings.OutputDirectoryPath, "Metallica", "Ride The Lightning");
            var path = albumFormatter.GetFolderToWriteTo(_settings.OutputDirectoryPath, artist, album);

            Assert.AreEqual(expectedPath, path.FullName);

            albumFormatter.FormatMp3Tags(mp3, album);

            Assert.AreEqual("3", mp3.Track);
            Assert.AreEqual("Metallica", mp3.AlbumArtist);
            Assert.AreEqual("For Whom The Bell Tolls", mp3.Title);

        }

        [TestMethod]
        public void TestAlbumFormatFactoryForPeteStyle()
        {
            var reader = new TestAlbumFileReader();
            var album = new Album("Ride The Lightning", "Metallica", reader);
            album.ReleaseYear = "1984";
            var artist = new Artist("Metallica");

            var mp3 = new TestMp3
            {
                Album = "Ride The Lightning",
                AlbumArtist = "",
                Artist = "Metallica",
                Track = "03",
                Title = "For Whom The Bell Tolls",
                Year = "1984"
            };

            var albumFormatter = AlbumFormatFactory.GetAlbumFormatter(FormatStyle.Pete);

            var expectedPath = Path.Combine(_settings.OutputDirectoryPath, "Metallica - 1984 - Ride The Lightning");
            var path = albumFormatter.GetFolderToWriteTo(_settings.OutputDirectoryPath, artist, album);

            Assert.AreEqual(expectedPath, path.FullName);

            albumFormatter.FormatMp3Tags(mp3, album);

            Assert.AreEqual("03", mp3.Track);
            Assert.AreEqual("Metallica", mp3.AlbumArtist);
            Assert.AreEqual("03 - For Whom The Bell Tolls", mp3.Title);

        }
    
    }

}
