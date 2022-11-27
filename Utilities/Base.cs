using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Smartex.Utilities
{
    public class Base
    {
        private ThreadLocal<IWebDriver> driver = new();
        private ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void Setup()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            var reportPath = projectDirectory + "//index.html";
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Pedro Silva PC");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Pedro Silva");
        }
       
        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            var browserName = TestContext.Parameters["browserName"] ?? ConfigurationManager.AppSettings["browser"];

            InitBrowser(browserName);

            if (driver.Value == null) return;
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://courses.ultimateqa.com/users/sign_in";
        }

        private void InitBrowser(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    driver.Value = new EdgeDriver();
                    break;
            }
        }

        protected static JsonReader GetDataParser()
        {
            return new JsonReader();
        }

        protected IWebDriver GetDriver()
        {
            return driver.Value;
        }

        [TearDown]
        public void CloseBrowser()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var fileName = "Screenshot_" + DateTime.Now.ToString("h_mm_ss") + ".png";

            switch (status)
            {
                case TestStatus.Failed:
                    test.Fail("Test failed", captureScreenshot(driver.Value, fileName));
                    test.Log(Status.Fail, "Test failed with logtrace " + stackTrace);
                    break;
                case TestStatus.Passed:
                    break;
                case TestStatus.Inconclusive:
                    break;
                case TestStatus.Skipped:
                    break;
                case TestStatus.Warning:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            extent.Flush();
           
            driver.Value?.Quit();
        }

        private MediaEntityModelProvider captureScreenshot(IWebDriver driver,string screenShotName)
        {
            var ts = (ITakesScreenshot)driver;
            var screenshotts = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotts, screenShotName).Build();
        }
    }
}
