using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders {
    public class PauseScreen {
        private Texture2D _rectangleTexture;
        private SpriteFont _font;
        private int _pauseScreenX; 
        private int _pauseScreenY;
        private int _pauseScreenHeight;
        private int _pauseScreenWidth;
        private bool? _isMenuActive;
        private double _pauseCooldown;
        private DateTime? _lastTimeWherePaused;
        public bool isMenuActive {
            get {
                if (_isMenuActive == null) {
                    return false;
                }
                return _isMenuActive.Value;
            }
        }

        private bool canItPause () {
            if (_lastTimeWherePaused == null) {
                return true;
            }
            DateTime currentTime = DateTime.Now;
            TimeSpan timeDifference = currentTime - _lastTimeWherePaused.Value;
            if (timeDifference.TotalMilliseconds >= 1000*_pauseCooldown) {
                return true;
            }
            return false;
        }

        public void triggerPause () {
            if (_isMenuActive == null) {
                _isMenuActive = true;
                _lastTimeWherePaused = DateTime.Now;
            } else {
                if (canItPause ()) {
                    _isMenuActive = !_isMenuActive;
                    _lastTimeWherePaused = DateTime.Now;
                }
            }
        }

        public PauseScreen (Texture2D rectangleTexture, SpriteFont font, int pauseScreenX, int pauseScreenY, int pauseScreenWidth, int pauseScreenHeight, double pauseCooldown) {
            _rectangleTexture = rectangleTexture;
            _font = font;
            _pauseScreenHeight = pauseScreenHeight;
            _pauseScreenWidth = pauseScreenWidth;
            _pauseScreenX = pauseScreenX;
            _pauseScreenY = pauseScreenY;
            _pauseCooldown = pauseCooldown;
            _lastTimeWherePaused = DateTime.Now;
        }
        public void DrawPauseScreen (SpriteBatch spriteBatch) {
            Color semiTransparentBlackInt = new Color(0, 0, 0, 128); // Red, Green, Blue, Alpha
            spriteBatch.Draw (
                _rectangleTexture,
                new Rectangle (
                    _pauseScreenX,
                    _pauseScreenY,
                    _pauseScreenWidth,
                    _pauseScreenHeight
                ),
                semiTransparentBlackInt
            );
            // Draw "Paused" text with scaling for larger appearance
            string pausedText = "Paused";
            float largeScale = 1.0f; // Scale factor to make text appear larger
            Vector2 pausedTextSize = _font.MeasureString(pausedText) * largeScale;
            Vector2 pausedTextPosition = new Vector2(
                _pauseScreenX + (_pauseScreenWidth - pausedTextSize.X) / 2,
                _pauseScreenY + (_pauseScreenHeight - pausedTextSize.Y) / 2 - 20
            );
            spriteBatch.DrawString(_font, pausedText, pausedTextPosition, Color.White, 
                                0f, Vector2.Zero, largeScale, SpriteEffects.None, 0f);

            // Draw "Press P to continue" text at normal/smaller scale
            string continueText = "Press P to continue";
            float smallScale = 0.5f; // Normal scale
            Vector2 continueTextSize = _font.MeasureString(continueText) * smallScale;
            Vector2 continueTextPosition = new Vector2(
                _pauseScreenX + (_pauseScreenWidth - continueTextSize.X) / 2,
                pausedTextPosition.Y + pausedTextSize.Y
            );
            spriteBatch.DrawString(_font, continueText, continueTextPosition, Color.White,
                                0f, Vector2.Zero, smallScale, SpriteEffects.None, 0f);

        }


    }
}