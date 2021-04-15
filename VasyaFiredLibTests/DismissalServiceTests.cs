using System.Threading.Tasks;
using NUnit.Framework;
using VasyaFiredLib.Dismissals;
using VasyaFiredLib.Organization;

namespace VasyaFiredLibTests
{
    [TestFixture]
    public class DismissalServiceTests
    {

        [TestCaseSource(typeof(GetStampsTestCases), nameof(GetStampsTestCases.TestCases))]
        [Timeout(5_000)]
        
        public void GetStampsTest(DismissalService service,
            Vasya vasya,
            DepartmentId q,
            Organization organization,
            GetStampsResult expected)
        {
            GetStampsResult actual = service.GetStamps(vasya, q, organization);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(typeof(GetStampsTestCases), nameof(GetStampsTestCases.TestCases))]
        [Timeout(5_000)]
        public void GetStampsFromDifferentThreadsTest(DismissalService service,
            Vasya vasya,
            DepartmentId q,
            Organization organization,
            GetStampsResult expected)
        {
            Parallel.For(0, 16, _ =>
            {
                GetStampsResult actual = service.GetStamps(vasya, q, organization);
                Assert.AreEqual(expected, actual);
            });
        }
    }
}