using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NuClear.Telemetry.Probing;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming
{
    internal static class MergeTool
    {
        public static MergeResult<T> Merge<T>(IEnumerable<T> data1, IEnumerable<T> data2)
        {
            using (Probe.Create("Merging", typeof(T).Name))
            {
                var set1 = new HashSet<T>(data1);
                var set2 = new HashSet<T>(data2);

                // NOTE: avoiding enumerable extensions to reuse hashset performance
                var difference = set1.Where(x => !set2.Contains(x));
                var intersection = set1.Where(x => set2.Contains(x));
                var complement = set2.Where(x => !set1.Contains(x));

                return new MergeResult<T> { Difference = difference, Intersection = intersection, Complement = complement };
            }
        }

        public interface IMergeResult
        {
            IEnumerable Difference { get; }
            IEnumerable Intersection { get; }
            IEnumerable Complement { get; }
        }

        public class MergeResult<T> : IMergeResult
        {
            public IEnumerable<T> Difference { get; set; }
            public IEnumerable<T> Intersection { get; set; }
            public IEnumerable<T> Complement { get; set; }

            IEnumerable IMergeResult.Difference
            {
                get { return Difference; }
            }

            IEnumerable IMergeResult.Intersection
            {
                get { return Intersection; }
            }

            IEnumerable IMergeResult.Complement
            {
                get { return Complement; }
            }
        }
    }
}