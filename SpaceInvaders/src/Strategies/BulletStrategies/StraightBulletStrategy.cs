public class StraightBulletStrategy : IBulletStrategy {
    public bool move (Bullet bullet) {
        return bullet.move (0, + bullet.speed * Direction.directionCoefficient (bullet.source));
    }
}