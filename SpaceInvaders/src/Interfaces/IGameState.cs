using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders {
    public interface IGameState {
        void Update(GameTime gameTime, SpaceInvadersGame game);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpaceInvadersGame game);
    }
}