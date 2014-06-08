using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.Interfaces;
using System.Reflection;
using System.IO;

namespace Tests
{
    [TestClass]
    public class FormatterSettingsTests
    {

        [TestInitialize]
        public void Setup()
        {
        }

        public static FormatterSettings GetDefaultSettings()
        {
            var testSettings = new FormatterSettingsTests();
            return testSettings.GetADefaultSettingsObject();
        }

        public FormatterSettings GetADefaultSettingsObject()
        {
            return new FormatterSettings()
            {
                SourceDirectoryPath = GetTestingFolder() + "\\Source",
                OutputDirectoryPath = GetTestingFolder() + "\\out",
                CopyOrMove = CopyType.Copy,
                FixTags = true,
                Format = FormatStyle.Gav
            };
        }

        public string GetTestingFolder()
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            var assemblyLocation = assembly.Location;

            for (int i = 0; i < 4; i++)
            {
                assemblyLocation = assemblyLocation.Remove(assemblyLocation.LastIndexOf("\\"));
            }

            var testingPath = Path.Combine(assemblyLocation, "Testing");
            return testingPath;
        }

        [TestMethod]
        public void TestCreateSettings()
        {
            FormatterSettings settings = GetDefaultSettings();
            Assert.AreEqual(GetTestingFolder() + "\\Source", settings.SourceDirectoryPath);
        }

        [TestMethod]
        public void TestGetTestingFolder()
        {
            var path = GetTestingFolder();
            Assert.AreEqual("C:\\Users\\Gavin\\Documents\\Visual Studio 2013\\Projects\\RefactoredMP3Format\\Testing", path);
        }

        [TestMethod]
        public void TestSettingObjectIsPopulated()
        {
            IFormatterSettings setting = GetDefaultSettings();
            Assert.AreEqual(CopyType.Copy, setting.CopyOrMove);
            Assert.AreEqual (true, setting.FixTags);
            Assert.AreEqual(FormatStyle.Gav, setting.Format);
        }
    }
}
