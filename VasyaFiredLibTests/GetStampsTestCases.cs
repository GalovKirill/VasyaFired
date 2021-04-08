using System.Collections;
using NUnit.Framework;
using VasyaFiredLib;

namespace VasyaFiredLibTests
{
    public class GetStampsTestCases
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return SimpleCase();
                yield return InfinityCycleCase();
            }
        }
        public static IEnumerable TestCasesParallel
        {
            get
            {
                yield return SimpleCase();
                yield return InfinityCycleCase();
            }
        }
        private static TestCaseData InfinityCycleCase()
        {
            Organization.Builder builder = new Organization.Builder()
                .AddStamps(4, out var ss)
                .AddDepartments(4, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[0])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1]);
            
            var v = new Vasya {A = ds[0], Z = ds[3]};
            DismissalService dismissalService = new();

            var expected = new GetStampsResult
            {
                InfinityCycle = false, 
                NoVisit = false, 
                VisitCount = 1,
                StampsSets = new[]
                {
                    new StampId[] {new(1), new(2), new(3)}
                }
            };

            return new TestCaseData(dismissalService, v, ds[3], builder.Build(), expected)
                .SetName("Простой кейс c бесконечным циклом")
                .SetDescription("0 -> 1 -> 2 -> 1");
        }

        private static TestCaseData SimpleCase()
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

            var expected = new GetStampsResult
            {
                InfinityCycle = false, 
                NoVisit = false, 
                VisitCount = 1,
                StampsSets = new[]
                {
                    new StampId[] {new(1), new(2), new(3)}
                }
            };

            return new TestCaseData(dismissalService, v, ds[3], builder.Build(), expected)
                .SetName("Простой кейс")
                .SetDescription("0 -> 1 -> 2 -> !3");
        }
    }
}