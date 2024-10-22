//using Shouldly;
using FluentAssertions;
using System;
using Xunit;

namespace Snow.Test
{
    public class TypedTable
    {

        [Fact]
        public void ShouldUpdateBasicTableUrl()
        {
            //Act
            var UserTable = TestScope.TableInstance();
            //prevent from trying to connect
            UserTable.SN.Token = "blablabla";

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields={String.Join(",", TestScope.PropNames)}");
        }

        [Fact]
        public void ShouldAddLimit()
        {
            //Act
            var UserTable = TestScope.TableInstance();
            UserTable.SN.Token = "blablabla";
            UserTable.Limit(10);

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields={String.Join(",", TestScope.PropNames)}&sysparm_limit=10");
        }

        [Fact]
        public void ShouldAddOrderBy()
        {
            //Act
            var UserTable = TestScope.TableInstance();
            UserTable.SN.Token = "blablabla";
            UserTable
                .OrderBy(x => x.Age)
                .OrderByDesc(x => x.Name);

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields={String.Join(",", TestScope.PropNames)}&sysparm_limit=10000&sysparm_query=ORDERBYage^ORDERBYDESCstrange_name");
        }

        [Fact]
        public void ShouldSelect()
        {
            //Act
            var UserTable = TestScope.TableInstance();
            UserTable.SN.Token = "blablabla";
            UserTable
                .Select(x => x.Age, x => x.Name);

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields=age,strange_name");
        }


        [Fact]
        public void ShouldUpdateUrlParamsWhere()
        {
            //Act
            var UserTable = TestScope.TableInstance();
            UserTable.SN.Token = "blablabla";
            UserTable
                .Where(x => x.Name is DumpServer && x.Name.Contains("Branco") && x.Age != 10 && x.CamelCase == "");

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields={String.Join(",", TestScope.PropNames)}&sysparm_limit=10000&sysparm_query=strange_nameINSTANCEOFsnow_table_name^strange_nameLIKEBranco^age!=10^camel_case=&sysparm_exclude_reference_link=true");
        }



        [Fact]
        public void ShouldUpdateUrlParamsWhereWithFilter()
        {
            //Act
            var UserTable = TestScope.TableInstance2();
            UserTable.SN.Token = "blablabla";
            UserTable
                .Where(x => x.Name is DumpServer && x.Age != 10);

            //Assert
            UserTable.RequestUrl.Should().Contain($"{TestScope.Config.BaseAddress}/table/{TestScope.tableName}?sysparm_fields={String.Join(",", TestScope.PropNames2)}&sysparm_limit=10000&sysparm_query=nameINSTANCEOFsnow_table_name^age!=10^nameLikeBottero&sysparm_exclude_reference_link=true");
        }

        [Fact]
        public void ShouldHaveCorrectUrl()
        {
            var relTable = TestScope.RelationTable();
            //
            relTable.RequestUrl.Should().Be($"{TestScope.Config.BaseAddress}/table/cmdb_rel_ci?sysparm_fields=sys_id&sysparm_limit=10000&sysparm_query=installed_on.sys_class_name!=cmdb_ci_pc_hardware^display_nameNOT LIKEManagement%20Studio^display_nameSTARTSWITHSQL%20Server^display_nameENDSWITHIntegration%20Services^ORdisplay_nameLIKEEngine%20Services^ORdisplay_nameLIKEAnalysis%20Services^ORdisplay_nameENDSWITHReporting%20Services^ORdisplay_name=Microsoft%20Power%20BI%20Report%20Server&sysparm_exclude_reference_link=true");
        }
    }
}
