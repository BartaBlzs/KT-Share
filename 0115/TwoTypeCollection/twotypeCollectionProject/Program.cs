using System.Reflection;

namespace twotypeCollectionProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TwoTypeCollection<string, float> t = new(shouldThrow: true);
            TwoTypeCollection<string, float> t11 = new();
            t.FillWithRandom<float>(10, 0, 50);
            t11.Add(t);
            Console.WriteLine(t11);
        }
    }
}
