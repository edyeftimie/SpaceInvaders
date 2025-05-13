using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public abstract class Character : DamageableObject, IFire {
    public Weapon weapon {get; set;}
    protected float movementSpeed {get; set;}

    // public required string shootingDirection {get; set;}

    protected Character(int x, int y, int width, int height, Texture2D Texture, int health, int damage, Weapon weapon, float movementSpeed)
    : base(x, y, width, height, Texture, health, damage)
    {
        this.weapon = weapon;
        this.movementSpeed = movementSpeed;
    }

    public BulletCollection Fire() {
        // int currentX = this.x;
        // int currentY = this.y;
        int currentX = this.middleX;
        int currentY = this.middleY;
        return weapon.Fire (currentX, currentY, this.damage, this);
    }
    
    public bool move (string direction) {
        if (isValidDirection (direction)) {
            switch (direction){
                // case "up": y -= (int)movementSpeed; break;
                case "down": return move(0,+(int)movementSpeed);
                case "left": return move(-(int)movementSpeed,0);
                case "right": return move(+(int)movementSpeed,0);
                default: return false;
            }
        } else {
            return false;
        }
    }

    // protected void fire (string direction) {
    //     if (isValidDirection (direction)) {
    //         weapon.fire (this.x, this.y, direction, this);
    //     }
    // }

    protected abstract bool isValidDirection (string direction);
}