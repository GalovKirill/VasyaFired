using System;

namespace VasyaFiredLib.Organization
{
    public readonly struct StampId : IEquatable<StampId>
    {
        private readonly int _id;

        public StampId(int id)
        {
            _id = id;
        }

        public bool Equals(StampId other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is StampId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public static implicit operator StampId(int id)
        {
            return new(id);
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}