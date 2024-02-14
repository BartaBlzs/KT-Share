namespace IntelligentCollectionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IntelligentCollection<float> c1 = new();
            c1.FillWithRandom(5, 5, 30);
            // létrehozás
            IntelligentCollection<float> collection = new();
            // feltöltés random számokkal
            collection.FillWithRandom(4, 10, 100);
            // átvétel másik objektumból
            collection.Add(c1);
            // kiiratás
            Console.WriteLine($"Lista: {collection}");
            // összeg
            Console.WriteLine($"Szumma: {collection.Sum()}");
            // átlag
            Console.WriteLine($"Átlag: {collection.Average()}");
            // minimum
            Console.WriteLine($"Minimum: {collection.Min()}");
            // maximum
            Console.WriteLine($"Maximum: {collection.Max()}");
        }
    }
}
