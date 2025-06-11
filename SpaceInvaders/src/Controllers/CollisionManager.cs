using Microsoft.Xna.Framework;
using SpaceInvaders;

public class CollisionManager
{
    public void DetectCollisions (
        Player player,
        Collection<Enemy> enemyList,
        Collection<Bullet> bulletList,
        Collection<DamageableObject> destroyableObjects
    ) {
        if (bulletList is not { Count: > 0 }) {
            return;
        }

        Collection<Bullet> collidedBullets = new Collection<Bullet>();

        DetectCharacterCollisions<Player, Enemy> (player, bulletList, collidedBullets, destroyableObjects, "player");

        if (enemyList is { Count: > 0 }) {
            foreach (var enemy in enemyList) {
                DetectCharacterCollisions<Enemy, Player> (enemy, bulletList, collidedBullets, destroyableObjects, "enemy");
            }
        }

        foreach (var bullet in collidedBullets)
        {
            destroyableObjects.Add(bullet);
        }
    }

    private void DetectCharacterCollisions<TTarget, TSource>(
        TTarget target,
        Collection<Bullet> bulletList,
        Collection<Bullet> collidedBullets,
        Collection<DamageableObject> destroyableObjects,
        string targetType
        ) where TTarget : Character where TSource : Character {
        foreach (var bullet in bulletList) {
            // Only check bullets from the right source type that haven't already collided
            if (bullet.source is TSource && !collidedBullets.Contains(bullet)) {
                if (CheckCollision(bullet, target)) {
                    // Target was hit by a bullet
                    ProcessHit(target, bullet, destroyableObjects, collidedBullets, targetType);
                    
                    // If the target is an enemy, a bullet can only hit one enemy, so break
                    if (target is Enemy) {
                        break;
                    }
                }
            }
        }
    }

    private void ProcessHit(
        DamageableObject target,
        Bullet bullet,
        Collection<DamageableObject> destroyableObjects,
        Collection<Bullet> collidedBullets,
        string targetType
    ) {
        target.takeDamage(bullet.damage);

        collidedBullets.Add(bullet);

        Logger.Log($"{targetType} hit by bullet. {targetType} health: {target.health}");

        if (target.isDestroyed) {
            Logger.Log($"{targetType} destroyed!");

            if (targetType == "enemy") {
                destroyableObjects.Add(target);
            }
            else if (targetType == "player") {
                // todo
                // Handle player destruction (game over)
                // game.ChangeState(new GameOverState());
            }
        }
    }

    private bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        Rectangle rect1 = new Rectangle (
            (int)obj1.x,
            (int)obj1.y,
            (int)obj1.width,
            (int)obj1.height
        );
        
        Rectangle rect2 = new Rectangle (
            (int)obj2.x,
            (int)obj2.y,
            (int)obj2.width,
            (int)obj2.height
        );
        
        return rect1.Intersects(rect2);
    }
}