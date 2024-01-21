using Models;
using SNow;
using SNow.Core;
using SNow.Core.Authentication;
using SNow.Core.Extensions;
using System;
using System.Dynamic;
using System.Text.Json;

namespace Test_Console
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            ConsoleColor.Green.WriteLine("Service Now Console Demo! (with service account)");
            //var config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");
            var config = BasicAuthenticationConfig.ReadFromJsonFile("appsettings.json");
            try
            {
                //Typed Version...
                ConsoleColor.Yellow.WriteLine($"Typed Example!");
                ConsoleColor.Cyan.WriteLine($"Typed Get!");
                var ServiceNow = new ServiceNow(config);
                //var usersTable = ServiceNow
                //    .UsingTable<User>("sys_user")
                //    .Limit(2)
                //    .WithQuery(x => $"{x.Name} like Branco and {x.Country} = BR");
                //var users = await usersTable.ToListAsync();

                var customApi = ServiceNow.UsingAPI("x_755331_test");
                var data = await customApi.GetAsync("osdb/servers");
                var response = await data.Content.ReadAsStringAsync();
                ConsoleColor.Green.WriteLine(response);

                var id = new Guid("3682abf03710200044e0bfc8bcbe5d00");

                var name = "Jimmie";
                var usersTable = ServiceNow
                    .UsingTable<User>("sys_user")
                    .Limit(2)
                    .Where(x => !x.Id.Equals(id) && x.Name.Contains(name) && !x.Email.Contains("&"));
                    //.Where(x => x.Name.Contains(name) && x.Email.Contains("&"));

                var users = await usersTable.ToListAsync();

                while (users.Count > 0)
                {
                    foreach (var user in users)
                        Console.WriteLine(user.ToString());
                    //Next Request will get the next chunk of data
                    users = await usersTable.ToListAsync();
                }
                ConsoleColor.Green.WriteLine("Done...");

                ConsoleColor.Cyan.WriteLine($"Typed GetById!");

                var someUser = await usersTable.GetByIdAsync(new Guid("1e114e1f1b08a8107b9e8734ec4bcb4a"));

                if(someUser == null)
                    ConsoleColor.Red.WriteLine($"user not found");
                else
                    ConsoleColor.Blue.WriteLine($"user ={someUser.Id}: {someUser.Name}");

                someUser = await usersTable.GetByIdAsync(new Guid("02826bf03710200044e0bfc8bcbe5d55"));

                if (someUser == null)
                    ConsoleColor.Red.WriteLine($"user not found");
                else
                    ConsoleColor.Blue.WriteLine($"user ={someUser.Id}: {someUser.Name}");

                //Non Typed Version...
                ConsoleColor.Yellow.WriteLine($"Not Typed Example!");

                Console.WriteLine("****** USERS");

                var usersTableNotTyped = ServiceNow
                    .UsingTable("sys_user")
                    //.Select(new[] { "country", "state", "name", "u_city_code", "email", "user_name", "sys_id"})
                    .Limit(2)
                    .WithQuery("name like Jimmie and email like zarzyckiR");
                ConsoleColor.Red.WriteLine(usersTableNotTyped.RequestUrl);
                var usersNotTyped = await usersTableNotTyped.ToListAsync();

                while (usersNotTyped.Count > 0)
                {
                    usersNotTyped.ForEach(userNotTyped => userNotTyped.Display());
                    usersNotTyped = await usersTableNotTyped.ToListAsync();
                }
                ConsoleColor.Green.WriteLine("✔ Done...");

                var someNotTypedUser = await usersTableNotTyped.GetByIdAsync(new Guid("02826bf03710200044e0bfc8bcbe5d55"));

                ConsoleColor.Magenta.WriteLine($"User: {someNotTypedUser.GetProperty("sys_id")} => {someNotTypedUser.GetProperty("name")} found!!!");

                Console.WriteLine("******* INCIDENTS");
                var incidentsTableNotTyped = ServiceNow
                  .UsingTable("incident")
                  .Limit(10);

                //Creating
                //var done = await incidentsTableNotTyped.Create(new { short_description = "somenice description here..." });
                //if (done)
                //    Console.WriteLine("Incident Created");
                //else
                //    Console.WriteLine("Incident NOT Created");

                var incidentsNotTyped = await incidentsTableNotTyped
                    .Select(new[] { "sys_id", "short_description" })
                    .WithQuery("short_description like some nice")
                    .OrderBy("sys_id")
                    .AllToListAsync();

                incidentsNotTyped.ForEach(incident => incident.Display());

                //Updating
                //incidentsNotTyped.ForEach(async incident =>
                //{
                //    Guid id = new Guid(incident.GetProperty("sys_id").ToString());
                //    ExpandoObject inc = incident.ToObject();
                //    inc.UpdateProp("short_description", "changed description on non typed value");
                //    var changed = await incidentsTableNotTyped.Update(id, inc);
                //    if (changed)
                //        Console.WriteLine("Incident Changed");
                //    else
                //        Console.WriteLine("Incident NOT Changed");
                //});

                ConsoleColor.Green.WriteLine("🔔 Done...");

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

    }
}
