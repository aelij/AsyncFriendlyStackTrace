#if !NET6_0_OR_GREATER
using System;

namespace AsyncFriendlyStackTrace
{
    internal static class EnvironmentUtil
    {
        // see http://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/#runtime-conditionals
        private static readonly Lazy<bool> _runningOnMono = new Lazy<bool>(() => Type.GetType("Mono.Runtime") != null);

        internal static bool IsRunningOnMono()
        {
            return _runningOnMono.Value;
        }
    }
}
#endif
