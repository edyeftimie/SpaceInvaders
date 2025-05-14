using System;
using Microsoft.Xna.Framework.Graphics;

public class EnemyFactory {
    public static EnemyFactory Instance {get;} = new EnemyFactory ();
    private int _width;
    private int _height;
    private Texture2D _Texture;
    private int _health;
    // private int _rarityRateOfNewStrategies;
    // private Collection<string> _strategiesType;
    private int _startDamageInterval;
    private int _endDamageInterval;
    private double _startCooldownInterval;
    private double _endCooldownInterval;
    private int _startMovementSpeed;
    private int _endMovementSpeed;
    private int _ammo;


    public void Initialize (
        int width,
        int height,
        Texture2D Texture, // Note the uppercase 'T' to match the parameter name
        int health,
        int startDamageInterval,
        int endDamageInterval,
        double startCooldownInterval,
        double endCooldownInterval,
        int startMovementSpeed,
        int endMovementSpeed,
        int ammo) {
        _width = width;
        _height = height;
        _Texture = Texture; // Corrected assignment
        _health = health;
        _startDamageInterval = startDamageInterval;
        _endDamageInterval = endDamageInterval;
        _startCooldownInterval = startCooldownInterval;
        _endCooldownInterval = endCooldownInterval;
        _startMovementSpeed = startMovementSpeed;
        _endMovementSpeed = endMovementSpeed;
        _ammo = ammo;
    }

    public Enemy CreateEnemy (int x, int y, int damage, Weapon weapon, int movementSpeed) {
        return new Enemy (x, y, _width, _height, _Texture, _health, damage, weapon, movementSpeed);
    }

    public int GetValueBetween (int start, int end) {
        Random random = new Random();
        return random.Next(start, end + 1);
    }

    public int GetDamage () {
        return GetValueBetween (_startDamageInterval, _endDamageInterval);
    }

    public double GetCooldown () {
        return GetValueBetween ((int)(_startCooldownInterval*10), (int)(_endCooldownInterval*10))/10;
    }

    public int GetMovement () {
        return GetValueBetween (_startMovementSpeed, _endMovementSpeed);
    }

    public Collection<Enemy> CreateRowOfEnemies (int y, int startX, int endX, int spacing) {
        Collection<Enemy> enemies = new Collection<Enemy> ();
        for (; startX + _width < endX; startX += _width + spacing) {
            Weapon weapon = new Weapon (GetCooldown (), _ammo);
            enemies.Add (CreateEnemy (startX, y, GetDamage (), weapon, GetMovement ()));
        } 
        return enemies;
    }
    
}