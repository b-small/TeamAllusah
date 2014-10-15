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
    public class Asteroid
    {

        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Rectangle boundingBox;
        public float rotationAngle; // rotation of the sprite
        public int speed;
        public bool isColliding;
        public bool isDestroyed;


        public Asteroid()
        {
            position = new Vector2(400, -50);
            texture = null;
            speed = 4;
            isColliding = false;
            isDestroyed = false;
        }


        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("asteroid");
            //finding the center of our origin sprite
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void Update(GameTime gt)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); // 45,45

            position.Y = position.Y + speed;

            if (position.Y >= 950)
            {
                position.Y = -50;
            }

            float elapsed = (float)gt.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;

        }

        public void Draw(SpriteBatch sb)
        {
            if (!isDestroyed)
            {
                sb.Draw(texture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            }
        }

    }
}