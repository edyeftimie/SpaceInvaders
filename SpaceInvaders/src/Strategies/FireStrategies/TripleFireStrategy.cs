using System.Collections.Generic;

public class TripleFireStrategy : IFireStrategy {
    private int _spacing;
    public TripleFireStrategy (int spacing = 10) {
        _spacing = spacing;
    }

    public BulletCollection fire (int x, int y, int damage, string bulletStrategyType, Character source) {
        BulletCollection bulletCollection = new BulletCollection ();

        Bullet firstBullet = BulletFactory.Instance.CreateBullet (x, y-_spacing, damage, bulletStrategyType, source);
        bulletCollection.Add (firstBullet);

        Bullet secondBullet = BulletFactory.Instance.CreateBullet (x, y, damage, bulletStrategyType, source);
        bulletCollection.Add (secondBullet);
        
        Bullet thirdBullet = BulletFactory.Instance.CreateBullet (x, y+_spacing, damage, bulletStrategyType, source);
        bulletCollection.Add (thirdBullet);
        
        return bulletCollection;
        // return new List<Bullet> { firstBullet, secondBullet, thirdBullet };
    }
}