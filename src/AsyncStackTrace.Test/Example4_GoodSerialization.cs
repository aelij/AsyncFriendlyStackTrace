using System;

namespace AsyncStackTrace.Test
{
    internal class Example4_GoodSerialization : Example3_BadSerialization
    {
        protected override void PrepareException(Exception exception)
        {
            exception.PrepareForAsyncSerialization();
        }
    }
}