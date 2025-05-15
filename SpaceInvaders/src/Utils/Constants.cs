using System.Dynamic;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders {
    public class Constants {
        private double _screenWidthDifferenceCoefficient {get; set;}
        private double _screenHeightDifferenceCoefficient {get; set;}
        private int _defaultScreenWidth {get; set;} = 1920;
        private int _defaultScreenHeight {get; set;} = 1080;
        public int screenWidth {get; private set;}
        public int screenHeight {get; private set;}
        public int pauseScreenWidth {get; private set;} = 500;
        public int pauseScreenHeight {get; private set;} = 300;
        public int pauseScreenX {get; private set;}
        public int pauseScreenY {get; private set;}
        public double pauseCooldown {get;} = 0.4;
        public int playerDefaultX {get; } = 800;
        public int playerDefaultY {get; } = 800;
        public int playerWidth {get; private set;} = 200;
        public int playerHeight {get; private set;} = 200;
        public int playerHealth {get;} = 100;
        public int playerDamage {get;} = 1;
        public double playerMovementSpeed {get;} = 10;
        public int enemyWidth {get; private set;} = 100;
        public int enemyHeight {get; private set;} = 100;
        public int enemyHealth {get; private set;} = 5;
        public int enemyStartDamageInterval {get; private set;} = 1;
        public int enemyEndDamageInterval {get; private set;} = 3;
        public double enemyStartCooldownInterval {get; private set;} = 0.5;
        public double enemyEndCooldownInterval {get; private set;} = 2.2;
        public double enemyStartMovementSpeedInterval {get; private set;} = 1;
        public double enemyEndMovementSpeedInterval {get; private set;} = 3;
        public int rarityRateOfNewStrategies { get; private set; } = 80;
        public int enemyAmmo {get; private set;} = 10;
        public int spaceBetweenEnemies { get; private set; } = 50;
        public double weaponCooldown {get;} = 0.4;
        public int weaponAmmo {get;} = 1000;
        public int bulletWidth {get; private set;} = 60;
        public int bulletHeight {get; private set;} = 60;
        public int bulletHealth {get; private set;} = 1;
        public int spaceBetweenBullets { get; private set; } = 50;
        public int zigzagStrategySwitchFrame {get; private set;} = 20;
        public int diagonalStrategyCoefficient {get; private set;} = 33;

        public Constants(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            Logger.Log("screen_height: " + screenHeight);
            Logger.Log("screen_width: " + screenWidth);
            if (screenHeight != _defaultScreenHeight)
            {
                _screenHeightDifferenceCoefficient = screenHeight / (double)_defaultScreenHeight;
                Logger.Log("height_coef: " + _screenHeightDifferenceCoefficient);
                playerHeight = (int)(playerHeight * _screenHeightDifferenceCoefficient);
                enemyHeight = (int)(enemyHeight * _screenHeightDifferenceCoefficient);
                bulletHeight = (int)(bulletHeight * _screenHeightDifferenceCoefficient);
            }

            if (screenWidth != _defaultScreenWidth)
            {
                _screenWidthDifferenceCoefficient = screenWidth / (double)_defaultScreenWidth;
                Logger.Log("width_coef: " + _screenWidthDifferenceCoefficient);
                playerWidth = (int)(playerWidth * _screenWidthDifferenceCoefficient);
                enemyWidth = (int)(enemyWidth * _screenWidthDifferenceCoefficient);
                bulletWidth = (int)(bulletWidth * _screenWidthDifferenceCoefficient);
                spaceBetweenBullets = (int)(spaceBetweenBullets * _screenHeightDifferenceCoefficient);
            }
            playerDefaultX = screenWidth / 2 - playerWidth / 2;
            Logger.Log("playerX: " + playerDefaultX);
            playerDefaultY = screenHeight - playerHeight;
            Logger.Log("playerX: " + playerDefaultY);

            pauseScreenX = screenWidth / 2 - pauseScreenWidth / 2;
            pauseScreenY = screenHeight / 2 - pauseScreenHeight / 2;
            spaceBetweenBullets = bulletWidth;
            spaceBetweenEnemies = enemyWidth;
        }
    }
}