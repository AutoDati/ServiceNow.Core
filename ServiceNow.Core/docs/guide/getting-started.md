## Getting Started

- Install the [nuget package](https://www.nuget.org/packages/ServiceNow.Core/)
- See [Configurations](/config/Authentication) to setup Authentication as well Custom Serializers
- Create an instance of ServiceNow.

```csharp
var config = BasicAuthenticationConfig.ReadFromJsonFile("appsettings.json");
var ServiceNow = new ServiceNow(config);
```

- It is possible to use Injection as well

```csharp
services.AddSingleton<IServiceNow>(new ServiceNow(config));
```

## Not typed

- Create a Table Instance (Not Typed) and set request parameters

```csharp
var usersTableNotTyped = ServiceNow
    .UsingTable("sys_user")
    .Limit(2)
    .WithQuery("name like Branco and country = BR");

// The request is only made when ToListAsync is used.
var usersNotTyped = await usersTableNotTyped.ToListAsync();

while (usersNotTyped.Count > 0)
{
    usersNotTyped.ForEach(userNotTyped => userNotTyped.Display());
    //pagination is handled automatically and will be reset when we receive a response with 0 elements.
    usersNotTyped = await usersTableNotTyped.ToListAsync();
}
```

## Typed

- Create a Table Instance (Typed) and set request parameters
  To use types version it is needed an class inherited from ServiceNowBaseModel

  ::: tip
  Use Attributes to map table names (SnowTable) and properties (JsonPropertyName) used in ServiceNow. <br/>
  You also can set a Filter in the class, In this case there no need to use withQuery or Where methods. Very useful when reading the relational table <br/>
  You can just copy and past the query from the table view in ServiceNow!
  :::

Creating an table class model

```csharp
    //Remove the need to use .WithTable
    [SnowTable("sys_user")]
    //Remove the need to use .Where or .WithQuery and helps to keep context inside the class
    [SnowFilter("nameLikeBottero")]
    public class User : ServiceNowBaseModel
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("u_city_code")]
        public string CityCode { get; set; }
        public string Email { get; set; }

        [JsonPropertyName("user_name")]
        public string LanID { get; set; }

        public override string ToString()
        {
            return $"user:{Name}, State:{State}, CityCode:{CityCode}, email:{Email}";
        }
    }
```

Creating an table instance

```csharp
var usersTable = ServiceNow
    .UsingTable<User>("sys_user")
    .Limit(2)
    .Where(x => x.Name.Contains("Branco") && x.Country = "BR");
```

# More Examples

```csharp
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
