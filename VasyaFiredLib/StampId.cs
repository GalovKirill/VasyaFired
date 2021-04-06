using System;

namespace VasyaFiredLib
{
    public readonly struct StampId : IEquatable<StampId>
    {
        private readonly int _id;

        public StampId(int id) => _id = id;
        public bool Equals(StampId other) => _id == other._id;
        public override bool Equals(object obj) => obj is StampId other && Equals(other);
        public override int GetHashCode() => _id;
        
        public static implicit operator StampId(int id) => new(id);
        public override string ToString() => _id.ToString();
    }
}