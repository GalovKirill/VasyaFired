using System.Collections.Generic;

namespace VasyaFiredLib
{
    public partial class Organization
    {
        public StampId[] Stamps { get; init; }
        public ConditionRule[] ConditionRules { get; init; }
        public UnconditionalRule[] UnconditionalRules { get; init; }
        public Department[] Departments { get; init; }

        public ref readonly Department GetDepartment(DepartmentId id)
        {
            return ref Departments[id.Id];
        }

        public ref readonly ConditionRule GetConditionRule(RuleId id)
        {
            return ref ConditionRules[id.Id];
        }
        
        public ref readonly UnconditionalRule GetUnconditionalRule(RuleId id)
        {
            return ref UnconditionalRules[id.Id];
        }
    }
}