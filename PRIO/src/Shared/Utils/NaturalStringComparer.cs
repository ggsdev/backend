using System.Text.RegularExpressions;

namespace PRIO.src.Shared.Utils
{
    public class NaturalStringComparer : IComparer<string>
    {
        private static readonly Regex NumericPartRegex = new Regex(@"(\d+)", RegexOptions.Compiled);

        public int Compare(string x, string y)
        {
            var numericPartsX = NumericPartRegex.Matches(x).Cast<Match>().Select(m => int.Parse(m.Value));
            var numericPartsY = NumericPartRegex.Matches(y).Cast<Match>().Select(m => int.Parse(m.Value));

            using (var enumX = numericPartsX.GetEnumerator())
            using (var enumY = numericPartsY.GetEnumerator())
            {
                while (enumX.MoveNext() && enumY.MoveNext())
                {
                    int cmp = enumX.Current.CompareTo(enumY.Current);
                    if (cmp != 0)
                    {
                        return cmp;
                    }
                }

                if (enumX.MoveNext()) return 1;
                if (enumY.MoveNext()) return -1;
            }

            return string.Compare(x, y, StringComparison.Ordinal);
        }
    }
}