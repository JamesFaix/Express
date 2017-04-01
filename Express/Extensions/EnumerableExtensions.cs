using System.Collections.Generic;
using System.Text;

namespace Express {

    static class EnumerableExtensions {

        public static string ToDelimitedString<T>(this IEnumerable<T> seq, string delimiter, string format = "{0}") {
            var sb = new StringBuilder();
            using (var e = seq.GetEnumerator()) {
                if (e.MoveNext()) {
                    sb.AppendFormat(format, e.Current);
                }
                while (e.MoveNext()) {
                    sb.AppendFormat(delimiter + format, e.Current.ToString());
                }
            }
            return sb.ToString();
        }
    }
}
