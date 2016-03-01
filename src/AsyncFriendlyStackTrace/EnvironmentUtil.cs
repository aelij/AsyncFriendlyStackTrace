using System;

namespace AsyncFriendlyStackTrace
{
    internal static class EnvironmentUtil
    {
        // see http://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/#runtime-conditionals
        internal static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
