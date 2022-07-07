using SNow.Core;
using SNow.Core.Authentication;
using SNow.Core.Utils;
using System.Collections.Generic;

namespace Snow.Test
{
    public static class TestScope
    {
        public static AuthenticationConfig Config => new ()
        {
            BaseAddress = "https://someUrl.com/api/now/",
            ClientSecret = "Blablabla"
        };
        public static string tableName => "sys_user";

        public static List<string> PropNames => ClassReflections.GetPropertieNamesInJsonFormat<DumpUser>();
        public static List<string> PropNames2 => ClassReflections.GetPropertieNamesInJsonFormat<DumpUser2>();

        public static Table<DumpUser> TableInstance()
        {
            var SN = new ServiceNow(Config);

            var UserTable = SN.UsingTable<DumpUser>(tableName);
            return (Table<DumpUser>)UserTable;
        }

        public static Table<DumpUser2> TableInstance2()
        {
            var SN = new ServiceNow(Config);

            var UserTable = SN.UsingTable<DumpUser2>(tableName);
            return (Table<DumpUser2>)UserTable;
        }

    }
}
