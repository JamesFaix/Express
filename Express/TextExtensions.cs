using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Express {

    static class TextExtensions {

        public static string LeftOf(this string str, char c) =>
           str.Substring(0, str.IndexOf(c));

        public static string ToTitleCase(this string str) =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);

        public static StringBuilder AppendEach(this StringBuilder @this, IEnumerable<string> sequence) {
            foreach (var item in sequence) {
                @this.Append(item);
            }
            return @this;
        }

        public static StringBuilder AppendEach<T>(this StringBuilder @this, IEnumerable<T> items, Func<T, string> format) =>
            @this.AppendEach(items.Select(format));
    }
}
