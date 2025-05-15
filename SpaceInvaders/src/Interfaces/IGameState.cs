using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders {
    public interface IGameState {
        void Update(GameTime gameTime, Game1 game);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game);
    }
}