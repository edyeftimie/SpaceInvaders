using System.Collections;
using System.Collections.Generic;

public class BulletCollection : IEnumerable<Bullet>
{
    private readonly List<Bullet> _bullets = new();

    public void Add(Bullet bullet) => _bullets.Add(bullet);

    public IEnumerator<Bullet> GetEnumerator() => _bullets.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _bullets.Count;
}
