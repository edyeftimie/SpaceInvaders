using System;
using Microsoft.Xna.Framework.Graphics;

public class DamageableObject : GameObject
{
    public int health;
    protected int maximumHealth;
    public int damage { get; set; }
    public bool isDestroyed { get; set; }
    public DamageableObject(int x, int y, int width, int height, Texture2D Texture, int health, int damage) : base(x, y, width, height, Texture)
    {
        this.maximumHealth = health;
        this.health = this.maximumHealth;
        this.damage = damage;
        this.isDestroyed = false;
    }

    public void takeDamage(int amount)
    {
        int newHealthLevel = health - amount;
        this.health = (newHealthLevel > 0) ? newHealthLevel : 0;
        if (this.health == 0)
        {
            this.isDestroyed = true;
        }
    }

    public void destroyEntity()
    {
        isDestroyed = true;
    }
}