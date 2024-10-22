using System.ComponentModel;

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
