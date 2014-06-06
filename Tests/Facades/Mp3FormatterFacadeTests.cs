using Domain;
using Domain.Facade;
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
    public class Mp3FormatterFacadeTests
    {

        private FormatterSettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
            var outDir = new DirectoryInfo(_settings.OutputDirectoryPath);
        }


        [TestMethod]
        public void TestFacade_ReadSource()
        {
            var facade = new Mp3FormatterFacade(_settings);
            var collection = facade.ReadSourceDirectory();

            Assert.AreEqual(3, collection.Artists.Count(), "should have 3 artists");

            var lookup = collection.Artists.ToDictionary(f => f.Name);
            Assert.IsTrue(lookup.ContainsKey("Ride"));

        }

        [TestMethod]
        public void TestFacade_WriteAlbumsGavStyleNoFix()
        {
            var settings = FormatterSettingsTests.GetDefaultSettings();
            settings.Format = FormatStyle.Gav;
            settings.FixTags = false;
            settings.CopyOrMove = CopyType.Copy;

            var log = new TestLogObject();

            var facade = new Mp3FormatterFacade(settings);
            facade.ProcessLog = log;

            facade.Process();


        }
        
        [TestMethod]
        public void TestFacade_WriteAlbumsGavStyleWithFix()
        {
            var settings = FormatterSettingsTests.GetDefaultSettings();
            settings.Format = FormatStyle.Gav;
            settings.FixTags = true;
            settings.CopyOrMove = CopyType.Copy;

            var log = new TestLogObject();

            var facade = new Mp3FormatterFacade(settings);
            facade.ProcessLog = log;

            facade.Process();

            var mp3 = GetFile("Von Hertzen Brothers\\Nine Lives\\02-vhb-flowers_and_rust.mp3");

            Assert.AreEqual("Von Hertzen Brothers", mp3.AlbumArtist);
        }

        [TestMethod]
        public void TestFacade_WriteAlbumsPeteStyleWithFix()
        {
            var settings = FormatterSettingsTests.GetDefaultSettings();
            settings.Format = FormatStyle.Pete;
            settings.FixTags = true;
            settings.CopyOrMove = CopyType.Copy;

            var log = new TestLogObject();

            var facade = new Mp3FormatterFacade(settings);
            facade.ProcessLog = log;

            facade.Process();

            var mp3 = GetFile("Von Hertzen Brothers - 2013 - Nine Lives\\02-vhb-flowers_and_rust.mp3");

            Assert.AreEqual("Von Hertzen Brothers", mp3.AlbumArtist);
        }

        private IMp3 GetFile(string filepath)
        {
            
            var fileLocation = Path.Combine(_settings.OutputDirectoryPath, filepath);
            return Mp3Adapter.GetMp3(fileLocation);

        }
        
    }
}
