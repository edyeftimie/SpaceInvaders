using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GameObject _bug;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        // bug = GameObject(1,1,10,10,'bug.png');
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D bugTexture = Content.Load<Texture2D>("bug");
        // 1920 x 1080
        _bug = new GameObject(800,800,200,200, bugTexture);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // GraphicsDevice.Clear(Color.CornflowerBlue);
        GraphicsDevice.Clear(Color.Bisque);
        // GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        // Draw the bug
        _spriteBatch.Draw(
            _bug.Texture,                          // Texture2D
            new Rectangle(
                (int)_bug.x, 
                (int)_bug.y, 
                (int)_bug.width, 
                (int)_bug.height
            ), 
            Color.White
        );

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
