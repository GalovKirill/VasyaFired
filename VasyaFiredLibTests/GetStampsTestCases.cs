using System;
using System.Collections;
using System.Collections.Generic;
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
                yield return Simple();
                yield return InfinityCycle();
                yield return TwiceVisit();
                yield return TwiceVisitWithEqualStampsSet();
                yield return SimpleNoVisitQ();
                yield return InfinityCycleWhitVisitQ();
            }
        }

        private static TestCaseData SimpleNoVisitQ()
        {
            Organization organization = new Organization.Builder()
                .AddStamps(4, out List<StampId> ss)
                .AddDepartments(4, out List<DepartmentId> ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[3])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1])
                .Build();

            Vasya v = new() {A = ds[0], Z = ds[2]};
            DismissalService dismissalService = new();
            DepartmentId q = ds[3];

            GetStampsResult expected = new()
            {
                InfinityCycle = false, 
                NoVisit = true, 
                VisitCount = 0,
                StampsSets = new HashSet<StampsCollection>()
            };

            return MakeTestCase(dismissalService, v, q, organization, expected)
                .SetName("Без цикла и посещения Q");
        }
        
        private static TestCaseData TwiceVisitWithEqualStampsSet()
        {
            Organization organization = new Organization.Builder()
                .AddStamps(5, out var ss)
                .AddDepartments(5, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[4], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddConditionalRule(ds[2], ss[3], ss[1], ss[4], ds[4], ss[1], ss[4], ds[3])
                .AddUnconditionalRule(ds[3], ss[3], ss[4], ds[1])
                .AddUnconditionalRule(ds[4], ss[1], ss[4], ds[0])
                .Build();

            DepartmentId q = ds[1];
            Vasya v = new() {A = ds[0], Z = ds[4]};
            GetStampsResult expected = new()
            {
                InfinityCycle = true, NoVisit = false,
                VisitCount = 2,
                StampsSets = new HashSet<StampsCollection>
                {
                    new () {ss[0], ss[1]}
                }
            };
            return MakeTestCase(new DismissalService(), v, q, organization, expected)
                .SetName("Двойное посещение Q но с одним различным обходным листом");
        }

        private static TestCaseData InfinityCycle()
        {
            Organization organization = new Organization.Builder()
                .AddStamps(4, out var ss)
                .AddDepartments(4, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[0])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1])
                .Build();
            
            Vasya v = new() {A = ds[0], Z = ds[3]};
            DismissalService dismissalService = new();
            DepartmentId q = ds[3];

            GetStampsResult expected = new()
            {
                InfinityCycle = true, 
                NoVisit = true, 
                VisitCount = 0,
                StampsSets = new HashSet<StampsCollection>()
            };


            return MakeTestCase(dismissalService, v, q, organization, expected)
                .SetName("C бесконечным циклом без посещения Q")
                .SetDescription("0 -> 1 -> 2 -> 1");// как заменить кран буксу
        }
        
        private static TestCaseData InfinityCycleWhitVisitQ()
        {
            Organization organization = new Organization.Builder()
                .AddStamps(4, out var ss)
                .AddDepartments(4, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[0])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1])
                .Build();
            
            Vasya v = new() {A = ds[0], Z = ds[3]};
            DismissalService dismissalService = new();
            DepartmentId q = ds[2];

            GetStampsResult expected = new()
            {
                InfinityCycle = true,
                NoVisit = false,
                VisitCount = 1,
                StampsSets = new HashSet<StampsCollection>
                {
                    new() {ss[0], ss[1], ss[2]}
                }
            };


            return MakeTestCase(dismissalService, v, q, organization, expected)
                .SetName("Бесконечный цикл с посещением Q")
                .SetDescription("0 -> 1 -> 2 -> 0");
        }

        private static TestCaseData TwiceVisit()
        {
            Organization organization = new Organization.Builder()
                .AddStamps(5, out var ss)
                .AddDepartments(5, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[1], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[0], ds[2])
                .AddConditionalRule(ds[2], ss[3], ss[1], ss[4], ds[4], ss[4], ss[1], ds[3])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1])
                .AddUnconditionalRule(ds[4], ss[4], ss[0], ds[0])
                .Build();

            DepartmentId q = ds[1];
            Vasya v = new() {A = ds[0], Z = ds[4]};
            GetStampsResult expected = new()
            {
                InfinityCycle = false, NoVisit = false,
                VisitCount = 2,
                StampsSets = new HashSet<StampsCollection>
                {
                    new() {ss[1]},
                    new() {ss[1], ss[3], ss[4]}
                }
            };
            return MakeTestCase(new DismissalService(), v, q, organization, expected)
                .SetName("Двойное посещение Q");
        }

        private static TestCaseData Simple()
        {
            Organization.Builder builder = new Organization.Builder()
                .AddStamps(4, out var ss)
                .AddDepartments(4, out var ds)
                .AddUnconditionalRule(ds[0], ss[0], ss[3], ds[1])
                .AddUnconditionalRule(ds[1], ss[1], ss[3], ds[2])
                .AddUnconditionalRule(ds[2], ss[2], ss[3], ds[3])
                .AddUnconditionalRule(ds[3], ss[3], ss[0], ds[1]);
            
            Vasya v = new() {A = ds[0], Z = ds[3]};
            DismissalService dismissalService = new();

            GetStampsResult expected = new()
            {
                InfinityCycle = false, 
                NoVisit = false, 
                VisitCount = 1,
                StampsSets = new HashSet<StampsCollection> 
                {
                    new () {ss[1], ss[2], ss[3]}
                }
            };

            return MakeTestCase(dismissalService, v, ds[3], builder.Build(), expected)
                .SetName("Простой кейс c одним посещением Q")
                .SetDescription("0 -> 1 -> 2 -> !3");
        }

        private static TestCaseData MakeTestCase(DismissalService dismissalService,
            Vasya v,
            DepartmentId q,
            Organization organization,
            GetStampsResult expected)
        {
            return new (dismissalService, v, q, organization, expected);
        }
    }
}