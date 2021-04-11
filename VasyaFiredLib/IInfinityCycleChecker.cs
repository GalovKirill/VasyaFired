namespace VasyaFiredLib
{
    internal interface IInfinityCycleChecker
    {
        void AddToVisited(DepartmentId id);
        bool IsInInfinityCycle(StampsCollection currentStamps, DepartmentId next, DepartmentId current);
    }
}