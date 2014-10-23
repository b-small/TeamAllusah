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
    public class Asteroid : GameUnit, IExplosible
    {
        private float rotationAngle; // rotation of the sprite
        Random random = new Random();
        private float randX, randY;
        

        public Asteroid(Texture2D newTexture, Vector2 newPosition)
            : base(newTexture, newPosition)
        {
            this.RandX = random.Next(0, 750);
            this.RandY = random.Next(-600, -50);
        }

        public float RotationAngle
        {
            get { return this.rotationAngle; }
            set { this.rotationAngle = value; }
        }

        public float RandX
        {
            get { return this.randX; }
            set { this.randX = value; }
        }

        public float RandY
        {
            get { return this.randY; }
            set { this.randY = value; }
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime) //gt
        {
            this.BoundingBox = new Rectangle((int)position.X, (int)position.Y, 45, 45); // 45,45

            //finding the center of our origin sprite
            this.origin.X = this.Texture.Width / 2;
            this.origin.Y = this.Texture.Height / 2;

            position.Y = position.Y + this.Speed;

            if (position.Y >= 950)
            {
                position.Y = -50;
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.RotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            this.RotationAngle = this.RotationAngle % circle;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsVisible)
            {
                spriteBatch.Draw(this.Texture, position, Color.White);
            }
        }



        public List<Bullet> BulletList
        {
            get
            {
                return new List<Bullet>();
            }
            set
            {
                this.BulletList = new List<Bullet>();
            }
        }

        public Vector2 position2
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
    }
}
