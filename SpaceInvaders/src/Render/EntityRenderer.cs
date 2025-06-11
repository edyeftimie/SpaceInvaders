using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders;

public class EntityRenderer
{
    public void DrawGame(
        SpriteBatch spriteBatch,
        Player player,
        Collection<Enemy> enemyList,
        Collection<Bullet> bulletList)
    {
        DrawBullets(spriteBatch, bulletList);
        DrawEnemies(spriteBatch, enemyList);
        DrawPlayer(spriteBatch, player);
    }

    private void DrawBullets(SpriteBatch spriteBatch, Collection<Bullet> bulletList)
    {
        if (bulletList is { Count: > 0 })
        {
            foreach (var bullet in bulletList)
            {
                spriteBatch.Draw(
                    bullet.Texture,
                    new Rectangle(
                        (int)bullet.x,
                        (int)bullet.y,
                        (int)bullet.width,
                        (int)bullet.height
                    ),
                    Color.White
                );
            }
        }
    }

    private void DrawEnemies(SpriteBatch spriteBatch, Collection<Enemy> enemyList)
    {
        if (enemyList is { Count: > 0 })
        {
            foreach (var enemy in enemyList)
            {
                spriteBatch.Draw(
                    enemy.Texture,
                    new Rectangle(
                        (int)enemy.x,
                        (int)enemy.y,
                        (int)enemy.width,
                        (int)enemy.height
                    ),
                    Color.White
                );
            }
        }
    }

    private void DrawPlayer(SpriteBatch spriteBatch, Player player)
    {
        spriteBatch.Draw(
            player.Texture,
            new Rectangle(
                (int)player.x,
                (int)player.y,
                (int)player.width,
                (int)player.height
            ),
            Color.White
        );
    }
}