using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace _2DShooter
{
    public abstract class GameCharacter : IDrawable, IShooting
    {
        private Rectangle boundingBox;
        private Texture2D texture, bulletTexture;
        private bool isVisible;
        private int speed, health;
        private float bulletDelay;
        private List<Bullet> bulletList;

        public GameCharacter(Texture2D newTexture, Texture2D newBulletTexture)
        {
            this.BulletList = new List<Bullet>();
            this.Texture = newTexture;
            this.BulletTexture = newBulletTexture;
            this.Health = 5;
            this.BulletDelay = 40;
            this.Speed = 5;
            this.IsVisible = true;
        }

        public GameCharacter()
        {
            this.BulletList = new List<Bullet>();
            this.Texture = null;
            this.BulletDelay = 20;
            this.Speed = 10;
            this.Health = 200;
        }

        public Rectangle BoundingBox
        {
            get { return this.boundingBox; }
            set { this.boundingBox = value; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Texture2D BulletTexture
        {
            get { return this.bulletTexture; }
            set { this.bulletTexture = value; }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public int Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public float BulletDelay
        {
            get { return this.bulletDelay; }
            set { this.bulletDelay = value; }
        }

        public List<Bullet> BulletList
        {
            get { return this.bulletList; }
            set { this.bulletList = value; }
        }

        public abstract void LoadContent(ContentManager Content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
        public abstract void Shoot();
        public abstract void UpdateBullets();
    }
}
