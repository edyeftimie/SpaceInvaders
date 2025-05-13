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
    public Bullet CreateBullet (int x, int y, int damage, string bulletStrategyType, Character source) {
        IBulletStrategy? bulletStrategy = BulletStrategyFactory.GetStrategy (bulletStrategyType);
        x -= _width/2;
        y -= _height/2;
        if (bulletStrategy != null) {
            return new Bullet (x, y, _width, _height, _Texture, _health, damage, bulletStrategy, source);
        }
        return new Bullet (x, y, _width, _height, _Texture, _health, damage, new StraightBulletStrategy (), source);
    }
}