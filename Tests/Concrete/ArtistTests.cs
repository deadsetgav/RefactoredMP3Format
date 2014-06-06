using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Concrete;
using Domain.Interfaces;

namespace Tests
{
    [TestClass]
    public class ArtistTests
    {

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void TestCreateArtistWithAlbums()
        {
            IArtist artist = new Artist("Machine Head");
            Assert.AreEqual(0, artist.Albums.Count());

            IAlbum burnMyEyes = new Album("Burn My Eyes", artist.Name,null);
            artist.AddAlbum(burnMyEyes);

            Assert.AreEqual(1, artist.Albums.Count());
            Assert.AreEqual("Machine Head", burnMyEyes.ArtistName);
        }

    }
}
