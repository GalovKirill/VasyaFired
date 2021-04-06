using NUnit.Framework;
using VasyaFiredLib;

namespace VasyaFiredLibTests
{
    public class OrganizationBuilderTests
    {


        [Test]
        public void SameOrganizationIsEqual()
        {
            var builder = new Organization.Builder();

            builder.AddStamp(out var s1)
                .AddStamp(out var s2)
                .AddStamp(out var s3)
                .AddDepartments(2, out var departmentIds)
                .AddRule(departmentIds[0], new ConditionRule(s1, s2, s3, departmentIds[1], s2, s3, departmentIds[1]))
                .AddRule(departmentIds[1], new UnconditionalRule(s1, s2, departmentIds[0]));

            Organization actual = builder.Build();

            var expected = new Organization
            {
                Stamps = new StampId[] {0, 1, 2},
                ConditionRules = new ConditionRule[] {new(0, 1, 2, 1, 1, 2, 1)},
                UnconditionalRules = new UnconditionalRule[] {new(0, 1, 0)},
                Departments = new Department[] {new(0, RuleType.Conditional), new(0, RuleType.Unconditional)}
            };
            
            Assert.AreEqual(expected, actual);
        }
    }
}