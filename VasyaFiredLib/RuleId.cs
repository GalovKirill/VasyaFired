using System;

namespace VasyaFiredLib
{
    public readonly struct RuleId : IEquatable<RuleId>
    {
        public readonly int Id;

        public RuleId(int id) => Id = id;

        public bool Equals(RuleId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is RuleId other && Equals(other);
        public override int GetHashCode() => Id;
        
    }
}