using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Domain.Enum
{
    public enum StatusType
    {
        [Description("Success")]
        Ok = 200,
        [Description("Process Error")]
        ProcessError = 400,
        [Description("Unhandled Exception")]
        UnHandledException = 500
    }
}
