using Newtonsoft.Json.Linq;

namespace Smartex.Utilities
{
    public class JsonReader
    {
        public string ExtractData(string tokenName)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            var reportPath = projectDirectory + "//utilities//testData.json";
            var myJsonString = File.ReadAllText(reportPath);

            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
    }
}
