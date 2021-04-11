using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VasyaFiredLib
{
    public class GetStampsResult : IEquatable<GetStampsResult>
    {
        public bool InfinityCycle { get; set; }
        public bool NoVisit { get; set; }
        public int VisitCount { get; set; }
        public ISet<StampsCollection> StampsSets { get; set; }

        public bool Equals(GetStampsResult other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            return InfinityCycle == other.InfinityCycle &&
                   NoVisit == other.NoVisit &&
                   VisitCount == other.VisitCount && 
                   StampsSets.SetEquals(other.StampsSets);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GetStampsResult) obj);
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.AppendLine($"{nameof(InfinityCycle)}: {InfinityCycle}")
                .AppendLine($"{nameof(NoVisit)}: {NoVisit}")
                .AppendLine($"{nameof(VisitCount)}: {VisitCount}");
            
            foreach (var stampsSet in StampsSets)
            {
                builder.Append('[');
                foreach (StampId stampId in stampsSet)
                {
                    builder.Append(stampId).Append(',');
                }
                builder.Remove(builder.Length - 1, 1).Append(']').Append('\n');
            }

            return builder.ToString();
        }
        
    }
}