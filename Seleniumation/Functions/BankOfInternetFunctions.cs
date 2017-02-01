using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Seleniumation.Functions
{
    /// <summary>
    /// Function repository for BankOfInternet webpage
    /// </summary>
    public partial class BankOfInternetFunctions
    {
        private IWebDriver Driver;
        private WebDriverWait Wait;

        public int Timeout;

        /// <summary>
        /// Constructor. Configures certain parameters of web driver
        /// </summary>
        /// <param name="driver">Browser driver.</param>
        /// <param name="Timeout">Timeout for implicit waits.</param>
        public BankOfInternetFunctions(IWebDriver driver, int Timeout = 30) {
            this.Driver = driver;
            this.Timeout = Timeout;
            this.Wait = new WebDriverWait(this.Driver, new TimeSpan(0, 0, Timeout));
        }

        /// <summary>
        /// Waits until browser window shows expected url.
        /// </summary>
        /// <param name="String">Url to wait for.</param>
        /// <returns>bool</returns>
        private bool WaitForUrlToContain(string String) {
            return Wait.Until(ExpectedConditions.UrlContains(String));
        }

        /// <summary>
        /// Look for the locator descriptor and returns a WebElement instance.
        /// </summary>
        /// <param name="Object">Object name</param>
        /// <returns>IWebElement</returns>
        private IWebElement GetObject(string Object)
        {
            return this.Driver.FindElement(By.XPath(ConfigurationSettings.AppSettings[Object]));
        }

        #region BankOfInternet specific functions
        public void ClickOpenAccount() {
            GetObject("btnOpenAccount").Click();
        }

        public void ClickEnrollNow() {
            GetObject("btnEnrollNow").Click();
        }

        public void SaveAndContinue() {
            GetObject("btnSaveAndContinue").Click();
        }

        public void IntroduceName(string FirstName, string LastName)
        {
            GetObject("txtFirstName").SendKeys(FirstName);
            GetObject("txtLastName").SendKeys(LastName);
        }

        public void IntroduceInitialContactInformation(string Email, string CellPhone, string AlternateEmail = "", string BusinessCellPhone = "", string HomePhone = "")
        {
            GetObject("txtEmail").SendKeys(Email);
            GetObject("txtConfirmEmail").SendKeys(Email);
            GetObject("txtPhoneNumber").SendKeys(CellPhone);
        }

        public void IntroducePassword(string Password)
        {
            GetObject("txtPassword").SendKeys(Password);
            GetObject("txtConfirmPassword").SendKeys(Password);
        }

        public bool SelectAccountType()
        {
            IWebElement RadioButton = GetObject("rdbtnIndividualAccount");
            RadioButton.Click();
            return RadioButton.Selected;
        }

        public bool SelectMoneyMarket()
        {
            IWebElement CheckButton = GetObject("chkMoneyMarketSavings");
            CheckButton.Click();
            return CheckButton.Selected;
        }

        public bool SelectSavingAccounts()
        {
            IWebElement CheckButton = GetObject("chkHighYieldSavings");
            CheckButton.Click();
            return CheckButton.Selected;
        }

        public bool SelectCertificateOfDeposit()
        {
            IWebElement CheckButton = GetObject("chkPersonal3MonthCD");
            CheckButton.Click();
            return CheckButton.Selected;
        }

        public bool SelectCheckingAccounts()
        {
            IWebElement CheckButton = GetObject("chkCashBackChecking");
            CheckButton.Click();
            return CheckButton.Selected;
        }

        public bool SelectOtherProducts()
        {
            IWebElement CheckButton = GetObject("chkFreeDirectDeposit");
            CheckButton.Click();
            return CheckButton.Selected;
        }

        public bool CheckTermsConditionsAndFees()
        {
            GetObject("chkTermsConditionsFeesUnchecked").Click();
            GetObject("chkTermsConditionsFeesUnchecked").Click();
            if (!GetObject("chkTermsConditionsFeesChecked").Displayed)
                return false;
            GetObject("chkElectronicDeliveryConsentUnchecked").Click();
            GetObject("chkElectronicDeliveryConsentUnchecked").Click();
            if (!GetObject("chkElectronicDeliveryConsentChecked").Displayed)
                return false;
            GetObject("chkPrivacyPolicyUnchecked").Click();
            GetObject("chkPrivacyPolicyUnchecked").Click();
            if (!GetObject("chkPrivacyPolicyChecked").Displayed)
                return false;
            return true;
        }

        public void IntroducePersonalInfo(string FirstName, string LastName, string DateOfBirth, string SSN)
        {
            this.IntroduceName(FirstName, LastName);
            SelectElement element = new SelectElement(GetObject("listSuffix"));
            element.SelectByIndex(4);
            GetObject("txtDateOfBirth").SendKeys(DateOfBirth);
            GetObject("txtSocialSecurityNumber").SendKeys(SSN);
            GetObject("txtConfirmSocialSecurityNumber").SendKeys(SSN);
            element = new SelectElement(GetObject("listCitizenship"));
            element.SelectByIndex(0);
        }

        public void IntroduceAddress(string HomeAddress, string Zipcode, string City, string State, string Years, string Months)
        {
            GetObject("txtHomeAddress1").SendKeys(HomeAddress);
            GetObject("txtZipCode").SendKeys(Zipcode);
            GetObject("txtCity").SendKeys(City);
            SelectElement element = new SelectElement(GetObject("listState"));
            element.SelectByText(State);
            GetObject("txtNumberOfYears").SendKeys(Years);
            GetObject("txtNumberOfMonths").SendKeys(Months);
            element = new SelectElement(GetObject("listSameAddressMailing"));
            element.SelectByText("Yes");
        }

        public bool VerifyEnrollPage() {
            return this.WaitForUrlToContain("bankenrollment");
        }

        public bool VerifyGettingStartedPage() {
            return this.WaitForUrlToContain("gettingstarted");
        }

        public bool VerifySelectProductsPage()
        {
            return this.WaitForUrlToContain("productselection");
        }

        public bool VerifyTermsAndConditionsPage()
        {
            return this.WaitForUrlToContain("terms");
        }

        public bool VerifyPersonalInformationPage()
        {
            return this.WaitForUrlToContain("personalinformation");
        }
        #endregion
    }
}
