using Microsoft.Xna.Framework.Graphics;

class Player : Character {
    public Player(int x, int y, int width, int height, Texture2D Texture, int health,
    int damage, Weapon weapon, float movementSpeed)
    : base(x, y, width, height, Texture, health, damage)
    {
        // shootingDirection = "up";
        this.damage = damage;
        this.weapon = weapon;
        this.movementSpeed = movementSpeed;
    }

    protected override bool isValidDirection(string direction)
    {
        return direction == "left" || direction == "right";
    }

    public void levelUp () {
        this.health = this.maximumHealth;
        this.damage *= 2;
    }

    public void changeFireStrategy (string strategyType) {
        weapon.changeFireStrategy (strategyType);
    }
}