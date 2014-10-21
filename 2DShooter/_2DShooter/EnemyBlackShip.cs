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
    public class EnemyBlackShip : Enemy
    {
         public int currentDifficultyLevel;
         public Vector2 position;

         public EnemyBlackShip(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
             : base(newTexture, newBulletTexture)
         {
             position = newPosition;
             currentDifficultyLevel = 1;
         }

        public override void Update(GameTime gameTime)
        {
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);

            position.Y += Speed;

            if (position.Y >= 950)
            {
                position.Y = -75;
            }

            Shoot();
            UpdateBullets();
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
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y + b.speed;

                // if bullet hits the top of screen make it invisible
                if (b.position.Y >= 950)
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

        public override void Shoot()
        {
            if (BulletDelay >= 0)
            {
                BulletDelay--;
            }

            if (BulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(BulletTexture);
                newBullet.position = new Vector2(position.X + Texture.Width / 2 - newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;

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

        public override void LoadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }
    }
}
