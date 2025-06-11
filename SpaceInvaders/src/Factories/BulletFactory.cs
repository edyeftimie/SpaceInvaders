using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

public class BulletFactory {
    public static BulletFactory Instance {get;} = new BulletFactory ();
    private int _width;
    private int _height;
    private Texture2D _PlayerTexture;
    private Texture2D _EnemyTexture;
    private int _health;

    // private BulletFactory () {
    // }
    
    public void Initialize (int width, int height, Texture2D playerTexture, Texture2D enemyTexture, int health) {
        _width = width;
        _height = height;
        _PlayerTexture = playerTexture;
        _EnemyTexture = enemyTexture;
        _health = health;
    }
    public Bullet CreateBullet (int x, int y, int damage, string bulletStrategyType, Character source) {
        IBulletStrategy? bulletStrategy = BulletStrategyFactory.GetStrategy (bulletStrategyType);
        x -= _width/2;
        y -= _height/2;
        var currentTexture = (source is Enemy) ? _EnemyTexture : _PlayerTexture;
        if (bulletStrategy != null) {
            return new Bullet (x, y, _width, _height, currentTexture, _health, damage, bulletStrategy, source);
        }
        return new Bullet (x, y, _width, _height, currentTexture, _health, damage, new StraightBulletStrategy (), source);
    }
}