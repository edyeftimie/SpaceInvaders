using Microsoft.Xna.Framework.Graphics;

class Enemy : Character {
    public Enemy(int x, int y, int width, int height, Texture2D Texture, int health,
    int damage, Weapon weapon, float movementSpeed)
    : base(x, y, width, height, Texture, health, damage) {
        // shootingDirection = "down";
        this.damage = damage;
        this.weapon = weapon;
        this.movementSpeed = movementSpeed;
    }

    protected override bool isValidDirection(string direction) {
        return direction == "down";
    }

    public void changeBulletStrategy (string bulletStrategy) {
        weapon.changeBulletStrategy (bulletStrategy);
    }

    // public void move () {
    //     move ("down");
    // }
}