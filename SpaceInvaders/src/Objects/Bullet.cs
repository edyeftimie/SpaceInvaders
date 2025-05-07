using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

public class Bullet: DamageableObject {
    IBulletStrategy bulletStrategy;
    Character source;
    public int speed; 

    public Bullet(int x, int y, int width, int height, Texture2D Texture, int health, int damage,
    IBulletStrategy bulletStrategy, Character source)
     : base(x, y, width, height, Texture, health, damage)
    {
        this.bulletStrategy = bulletStrategy;
        this.source = source;
        this.speed = 10;
    }

    public void move () {
        bulletStrategy.move (this);
    }
}