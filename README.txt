-> To run test catergory Rgression with Edge
	dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Edge\")

-> To run test catergory Rgression with Chrome
	dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Chrome\")

-> To run test catergory Rgression with Firefox
	dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Firefox\")

-> To Run all tests
	dotnet test

-> In the end of the Run Test process a index.html is created with the tests report
   The file is in the project folder.

Best Regards,
PS
	
