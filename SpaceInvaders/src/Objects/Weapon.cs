using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Weapon {
    public double cooldown;
    private double _currentCooldownProgress = 0f;
    // private DateTime? _lastTimeWhereTheUserShooted;
    public int ammo;
    IFireStrategy fireStrategy;
    string bulletStrategyType;

    public Weapon (double cooldown, int ammo, string fireStrategyType = "single", string bulletStrategyType = "straight") {
        this.cooldown = cooldown;
        this.ammo = ammo;
        IFireStrategy? fireStrategy = FireFactory.GetStrategy (fireStrategyType);
        if (fireStrategy != null)
            this.fireStrategy = fireStrategy;
        else 
            this.fireStrategy = new SingleFireStrategy ();
        this.bulletStrategyType = bulletStrategyType;
        // this._lastTimeWhereTheUserShooted = null;
        this._currentCooldownProgress = 0f;
    }

    public Weapon (double cooldown, int ammo) {
        this.cooldown = cooldown;
        this.ammo = ammo;
        // this._lastTimeWhereTheUserShooted = DateTime.Now;
        this._currentCooldownProgress = 0f;

        this.bulletStrategyType = "straight";
        String fireStrategyType = "single";

        IFireStrategy? fireStrategy = FireFactory.GetStrategy (fireStrategyType);
        if (fireStrategy != null)
            this.fireStrategy = fireStrategy;
        else 
            this.fireStrategy = new SingleFireStrategy ();
    }

    public Collection<Bullet> Fire (int x, int y, int damage, Character source) {
        // return BulletFactory.Instance.CreateBullet (x, y, bulletStrategy, )
        if (canItFire ()) {
            // _lastTimeWhereTheUserShooted = DateTime.Now;
            _currentCooldownProgress = 0f;
            return fireStrategy.fire (x, y, damage, bulletStrategyType, source);
        }
        return new Collection<Bullet> ();
    }

    private bool canItFire () {
        return _currentCooldownProgress >= cooldown;
        // if (_lastTimeWhereTheUserShooted == null)
        //     return true;
        // DateTime currentTime = DateTime.Now;
        // TimeSpan timeDifference = currentTime - _lastTimeWhereTheUserShooted.Value;
        // if (timeDifference.TotalMilliseconds >= 1000*cooldown) {
        //     return true;
        // }
        // return false;
    }

    public bool changeFireStrategy (string strategyType) {
        IBulletStrategy? currentStrategy = BulletStrategyFactory.GetStrategy (strategyType); 
        if (currentStrategy != null) {
        //     this.bulletStrategy = currentStrategy;
            bulletStrategyType = strategyType;
            return true;
        }
        return false;
    }

    public bool changeBulletStrategy (string strategyType) {
        IFireStrategy? currentStrategy = FireFactory.GetStrategy (strategyType); 
        if (currentStrategy != null) {
            this.fireStrategy = currentStrategy;
            return true;
        }
        return false;
    }

    public void UpdateGameTIme (GameTime gameTime) {
        if (_currentCooldownProgress < cooldown) {
            _currentCooldownProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}