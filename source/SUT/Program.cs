using System;

namespace SUT
{
    class Program
    {
        static void Main(string[] args)
        {
            var classA = new ClassA();
            var classB = new ClassB();

            Console.WriteLine(classB.g(classA));
        }
    }
}
