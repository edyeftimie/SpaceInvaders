class ZigZagBulletStrategy : IBulletStrategy {
    private int _frameCount = 0;
    private int _direction = 1;

    public void move (Bullet bullet) {
        _frameCount ++;
        if (_frameCount % 10 == 0) {
            _direction *= -1;
        }
        int speed = bullet.speed;
        bullet.move (_direction*speed, speed);
    }
}