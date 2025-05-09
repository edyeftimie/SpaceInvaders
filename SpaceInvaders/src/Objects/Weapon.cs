using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;

public class Weapon {
    public double cooldown;
    private DateTime? _lastTimeWhereTheUserShooted;
    public int ammo;
    IFireStrategy fireStrategy;
    IBulletStrategy bulletStrategy;

    public Weapon (double cooldown, int ammo, IFireStrategy fireStrategy, IBulletStrategy bulletStrategy) {
        this.cooldown = cooldown;
        this.ammo = ammo;
        this.fireStrategy = fireStrategy;
        this.bulletStrategy = bulletStrategy;
        this._lastTimeWhereTheUserShooted = null;
    }

    public List<Bullet> Fire (int x, int y, int damage, Character source) {
        // return BulletFactory.Instance.CreateBullet (x, y, bulletStrategy, )
        if (canItShoot ()) {
            _lastTimeWhereTheUserShooted = DateTime.Now;
            return fireStrategy.fire (x, y, damage, bulletStrategy, source);
        }
        return new List<Bullet> { };
    }

    private bool canItShoot () {
        if (_lastTimeWhereTheUserShooted == null)
            return true;
        DateTime currentTime = DateTime.Now;
        TimeSpan timeDifference = currentTime - _lastTimeWhereTheUserShooted.Value;
        if (timeDifference.TotalMilliseconds >= 1000*cooldown) {
            return true;
        }
        return false;
    }

    public bool changeFireStrategy (string strategyType) {
        IBulletStrategy? currentStrategy = BulletStrategyFactory.GetStrategy (strategyType); 
        if (currentStrategy != null) {
            this.bulletStrategy = currentStrategy;
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
}