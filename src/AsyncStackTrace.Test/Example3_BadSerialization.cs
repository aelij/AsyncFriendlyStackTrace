using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AsyncStackTrace.Test
{
    internal class Example3_BadSerialization : IExample
    {
        public async Task Run()
        {
            try
            {
                await new Example1().Run();
            }
            catch (Exception e)
            {
                PrepareException(e);
                var serializedException = SerializeAndDeserializeException(e);
                throw new AggregateException(serializedException);
            }
        }

        protected virtual void PrepareException(Exception exception)
        {
            // do nothing
        }

        private static Exception SerializeAndDeserializeException(Exception e)
        {
            using (var stream = new MemoryStream())
            {
                // The caveat of using PrepareForAsyncSerialization is that it adds items
                // to the exception's Data dictionary, which can't be serialized by default using DCS.
                // Thus you must specify the dictionary's (internal) type as a known type...
                var serializer = new DataContractSerializer(e.GetType(), new[] { e.Data.GetType() });
                serializer.WriteObject(stream, e);
                stream.Position = 0;
                return (Exception)serializer.ReadObject(stream);
            }
        }
    }
}