using System.Collections;
using System.Collections.Generic;

public class Collection<T> : IEnumerable<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);
    public bool Remove (T item) {
        if (item == null) {
            return false;
        }
        return _items.Remove(item);
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _items.Count;
}