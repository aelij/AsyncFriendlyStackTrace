using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace AsyncFriendlyStackTrace
{
    /// <summary>
    /// Provides extension methods for <see cref="StackTrace"/>.
    /// </summary>
    public static class StackTraceExtensions
    {
        private const string AtString = "at";
        private const string LineFormat = "in {0}:line {1}";
        private const string AsyncMethodPrefix = "async ";

        /// <summary>
        /// Produces an async-friendly readable representation of the stack trace.
        /// </summary>
        /// <remarks>
        /// The async-friendly formatting is archieved by:
        /// * Skipping all awaiter frames (all methods in types implementing <see cref="INotifyCompletion"/>).
        /// * Inferring the original method name from the async state machine class (<see cref="IAsyncStateMachine"/>)
        ///   and removing the "MoveNext" - currently only for C#.
        /// * Adding the "async" prefix after "at" on each line for async invocations.
        /// * Appending "(?)" to the method signature to indicate that parameter information is missing.
        /// * Removing the "End of stack trace from previous location..." text.
        /// </remarks>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>An async-friendly readable representation of the stack trace.</returns>
        public static string ToAsyncString(this StackTrace stackTrace)
        {
            if (stackTrace == null) throw new ArgumentNullException(nameof(stackTrace));
            var stackFrames = stackTrace.GetFrames();
            if (stackFrames == null) return string.Empty;

            var displayFilenames = true;
            var firstFrame = true;
            var stringBuilder = new StringBuilder(255);

            foreach (var frame in stackFrames)
            {
                var method = frame.GetMethod();
                
                if (method == null) continue;
                var declaringType = method.DeclaringType?.GetTypeInfo();
                // skip awaiters
                if (declaringType != null && typeof(INotifyCompletion).GetTypeInfo().IsAssignableFrom(declaringType)) continue;

                if (firstFrame)
                {
                    firstFrame = false;
                }
                else
                {
                    stringBuilder.Append(Environment.NewLine);
                }
                stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "   {0} ", AtString);

                var isAsync = FormatMethodName(stringBuilder, declaringType);
                if (!isAsync)
                {
                    stringBuilder.Append(method.Name);
                    var methodInfo = method as MethodInfo;
                    if (methodInfo != null && methodInfo.IsGenericMethod)
                    {
                        FormatGenericArguments(stringBuilder, methodInfo.GetGenericArguments());
                    }
                }
                else if (declaringType?.IsGenericType == true)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    FormatGenericArguments(stringBuilder, declaringType.GenericTypeArguments);
                }
                stringBuilder.Append("(");
                if (isAsync)
                {
                    stringBuilder.Append("?");
                }
                else
                {
                    FormatParameters(stringBuilder, method);
                }
                stringBuilder.Append(")");
                displayFilenames = FormatFileName(displayFilenames, frame, stringBuilder);
            }
            return stringBuilder.ToString();
        }

        private static bool FormatMethodName(StringBuilder stringBuilder, TypeInfo declaringType)
        {
            if (declaringType == null) return false;
            var isAsync = false;
            var fullName = declaringType.FullName.Replace('+', '.');
            if (typeof(IAsyncStateMachine).GetTypeInfo().IsAssignableFrom(declaringType))
            {
                isAsync = true;
                stringBuilder.Append(AsyncMethodPrefix);
                var start = fullName.LastIndexOf('<');
                var end = fullName.LastIndexOf('>');
                if (start >= 0 && end >= 0)
                {
                    stringBuilder.Append(fullName.Remove(start, 1).Substring(0, end - 1));
                }
                else
                {
                    stringBuilder.Append(fullName);
                }
            }
            else
            {
                stringBuilder.Append(fullName);
                stringBuilder.Append(".");
            }
            return isAsync;
        }

        private static bool FormatFileName(bool displayFilenames, StackFrame frame, StringBuilder stringBuilder)
        {
            if (displayFilenames && frame.GetILOffset() != -1)
            {
                string text = null;
                try
                {
                    text = frame.GetFileName();
                }
                catch (NotSupportedException)
                {
                    displayFilenames = false;
                }
                catch (SecurityException)
                {
                    displayFilenames = false;
                }
                if (text != null)
                {
                    stringBuilder.Append(' ');
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, LineFormat, text, frame.GetFileLineNumber());
                }
            }
            return displayFilenames;
        }

        private static void FormatParameters(StringBuilder stringBuilder, MethodBase method)
        {
            var parameters = method.GetParameters();
            var firstParam = true;
            foreach (var t in parameters)
            {
                if (!firstParam)
                {
                    stringBuilder.Append(", ");
                }
                else
                {
                    firstParam = false;
                }
                // ReSharper disable once ConstantConditionalAccessQualifier
                // ReSharper disable once ConstantNullCoalescingCondition
                var typeName = t.ParameterType?.Name ?? "<UnknownType>";
                stringBuilder.Append($"{typeName} {t.Name}");
            }
        }

        private static void FormatGenericArguments(StringBuilder stringBuilder, Type[] genericArguments)
        {
            stringBuilder.Append("[");
            var k = 0;
            var firstTypeParam = true;
            while (k < genericArguments.Length)
            {
                if (!firstTypeParam)
                {
                    stringBuilder.Append(",");
                }
                else
                {
                    firstTypeParam = false;
                }
                stringBuilder.Append(genericArguments[k].Name);
                k++;
            }
            stringBuilder.Append("]");
        }
    }
}