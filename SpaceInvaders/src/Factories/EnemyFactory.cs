using System;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Xna.Framework.Graphics;

public class EnemyFactory
{
    public static EnemyFactory Instance { get; } = new EnemyFactory();
    private int _width;
    private int _height;
    private Texture2D _Texture;
    private int _health;
    private int _rarityRateOfNewStrategies;
    // private Collection<string> _strategiesType;
    private int _startDamageInterval;
    private int _endDamageInterval;
    private double _startCooldownInterval;
    private double _endCooldownInterval;
    private double _startMovementSpeed;
    private double _endMovementSpeed;
    private int _ammo;


    public void Initialize(
        int width,
        int height,
        Texture2D Texture, // Note the uppercase 'T' to match the parameter name
        int health,
        int startDamageInterval,
        int endDamageInterval,
        double startCooldownInterval,
        double endCooldownInterval,
        double startMovementSpeed,
        double endMovementSpeed,
        int ammo,
        int rarityRateOfNewStrategies)
    {
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
        _rarityRateOfNewStrategies = rarityRateOfNewStrategies;
    }

    public Enemy CreateEnemy(int x, int y, int damage, Weapon weapon, double movementSpeed)
    {
        return new Enemy(x, y, _width, _height, _Texture, _health, damage, weapon, movementSpeed);
    }

    public int GetValueBetween(int start, int end)
    {
        Random random = new Random();
        return random.Next(start, end + 1);
    }

    public int GetDamage()
    {
        return GetValueBetween(_startDamageInterval, _endDamageInterval);
    }

    public double GetCooldown()
    {
        return GetValueBetween((int)(_startCooldownInterval * 10), (int)(_endCooldownInterval * 10)) / 10.0;
    }

    public double GetMovement()
    {
        return GetValueBetween((int)(_startMovementSpeed * 10), (int)(_endMovementSpeed * 10)) / 10.0;
    }

    public Collection<Enemy> CreateRowOfEnemies(int y, int startX, int endX, int spacing)
    {
        Collection<Enemy> enemies = new Collection<Enemy>();
        for (; startX + _width < endX; startX += _width + spacing)
        {
            Weapon weapon = new Weapon(GetCooldown(), _ammo, bulletStrategyType: GetStrategyType ());
            enemies.Add(CreateEnemy(startX, y, GetDamage(), weapon, GetMovement()));
        }
        return enemies;
    }

    private string GetStrategyType()
    {
        int chance = GetValueBetween(0, 100);
        if (chance <= _rarityRateOfNewStrategies)
        {
            return "straight";
        }
        else if (chance > _rarityRateOfNewStrategies && chance <= (_rarityRateOfNewStrategies + 100) / 2)
        {
            return "zigzag";
        }
        else
        {
            return "diagonal";
        }
    }
    
}