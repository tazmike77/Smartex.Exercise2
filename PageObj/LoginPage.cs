using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Smartex.PageObj
{
    public class LoginPage
    {
        [FindsBy(How = How.Id, Using = "user[email]")]
        private IWebElement username;

        [FindsBy(How = How.Id, Using = "user[password]")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign in']")]
        private IWebElement loginbutton;

        [FindsBy(How = How.CssSelector, Using = "a[href='/users/sign_up']")]
        private IWebElement newAccountButton;

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void LoginForm(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            loginbutton.Click();
        }

        public SignUpPage SignInLink()
        {
            newAccountButton.Click();
            return new SignUpPage(driver);
        }

        public void WaitForWrongPassword(string pass)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("message-text")));
            
            Assert.IsTrue(this.driver.FindElement(By.ClassName("message-text")).Text == "Invalid email or password.");
        }

        public void WaitForConfirmation()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("message-text")));

            Assert.IsTrue(this.driver.FindElement(By.ClassName("message-text")).Text == "Signed in successfully.");
        }
    }
}
