using Microsoft.Xna.Framework.Graphics;

public class Enemy : Character {
    public Enemy(int x, int y, int width, int height, Texture2D Texture, int health, int damage, Weapon weapon, double movementSpeed)
    : base(x, y, width, height, Texture, health, damage, weapon, movementSpeed) {
        // shootingDirection = "down";
    }

    protected override bool isValidDirection(string direction) {
        return direction == "down";
    }

    public void changeBulletStrategy (string bulletStrategy) {
        weapon.changeBulletStrategy (bulletStrategy);
    }

    public bool move () {
        return move("down");
    }
}