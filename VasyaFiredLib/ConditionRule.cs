namespace VasyaFiredLib
{
    public readonly struct ConditionRule
    {
        public readonly StampId S;
        
        public readonly StampId I;
        public readonly StampId J;
        public readonly DepartmentId K;
        
        public readonly StampId T;
        public readonly StampId R;
        public readonly DepartmentId P;

        public ConditionRule(StampId s, StampId i, StampId j, DepartmentId k, StampId t, StampId r, DepartmentId p)
        {
            S = s;
            I = i;
            J = j;
            K = k;
            T = t;
            R = r;
            P = p;
        }
    }
}