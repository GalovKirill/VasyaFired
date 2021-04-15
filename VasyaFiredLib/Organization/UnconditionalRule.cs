using System;

namespace VasyaFiredLib.Organization
{
    public readonly struct UnconditionalRule : IEquatable<UnconditionalRule>
    {
        public readonly StampId I;
        public readonly StampId J;
        public readonly DepartmentId K;

        /// <param name="i">Поставить новую печать I если ее еще нет (или она зачеркнута) или не ставить никакую.</param>
        /// <param name="j">Зачеркнуть существующую печать J если она уже есть и незачеркнута или не зачеркивать никакую.</param>
        /// <param name="k">Отправить Васю в следующий отдел K.</param>
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

        public override string ToString()
        {
            return $"[{I}, {J}, {K}]";
        }
    }
}