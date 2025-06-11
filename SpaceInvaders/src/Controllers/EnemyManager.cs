using Microsoft.Xna.Framework;
using SpaceInvaders;

public class EnemyManager
{
    public void ManageEnemies(
        GameTime gameTime,
        Collection<Enemy> enemyList,
        Collection<DamageableObject> destroyableObjects,
        Collection<Bullet> bulletList,
        Constants constants)
    {
        if (enemyList is { Count: <= 0 })
        {
            SpawnEnemies(enemyList, constants);
        }
        else
        {
            UpdateExistingEnemies(gameTime, enemyList, destroyableObjects, bulletList);
        }
    }

    private void SpawnEnemies(Collection<Enemy> enemyList, Constants constants)
    {
        Collection<Enemy> spawnEnemies = EnemyFactory.Instance.CreateRowOfEnemies(
            constants.enemyHeight / 2,
            0 + 100,
            constants.screenWidth,
            constants.spaceBetweenEnemies
        );

        foreach (var enemy in spawnEnemies)
        {
            enemyList.Add(enemy);
        }
    }

    private void UpdateExistingEnemies(
        GameTime gameTime,
        Collection<Enemy> enemyList,
        Collection<DamageableObject> destroyableObjects,
        Collection<Bullet> bulletList)
    {
        foreach (var enemy in enemyList)
        {
            enemy.UpdateGameTime(gameTime);

            Collection<Bullet> currentBullets = enemy.Fire();
            if (currentBullets is { Count: > 0 })
            {
                foreach (var bullet in currentBullets)
                {
                    bulletList.Add(bullet);
                }
            }
            else
            {
                bool isValidMove = enemy.move();
                if (!isValidMove)
                {
                    destroyableObjects.Add(enemy);
                }
            }
        }
    }
}