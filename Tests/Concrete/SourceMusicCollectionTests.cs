using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Concrete;
using Domain.Interfaces;

namespace Tests.Concrete
{
    [TestClass]
    public class SourceMusicCollectionTests
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void TestCreateNewCollection()
        {
            var setting = FormatterSettingsTests.GetDefaultSettings();

            IMusicCollection collection = new MusicCollection(setting.SourceDirectoryPath);
            collection.ScanItemsToCollection();

            var artists = collection.Artists;

            Assert.AreEqual(3, artists.Count());
            
        }

    }
}
