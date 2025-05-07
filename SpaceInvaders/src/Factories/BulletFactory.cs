using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

public class BulletFactory {
    public static BulletFactory Instance {get;} = new BulletFactory ();
    private int _width;
    private int _height;
    private Texture2D _Texture;
    private int _health;

    // private BulletFactory () {
    // }
    
    public void Initialize (int width, int height, Texture2D Texture, int health) {
        _width = width;
        _height = height;
        _Texture = Texture;
        _health = health;
    }
    public Bullet CreateBullet (int x, int y, int damage, IBulletStrategy bulletStrategy, Character source) {
        return new Bullet (x, y, _width, _height, _Texture, _health, damage, bulletStrategy, source);
    }
}