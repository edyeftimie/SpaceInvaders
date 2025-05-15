using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders {
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Constants _constants;

        public Player _player;
        public Collection<Bullet> _listOfBullets;
        public Collection<Enemy> _listOfEnemies;


        internal PauseScreen _pauseScreen;
        public IGameState CurrentState {get; private set;}
        public RunningState _runningState;
        private PausedState _pausedState;

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
            _constants = new Constants (screenWidth, screenHeight);

            Window.Title = $"SpaceInvaders++ ({_constants.screenWidth}x{_constants.screenHeight})";

            _runningState = new RunningState ();
            _pausedState = new PausedState ();
            CurrentState = _runningState;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D bugTexture = Content.Load<Texture2D>("bug");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");

            Texture2D rectangleTexture = new Texture2D (GraphicsDevice, 1, 1);
            rectangleTexture.SetData([Color.White]);
            SpriteFont _defaultFont = Content.Load<SpriteFont>("Fonts/GameFont");
            _pauseScreen = new PauseScreen (rectangleTexture, _defaultFont, _constants.pauseScreenX, _constants.pauseScreenY, _constants.pauseScreenWidth, _constants.pauseScreenHeight, _constants.pauseCooldown);

            BulletFactory.Instance.Initialize (
                _constants.bulletWidth,
                _constants.bulletHeight, 
                bulletTexture,
                _constants.bulletHealth
            );

            BulletStrategyFactory.RegisterStrategy (
                "straight",
                () => new StraightBulletStrategy ()
            );
            BulletStrategyFactory.RegisterStrategy (
                "diagonal", 
                () => new DiagonalBulletStrategy (_constants.diagonalStrategyCoefficient)
            );
            BulletStrategyFactory.RegisterStrategy (
                "zigzag",
                () => new ZigZagBulletStrategy (_constants.zigzagStrategySwitchFrame)
            );

            FireFactory.RegisterStrategy (
                "single",
                () => new SingleFireStrategy ()
            );
            FireFactory.RegisterStrategy (
                "double",
                () => new DoubleFireStrategy (_constants.spaceBetweenBullets)
            );
            FireFactory.RegisterStrategy (
                "triple",
                () => new TripleFireStrategy (_constants.spaceBetweenBullets)
            );

            // EnemyFactory.Instance.Initialize (_constants.ENEMY_WIDTH, _constants.ENEMY_HEIGHT, bugTexture, 5, 1, 5, 0.4, 2, 1, 3, 10);
            EnemyFactory.Instance.Initialize (
                _constants.enemyWidth,
                _constants.enemyHeight,
                bugTexture,
                _constants.enemyHealth,
                _constants.enemyStartDamageInterval,
                _constants.enemyEndDamageInterval,
                _constants.enemyStartCooldownInterval,
                _constants.enemyEndCooldownInterval,
                _constants.enemyStartMovementSpeedInterval,
                _constants.enemyEndMovementSpeedInterval,
                _constants.enemyAmmo
            );

            _listOfBullets = new Collection<Bullet> ();
            _listOfEnemies = new Collection<Enemy> ();

            Weapon simpleWeapon = new Weapon (
                _constants.weaponCooldown,
                _constants.weaponAmmo,
                "triple", 
                "straight");
            _player = new Player (
                _constants.playerDefaultX,
                _constants.playerDefaultY,
                _constants.playerWidth,
                _constants.playerHeight, 
                bugTexture, 
                _constants.playerHealth,
                _constants.playerDamage,
                simpleWeapon,
                _constants.playerMovementSpeed);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            currentState.IsKeyDown(Keys.Escape))
                Exit();

            if (currentState.IsKeyDown (Keys.P)) {
                if (_pauseScreen.triggerPause ()) {
                    CurrentState = CurrentState == _runningState ? (IGameState)_pausedState : _runningState;
                }
            }
            CurrentState.Update (gameTime, this);
            base.Update (gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear (Color.Bisque);
            _spriteBatch.Begin ();
            CurrentState.Draw (gameTime, _spriteBatch, this);
            _spriteBatch.End ();
            base.Draw (gameTime);
        }
    }
}