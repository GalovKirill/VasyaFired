using System;

namespace VasyaFiredLib
{
    public readonly struct UnconditionalRule : IEquatable<UnconditionalRule>
    {
        public readonly StampId I;
        public readonly StampId J;
        public readonly DepartmentId K;

        public UnconditionalRule(StampId i, StampId j, DepartmentId k)
        {
            I = i;
            J = j;
            K = k;
        }

        public bool Equals(UnconditionalRule other)
        {
            return I.Equals(other.I) && J.Equals(other.J) && K.Equals(other.K);
        }

        public override bool Equals(object obj)
        {
            return obj is UnconditionalRule other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(I, J, K);
        }
    }
}