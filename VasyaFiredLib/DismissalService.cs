using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace VasyaFiredLib
{
    public class DismissalService
    {
        public GetStampsResult GetStamps(Vasya vasya, DepartmentId q, Organization organization)
        {
            HashSet<DepartmentId> visited = new();
            Stack<HashSet<StampId>> result = new();
            result.Push(new HashSet<StampId>());
            
            DepartmentId next = vasya.A, 
                current = DepartmentId.Null, 
                previous = DepartmentId.Null;
            do
            {
                previous = current;
                current = next;
                next = DepartmentId.Null;
                
                if (q.Equals(previous))
                {
                    result.Push(new HashSet<StampId>(result.Peek()));
                }
                
                ref readonly Department department = ref organization.GetDepartment(current);
                

                HashSet<StampId> currentStamps = result.Peek();
                switch (department.RuleType)
                {
                    case RuleType.Conditional:
                    {
                        ref readonly ConditionalRule rule = ref organization.GetConditionRule(department.RuleId);
                        var (add, remove, goTo) = currentStamps.Contains(rule.S) ? (rule.I, rule.J, rule.K) : (rule.T, rule.R, rule.P);
                        currentStamps.Add(add);
                        currentStamps.Remove(remove);
                        next = goTo;
                        break;
                    }
                    case RuleType.Unconditional:
                    {
                        ref readonly UnconditionalRule rule = ref organization.GetUnconditionalRule(department.RuleId);
                        currentStamps.Add(rule.I);
                        currentStamps.Remove(rule.J);
                        next = rule.K;
                        break;
                    }
                }

                visited.Add(current);
            } while (!current.Equals(vasya.Z));
            
            return new GetStampsResult
            {
                InfinityCycle = false,
                NoVisit = false,
                VisitCount = result.Count,
                StampsSets = result.Reverse().Select(set => set.ToArray()).ToArray()
            };
        }
    }
}