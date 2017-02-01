using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seleniumation.Functions;
using Seleniumation.Tests;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Seleniumation
{
    [TestClass]
    public class Main
    {
        IWebDriver driver;
        BankOfInternetTests BankOfInternetTest;

        [TestInitialize]
        public void Initialize()
        {
            this.driver = Configuration.InitializeDriver();
            this.BankOfInternetTest = new BankOfInternetTests(driver);
            
        }

        [TestMethod]
        public void Test_EnrollWithValidInformation()
        {
            this.BankOfInternetTest.TestBankOfInternetEnroll("EnrollValid");
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.driver.Close();
            this.driver = null;
        }
    }
}
