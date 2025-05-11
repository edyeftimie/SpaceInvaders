using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _bug;
    public BulletCollection _listOfBullets;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _listOfBullets = new BulletCollection ();
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
        Texture2D bulletTexture = Content.Load<Texture2D>("bullet");

        // 1920 x 1080
        BulletFactory.Instance.Initialize (100, 100, bulletTexture, 1);
        BulletStrategyFactory.RegisterStrategy ("straight", () => new StraightBulletStrategy ());
        BulletStrategyFactory.RegisterStrategy ("diagonal", () => new DiagonalBulletStrategy ());
        BulletStrategyFactory.RegisterStrategy ("zigzag", () => new ZigZagBulletStrategy ());
        FireFactory.RegisterStrategy ("single", () => new SingleFireStrategy ());
        FireFactory.RegisterStrategy ("double", () => new DoubleFireStrategy ());
        FireFactory.RegisterStrategy ("triple", () => new TripleFireStrategy ());

        Weapon simpleWeapon = new Weapon (0.4, 1000, "single", "straight");
        _bug = new Player (800,800,200,200, bugTexture, 100, 10, simpleWeapon, 10);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState currentState = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
        currentState.IsKeyDown(Keys.Escape))
            Exit();
        if (currentState.IsKeyDown (Keys.Right) ||
        currentState.IsKeyDown(Keys.D)) {
           _bug.move ("right"); 
        } else if (currentState.IsKeyDown (Keys.Left) ||
        currentState.IsKeyDown(Keys.A)) {
            _bug.move ("left");
        } 
        if (currentState.IsKeyDown (Keys.Space)) {
            BulletCollection currentBullets = _bug.Fire();
            if (currentBullets is { Count: > 0})
                foreach (var bullet in currentBullets) {
                    _listOfBullets.Add (bullet);
                } 
        }

        if (_listOfBullets is { Count: > 0}) {
            foreach (var bullet in _listOfBullets) {
                bullet.move ();
            }
        }

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
        if (_listOfBullets is { Count: > 0}) {
            foreach (var bullet in _listOfBullets) {
                _spriteBatch.Draw (
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

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
