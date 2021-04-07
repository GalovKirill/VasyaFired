namespace VasyaFiredLib
{
    public static class OrganizationBuilderExtensions
    {
        public static Organization.Builder AddConditionalRule(this Organization.Builder builder,
            DepartmentId departmentId, 
            StampId s, 
            StampId i, 
            StampId j, 
            DepartmentId k, 
            StampId t, 
            StampId r,
            DepartmentId p)
        {
            return builder.AddRule(departmentId, new ConditionalRule(s, i, j, k, t, r, p));
        }
        
        public static Organization.Builder AddUnconditionalRule(this Organization.Builder builder,
            DepartmentId departmentId, 
            StampId i, 
            StampId j, 
            DepartmentId k)
        {
            return builder.AddRule(departmentId, new UnconditionalRule(i, j, k));
        }
    }
}