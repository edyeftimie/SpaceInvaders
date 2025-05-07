using System.Collections.Generic;

public class DoubleFireStrategy : IFireStrategy {
    private int _spacing;
    public DoubleFireStrategy (int spacing = 10) {
        _spacing = spacing;
    }

    public List<Bullet> fire (int x, int y, int damage, IBulletStrategy bulletStrategy, Character source) {
        Bullet firstBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategy, source);
        firstBullet.move (0, -_spacing/2);
        Bullet secondBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategy, source);
        secondBullet.move (0, +_spacing/2);
        return new List<Bullet> { firstBullet, secondBullet };
    }
}