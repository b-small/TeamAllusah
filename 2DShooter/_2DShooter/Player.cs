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
    class Player
    {

        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int speed;
        public float bulletDelay;
        public bool isColliding;
        public Rectangle boundingBox;
        public List<Bullet> bulletList;

        public Player()
        {
            bulletList = new List<Bullet>();
            texture = null;
            position = new Vector2(300, 300);
            bulletDelay = 5;
            speed = 10;
            isColliding = false;
        }

        // load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ship");
            bulletTexture = Content.Load<Texture2D>("playerbullet");

        }

        // draw 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        // update
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); // read the keyboard every frame

            // fire Bullets
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }

            UpdateBullets();
            // Ship Controls
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y = position.Y - speed;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + speed;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed;
            }

            // Keep player ship in the screen bounds
            if (position.X <= 0)
            {
                position.X = 0;
            }

            if (position.X >= 800 - texture.Width)
            {
                position.X = 800 - texture.Width;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
            }

            if (position.Y >= 720 - texture.Height)
            {
                position.Y = 720 - texture.Height;
            }
        }
        // Shoot Method startimg position of our bullet
        public void Shoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + 32 - newBullet.texture.Width / 2,
                                                 position.Y + 30);

                newBullet.isVisible = true;

                if (bulletList.Count() < 20)
                {
                    bulletList.Add(newBullet);
                }
            }

            if (bulletDelay == 0)
            {
                bulletDelay = 5;
            }            
        }

        public void UpdateBullets()
        {
            foreach (Bullet b in bulletList)
            {
                b.position.Y = b.position.Y - b.speed;

                // if bullet hits the top of screen make i t invisible
                if (b.position.Y <= 0)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

