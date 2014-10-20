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
    public class Asteroid : IDrawable
    {

        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Rectangle boundingBox;
        public float rotationAngle; // rotation of the sprite
        public int speed;

        public bool isVisible;
        Random random = new Random();
        public float randX, randY;

        public Asteroid(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);
        }


        public void LoadContent(ContentManager Content)
        {
       
        }

        public void Update(GameTime gameTime) //gt
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 45, 45); // 45,45

            //finding the center of our origin sprite
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            position.Y = position.Y + speed;

            if (position.Y >= 950)
            {
                position.Y = -50;
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

    }
}
