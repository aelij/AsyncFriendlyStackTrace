﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace AsyncFriendlyStackTrace
{
    /// <summary>
    /// Provides extension methods for <see cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        #if NET6_0_OR_GREATER

        /// <summary>
        /// Gets an async-friendly <see cref="Exception"/> string using <see cref="StackTraceExtensions.ToAsyncString"/>. On .NET 6+ just calls <see cref="Exception.ToString"/>.
        /// </summary>
        [Obsolete("This package is no longer needed. Use Exception.ToString() instead.")]
        public static string ToAsyncString(this Exception exception) => exception.ToString();

        /// <summary>
        /// Prepares an <see cref="Exception"/> for serialization by including the async-friendly stack trace. On .NET 6+ is a no-op.
        /// </summary>
        [Obsolete("This package is no longer needed.")]
        public static void PrepareForAsyncSerialization(this Exception exception) {}

        #else

        private const string EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";
        private const string AggregateExceptionFormatString = "{0}{1}---> (Inner Exception #{2}) {3}{4}{5}";
        private const string AsyncStackTraceExceptionData = "AsyncFriendlyStackTrace";

        private static readonly Lazy<string> StackTraceFieldNameGuesser = new Lazy<string>(
            () => EnvironmentUtil.IsRunningOnMono()
                && ReflectionUtil.HasField<Exception>("stack_trace") ? "stack_trace" : "_stackTraceString");

        private static Func<Exception, string> GetStackTraceString => 
            ReflectionUtil.GenerateGetField<Exception, string>(StackTraceFieldNameGuesser.Value);

        private static readonly Func<Exception, string> GetRemoteStackTraceString =
            ReflectionUtil.GenerateGetField<Exception, string>("_remoteStackTraceString");

        /// <summary>
        /// Gets an async-friendly <see cref="Exception"/> string using <see cref="StackTraceExtensions.ToAsyncString"/>.
        /// Includes special handling for <see cref="AggregateException"/>s.
        /// </summary>
        /// <param name="exception">The exception to format.</param>
        /// <returns>An async-friendly string representation of an <see cref="Exception"/>.</returns>
        public static string ToAsyncString(this Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            var innerExceptions = GetInnerExceptions(exception);
            if (innerExceptions != null)
            {
                return ToAsyncAggregateString(exception, innerExceptions);
            }

            return ToAsyncStringCore(exception, includeMessageOnly: false);
        }

        /// <summary>
        /// Prepares an <see cref="Exception"/> for serialization by including the async-friendly
        /// stack trace as additional <see cref="Exception.Data"/>.
        /// Note that both the original and the new stack traces will be serialized.
        /// This method operates recursively on all inner exceptions,
        /// including ones in an <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void PrepareForAsyncSerialization(this Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            if (exception.Data[AsyncStackTraceExceptionData] != null ||
                GetStackTraceString(exception) != null)
                return;

            exception.Data[AsyncStackTraceExceptionData] = GetAsyncStackTrace(exception);

            var innerExceptions = GetInnerExceptions(exception);
            if (innerExceptions != null)
            {
                foreach (var innerException in innerExceptions)
                {
                    innerException.PrepareForAsyncSerialization();
                }
            }
            else
            {
                exception.InnerException?.PrepareForAsyncSerialization();
            }
        }

        private static IList<Exception> GetInnerExceptions(Exception exception)
        {
            if (exception is AggregateException aggregateException)
            {
                return aggregateException.InnerExceptions;
            }

            if (exception is ReflectionTypeLoadException reflectionTypeLoadException)
            {
                return reflectionTypeLoadException.LoaderExceptions;
            }

            return null;
        }

        private static string ToAsyncAggregateString(Exception exception, IList<Exception> inner)
        {
            var s = ToAsyncStringCore(exception, includeMessageOnly: true);
            for (var i = 0; i < inner.Count; i++)
            {
                s = string.Format(CultureInfo.InvariantCulture, AggregateExceptionFormatString, s,
                    Environment.NewLine, i, inner[i].ToAsyncString(), "<---", Environment.NewLine);
            }
            return s;
        }

        private static string ToAsyncStringCore(Exception exception, bool includeMessageOnly)
        {
            var message = exception.Message;
            var className = exception.GetType().ToString();
            var s = message.Length <= 0 ? className : className + ": " + message;

            var innerException = exception.InnerException;
            if (innerException != null)
            {
                if (includeMessageOnly)
                {
                    do
                    {
                        s += " ---> " + innerException.Message;
                        innerException = innerException.InnerException;
                    } while (innerException != null);
                }
                else
                {
                    s += " ---> " + innerException.ToAsyncString() + Environment.NewLine +
                         "   " + EndOfInnerExceptionStack;
                }
            }

            s += Environment.NewLine + GetAsyncStackTrace(exception);

            return s;
        }

        private static string GetAsyncStackTrace(Exception exception)
        {
            var stackTrace = exception.Data[AsyncStackTraceExceptionData] ??
                             GetStackTraceString(exception) ??
                             new StackTrace(exception, true).ToAsyncString();
            var remoteStackTrace = GetRemoteStackTraceString(exception);
            return remoteStackTrace + stackTrace;
        }

        #endif
    }
}
