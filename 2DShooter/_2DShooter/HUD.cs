using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _2DShooter
{
    public class HUD
    {
        private int playerScore, screenWidth, screenHeight;
        private SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        private bool showHud;

        public HUD()
        {
            playerScore = 0;
            showHud = true;
            screenHeight = 800;
            screenWidth = 800;
            playerScoreFont = null;
            playerScorePos = new Vector2(screenWidth / 2, 50);
        }

        public int PlayerScore
        {
            get { return this.playerScore; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("score can not be negative");
                }
                this.playerScore = value;
            }
        }

        public int ScreenWidth
        {
            get { return this.screenWidth; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("screen width can not be negative");
                }
                this.screenWidth = value;
            }
        }

        public int ScreenHeight
        {
            get { return this.screenHeight; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("screen height can not be negative");
                }
                this.screenHeight = value;
            }
        }

        public SpriteFont PlayerScoreFont
        {
            get { return this.playerScoreFont; }
            set { this.playerScoreFont = value; }
        }

        public bool ShowHud
        {
            get { return this.showHud; }
            set { this.showHud = value; }
        }

        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("georgia");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHud)
            {
                spriteBatch.DrawString(playerScoreFont, "Score - " + playerScore, playerScorePos, Color.Red);
            }
        }

    }
}
