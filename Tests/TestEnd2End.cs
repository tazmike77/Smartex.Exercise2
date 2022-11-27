using NUnit.Framework;
using Smartex.Utilities;
using Smartex.PageObj;

namespace Smartex.Tests
{
    public class E2ETest : Base
    {
        [Test, TestCaseSource("LoginDataConfig"), Category("Regression")]
        public void EndToEndTests(string email, string password)
        {
            var loginPage = new LoginPage(GetDriver());

            loginPage.LoginForm(email, password);
            loginPage.WaitForConfirmation();
        }

        [Test, TestCaseSource("SignInDataConfig"), Category("Regression")]
        public void SignInWithAlreadyExistedEmail(string email, string password, string firstname, string lastname)
        {
            var loginPage = new LoginPage(GetDriver());
            var signUpPage = loginPage.SignInLink();

            signUpPage.SigInForm(firstname, lastname, email, password);
            signUpPage.WaitForValidationIfEmailAlreadyExists();
        }

        [Test, TestCaseSource("SignInDataConfig"), Category("Regression")]
        public void SignInWithnewEmail(string email, string password, string firstname, string lastname)
        {
            email = Stringutilities.GenerateRandomEmail();
            var loginPage = new LoginPage(GetDriver());
            var signUpPage = loginPage.SignInLink();

            signUpPage.SigInForm(firstname, lastname, email, password);
            signUpPage.WaitForSigInSuccess();
        }

        [TestCaseSource("LoginFailedDataConfig")]
        [Test, Category("Smoke")] //wrong and empty password tests
        public void Login_Failed(string email, string wrongPassword)
        {
            var loginPage = new LoginPage(GetDriver());
            loginPage.LoginForm(email, wrongPassword);
            loginPage.WaitForWrongPassword(wrongPassword);
        }

        public static IEnumerable<TestCaseData> LoginDataConfig()
        {
            yield return new TestCaseData(GetDataParser().ExtractData("email"), 
                GetDataParser().ExtractData("password"));
        }

        public static IEnumerable<TestCaseData> LoginFailedDataConfig()
        {
            yield return new TestCaseData(GetDataParser().ExtractData("email"),
            GetDataParser().ExtractData("empty_password"));

            yield return new TestCaseData(GetDataParser().ExtractData("email"),
                GetDataParser().ExtractData("wrong_password"));
        }

        public static IEnumerable<TestCaseData> SignInDataConfig()
        {
            yield return new TestCaseData(GetDataParser().ExtractData("email"),
                GetDataParser().ExtractData("password"),
                GetDataParser().ExtractData("firstname"),
                GetDataParser().ExtractData("lastname"));
        }
    }
}
