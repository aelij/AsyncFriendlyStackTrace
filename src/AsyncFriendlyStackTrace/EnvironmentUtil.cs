using System;

namespace AsyncFriendlyStackTrace
{
    internal static class EnvironmentUtil
    {
        private static Lazy<bool> _runningOnMono = new Lazy<bool>(() => Type.GetType("Mono.Runtime") != null);

        // see http://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/#runtime-conditionals
        internal static bool IsRunningOnMono()
        {
            return _runningOnMono.Value;
        }
    }
}
