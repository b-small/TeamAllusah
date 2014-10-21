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
        private Texture2D healthTexture;
        private Vector2 healthBarPosition;
        private bool isColliding;
        private Rectangle healthRectangle;

        public Vector2 position;
        SoundManager sound = new SoundManager();

        public Player()
            : base()
        {
            this.IsColliding = false;
            this.HealthBarPosition = new Vector2(50, 50);
            position = new Vector2(400, 900);
        }

        public Texture2D HealthTexture
        {
            get { return this.healthTexture; }
            set { this.healthTexture = value; }
        }
        public Vector2 HealthBarPosition
        {
            get { return this.healthBarPosition; }
            set { this.healthBarPosition = value; }
        }
        public bool IsColliding
        {
            get { return this.isColliding; }
            set { this.isColliding = value; }
        }
        public Rectangle HealthRectangle
        {
            get { return this.healthRectangle; }
            set { this.healthRectangle = value; }
        }
   
        // load content
        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("ship");
            BulletTexture = Content.Load<Texture2D>("playerLaser");
            healthTexture = Content.Load<Texture2D>("healthbar");
            sound.LoadContent(Content);
        }

        // draw 
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, position, Color.White);
            spriteBatch.Draw(this.HealthTexture, this.HealthRectangle, Color.White);

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
                position.Y = position.Y - this.Speed;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - this.Speed;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + this.Speed;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + this.Speed;
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
            this.HealthRectangle = new Rectangle((int)this.HealthBarPosition.X, (int)this.HealthBarPosition.Y, Health, 25);

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
                sound.playerShootSound.Play();
                Bullet newBullet = new Bullet(BulletTexture);
                newBullet.position = new Vector2(position.X + 32 - newBullet.Texture.Width / 2,
                                                 position.Y + 30);

                newBullet.IsVisible = true;

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
                b.BoundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.Texture.Width, b.Texture.Height);
                b.position.Y = b.position.Y - b.Speed;

                // if bullet hits the top of screen make it invisible
                if (b.position.Y <= 0)
                {
                    b.IsVisible = false;
                }
            }

            for (int i = 0; i < BulletList.Count; i++)
            {
                if (!BulletList[i].IsVisible)
                {
                    BulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

