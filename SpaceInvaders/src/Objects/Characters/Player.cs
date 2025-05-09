using Microsoft.Xna.Framework.Graphics;

public class Player : Character {
    public Player(int x, int y, int width, int height, Texture2D Texture, int health, int damage, Weapon weapon, float movementSpeed)
    : base(x, y, width, height, Texture, health, damage, weapon, movementSpeed)
    {
        // this.weapon = weapon;
        // shootingDirection = "up";
    }

    protected override bool isValidDirection(string direction)
    {
        return direction == "left" || direction == "right";
    }

    public void levelUp () {
        this.health = this.maximumHealth;
        this.damage *= 2;
    }

    public bool changeFireStrategy (string strategyType) {
        return weapon.changeFireStrategy (strategyType);
    }
}