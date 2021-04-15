using VasyaFiredLib.Organization;

namespace VasyaFiredLib.Dismissals
{
    internal interface ICycleChecker
    {
        void AddToVisited(DepartmentId id);
        bool IsInInfinityCycle(StampsCollection currentStamps, DepartmentId next, DepartmentId current);
    }
}