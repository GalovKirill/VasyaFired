using System;

namespace VasyaFiredLib
{
    public readonly struct DepartmentId : IEquatable<DepartmentId>
    {
        public readonly int Id;

        public DepartmentId(int id) => Id = id;

        public bool Equals(DepartmentId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is DepartmentId other && Equals(other);
        public override int GetHashCode() => Id;
        public static implicit operator DepartmentId(int id) => new(id);
        public override string ToString() => Id.ToString();
    }
}