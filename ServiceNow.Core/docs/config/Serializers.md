## Default Serializers for Guid and DateTime

When there is no value in the json response (while using not nullable version) it will return:
- new DateTime() "{1/1/0001 12:00:00 AM}" 
- Guid.Empty

That happens because ServiceNow not always return null, in some cases it returns an empty string.

The response from REST requests are deserialized based on the type of the properties.
By default Guid values as "sys_id" and DateTime as update as serialize with no configurations.

Custom Serializers can be use to convert number to Enums.

``` csharp
var customEnum = new CustomEnumConverter<YourEnum>
new ServiceNow(config, new []{customEnum})
```

``` csharp
enum YourEnum{
    Valid,
    Invalid,
    Pending
}
```


## You can set custom Serializers (You must set it only once, this is optional)
``` csharp
 //AddCustom Custom Serializers to static class used in ServiceNow
JsonConverterOptions.ConfigureCustomSerializers(new[] {
    new CustomRequestStateConverter() 
});
```