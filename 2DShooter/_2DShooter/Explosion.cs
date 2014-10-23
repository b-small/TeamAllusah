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

        private float timer, interval;
        private int currentFrame, spriteWidth, spriteHeight;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
            : base(newTexture, newPosition)
        {
            this.Timer = 0f;
            this.Interval = 30f;
            this.CurrentFrame = 1;
            this.SpriteWidth = 64;
            this.SpriteHeight = 64;
        }

        public float Timer
        {
            get { return this.timer; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("timer value can not be negative");
                }
                this.timer = value;
            }
        }

        public float Interval
        {
            get { return this.interval; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("interval value can not be negative");
                }
                this.interval = value;
            }
        }

        public int CurrentFrame{
            get { return this.currentFrame; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("current sprite frame value can not be negative");
                }
                this.currentFrame = value;
            }
        }

        public int SpriteWidth {
            get { return this.spriteWidth; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("sprite width value can not be negative");
                }
                this.spriteWidth = value;
            }
        }

        public int SpriteHeight
        {
            get { return this.spriteHeight; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("sprite height value can not be negative");
                }
                this.spriteHeight = value;
            }
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void Update(GameTime gameTime)
        {
            //increase the timer by nr of mlsecs since last update
            this.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer > this.Interval)
            {
                this.CurrentFrame++;
                //reset timer
                this.Timer = 0f;
            }

            //we're on the last frame
            if (this.CurrentFrame == 17)
            {
                this.IsVisible = false;
                this.CurrentFrame = 0;
                this.Timer = 0f;
            }

            this.BoundingBox = new Rectangle(CurrentFrame * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            origin = new Vector2(this.BoundingBox.Width, this.BoundingBox.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsVisible)
            {
                spriteBatch.Draw(this.Texture, position, this.BoundingBox, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
