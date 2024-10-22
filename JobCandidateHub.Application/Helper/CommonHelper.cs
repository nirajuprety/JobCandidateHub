using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Helper
{
    [ExcludeFromCodeCoverage]
    public static class CommonHelper
    {
        public static TimeOnly ConvertStringToTimeOnly(string timeString)
        {
            if (TimeOnly.TryParse(timeString, out TimeOnly time))
            {
                return time;
            }
            return new TimeOnly();
        }
    }
}
