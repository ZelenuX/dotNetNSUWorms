using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Containers
{
    class Storage2dInfinite<T> : AbstractStorage2d<T>
    {
        private IDictionary<long, T> container = new Dictionary<long, T>();

        private long xyToKey(int x, int y)
        {
            long res = ((long)x << sizeof(int) * 8) | (y & 0xffffffff);
            return res;
        }

        public override int Count { get { return container.Count; } }

        private int keyToX(long key)
        {
            return (int)(key >> sizeof(int) * 8);
        }

        private int keyToY(long key)
        {
            return (int)key;
        }

        public override void ForEach(Action<int, int, T> action)
        {
            foreach (var xyKey in new List<long>(container.Keys))
            {
                int x = keyToX(xyKey);
                int y = keyToY(xyKey);
                container.TryGetValue(xyKey, out var val);
                action(x, y, val);
            }
        }

        public override bool TryGet(int x, int y, out T result)
        {
            return container.TryGetValue(xyToKey(x, y), out result);
        }

        public override bool Remove(int x, int y)
        {
            return container.Remove(xyToKey(x, y));
        }

        public override bool TrySet(int x, int y, T val)
        {
            return container.TryAdd(xyToKey(x, y), val);
        }
    }
}
