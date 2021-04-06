using System;

namespace VasyaFiredLib
{
    public readonly struct Department : IEquatable<Department>
    {
        public readonly RuleId RuleId;
        public readonly RuleType RuleType;

        public Department(RuleId ruleId, RuleType ruleType)
        {
            RuleId = ruleId;
            RuleType = ruleType;
        }

        public bool Equals(Department other)
        {
            return RuleId.Equals(other.RuleId) && RuleType == other.RuleType;
        }

        public override bool Equals(object obj)
        {
            return obj is Department other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RuleId, (int) RuleType);
        }

        public override string ToString()
        {
            return $"[{RuleId}, {(int) RuleType}]";
        }
    }
}