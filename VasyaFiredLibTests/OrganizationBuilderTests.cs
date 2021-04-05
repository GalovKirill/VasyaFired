using System.Runtime.InteropServices;
using NUnit.Framework;
using VasyaFiredLib;

namespace VasyaFiredLibTests
{
    public class OrganizationBuilderTests
    {


        [Test]
        public void Test1()
        {
            var builder = new Organization.Builder();

            builder.AddStamp(out var s1)
                .AddStamp(out var s2)
                .AddStamp(out var s3)
                .AddDepartments(2, out var departmentIds)
                .AddRule(departmentIds[0], new ConditionRule(s1, s2, s3, departmentIds[1], s2, s3, departmentIds[1]))
                .AddRule(departmentIds[1], new UnconditionalRule(s1, s2, departmentIds[0]));

            Organization actual = builder.Build();
        }
    }
}