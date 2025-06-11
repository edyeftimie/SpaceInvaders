using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class Collection<T> : IEnumerable<T>
{
    private List<T> _items = new();

    public void Add(T item) => _items.Add(item);
    public bool Remove(T item)
    {
        if (item == null)
        {
            return false;
        }
        return _items.Remove(item);
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _items.Count;

    public void Clear()
    {
        _items = new();
    }

    public bool Contains (T obj) {
        if (_items.Count > 0) {
            foreach (var item in _items) {
                if (item != null && item.GetType () == typeof (T)) {
                    return true;
                }
            }
        }
        return false;
    }
}