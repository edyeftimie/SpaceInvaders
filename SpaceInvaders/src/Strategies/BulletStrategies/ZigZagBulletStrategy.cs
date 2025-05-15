using System;

class ZigZagBulletStrategy : IBulletStrategy {
    private int _frameCount;
    private int _switchAtFrameNumber;
    private int _direction;

    public ZigZagBulletStrategy (int switchAtFrameNumber) {
        _frameCount = 0;
        _switchAtFrameNumber = switchAtFrameNumber;
        _direction = GetRandomDirection ();
    }
    
    public static int GetRandomDirection () {
        return Random.Shared.Next (2) == 0 ? -1 : 1;
    }

    public bool move (Bullet bullet) {
        _frameCount ++;
        if (_frameCount % _switchAtFrameNumber == 0) {
            _direction *= -1;
        }
        int speed = (int)(bullet.speed*0.7);
        return bullet.move (_direction*speed, speed*Direction.directionCoefficient (bullet.source));
    }
    //todo , change from _frameCount to distance ...
}