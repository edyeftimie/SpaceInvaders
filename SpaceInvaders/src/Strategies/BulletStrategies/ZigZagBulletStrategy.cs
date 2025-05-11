using System;

class ZigZagBulletStrategy : IBulletStrategy {
    private int _frameCount = 0;
    private int _direction;

    public ZigZagBulletStrategy () {
        _direction = GetRandomDirection ();
    }
    
    public static int GetRandomDirection () {
        return Random.Shared.Next (2) == 0 ? -1 : 1;
    }

    public void move (Bullet bullet) {
        _frameCount ++;
        if (_frameCount % 20 == 0) {
            _direction *= -1;
        }
        int speed = bullet.speed;
        bullet.move (_direction*speed, speed*Direction.directionCoefficient (bullet.source));
    }
    //todo , change from _frameCount to distance ...
}