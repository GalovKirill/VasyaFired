using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VasyaFiredLib.Organization
{
    /// <summary>
    /// Класс - обходной лист
    /// </summary>
    public class StampsCollection : IEquatable<StampsCollection>, ICollection<StampId>
    {
        private readonly HashSet<StampId> _stampIds;

        public StampsCollection() : this(Array.Empty<StampId>()) { }
        public StampsCollection(IEnumerable<StampId> ids)
        {
            _stampIds = new HashSet<StampId>(ids);
        }

        public void Add(StampId id)
        {
            _stampIds.Add(id);
        }

        public void Clear()
        {
            _stampIds.Clear();
        }

        public void CopyTo(StampId[] array, int arrayIndex)
        {
            _stampIds.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _stampIds.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(StampId id)
        {
            return _stampIds.Remove(id);
        }

        public bool Contains(StampId id)
        {
            return _stampIds.Contains(id);
        }

        public bool Equals(StampsCollection other)
        {
            if (ReferenceEquals(null, other)) return false;
            return _stampIds.SetEquals(other._stampIds);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StampsCollection) obj);
        }
        
        public override int GetHashCode()
        {
            return _stampIds.Aggregate(0, (acc, id) => acc ^ id.GetHashCode());
        }

        public IEnumerator<StampId> GetEnumerator()
        {
            return _stampIds.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}