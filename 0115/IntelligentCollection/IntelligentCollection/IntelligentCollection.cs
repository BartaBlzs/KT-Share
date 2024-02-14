using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentCollectionProject
{
    public class IntelligentCollection<T> : IEnumerable, ICollection
    {
        private List<T> list;

        public IntelligentCollection()
        {
            list = new();
            try
            {
                decimal _ = (decimal)Convert.ChangeType(Activator.CreateInstance(typeof(T)), typeof(decimal));
            }
            catch
            {
                throw new FormatException("IntelligentCollection only supports decimal types.");
            }
        }

        public IntelligentCollection(IntelligentCollection<T> collection)
        {
            try
            {
                decimal _ = (decimal)Convert.ChangeType(Activator.CreateInstance(typeof(T)), typeof(decimal));
            }
            catch
            {
                throw new FormatException("IntelligentCollection only supports decimal types.");
            }
            list = collection.list;
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Add(IntelligentCollection<T> listInput)
        {
            foreach(T item in listInput)
            {
                if (!list.Contains(item)) list.Add(item);
            }
        }

        public void FillWithRandom(int count, int minValue, int maxValue)
        {
            Random rnd = new();
            for (int i = 0; i < count; i++)
            {
               list.Add((T)Convert.ChangeType(rnd.NextDouble() * (maxValue - minValue) + minValue, typeof(T)));
            }
        }

        public T Sum()
        {
            dynamic sum = 0;
            foreach (T t in list)
            {
                sum += t;
            }
            return sum;
        }

        public double Average()
        {
            return Sum()/(dynamic)(double)Count;
        }

        public T Min() => list.Min();

        public T Max() => list.Max();

        public override string ToString()
        {
            return string.Join(", ", list);
        }

        #region Interface Implementations
        public int Count => list.Count;
        public bool IsSynchronized => true;
        public object? SyncRoot => null;

        public void CopyTo(Array array, int index)
        {
            list.ToArray().CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
        #endregion
    }
}
