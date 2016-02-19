using System;

namespace AsyncStackTrace.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run<Example1>();
            Run<Example2_Wait>();
            Run<Example3_BadSerialization>();
            Run<Example4_GoodSerialization>();
        }

        private static void Run<TExample>()
            where TExample : IExample, new()
        {
            Console.WriteLine($"============== {typeof(TExample).Name} ==============");
            Console.WriteLine();
            try
            {
                new TExample().Run().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine("======= OLD =======");
                Console.WriteLine(e.ToString());
                Console.WriteLine();

                Console.WriteLine("======= NEW =======");
                Console.WriteLine(e.ToAsyncString());
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
