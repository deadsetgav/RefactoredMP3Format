using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Interfaces;
using Domain.Concrete;
using System.IO;
using Id3LibTagAdapter;

namespace Tests
{
    [TestClass]
    public class AlbumTests
    {

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void TestCreateAlbum()
        {
            IArtist various = new Artist("Various Artists");
            IAlbum album = new Album("The Crow OST", various.Name, null);
            various.AddAlbum(album);

            Assert.AreEqual("The Crow OST", album.Title);
            Assert.AreEqual("Various Artists", album.ArtistName);
            
        }

        [TestMethod]
        public void TestSetYear()
        {
            IArtist various = new Artist("Various Artists");
            IAlbum myNewAlbum = new Album("My New Album", various.Name, null);
            myNewAlbum.ReleaseYear = "2004";
            
            Assert.AreEqual("2004", myNewAlbum.ReleaseYear);
        }

        [TestMethod]
        public void TestAlbumsAverageBitRate()
        {
            var reader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var album = new Album("Album of the Year","Faith No More", reader);

            var avgBitRate = album.GetAverageBitRateFromTracks();
            Assert.AreEqual(192, avgBitRate);
        }

        [TestMethod]
        public void TestAlbumFileReader_GetTracks()
        {
            var settings = FormatterSettingsTests.GetDefaultSettings();
            var path = Path.Combine(settings.SourceDirectoryPath, "Ride", "Going Blank Again"); 
            var albumReader = new AlbumFileReader(path);

            var tracks = albumReader.GetAlbumTracks();
            Assert.AreEqual(2, tracks.Count);

            foreach (IMp3 track in tracks)
            {
                Assert.AreEqual("1992", track.Year);
            }          
        }

        [TestMethod]
        public void TestAlbumFileReader_GetExtraTracks()
        {
            var settings = FormatterSettingsTests.GetDefaultSettings();
            var path = Path.Combine(settings.SourceDirectoryPath, "Ride", "Going Blank Again");
            var albumReader = new AlbumFileReader(path);
 
            var artFiles = albumReader.GetAdditionalFiles();
            var albumArt = (from FileInfo file in artFiles
                            where file.Name.Equals( "folder.jpg", StringComparison.CurrentCultureIgnoreCase)
                            select file).FirstOrDefault();

            Assert.IsNotNull(albumArt);

        }

        [TestMethod]
        public void TestAlbumFileReader_GetCommonAlbumTitle() 
        {
            var reader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var album = new Album("Album of the Year", "Faith No More", reader);

            var commonName = AlbumReader.GetMostCommonAlbumTitleFromTracks(album);

            Assert.AreEqual("Album Of The Year", commonName);
        }

        [TestMethod]
        public void TestAlbumFileReader_GetCommonArtist() 
        {
            var reader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var album = new Album("Album of the Year", "Faith No More", reader);

            var commonArtist = AlbumReader.GetMostCommonArtistNameFromTracks(album);

            Assert.AreEqual("Faith No More", commonArtist);

            var artist = new Artist("Faith no More");
            artist.AddAlbum(album);

            var commonFromArtist = ArtistReader.GetMostCommonArtistNameFromAlbums(artist);
            Assert.AreEqual("Faith No More", commonArtist);
        }

        [TestMethod]
        public void TestArtistReader_GetCommonArtist()
        {
            var artist = TestAlbumFileReader.GetFNM();
            var commonFromArtist = ArtistReader.GetMostCommonArtistNameFromAlbums(artist);

            Assert.AreEqual("Faith No More", commonFromArtist);
            
        }


        [TestMethod]
        public void TestArtistReader_stringDistance()
        {
            var result_slightDiff = DamerauLevenshtein.Distance("Metallica", "metallica");
            Assert.AreEqual(1, result_slightDiff);

            var result_exactMatch = DamerauLevenshtein.Distance("Metallica", "Metallica");
            Assert.AreEqual(0, result_exactMatch);

            var result_totallyDiff = DamerauLevenshtein.Distance("Blink182", "Emperor");
            Assert.IsTrue(result_totallyDiff > 7);
        }
    }
}
