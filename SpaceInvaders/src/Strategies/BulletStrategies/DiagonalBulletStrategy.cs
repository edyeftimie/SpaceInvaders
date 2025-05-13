using System;
using System.Security.Cryptography.X509Certificates;

public class DiagonalBulletStrategy : IBulletStrategy {
    private int _direction;

    public static int GetRandomDirection () {
        return Random.Shared.Next (2) == 0 ? -1 : 1;
    }
    
    public DiagonalBulletStrategy (int direction) {
        if (direction != 1 || direction != -1) {
            _direction = GetRandomDirection ();
        } else {
            _direction = direction;
        }
    }

    public DiagonalBulletStrategy () {
        _direction = GetRandomDirection ();
    }

    public void move (Bullet bullet) {
        int speed = Convert.ToInt32 (bullet.speed * 1.4);
        //todo to shoot on oposite diagonal
        bullet.move (_direction*speed/3, speed*Direction.directionCoefficient (bullet.source));
    }


}