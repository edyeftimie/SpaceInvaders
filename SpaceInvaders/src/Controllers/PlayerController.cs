using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerController
{
    public void HandleInput(GameTime gameTime, Player player, Collection<Bullet> bulletList)
    {
        KeyboardState currentState = Keyboard.GetState();
        player.UpdateGameTime(gameTime);

        HandleMovement(currentState, player);
        HandleFiring(currentState, player, bulletList);
    }

    private void HandleMovement(KeyboardState currentState, Player player)
    {
        if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D))
        {
            player.move("right");
        }
        else if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A))
        {
            player.move("left");
        }
    }

    private void HandleFiring(KeyboardState currentState, Player player, Collection<Bullet> bulletList)
    {
        if (currentState.IsKeyDown(Keys.Space))
        {
            Collection<Bullet> currentBullets = player.Fire();
            if (currentBullets is { Count: > 0 })
            {
                foreach (var bullet in currentBullets)
                {
                    bulletList.Add(bullet);
                }
            }
        }
    }
}