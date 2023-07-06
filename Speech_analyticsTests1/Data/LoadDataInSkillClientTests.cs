using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speech_analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech_analytics.Data.Tests
{
    [TestClass()]
    public class LoadDataInSkillClientTests
    {
        [TestMethod()]
        public void LoadDataInSkillClientTest()
        {
            LoadDataInSkillClient loadDataInSkillClient = new LoadDataInSkillClient("104");
            loadDataInSkillClient.Process();
        }

        [TestMethod()]
        public void ProcessTest()
        {
            Assert.Fail();
        }
    }
}