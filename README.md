# Service Now Fluent API

Easy to connect, and interact with Service Now API

📕 See the [Full Documentation](https://autodati.github.io/ServiceNow.Core/)

## Description

Make request using fluent API!

- 🧰 Easy to use, based on the config file it connects and authenticate without one line of code
- 🔑 Typed data, you can use properties to manipulate and restrict requests
- 🛠 Typed properties can be used in Selects, Queries, Ordering...
- 🧩 Auto serialize/deserialize included
- 🎲 Generic Json Elements available as responses
- 📑 Methods and classes have documentation comments, get feedback as you type
- 🗜 Compress requests by default
- 👓 Extensions methods to change and get data from generic responses, log and much more...

### Dependencies

- .Net Core 3.1
- .Net Framework 4.6.1

### Installing

- Install the package.
- Set an appsettings.json with correct connection values.
  - The library check if scope has and .default or not to define how it will get access tokens
  - You can use Credentials or Client Secrets for daemon applications
- Create an instance of ServiceNow.
- Create an instance of a Table, (typed or not).
- Configure the request as you like.
- make the request with ToListAsync.

### Basic Examples

- Typed (Create any class derived from ServiceNowBaseModel class, so we have an Id Guid)
``` C#
//Creating an ServiceNow instance
var ServiceNow = new ServiceNow(config);

//Creating an table instance
var usersTable = ServiceNow
    .UsingTable<User>("sys_user")
    .Limit(2)
    .WithQuery(x => $"{x.Name} like Branco and {x.Country} = BR");

//Getting data
var users = await usersTable.ToListAsync();

while (users.Count > 0)
{
    foreach (var user in users)
        Console.WriteLine(user.ToString());
    //Next Request will get the next chunk of data
    users = await usersTable.ToListAsync();
}
```

Same as above but using Where
``` C#
//Creating an table instance
var usersTable = ServiceNow
    .UsingTable<User>("sys_user")
    .Limit(2)
    .Where(x => x.Name.Contains("Branco") && x.Country = "BR");
```

- Not typed
```C#
var usersTableNotTyped = ServiceNow
    .UsingTable("sys_user")
    .Limit(2)
    .WithQuery("name like Branco and country = BR");
var usersNotTyped = await usersTableNotTyped.ToListAsync();

while (usersNotTyped.Count > 0)
{
    usersNotTyped.ForEach(userNotTyped => userNotTyped.Display());
    usersNotTyped = await usersTableNotTyped.ToListAsync();
}
```

- Changing not typed Data
``` C#
//Creating a table instance
var incidentsTableNotTyped = ServiceNow
    .UsingTable("incident")
    .Limit(10);

//Getting data
var incidentsNotTyped = await incidentsTableNotTyped
        .Select(new[] { "sys_id", "short_description" })
        .WithQuery("short_description like some nice")
        .OrderBy("sys_id")
        .ToListAsync();

//Updating
incidentsNotTyped.ForEach(async incident =>
{
    Guid id = new Guid(incident.GetProperty("sys_id").ToString());
    ExpandoObject inc = incident.ToObject();
    inc.UpdateProp("short_description", "changed description on non typed value");
    var changed = await incidentsTableNotTyped.Update(id, inc);
    if (changed)
        Console.WriteLine("Incident Changed");
    else
        Console.WriteLine("Incident NOT Changed");
});
```

## You can create CatalogItem

See [Service Now Docs](https://docs.servicenow.com/bundle/orlando-it-service-management/page/product/service-catalog-management/task/t_DefineACatalogItem.html) to learn more

- You must first create an application in studio inside ServiceNow and then:
    - Create a catalogItem
    - create a flow

``` C#
var requestCatalog = serviceNow.UsingCatalog<Request>(new Guid("catalogItemIdHere"));

var request = await requestCatalog.Request(new{
    varNameHereString = "string",
    varNameHereNumber = 10,
    varNameHereReference = new Guid(sys_id),
}); ;
```

## You can set custom Serializers (You must set it only once, this is optional)
```C#
 //AddCustom Custom Serializers to static class used in ServiceNow
JsonConverterOptions.ConfigureCustomSerializers(new[] {
    new CustomRequestStateConverter() 
});
```

## Default Serializers for Guid and DateTime

When there is no value in the json response (while using not nullable version) it will return:
- new DateTime() "{1/1/0001 12:00:00 AM}" 
- Guid.Empty

That happens because ServiceNow not always return null, in those cases it returns an empty string.

## Help

- You can open issues to help improve and collaborate with the library.
- Demos included in project (only the console version for now)

## Authors

Emerson Bottero Branco DBAM Automation

## Version History

- 0.8.0
    - Table Creation
    - Select
    - Set Headers
    - With Query
    - Limit
    - OrderBy
    - OrderByDesc
    - ToList
    - Get
    - Delete
    - Create
    - Update 
    - Compression
    - Bug fix (authenticate and when token expiries)
    - Extensions method
    - More complex console demo examples
    - Authentication Improvements
    - Working web API demo (unavailable for now)
    - Custom Serializers Settings
    - New JsonConverters for null-able and normal types
    - New Update method
    - When writing new Classes don't set sys_id as null
    - Enum.ToDescription Method
    - WithQuery with no arguments
    - ImportSet API
    - SnowTable Attribute in classes (remove the need to pass table name as argument in WithTable methods)
- 0.8.1
    - Where Statement for usingTables
- 0.8.2
    - Small Fix and simples documentation
- 0.8.3
    - INSTANCEOF and set property to lower case in sysparam
