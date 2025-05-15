using System;
using Microsoft.Xna.Framework.Graphics;

public class DamageableObject : GameObject {
    protected int health;
    protected int maximumHealth;
    public int damage {get;set;}
    private bool _isDestroyed {get; set;}
    public DamageableObject (int x, int y, int width, int height, Texture2D Texture, int health, int damage) : base(x, y, width, height, Texture) {
        this.maximumHealth = health;
        this.health = this.maximumHealth;
        this.damage = damage;
        this._isDestroyed = false;
    }
    
    public void takeDamage (int amount) {
        int newHealthLevel = health - amount;
        this.health = (newHealthLevel > 0) ? newHealthLevel : 0;
        if (this.health == 0) {
            this._isDestroyed = true;
        }
    }

    public void destroyEntity () {
        _isDestroyed = true;
    }
}