using System.Collections.Generic;

public class SingleFireStrategy : IFireStrategy {
    public SingleFireStrategy () {}

    public List<Bullet> fire (int x, int y, int damage, IBulletStrategy bulletStrategy, Character source) {
        Bullet bullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategy, source);
        return new List<Bullet> { bullet };
    }
}