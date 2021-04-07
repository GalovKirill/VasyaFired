using NUnit.Framework;
using VasyaFiredLib;

namespace VasyaFiredLibTests
{
    public class DismissalServiceTests
    {
        [Test]
        public void Case1()
        {
            Organization.Builder builder = new Organization.Builder()
                .AddStamps(4, out var ss)
                .AddDepartments(4, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[3])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1]);

            var v = new Vasya {A = ds[0], Z = ds[3]};
            DismissalService dismissalService = new();

            GetStampsResult expected = new GetStampsResult
            {
                InfinityCycle = false, NoVisit = false, VisitCount = 1,
                StampsSets = new[]
                {
                    new StampId[] {new(1), new(2), new(3)}
                }
            };

            GetStampsResult actual = dismissalService.GetStamps(v, ds[3], builder.Build());
            
            Assert.AreEqual(expected, actual);
        }
    }
}