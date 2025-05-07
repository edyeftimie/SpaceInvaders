using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public abstract class Character : DamageableObject, IFire {
    public required Weapon weapon {get;set;}
    protected float movementSpeed {get; set;}

    // public required string shootingDirection {get; set;}

    protected Character(int x, int y, int width, int height, Texture2D Texture, int health, int damage)
    : base(x, y, width, height, Texture, health, damage)
    {
    }

    public List<Bullet> Fire() {
        return weapon.Fire (this.x, this.y, this.damage, this);
    }
    
    public void move (string direction) {
        if (isValidDirection (direction)) {
            switch (direction){
                // case "up": y -= (int)movementSpeed; break;
                case "down": move(0,+(int)movementSpeed); break;
                case "left": move(-(int)movementSpeed,0); break;
                case "right": move(+(int)movementSpeed,0); break;
            }
        }
    }

    // protected void fire (string direction) {
    //     if (isValidDirection (direction)) {
    //         weapon.fire (this.x, this.y, direction, this);
    //     }
    // }

    protected abstract bool isValidDirection (string direction);
}