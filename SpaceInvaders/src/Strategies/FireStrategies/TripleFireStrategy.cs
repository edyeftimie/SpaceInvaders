using System.Collections.Generic;

public class TripleFireStrategy : IFireStrategy {
    private int _spacing;
    public TripleFireStrategy (int spacing = 10) {
        _spacing = spacing;
    }

    public List<Bullet> fire (int x, int y, int damage, IBulletStrategy bulletStrategy, Character source) {
        Bullet firstBullet = BulletFactory.Instance.CreateBullet (x, y-_spacing, damage, bulletStrategy, source);
        Bullet secondBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategy, source);
        Bullet thirdBullet = BulletFactory.Instance.CreateBullet (x, y+_spacing, damage, bulletStrategy, source);
        return new List<Bullet> { firstBullet, secondBullet, thirdBullet };
    }
}