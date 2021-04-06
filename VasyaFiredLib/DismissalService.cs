using System;
using System.Collections.Generic;

namespace VasyaFiredLib
{
    public class DismissalService
    {
        public IEnumerable<IEnumerable<StampId>> GetStamps(Vasya vasya, DepartmentId q, Organization organization)
        {
            HashSet<DepartmentId> visited = new();
            Stack<HashSet<StampId>> result = new();

            
            DepartmentId next = default, current = vasya.A;
            do
            {
                visited.Add(current);
                ref readonly Department department = ref organization.GetDepartment(current);
                if (q.Equals(current))
                {
                    result.Push(new HashSet<StampId>(result.Peek()));
                }

                HashSet<StampId> currentStamps = result.Peek();
                switch (department.RuleType)
                {
                    case RuleType.Conditional:
                    {
                        ref readonly ConditionRule rule = ref organization.GetConditionRule(department.RuleId);
                        if (currentStamps.Contains(rule.S))
                        {
                            currentStamps.Add(rule.I);
                            currentStamps.Remove(rule.J);
                            next = rule.K;
                        }
                        else
                        {
                            currentStamps.Add(rule.T);
                            currentStamps.Remove(rule.R);
                            next = rule.P;
                        }

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
                
                
            } while (!current.Equals(vasya.Z));

            
            
            
            
            return result;
        }
    }
}