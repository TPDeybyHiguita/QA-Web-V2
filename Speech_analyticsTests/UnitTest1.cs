using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speech_analytics.Controllers;
using System;

namespace Speech_analyticsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AdminController adminController = new AdminController();
            int response = adminController.mayusculasNum("Holaaa");
        }
    }
}
