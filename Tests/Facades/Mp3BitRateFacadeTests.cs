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
    public class Mp3BitRateFacadeTests
    {

        private FormatterSettings _settings;

        [TestInitialize]
        public void Setup()
        {
            _settings = FormatterSettingsTests.GetDefaultSettings();
            var outDir = new DirectoryInfo(_settings.OutputDirectoryPath);
        }

        [TestMethod]
        [Ignore()]
        public void TestReadToDB()
        {
            _settings.SourceDirectoryPath = "L:\\Music";

            var facade = new Mp3BitRateFacade(_settings);
            var myLog = new LogFile("ReadToDBLog.txt");
            facade.ProcessLog = myLog;
            facade.Process();

            myLog.Close();
        }

        
    }
}
