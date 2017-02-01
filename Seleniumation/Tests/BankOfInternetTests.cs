using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seleniumation.Functions;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Seleniumation.Tests
{
    /// <summary>
    /// BankOfInternet Test library
    /// </summary>
    public class BankOfInternetTests
    {
        /// <summary>
        /// Instance of function repository
        /// </summary>
        private BankOfInternetFunctions Functions;

        /// <summary>
        /// Initializes the BankOfInternet function repository.
        /// </summary>
        /// <param name="driver">Browser driver instance.</param>
        public BankOfInternetTests(IWebDriver driver) {
            Functions = new BankOfInternetFunctions(driver);
        }

        /// <summary>
        /// Looks for the specified parameter in the config file.
        /// </summary>
        /// <param name="TestScenario">Scenario to run.</param>
        /// <param name="TestSetting">Name of the setting.</param>
        /// <returns>string</returns>
        private string GetData(string TestScenario, string TestSetting)
        {
            return ConfigurationSettings.AppSettings[TestScenario + "_" + TestSetting];
        }

        #region Test Routines
        public void TestBankOfInternetEnroll(string Scenario)
        {
            this.Functions.ClickOpenAccount();
            Assert.IsTrue(this.Functions.VerifyEnrollPage(), "Enroll Page opened correctly");

            this.Functions.ClickEnrollNow();
            Assert.IsTrue(this.Functions.VerifyGettingStartedPage(), "Getting Started page opened correctly");

            this.Functions.IntroduceName(
                                GetData(Scenario,"FirstName"), 
                                GetData(Scenario, "LastName")
                                );
            this.Functions.IntroduceInitialContactInformation(
                                GetData(Scenario, "Email"), 
                                GetData(Scenario, "CellPhone")
                                );
            this.Functions.IntroducePassword(
                                GetData(Scenario, "Password")
                                );
            this.Functions.SaveAndContinue();
            Assert.IsTrue(this.Functions.VerifySelectProductsPage(), "Verify Products page opened correctly");

            Assert.IsTrue(this.Functions.SelectAccountType(), "Could not select an account type");
            Assert.IsTrue(this.Functions.SelectCheckingAccounts(), "Could not select checking account");
            Assert.IsTrue(this.Functions.SelectSavingAccounts(), "Could not select savings account");
            Assert.IsTrue(this.Functions.SelectMoneyMarket(), "Could not select money market");
            Assert.IsTrue(this.Functions.SelectCertificateOfDeposit(), "Could not select certificate of deposit");
            Assert.IsTrue(this.Functions.SelectOtherProducts(), "Could not select other products");
            this.Functions.SaveAndContinue();
            Assert.IsTrue(this.Functions.VerifyTermsAndConditionsPage(), "Could not verify Terms and conditions page");

            this.Functions.CheckTermsConditionsAndFees();
            Assert.IsTrue(this.Functions.VerifyPersonalInformationPage(), "Could not verify Personal Information page");

            this.Functions.IntroducePersonalInfo(
                                GetData(Scenario, "FirstName"), 
                                GetData(Scenario, "LastName"), 
                                GetData(Scenario, "DateOfBirth"), 
                                GetData(Scenario, "SSN")
                                );
            this.Functions.IntroduceInitialContactInformation(
                                GetData(Scenario, "Email"), 
                                GetData(Scenario, "CellPhone"),
                                GetData(Scenario, "AlternateEmail"), 
                                GetData(Scenario, "BusinessPhone"), 
                                GetData(Scenario, "HomeAddress")
                                );
            this.Functions.IntroduceAddress(
                                GetData(Scenario, "HomeAddress"), 
                                GetData(Scenario, "ZipCode"), 
                                GetData(Scenario, "City"), 
                                GetData(Scenario, "State"), 
                                GetData(Scenario, "NoOfYears"), 
                                GetData(Scenario, "NoOfMonths")
                                );
        }
        #endregion
    }
}
