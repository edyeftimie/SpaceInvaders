using SpaceInvaders;

public class BulletManager
{
    public void UpdateBullets(
        Collection<Bullet> bulletList,
        Collection<DamageableObject> destroyableObjects)
    {
        if (bulletList is { Count: > 0 })
        {
            foreach (var bullet in bulletList)
            {
                bool isValidMove = bullet.move();
                if (!isValidMove)
                {
                    destroyableObjects.Add(bullet);
                }
            }
        }
    }
}