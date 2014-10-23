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
    abstract class Enemy : GameCharacter, IExplosible
    {
        public int currentDifficultyLevel;
        public Vector2 position;

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
            : base(newTexture, newBulletTexture)
        {
            position = newPosition;
            currentDifficultyLevel = 1;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);

            foreach (Bullet b in BulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public override void UpdateBullets()
        {
            foreach (Bullet b in BulletList)
            {
                b.BoundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.Texture.Width, b.Texture.Height);
                b.position.Y = b.position.Y + b.Speed;

                // if bullet hits the top of screen make it invisible
                if (b.position.Y >= 950)
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

        public override void Shoot()
        {
            if (BulletDelay >= 0)
            {
                BulletDelay--;
            }

            if (BulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(BulletTexture);
                newBullet.position = new Vector2(position.X + Texture.Width / 2 - newBullet.Texture.Width / 2, position.Y + 30);

                newBullet.IsVisible = true;

                if (BulletList.Count() < 20)
                {
                    BulletList.Add(newBullet);
                }
            }

            if (BulletDelay == 0)
            {
                BulletDelay = 40;
            }
        }
    }
}
