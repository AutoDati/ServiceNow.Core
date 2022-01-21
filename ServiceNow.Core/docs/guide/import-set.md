## You can use ImportSet's

Used to send data to serviceNow like an feed process.
Allows multiples import set be exported in a single request.

See [Service Now Docs](https://docs.servicenow.com/bundle/orlando-it-service-management/page/product/service-catalog-management/task/t_DefineACatalogItem.html) to learn more

- You must first create an application in studio inside ServiceNow and then:
  - Create a catalogItem
  - create a flow

```csharp
var requestCatalog = serviceNow.UsingCatalog<Request>(new Guid("catalogItemIdHere"));

var request = await requestCatalog.Request(new{
    varNameHereString = "string",
    varNameHereNumber = 10,
    varNameHereReference = new Guid(sys_id),
}); ;
```
