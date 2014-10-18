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
    public class Enemy
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficultyLevel;
        public bool isVisible;
        public List<Bullet> bulletList;

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newBulletTexture;
            health = 5;
            position = newPosition;
            currentDifficultyLevel = 1;
            bulletDelay = 40;
            speed = 5;
            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            position.Y += speed;

            if (position.Y >= 950)
            {
                position.Y = -75;
            }

            EnemyShoot();
            UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }


        public void UpdateBullets()
        {
            foreach (Bullet b in bulletList)
            {
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.X, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y + b.speed;

                // if bullet hits the top of screen make it invisible
                if (b.position.Y >= 950)
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

        public void EnemyShoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;

                if (bulletList.Count() < 20)
                {
                    bulletList.Add(newBullet);
                }
            }

            if (bulletDelay == 0)
            {
                bulletDelay = 40;
            }
        }
    }
}
