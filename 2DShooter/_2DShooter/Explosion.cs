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
    public class Explosion : GameUnit
    {

        public float timer, interval;
        public int currentFrame, spriteWidth, spriteHeight;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
            : base(newTexture, newPosition)
        {
            timer = 0f;
            interval = 30f;
            currentFrame = 1;
            spriteWidth = 40;
            spriteHeight = 60;
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
                this.IsVisible = false;
                currentFrame = 0;
                timer = 0f;
            }

            this.BoundingBox = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(this.BoundingBox.Width, this.BoundingBox.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsVisible)
            {
                spriteBatch.Draw(this.Texture, position, this.BoundingBox, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
