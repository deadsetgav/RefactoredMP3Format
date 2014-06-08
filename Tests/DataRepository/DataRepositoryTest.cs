using DataRepository;
using Domain.Concrete;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataRepository
{
    [TestClass]
    public class DataRepositoryTest
    {
        [TestMethod]
        public void TestDB_00_Setup()
        {
            var dbAccess = new DataAccess("testCollection");
           // dbAccess.DropCollection();
        }

        [TestMethod]
        public void TestDB_01_SaveAlbum() 
        {
            var reader = TestAlbumFileReader.GetFNMAlbumFileReader();
            var album = new Album("Album of the Year", "Faith No More", reader);
            var dbAccess = new DataAccess("testCollection");
            dbAccess.SaveAlbum(album);

        }

        [TestMethod]
        public void TestDB_02_GetAlbum()
        {
            var dbAccess = new DataAccess("testCollection");
            var album = dbAccess.GetAlbum("Faith No More", "Album of the Year");

            Assert.AreEqual("Faith No More", album.ArtistName);
            Assert.AreEqual("Album of the Year", album.Title);
            Assert.AreEqual(12, album.Tracks().Count());

        }

        [TestMethod, ExpectedException(typeof(AlbumNotFoundException))]
        public void TestDB_03_GetAlbumThatsNotInDatabase()
        {
            var dbAccess = new DataAccess("testCollection");
            var album = dbAccess.GetAlbum("Pantera","Great Southern Trendkill");
        }
        
        [TestMethod]
        public void TestDB_04_TestAlbumExists()
        {
            var dbAccess = new DataAccess("testCollection");
            var exists = dbAccess.AlbumExists("Faith No More", "Album of the Year");

            Assert.IsTrue(exists);
        }
        
        [TestMethod]
        public void TestDB_04_TestAlbumDoesntExist()
        {
            var dbAccess = new DataAccess("testCollection");
            var exists = dbAccess.AlbumExists("Pantera", "Great Southern Trendkill");

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void TestDB_05_TestSaveArtist()
        {
            var dbAccess = new DataAccess("testCollection");
            var artist = new Artist("Machine Head");

            dbAccess.SaveArtist(artist);

        }

        [TestMethod]
        public void TestDB_06_TestGetArtist()
        {
            var dbAccess = new DataAccess("testCollection");
            var artist = dbAccess.GetArtist("Machine Head");

            Assert.AreEqual("Machine Head", artist.Name);
        }

        [TestMethod]
        public void TestDB_07_TestArtistExists()
        {
            var dbAccess = new DataAccess("testCollection");
            var exists = dbAccess.ArtistExists("Machine Head");

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void TestDB_08_TestArtistDoesntExists()
        {
            var dbAccess = new DataAccess("testCollection");
            var exists = dbAccess.ArtistExists("Fear Factory");

            Assert.IsFalse(exists);
        }

        [TestMethod, Ignore]
        public void TestDB_09_TestLiveDB()
        {
            var dbAceess = new DataAccess("Albums");
            var exists = dbAceess.AlbumExists("Soundgarden", "Superunknown");
            var album = dbAceess.GetAlbum("Soundgarden", "Superunknown");

            Assert.IsTrue(exists);
            Assert.IsNotNull(album);

        }
    }

}
