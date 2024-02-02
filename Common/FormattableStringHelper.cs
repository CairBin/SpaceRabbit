using System;
using System.Linq;

namespace Commons
{
    public static class FormattableStringHelper
    {
        public static string BuildUrl(FormattableString urlFmt)
        {
            var invariantParams = urlFmt.GetArguments().Select(x => FormattableString.Invariant($"{x}"));
            object[] escapParams = invariantParams.Select(x => (object)Uri.EscapeDataString(x)).ToArray();

            return string.Format(urlFmt.Format, escapParams);
        }
    }
}
