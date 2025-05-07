using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;

public class Weapon {
    public float cooldown;
    public int ammo;
    IFireStrategy fireStrategy;
    IBulletStrategy bulletStrategy;

    public Weapon (float cooldown, int ammo, IFireStrategy fireStrategy, IBulletStrategy bulletStrategy) {
        this.cooldown = cooldown;
        this.ammo = ammo;
        this.fireStrategy = fireStrategy;
        this.bulletStrategy = bulletStrategy;
    }

    public List<Bullet> Fire (int x, int y, int damage, Character source) {
        // return BulletFactory.Instance.CreateBullet (x, y, bulletStrategy, )
        return fireStrategy.fire (x, y, damage, bulletStrategy, source);
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