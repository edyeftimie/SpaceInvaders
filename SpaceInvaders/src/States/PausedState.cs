using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders {
    public class PausedState : IGameState {
        public void Update(GameTime gameTime, Game1 game) {
            // Add specific update logic for the paused state here if needed
            // For example, handling menu navigation input.
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game) {
            // Optionally draw the running game in the background
            // game.CurrentState.Draw(gameTime, spriteBatch, game);
            game._runningState.Draw (gameTime, spriteBatch, game);
            game._pauseScreen.DrawPauseScreen(spriteBatch);
        }
    }
}