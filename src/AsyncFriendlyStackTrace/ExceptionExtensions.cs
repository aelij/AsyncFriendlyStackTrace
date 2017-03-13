﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace AsyncFriendlyStackTrace
{
    /// <summary>
    /// Provides extension methods for <see cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        private const string EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";
        private const string AggregateExceptionFormatString = "{0}{1}---> (Inner Exception #{2}) {3}{4}{5}";
        private const string AsyncStackTraceExceptionData = "AsyncFriendlyStackTrace";

        private static Func<Exception, string> GetStackTraceString => 
            ReflectionUtil.GenerateGetField<Exception, string>(EnvironmentUtil.IsRunningOnMono() ? "stack_trace" : "_stackTraceString");

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

            var aggregate = exception as AggregateException;
            if (aggregate != null)
            {
                return ToAsyncAggregateString(exception, aggregate);
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

            var aggregate = exception as AggregateException;
            if (aggregate != null)
            {
                foreach (var innerException in aggregate.InnerExceptions)
                {
                    innerException.PrepareForAsyncSerialization();
                }
            }
            else
            {
                exception.InnerException?.PrepareForAsyncSerialization();
            }
        }

        private static string ToAsyncAggregateString(Exception exception, AggregateException aggregate)
        {
            var s = ToAsyncStringCore(exception, includeMessageOnly: true);
            for (var i = 0; i < aggregate.InnerExceptions.Count; i++)
            {
                s = string.Format(CultureInfo.InvariantCulture, AggregateExceptionFormatString, s,
                    Environment.NewLine, i, aggregate.InnerExceptions[i].ToAsyncString(), "<---", Environment.NewLine);
            }
            return s;
        }

        private static string ToAsyncStringCore(Exception exception, bool includeMessageOnly)
        {
            var message = exception.Message;
            var className = exception.GetType().ToString();
            var builder = new StringBuilder(message.Length <= 0 ? className : className + ": " + message);

            var innerException = exception.InnerException;
            if (innerException != null)
            {
                if (includeMessageOnly)
                {
                    do
                    {
                        builder.Append($" ---> {{{innerException.Message}}}");
                        innerException = innerException.InnerException;
                    } while (innerException != null);
                }
                else
                {
                    builder.Append($" ---> {{{innerException.ToAsyncString()}}}{Environment.NewLine}   {{{EndOfInnerExceptionStack}}}");
                }
            }

            builder.Append($"{{{Environment.NewLine}}}{{{GetAsyncStackTrace(exception)}}}");

            return builder.ToString();
        }

        private static string GetAsyncStackTrace(Exception exception)
        {
            var stackTrace = exception.Data[AsyncStackTraceExceptionData] ??
                             GetStackTraceString(exception) ??
                             new StackTrace(exception, true).ToAsyncString();
            var remoteStackTrace = GetRemoteStackTraceString(exception);
            return remoteStackTrace + stackTrace;
        }
    }
}