using System.Collections.Generic;

public class DoubleFireStrategy : IFireStrategy {
    private int _spacing;
    public DoubleFireStrategy (int spacing = 10) {
        _spacing = spacing;
    }

    public BulletCollection fire (int x, int y, int damage, string bulletStrategyType, Character source) {
        BulletCollection bulletCollection = new BulletCollection ();
        Bullet firstBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategyType, source);
        firstBullet.move (0, -_spacing/2);
        bulletCollection.Add (firstBullet);

        Bullet secondBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategyType, source);
        secondBullet.move (0, +_spacing/2);
        bulletCollection.Add (secondBullet);

        // return new List<Bullet> { firstBullet, secondBullet };
        return bulletCollection;
    }
}