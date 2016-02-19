using System;
using System.Threading.Tasks;

namespace AsyncFriendlyStackTrace.Test
{
    internal class Example1 : IExample
    {
        public async Task Run()
        {
            await A();
        }

        private async Task A()
        {
            await B();
        }

        private async Task B()
        {
            await C();
        }

        private async Task C()
        {
            await Task.Yield();
            throw new Exception("Crash! Boom! Bang!");
        }
    }
}