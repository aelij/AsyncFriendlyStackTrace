using System;
using System.IO;
using System.Linq;

namespace AsyncFriendlyStackTrace.Test
{
    public class Program
    {
        private static bool _outputToFile;

        public static void Main(string[] args)
        {
            _outputToFile = args.Any(arg => string.Equals(arg, "/file", StringComparison.OrdinalIgnoreCase));

            Run<Example1>();
            Run<Example2_Wait>();
            Run<Example3_BadSerialization>();
            Run<Example4_GoodSerialization>();
        }

        private static void Run<TExample>()
            where TExample : IExample, new()
        {
            if (_outputToFile)
            {
                using (var writer = File.CreateText($"{typeof (TExample).Name}.md"))
                {
                    Run<TExample>(writer);
                }
            }
            else
            {
                Run<TExample>(Console.Out);
            }
        }

        private static void Run<TExample>(TextWriter writer)
            where TExample : IExample, new()
        {
            var name = typeof(TExample).Name;
            writer.WriteLine(name);
            writer.WriteLine(new string('=', name.Length));
            try
            {
                new TExample().Run().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                writer.WriteLine("OLD");
                writer.WriteLine("---");
                writer.WriteLine("```");
                writer.WriteLine(e.ToString());
                writer.WriteLine("```");
                writer.WriteLine();

                writer.WriteLine("NEW");
                writer.WriteLine("---");
                writer.WriteLine("```");
                writer.WriteLine(e.ToAsyncString());
                writer.WriteLine("```");
            }
            writer.WriteLine();
            writer.WriteLine();
        }
    }
}
