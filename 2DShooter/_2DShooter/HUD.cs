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
       public int playerScore, screenWidth, screenHeight;
       public SpriteFont playerScoreFont;
       public Vector2 playerScorePos;
       public bool showHud;

       public HUD()
       {
           playerScore = 0;
           showHud = true;
           screenHeight = 800;
           screenWidth = 800;
           playerScoreFont = null;
           playerScorePos = new Vector2(screenWidth / 2, 50);
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
