using Domain.Concrete;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class LogTests
    {    
        [TestMethod]
        public void TestWriteToLog_NotSet()
        {
            Log.WriteInfoToLog("Tests");

            //No log set, but shouldn't fail, should just do nothing!
        }

        [TestMethod]
        public void TestSetLog()
        {
            var log = new TestLogObject();
            Log.SetLog (log);
            Log.WriteInfoToLog("does this work?");

            Assert.AreEqual("does this work?", log.SeeLogDetails().Trim());
        }

        [TestMethod]
        public void TestWriteErrorToLog()
        {
            var testlog = new TestLogObject();
            Log.SetLog(testlog);

            var exception = new Exception("This is going on the log");
            Log.WriteErrorToLog("We had a problem", exception);

            var testString = testlog.SeeLogDetails();
            Assert.IsTrue(testString.Contains("We had a problem"));
            Assert.IsTrue(testString.Contains(exception.Message));

        }
    }

   
}
