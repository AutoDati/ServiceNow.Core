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
            BaseAddress = "https://someUrl.com",
            ClientSecret = "Blablabla"
        };
        public static string tableName => "sys_user";

        public static List<string> PropNames => ClassReflections.GetPropertieNamesInJsonFormat<DumpUser>();

        public static Table<DumpUser> TableInstance()
        {
            var SN = new ServiceNow(Config);

            var UserTable = SN.UsingTable<DumpUser>(tableName);
            return (Table<DumpUser>)UserTable;
        }

    }
}
