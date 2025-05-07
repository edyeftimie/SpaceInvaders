public class StraightBulletStrategy : IBulletStrategy {
    public void move (Bullet bullet) {
        bullet.move (0, + bullet.speed);
    }
}