using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seleniumation
{
    public class Configuration
    {
        private static int _waitTime;
        private static string _pageUnderTest;

        private static IWebDriver _driver;

        public static IWebDriver InitializeDriver() {
            _waitTime = Int16.Parse(ConfigurationSettings.AppSettings["WaitTime"]);
            _pageUnderTest = ConfigurationSettings.AppSettings["PageUnderTest1"];
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(_waitTime));
            _driver.Navigate().GoToUrl(_pageUnderTest);
            _driver.Manage().Window.Maximize();
            return _driver;
        }
    }
}
