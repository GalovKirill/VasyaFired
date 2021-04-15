using System;
using System.Collections.Generic;
using VasyaFiredLib.Organization;

namespace VasyaFiredLib.Dismissals
{
    internal class CycleChecker : ICycleChecker
    {
        private readonly HashSet<DepartmentId> _visited = new();
        private readonly Dictionary<Edge, StampsCollection> _backwardsEdges = new();


        public void AddToVisited(DepartmentId id)
        {
            _visited.Add(id);
        }

        public bool IsInInfinityCycle(StampsCollection currentStamps, DepartmentId next, DepartmentId current)
        {
            if (!_visited.Contains(next)) 
                return false;
            
            Edge backwardsEdge = new(current, next);
            if (_backwardsEdges.TryGetValue(backwardsEdge, out var stamps))
            {
                if (currentStamps.Equals(stamps))
                    return true;
            }
            else
            {
                _backwardsEdges[backwardsEdge] = new StampsCollection(currentStamps);
            }

            return false;
        }
        
        private readonly struct Edge : IEquatable<Edge>
        {
            public readonly DepartmentId A;
            public readonly DepartmentId B;
            public Edge(DepartmentId a, DepartmentId b)
            {
                (A, B) = (a, b);
            }

            public bool Equals(Edge other)
            {
                return A.Equals(other.A) && B.Equals(other.B);
            }

            public override bool Equals(object obj)
            {
                return obj is Edge other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(A, B);
            }
        }
    }
}