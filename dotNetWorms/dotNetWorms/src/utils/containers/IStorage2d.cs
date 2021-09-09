using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Containers
{
    interface IStorage2d<T>
    {
        int Count { get; }
        bool TrySet(int x, int y, T val);
        bool Remove(int x, int y);
        bool TryGet(int x, int y, out T result);
        void ForEach(Action<int, int, T> action);//action(x, y, T) => void
    }
}
