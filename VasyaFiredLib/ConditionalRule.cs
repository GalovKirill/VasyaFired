using System;

namespace VasyaFiredLib
{
    public readonly struct ConditionalRule : IEquatable<ConditionalRule>
    {
        public readonly StampId S;
        
        public readonly StampId I;
        public readonly StampId J;
        public readonly DepartmentId K;
        
        public readonly StampId T;
        public readonly StampId R;
        public readonly DepartmentId P;

        public ConditionalRule(StampId s, StampId i, StampId j, DepartmentId k, StampId t, StampId r, DepartmentId p)
        {
            S = s;
            I = i;
            J = j;
            K = k;
            T = t;
            R = r;
            P = p;
        }

        public bool Equals(ConditionalRule other)
        {
            return S.Equals(other.S) &&
                   I.Equals(other.I) &&
                   J.Equals(other.J) &&
                   K.Equals(other.K) &&
                   T.Equals(other.T) &&
                   R.Equals(other.R) &&
                   P.Equals(other.P);
        }

        public override bool Equals(object obj)
        {
            return obj is ConditionalRule other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(S, I, J, K, T, R, P);
        }

        public override string ToString()
        {
            return $"[{S}, {I}, {J}, {K}, {T}, {R}, {P}]";
        }
    }
}