using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders {
    public class PausedState : IGameState {
        public void Update(GameTime gameTime, SpaceInvadersGame game) {
            //todo, idk exactly what?
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpaceInvadersGame game) {
            game._runningState.Draw (gameTime, spriteBatch, game);
            game._pauseScreen.DrawPauseScreen(spriteBatch);
            //todo
            //display score, when i will have score
        }
    }
}