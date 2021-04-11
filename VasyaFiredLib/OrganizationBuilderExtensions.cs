namespace VasyaFiredLib
{
    public static class OrganizationBuilderExtensions
    {
        /// <summary>
        /// Добавить отделу условное правило
        /// </summary>
        /// <param name="s">Если в обходном листе есть незачеркнутая печать S, то</param>
        /// <param name="i">Поставить новую печать I если ее еще нет (или она зачеркнута) или не ставить никакую.</param>
        /// <param name="j">Зачеркнуть существующую печать J если она уже есть и незачеркнута или не зачеркивать никакую.</param>
        /// <param name="k">Отправить Васю в следующий отдел K.</param>
        /// <param name="t">Иначе поставить новую печать T если ее еще нет (или она зачеркнута) или не ставить никакую.</param>
        /// <param name="r">Зачеркнуть существующую печать R если она уже есть (или незачеркнута) или не зачеркивать никакую.</param>
        /// <param name="p">Отправить Васю в следующий отдел P.</param>
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
        
        /// <summary>
        /// Добавить отделу безусловное правило
        /// </summary>
        /// <param name="i">Поставить новую печать I если ее еще нет (или она зачеркнута) или не ставить никакую.</param>
        /// <param name="j">Зачеркнуть существующую печать J если она уже есть и незачеркнута или не зачеркивать никакую.</param>
        /// <param name="k">Отправить Васю в следующий отдел K.</param>
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