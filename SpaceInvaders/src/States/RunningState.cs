using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;

public class RunningState : IGameState
{
    public void Update(GameTime gameTime, SpaceInvadersGame game) {
        KeyboardState currentState = Keyboard.GetState();
        game._player.UpdateGameTime (gameTime);
        if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D)) {
            game._player.move("right");
        } else if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A)) {
            game._player.move("left");
        }

        if (currentState.IsKeyDown(Keys.Space)) {
            Collection<Bullet> currentBullets = game._player.Fire();
            if (currentBullets is { Count: > 0 }) {
                foreach (var bullet in currentBullets) {
                    game._listOfBullets.Add(bullet);
                }
            }
        }

        if (game._destroyableObjects is { Count: > 0}) {
            foreach (var gameObject in game._destroyableObjects) {
                gameObject.destroyEntity ();
                if (gameObject is Enemy enemy) {
                    Logger.Log("destroy enemy is "+game._listOfEnemies.Remove (enemy));
                } else if (gameObject is Bullet bullet) {
                    Logger.Log("destrooy bullet is "+game._listOfBullets.Remove (bullet));
                }
            }
        }

        if (game._listOfEnemies is { Count: <= 0 })
        {
            Collection<Enemy> spawnEnemies = EnemyFactory.Instance.CreateRowOfEnemies(10, 100, 1700, 50);
            foreach (var enemy in spawnEnemies)
            {
                game._listOfEnemies.Add(enemy);
            }
        }
        else
        {
            foreach (var enemy in game._listOfEnemies)
            {
                // game._destroyableObjects = new Collection<DamageableObject> ();
                enemy.UpdateGameTime(gameTime);
                Collection<Bullet> currentBullets = enemy.Fire();
                if (currentBullets is { Count: > 0 })
                {
                    foreach (var bullet in currentBullets)
                    {
                        game._listOfBullets.Add(bullet);
                    }
                }
                else
                {
                    bool isValidMove = enemy.move();
                    if (!isValidMove)
                    {
                        game._destroyableObjects.Add(enemy);
                    }
                }
            }
        }

        if (game._listOfBullets is { Count: > 0 }) {
            foreach (var bullet in game._listOfBullets) {
                bool isValidMove = bullet.move();
                if (!isValidMove) {
                    game._destroyableObjects.Add(bullet);
                }
            }
        }
    }

    public void Draw (GameTime gameTime, SpriteBatch spriteBatch, SpaceInvadersGame game) {
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