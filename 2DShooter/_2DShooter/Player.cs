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

        public Texture2D texture, bulletTexture, healthTexture;
        public Vector2 position, healthBarPosition;
        public int speed, health;
        public float bulletDelay;
        public bool isColliding;
        public Rectangle boundingBox, healthRectangle;
        public List<Bullet> bulletList;
        SoundManager sm = new SoundManager();

        public Player()
        {
            bulletList = new List<Bullet>();
            texture = null;
            position = new Vector2(400, 900);
            bulletDelay = 20;
            speed = 10;
            isColliding = false;
            health = 200;
            healthBarPosition = new Vector2(50, 50);
        }

        // load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ship");
            bulletTexture = Content.Load<Texture2D>("playerbullet");
            healthTexture = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        // draw 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);

            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        // update
        public void Update(GameTime gameTime)
        {
            // read the keyboard every frame
            KeyboardState keyState = Keyboard.GetState();

            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, health, 25);

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
                sm.playerShootSound.Play();
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
                bulletDelay = 10;
            }
        }

        public void UpdateBullets()
        {
            foreach (Bullet b in bulletList)
            {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y - b.speed;

                // if bullet hits the top of screen make it invisible
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

