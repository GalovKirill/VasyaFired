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

            public Builder AddStamps(int n, out List<StampId> createdStamps)
            {
                createdStamps = new List<StampId>(InitCapacity);
                foreach (var stampId in Enumerable.Range(_stamps.Capacity, n))
                {
                    createdStamps.Add(stampId);
                    _stamps.Add(stampId);
                }
                
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