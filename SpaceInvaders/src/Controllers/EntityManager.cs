using SpaceInvaders;

public static class EntityManager
{
    public static void CleanupDestroyedObjects(
        Collection<DamageableObject> destroyableObjects,
        Collection<Enemy> enemyList,
        Collection<Bullet> bulletList)
    {
        if (destroyableObjects is { Count: > 0 })
        {
            foreach (var gameObject in destroyableObjects)
            {
                gameObject.destroyEntity();
                RemoveFromAppropriateList(gameObject, enemyList, bulletList);
            }
            destroyableObjects.Clear();
        }
    }

    private static void RemoveFromAppropriateList(
        DamageableObject gameObject,
        Collection<Enemy> enemyList,
        Collection<Bullet> bulletList)
    {
        if (gameObject is Enemy enemy)
        {
            bool removed = enemyList.Remove(enemy);
            if (!removed)
            {
                Logger.Warning("Enemy not found in _listOfEnemies: " + enemy);
            }
        }
        else if (gameObject is Bullet bullet)
        {
            bool removed = bulletList.Remove(bullet);
            if (!removed)
            {
                Logger.Warning("Bullet not found in _listOfBullets: " + bullet);
            }
        }
    }
}