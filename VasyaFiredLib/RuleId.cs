using System;

namespace VasyaFiredLib
{
    public readonly struct RuleId : IEquatable<RuleId>
    {
        public static readonly RuleId Null = new(-1);
        
        public readonly int Id;

        public RuleId(int id)
        {
            Id = id;
        }

        public bool Equals(RuleId other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is RuleId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static implicit operator RuleId(int id)
        {
            return new(id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}