using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DShooter
{
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer, interval;
        public Vector2 origin;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            timer = 0f;
            interval = 20f;
            currentFrame = 1;
            spriteWidth = 100;
            spriteHeight = 100;
            isVisible = true;
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void Update(GameTime gameTime)
        {
            //increase the timer by nr of mlsecs since last update
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                //reset timer
                timer = 0f;
            }

            //we're on the last frame
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
