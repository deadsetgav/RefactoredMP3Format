using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;
using System.IO;

namespace Tests.Concrete
{
    [TestClass]
    public class MusicDirectoryReaderTests
    {

        private string _artistPath;
        private string _albumPath;

        [TestInitialize]
        public void Setup()
        {
            var setting = FormatterSettingsTests.GetDefaultSettings();
            _artistPath = Path.Combine(setting.SourceDirectoryPath, "Ride");
            _albumPath = Path.Combine(setting.SourceDirectoryPath, "Ride", "Going Blank Again");
        }


        [TestMethod]
        public void TestReadArtistsFromDirectory()
        {

            var filePath = _artistPath;
            var artist = MusicDirectoryReader.GetArtistsAlbumsFromDirectory(filePath);
            
            Assert.AreEqual("Ride", artist.Name);
            Assert.AreEqual(1, artist.Albums.Count);

            var album = artist.Albums[0];
            Assert.AreEqual("Going Blank Again", album.Title);
            
        }

        [TestMethod]
        public void TestFolderContainsMusicFiles()
        {
            Assert.IsFalse(MusicDirectoryReader.FolderContainsMusicFiles(_artistPath));
            Assert.IsTrue(MusicDirectoryReader.FolderContainsMusicFiles(_albumPath));
        }

    }
}
