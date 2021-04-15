using System;
using System.Collections.Generic;
using System.Linq;

namespace VasyaFiredLib.Organization
{
    public partial class Organization
    {
        public class Builder
        {
            private const int InitCapacity = 126;
            private readonly List<StampId> _stamps = new(InitCapacity);
            private readonly List<ConditionalRule> _conditionRules = new (InitCapacity);
            private readonly List<UnconditionalRule> _unconditionalRules = new (InitCapacity);
            private readonly List<Department> _departments = new(InitCapacity);

            /// <summary>
            /// добавить n департаментов в организацию. Созданные департаменты будут помещены в departmentIds
            /// </summary>
            public Builder AddDepartments(int n, out List<DepartmentId> departmentIds)
            {
                departmentIds = new List<DepartmentId>(InitCapacity);
                foreach (var departmentId in Enumerable.Range(_departments.Count, n))
                {
                    departmentIds.Add(departmentId);
                    _departments.Add(new Department(RuleId.Null, RuleType.Unconditional));
                }

                return this;
            }

            /// <summary>
            /// Добавить отделу условное правило
            /// </summary>
            /// <param name="departmentId"></param>
            /// <param name="rule"></param>
            /// <returns></returns>
            public Builder AddRule(DepartmentId departmentId, ConditionalRule rule)
            {
                RuleId ruleId = new RuleId(_conditionRules.Count);
                _conditionRules.Add(rule);
                Department department = new Department(ruleId, RuleType.Conditional);
                _departments[departmentId.Id] = department;
                return this;
            }
            
            /// <summary>
            /// Добавить отделу безусловное правило
            /// </summary>
            public Builder AddRule(DepartmentId departmentId, UnconditionalRule rule)
            {
                RuleId ruleId = new RuleId(_unconditionalRules.Count);
                _unconditionalRules.Add(rule);
                Department department = new Department(ruleId, RuleType.Unconditional);
                _departments[departmentId.Id] = department;
                return this;
            }
            
            /// <summary>
            /// добавить n печатей в организацию. Созданные печати будут помещены в createdStamps
            /// </summary>
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
            
            /// <summary>
            /// Добавить печать в организацию
            /// </summary>
            /// <param name="stampId">Добавленная печать</param>
            public Builder AddStamp(out StampId stampId)
            {
                stampId = new StampId(_stamps.Count);
                _stamps.Add(stampId);
                return this;
            }

            public Organization Build()
            {
                if (_departments.Contains(new Department(RuleId.Null, RuleType.Unconditional)))
                    throw new Exception("Не всем отделам были заданы правила");
                
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