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
    public class Starfield
    {
        private Texture2D texture;
        private Vector2 bgPos1, bgPos2;
        private int speed;

        public Starfield()
        {
            this.Texture = null;
            this.BgPos1 = new Vector2(0, 0);
            this.BgPos2 = new Vector2(0, -950);
            this.Speed = 5;
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set
            { this.texture = value; }
        }

        public Vector2 BgPos1
        {
            get { return this.bgPos1; }
            set { this.bgPos1 = value; }
        }

        public Vector2 BgPos2
        {
            get { return this.bgPos2; }
            set { this.bgPos2 = value; }
        }

        public int Speed
        {
            get { return this.speed; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Speed can not be a negative number");
                }
                this.speed = value;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("space");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;

            if (bgPos1.Y >= 950)
            {
                bgPos1.Y = 0;
                bgPos2.Y = -950;
            }
        }
    }
}
