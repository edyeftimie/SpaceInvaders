using System.Collections.Generic;

public class SingleFireStrategy : IFireStrategy {
    public SingleFireStrategy () {}

    public BulletCollection fire (int x, int y, int damage, string bulletStrategyType, Character source) {
        Bullet bullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategyType, source);
        BulletCollection bulletCollection = new BulletCollection ();
        bulletCollection.Add (bullet);
        return bulletCollection;
    }
}