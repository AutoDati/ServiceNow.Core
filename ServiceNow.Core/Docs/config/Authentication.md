There are several options to authenticate in service Now.

## Basic Authentication

Usually you use this on developing in a dev instance of serviceNow

``` json
{
  "BaseAddress": "https://dev77132.service-now.com/api",
  "UserName": "admin",
  "Password": "CQ7K8RfgpaMg"
}
```

## AzureAd Authentication
  
  ::: tip
  The library check if scope has and .default or not to define how it will get access tokens <br/>
  You can use Credentials or Client Secrets for daemon applications
  :::

This uses Azure AD to handle authentication mostly used inside companies.

 ``` json
 {
  "Instance": "https://login.microsoftonline.com/",
  "TenantId": "Your company Azure Tenant ID",
  "ClientId": "You CLient Id",
  "ClientSecret": "You Client Secret",
  "BaseAddress": "the servicenow API address",
  "Scope": "the servicenow API address"
}
```

Those are parameters in the ServiceNow constructor 
``` csharp
var config = BasicAuthenticationConfig.ReadFromJsonFile("appsettings.json");
var ServiceNow = new ServiceNow(config);
```