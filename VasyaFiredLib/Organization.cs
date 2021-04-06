using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VasyaFiredLib
{
    public partial class Organization
    {
        public StampId[] Stamps { get; init; }
        public ConditionRule[] ConditionRules { get; init; }
        public UnconditionalRule[] UnconditionalRules { get; init; }
        public Department[] Departments { get; init; }

        public ref readonly Department GetDepartment(DepartmentId id) => ref Departments[id.Id];
        public ref readonly ConditionRule GetConditionRule(RuleId id) => ref ConditionRules[id.Id];
        public ref readonly UnconditionalRule GetUnconditionalRule(RuleId id) => ref UnconditionalRules[id.Id];

        public override bool Equals(object? obj) => obj is Organization other && Equals(other);

        public bool Equals(Organization other)
        {
            if (Stamps.Length != other.Stamps.Length
                || UnconditionalRules.Length != other.UnconditionalRules.Length
                || ConditionRules.Length != other.ConditionRules.Length
                || Departments.Length != other.Departments.Length)
                return false;
            
            return Stamps.Zip(other.Stamps).All(PairEquals)
                   && Departments.Zip(other.Departments).All(PairEquals)
                   && ConditionRules.Zip(other.ConditionRules).All(PairEquals)
                   && UnconditionalRules.Zip(other.UnconditionalRules).All(PairEquals);
            
            
            static bool PairEquals<T>((T first, T second) pair)
                where T: IEquatable<T> => pair.first.Equals(pair.second);
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            Dictionary<string, IEnumerable<object>> objects = new()
            {
                [nameof(Stamps)] = Stamps.Cast<object>(),
                [nameof(Departments)] = Departments.Cast<object>(),
                [nameof(ConditionRules)] = ConditionRules.Cast<object>(),
                [nameof(UnconditionalRules)] = UnconditionalRules.Cast<object>()
            };
            
            foreach (var (name, obs) in objects)
            {
                builder.Append(name).Append(": ");
                builder = obs.Aggregate(builder, (b, o) => b.Append(' ').Append(o));
                builder.Append('\n');
            }

            return builder.ToString();
        }
    }
}