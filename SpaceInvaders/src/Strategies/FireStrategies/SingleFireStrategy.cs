using System.Collections.Generic;

public class SingleFireStrategy : IFireStrategy {
    public SingleFireStrategy () {}

    public Collection<Bullet> fire (int x, int y, int damage, string bulletStrategyType, Character source) {
        Bullet bullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategyType, source);
        Collection<Bullet> bulletCollection = new Collection<Bullet> ();
        bulletCollection.Add (bullet);
        return bulletCollection;
    }
}