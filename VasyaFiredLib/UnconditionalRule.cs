namespace VasyaFiredLib
{
    public readonly struct UnconditionalRule
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
    }
}