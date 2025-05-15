using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders {
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        public Collection<Bullet> _listOfBullets;
        public Collection<Enemy> _listOfEnemies;
        public Constants constants;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            Logger.Initialize (null, false, true, false);
            Logger.Log ("Game is initializing");

            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges ();

            int screenWidth = GraphicsDevice.DisplayMode.Width;
            int screenHeight = GraphicsDevice.DisplayMode.Height;
            constants = new Constants (screenWidth, screenHeight);

            Window.Title = $"SpaceInvaders++ ({constants.screenWidth}x{constants.screenHeight})";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D bugTexture = Content.Load<Texture2D>("bug");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");

            BulletFactory.Instance.Initialize (
                constants.bulletWidth,
                constants.bulletHeight, 
                bulletTexture,
                constants.bulletHealth
            );

            BulletStrategyFactory.RegisterStrategy (
                "straight",
                () => new StraightBulletStrategy ()
            );
            BulletStrategyFactory.RegisterStrategy (
                "diagonal", 
                () => new DiagonalBulletStrategy (constants.diagonalStrategyCoefficient)
            );
            BulletStrategyFactory.RegisterStrategy (
                "zigzag",
                () => new ZigZagBulletStrategy (constants.zigzagStrategySwitchFrame)
            );

            FireFactory.RegisterStrategy (
                "single",
                () => new SingleFireStrategy ()
            );
            FireFactory.RegisterStrategy (
                "double",
                () => new DoubleFireStrategy (constants.spaceBetweenBullets)
            );
            FireFactory.RegisterStrategy (
                "triple",
                () => new TripleFireStrategy (constants.spaceBetweenBullets)
            );

            // EnemyFactory.Instance.Initialize (constants.ENEMY_WIDTH, constants.ENEMY_HEIGHT, bugTexture, 5, 1, 5, 0.4, 2, 1, 3, 10);
            EnemyFactory.Instance.Initialize (
                constants.enemyWidth,
                constants.enemyHeight,
                bugTexture,
                constants.enemyHealth,
                constants.enemyStartDamageInterval,
                constants.enemyEndDamageInterval,
                constants.enemyStartCooldownInterval,
                constants.enemyEndCooldownInterval,
                constants.enemyStartMovementSpeedInterval,
                constants.enemyEndMovementSpeedInterval,
                constants.enemyAmmo
            );

            _listOfBullets = new Collection<Bullet> ();
            _listOfEnemies = new Collection<Enemy> ();

            // Weapon simpleWeapon = new Weapon (0.4, 1000, "single", "straight");
            // Weapon simpleWeapon = new Weapon (0.4, 1000, "triple", "straight");
            Weapon simpleWeapon = new Weapon (
                constants.weaponCooldown,
                constants.weaponAmmo,
                "triple", 
                "straight");
            _player = new Player (
                constants.playerDefaultX,
                constants.playerDefaultY,
                constants.playerWidth,
                constants.playerHeight, 
                bugTexture, 
                constants.playerHealth,
                constants.playerDamage,
                simpleWeapon,
                constants.playerMovementSpeed);

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
            _player.move ("right"); 
            } else if (currentState.IsKeyDown (Keys.Left) ||
            currentState.IsKeyDown(Keys.A)) {
                _player.move ("left");
            } 

            if (currentState.IsKeyDown (Keys.Space)) {
                Collection<Bullet> currentBullets = _player.Fire();
                if (currentBullets is { Count: > 0})
                    foreach (var bullet in currentBullets) {
                        _listOfBullets.Add (bullet);
                    } 
            }

            if (_listOfEnemies is { Count : <= 0}) {
                Collection<Enemy> spawnEnemies = EnemyFactory.Instance.CreateRowOfEnemies (10, 100, 1700, 50);
                foreach (var enemy in spawnEnemies) {
                    _listOfEnemies.Add (enemy);
                }
            } else {
                foreach (var enemy in _listOfEnemies) {
                    Collection<Bullet> currentBullets = enemy.Fire ();
                    if (currentBullets is { Count: >0}){
                        foreach (var bullet in currentBullets) {
                            _listOfBullets.Add (bullet);
                        }
                    } else {
                        enemy.move ();
                    }
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
                _player.Texture,                          // Texture2D
                new Rectangle(
                    (int)_player.x, 
                    (int)_player.y, 
                    (int)_player.width, 
                    (int)_player.height
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

            if (_listOfEnemies is { Count: > 0}) {
                foreach (var enemy in _listOfEnemies) {
                    _spriteBatch.Draw (
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

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}