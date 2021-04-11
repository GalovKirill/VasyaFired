using System;

namespace VasyaFiredLib
{
    public readonly struct DepartmentId : IEquatable<DepartmentId>
    {
        public static readonly DepartmentId Null = new(-1);
        
        public readonly int Id;

        public DepartmentId(int id)
        {
            Id = id;
        }

        public bool Equals(DepartmentId other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is DepartmentId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static implicit operator DepartmentId(int id)
        {
            return new(id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static bool operator ==(DepartmentId a, DepartmentId b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DepartmentId a, DepartmentId b)
        {
            return !a.Equals(b);
        }
    }
}