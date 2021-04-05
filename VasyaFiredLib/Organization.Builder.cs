using System.Collections.Generic;
using System.Linq;

namespace VasyaFiredLib
{
    public partial class Organization
    {
        public class Builder
        {
            private const int InitCapacity = 126;
            private readonly List<StampId> _stamps = new(InitCapacity);
            private readonly List<ConditionRule> _conditionRules = new (InitCapacity);
            private readonly List<UnconditionalRule> _unconditionalRules = new (InitCapacity);
            private readonly List<Department> _departments = new(InitCapacity);

            public Builder AddDepartments(int n, out List<DepartmentId> departmentIds)
            {
                departmentIds = new List<DepartmentId>(InitCapacity);
                foreach (var departmentId in Enumerable.Range(_departments.Count, n))
                {
                    departmentIds.Add(departmentId);
                    _departments.Add(default);
                }

                return this;
            }

            public Builder AddRule(DepartmentId departmentId, ConditionRule rule)
            {
                RuleId ruleId = new RuleId(_conditionRules.Count);
                _conditionRules.Add(rule);
                Department department = new Department(ruleId, RuleType.Conditional);
                _departments[departmentId.Id] = department;
                return this;
            }
            
            public Builder AddRule(DepartmentId departmentId, UnconditionalRule rule)
            {
                RuleId ruleId = new RuleId(_unconditionalRules.Count);
                _unconditionalRules.Add(rule);
                Department department = new Department(ruleId, RuleType.Unconditional);
                _departments[departmentId.Id] = department;
                return this;
            }
            
            public Builder AddStamps(int n, out List<StampId> createdStamps)
            {
                createdStamps = new List<StampId>(InitCapacity);
                foreach (var stampId in Enumerable.Range(_stamps.Count, n))
                {
                    createdStamps.Add(stampId);
                    _stamps.Add(stampId);
                }
                
                return this;
            }
            public Builder AddStamp(out StampId stampId)
            {
                stampId = new StampId(_stamps.Count);
                _stamps.Add(stampId);
                return this;
            }

            public Builder AddDepartment(ConditionRule rule, out DepartmentId departmentId)
            {
                RuleId ruleId = new(_conditionRules.Count);
                _conditionRules.Add(rule);
                Department department = new (ruleId, RuleType.Conditional);
                departmentId = new (_departments.Count);
                _departments.Add(department);
                return this;
            }
            
            public Builder AddDepartment(UnconditionalRule rule, out DepartmentId departmentId)
            {
                RuleId ruleId = new(_unconditionalRules.Count);
                _unconditionalRules.Add(rule);
                Department department = new(ruleId, RuleType.Unconditional);
                departmentId = new(_departments.Count);
                _departments.Add(department);
                return this;
            }

            public Organization Build()
            {
                return new ()
                {
                    Stamps = _stamps.ToArray(),
                    ConditionRules = _conditionRules.ToArray(),
                    UnconditionalRules = _unconditionalRules.ToArray(),
                    Departments = _departments.ToArray(),
                };
            }
        }
    }
}