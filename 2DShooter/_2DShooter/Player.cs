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
    class Player : GameCharacter, IMovable
    {
        public Texture2D healthTexture;
        public Vector2 healthBarPosition;
        public bool isColliding;
        public Rectangle healthRectangle;
        public Vector2 position;

        SoundManager sm = new SoundManager();

        public Player()
            : base()
        {
            isColliding = false;
            healthBarPosition = new Vector2(50, 50);
            position = new Vector2(400, 900);
        }

        // load content
        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("ship");
            BulletTexture = Content.Load<Texture2D>("playerbullet");
            healthTexture = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        // draw 
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);

            foreach (Bullet b in BulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public void Move()
        {
            KeyboardState keyState = Keyboard.GetState();

            // fire Bullets
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }

            UpdateBullets();

            // Ship Controls
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y = position.Y - Speed;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - Speed;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + Speed;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + Speed;
            }

            // Keep player ship in the screen bounds
            if (position.X <= 0)
            {
                position.X = 0;
            }

            if (position.X >= 800 - Texture.Width)
            {
                position.X = 800 - Texture.Width;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
            }

            if (position.Y >= 720 - Texture.Height)
            {
                position.Y = 720 - Texture.Height;
            }
        }
        // update
        public override void Update(GameTime gameTime)
        {
            // read the keyboard every frame
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, Health, 25);

            // UpdateBullets();
            Move();
        }
        // Shoot Method startimg position of our bullet
        public override void Shoot()
        {
            if (BulletDelay >= 0)
            {
                BulletDelay--;
            }

            if (BulletDelay <= 0)
            {
                sm.playerShootSound.Play();
                Bullet newBullet = new Bullet(BulletTexture);
                newBullet.position = new Vector2(position.X + 32 - newBullet.texture.Width / 2,
                                                 position.Y + 30);

                newBullet.isVisible = true;

                if (BulletList.Count() < 20)
                {
                    BulletList.Add(newBullet);
                }
            }

            if (BulletDelay == 0)
            {
                BulletDelay = 10;
            }
        }

        public override void UpdateBullets()
        {
            foreach (Bullet b in BulletList)
            {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y - b.speed;

                // if bullet hits the top of screen make it invisible
                if (b.position.Y <= 0)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < BulletList.Count; i++)
            {
                if (!BulletList[i].isVisible)
                {
                    BulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

