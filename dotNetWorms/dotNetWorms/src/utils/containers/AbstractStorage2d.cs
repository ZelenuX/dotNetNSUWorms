using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Containers
{
    abstract class AbstractStorage2d<T> : StorageDataProvider<T>
    {
        public abstract int Count { get; }
        public abstract bool TrySet(int x, int y, T val);
        public abstract bool Remove(int x, int y);
        public abstract bool TryGet(int x, int y, out T result);
        public abstract void ForEach(Action<int, int, T> action);//action(x, y, T) => void

        public bool TrySet(Coords xy, T val)
        {
            return TrySet(xy.X, xy.Y, val);
        }
        public bool Remove(Coords xy)
        {
            return Remove(xy.X, xy.Y);
        }
        public bool TryGet(Coords xy, out T result)
        {
            return TryGet(xy.X, xy.Y, out result);
        }
        public void ForEach(Action<Coords, T> action)
        {
            ForEach((x, y, val) => action(new Coords(x, y), val));
        }
    }
}
