
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Smartex.PageObj
{
    public class SignUpPage
    {
        [FindsBy(How = How.Id, Using = "user[first_name]")]
        private IWebElement firstName;

        [FindsBy(How = How.Id, Using = "user[last_name]")]
        private IWebElement lastName;

        [FindsBy(How = How.Id, Using = "user[email]")]
        private IWebElement Email;

        [FindsBy(How = How.Id, Using = "user[password]")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "user[terms]")]
        private IWebElement checkBox;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign up']")]
        private IWebElement signUpButton;

        private IWebDriver driver;

        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
       
        public void SigInForm(string firstname, string lastname, string email, string pass)
        {
            firstName.Clear();
            firstName.SendKeys(firstname);
            lastName.Clear();
            lastName.SendKeys(lastname);
            Email.Clear();
            Email.SendKeys(email);
            password.Clear();
            password.SendKeys(pass);
            checkBox.Click();
            signUpButton.Click();
        }

        public void WaitForValidationIfEmailAlreadyExists()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@role='alert']")));
            Assert.IsTrue(this.driver.FindElement(By.XPath("//li[@role='alert']")).Text == "Email has already been taken");
        }

        public void WaitForSigInSuccess()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@class='header__nav-item']//a")));
            Assert.IsTrue(this.driver.FindElement(By.XPath("//li[@class='header__nav-item']//a")).Text == "My Dashboard");
        }
    }
}
