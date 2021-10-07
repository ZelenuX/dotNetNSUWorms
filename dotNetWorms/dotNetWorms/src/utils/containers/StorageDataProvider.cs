using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Containers
{
    public interface StorageDataProvider<T>
    {
        int Count { get; }
        bool TryGet(int x, int y, out T result);
        void ForEach(Action<int, int, T> action);//action(x, y, T) => void
        bool TryGet(Coords xy, out T result);
        void ForEach(Action<Coords, T> action);
    }
}
