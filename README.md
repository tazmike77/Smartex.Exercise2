# How to run the Project

### Run the Test Regression Category with Edge:
``` 
dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Edge\") 
```


### Run the Test Regression Category with Chrome:
``` 
dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Chrome\") 
```


### Run the Test Regression Category with Firefox:
``` 
dotnet test Smartex.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\",value=\"Firefox\") 
```


### To Run all tests

``` 
dotnet test
 ```


#### In the end of the Run Test process a index.html is created with the tests report. The file is in the project folder.