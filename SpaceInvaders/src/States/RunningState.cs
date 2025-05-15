using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;

public class RunningState : IGameState
{
    public void Update(GameTime gameTime, Game1 game) {
        KeyboardState currentState = Keyboard.GetState();

        if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D))
        {
            game._player.move("right");
        }
        else if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A))
        {
            game._player.move("left");
        }

        if (currentState.IsKeyDown(Keys.Space))
        {
            Collection<Bullet> currentBullets = game._player.Fire();
            if (currentBullets is { Count: > 0 })
                foreach (var bullet in currentBullets)
                    game._listOfBullets.Add(bullet);
        }

        if (game._listOfEnemies is { Count: <= 0 })
        {
            Collection<Enemy> spawnEnemies = EnemyFactory.Instance.CreateRowOfEnemies(10, 100, 1700, 50);
            foreach (var enemy in spawnEnemies)
                game._listOfEnemies.Add(enemy);
        }
        else
        {
            foreach (var enemy in game._listOfEnemies)
            {
                Collection<Bullet> currentBullets = enemy.Fire();
                if (currentBullets is { Count: > 0 })
                {
                    foreach (var bullet in currentBullets)
                        game._listOfBullets.Add(bullet);
                }
                else
                {
                    enemy.move();
                }
            }
        }

        if (game._listOfBullets is { Count: > 0 })
        {
            foreach (var bullet in game._listOfBullets)
                bullet.move();
        }
    }

    public void Draw (GameTime gameTime, SpriteBatch spriteBatch, Game1 game) {
        if (game._listOfBullets is { Count: > 0 }) {
            foreach (var bullet in game._listOfBullets) {
                spriteBatch.Draw (
                    bullet.Texture,
                    new Rectangle (
                        (int)bullet.x, 
                        (int)bullet.y, 
                        (int)bullet.width, 
                        (int)bullet.height
                    ),
                    Color.White
                );
            }
        }

        if (game._listOfEnemies is { Count: > 0 }) {
            foreach (var enemy in game._listOfEnemies) {
                spriteBatch.Draw (
                    enemy.Texture,
                    new Rectangle (
                        (int)enemy.x,
                        (int)enemy.y,
                        (int)enemy.width,
                        (int)enemy.height
                    ),
                    Color.White
                );
            }
        }

        spriteBatch.Draw( 
            game._player.Texture,
            new Rectangle(
                (int)game._player.x, 
                (int)game._player.y, 
                (int)game._player.width, 
                (int)game._player.height
            ), 
            Color.White
        );
    }
}