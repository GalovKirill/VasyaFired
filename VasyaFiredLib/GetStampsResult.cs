using System;
using System.Linq;
using System.Text;

namespace VasyaFiredLib
{
    public class GetStampsResult : IEquatable<GetStampsResult>
    {
        public bool InfinityCycle { get; set; }
        public bool NoVisit { get; set; }
        public int VisitCount { get; set; }
        public StampId[][] StampsSets { get; set; }

        public bool Equals(GetStampsResult other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            return InfinityCycle == other.InfinityCycle &&
                   NoVisit == other.NoVisit &&
                   VisitCount == other.VisitCount &&
                   StampsSetsEquals(StampsSets, other.StampsSets);
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

        private static bool StampsSetsEquals(StampId[][] stampsSets, StampId[][] otherStampsSets)
        {
            if (stampsSets.Length != otherStampsSets.Length)
                return false;
            for (int i = 0; i < stampsSets.Length; i++)
            {
                var set = stampsSets[i].ToHashSet();
                var otherSet = otherStampsSets[i].ToHashSet();
                // set.ExceptWith(otherSet);
                // otherSet.ExceptWith(set);
                set.SymmetricExceptWith(otherSet);
                if (set.Count != 0 )
                    return false;
            }
            return true;
        }
    }
}