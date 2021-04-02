using System;

namespace VasyaFiredLib
{
    public readonly struct DepartmentId : IEquatable<DepartmentId>
    {
        public readonly int Id;
        
        public bool Equals(DepartmentId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is DepartmentId other && Equals(other);
        public override int GetHashCode() => Id;
    }
}