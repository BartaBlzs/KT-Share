using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

//
//
//
//
//
//
//
//      Miután már készvoltam, aztán jöttem rá hogy félreértettem a feladatot :\ ==> Valószínűleg azért, mert nem néztem meg a megoldás fájlt a legelején.
//      
//      Ez a project 2 külön típusú változó fajtát tud tárolni, ugyan azokkal a metódusokkal amik a feladatban eredetileg voltak.
//
//      Lehet, hogy hibás pár része, ahogy rájöttem, hogy nem ez a feladat, abba is hagytam.
//
//
//
//
//
//
//
namespace twotypeCollectionProject
{
    /// <summary>
    /// A collection which can hold two types.
    /// </summary>
    public class TwoTypeCollection<T1, T2> : IEnumerable
    {
        private List<T1> T1List;
        private List<T2> T2List;
        private List<object?> CombinedObjectList;
        private bool ShouldThrow;

        /// <summary>
        /// Returns the number of elements in the collection.
        /// </summary>
        public int Count => T1List.Count;

        public IEnumerator GetEnumerator()
        {
            return CombinedObjectList.GetEnumerator();
        }

        /// <summary>
        /// Initializes a new instance of a collection which can hold two different types.
        /// </summary>
        public TwoTypeCollection(bool shouldThrow = true)
        {
            T1List = new();
            T2List = new();
            CombinedObjectList = new();
            ShouldThrow = shouldThrow;
        }

        /// <summary>
        /// Adds item of Type <typeparamref name="T1"/> to the collection.
        /// </summary>
        public void Add(T1 item)
        {
            T1List.Add(item);
            CombinedObjectList.Add(item);
        }

        /// <summary>
        /// Adds item of Type <typeparamref name="T2"/> to the collection.
        /// </summary>
        public void Add(T2 item)
        {
            T2List.Add(item);
            CombinedObjectList.Add(item);
        }

        /// <summary>
        /// Adds all the items from ano
        /// </summary>
        public void Add(TwoTypeCollection<T1, T2> collection)
        {
            foreach (var item in collection)
            {
                if (item is T1) Add((T1)item);
                if(item is T2) Add((T2)item);
            }
        }

        /// <summary>
        /// Returns the element of type <typeparamref name="T"/> at a specified index in a collection.
        /// </summary>
        /// <returns>The element of type <typeparamref name="T"/> at a specified index in a collection.</returns>
        public T? ElementAt<T>(int index)
        {
            if(typeof(T) == typeof(T1))
            {
                return (T)Convert.ChangeType(T1List[index], typeof(T));
            }
            else if(typeof(T) == typeof(T2))
            {
                return (T)Convert.ChangeType(T2List[index], typeof(T));
            }
            if(ShouldThrow) throw new FormatException("The given type was wrong.");
            return default;
        }

        /// <summary>
        /// Fills the collection with randoms of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">The number of random numbers to be generated.</param>
        /// <param name="minValue">The minimum value of the random number.</param>
        /// <param name="maxValue">The maximum value of the random number.</param>

        public void FillWithRandom<T>(int count, int minValue, int maxValue)
        {
            var t = typeof(T);
            Random rnd = new();
            if (t == typeof(T1))
            {
                if (t == typeof(int))
                {
                    for (int i = 0; i < count; i++) Add((T1)Convert.ChangeType(rnd.Next(minValue, maxValue), typeof(T1)));
                }
                else if (t == typeof(double))
                {
                    for (int i = 0; i < count; i++) Add((T1)Convert.ChangeType(rnd.NextDouble() * maxValue + minValue, typeof(T1)));
                }
                else if (t == typeof(float))
                {
                    for (int i = 0; i < count; i++) Add((T1)Convert.ChangeType(rnd.NextSingle() * maxValue + minValue, typeof(T1)));
                }
                else if (ShouldThrow) throw new InvalidOperationException("You can only fill the collection with types: int, double, float!");
            }
            else if (t == typeof(T2))
            {
                if (t == typeof(int))
                {
                    for (int i = 0; i < count; i++) Add((T2)Convert.ChangeType(rnd.Next(minValue, maxValue), typeof(T2)));
                }
                else if (t == typeof(double))
                {
                    for (int i = 0; i < count; i++) Add((T2)Convert.ChangeType(rnd.NextDouble() * maxValue + minValue, typeof(T2)));
                }
                else if (t == typeof(float))
                {
                    for (int i = 0; i < count; i++) Add((T2)Convert.ChangeType(rnd.NextSingle() * maxValue + minValue, typeof(T2)));
                }
                else if (ShouldThrow) throw new InvalidOperationException("You can only fill the collection with types: int, double, float!");
            }
            else if(ShouldThrow) throw new FormatException("The given type was wrong.");
        }

        public double? Sum()
        {
            double sum = 0;
            int count = 0;
            foreach (var item in CombinedObjectList)
            {
                try
                {
                    sum += Convert.ToDouble(item);
                    count++;
                }
                catch {}
            }
            if (count == 0) return null;
            return sum;
        }

        public double? Average()
        {
            double sum = 0;
            int count = 0;
            foreach (var item in CombinedObjectList)
            {
                try
                {
                    sum += Convert.ToDouble(item);
                    count++;
                }
                catch {}
            }
            if(count == 0) return null;
            return sum/count;
        }

        public double? Min()
        {
            double? min = Max();

            foreach (var item in CombinedObjectList)
            {
                try
                {
                    var d = Convert.ToDouble(item);
                    if (d < min) min = d;
                }
                catch { }
            }
            return min;
        }

        public double? Max()
        {
            double? max = null;
            foreach (var item in CombinedObjectList)
            {
                try
                {
                    var d = Convert.ToDouble(item);
                    max ??= d;
                    if (d > max) max = d;
                }
                catch { }
            }
            return max;
        }

        public object this[int index]
        {
            get => CombinedObjectList[index];
        }

        public override string ToString()
        {
            return string.Join(", ", CombinedObjectList);
        }
    }
}