using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _2DShooter
{
    class BlackEnemy : Enemy
    {
        public BlackEnemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
            : base(newTexture, newPosition, newBulletTexture)
        {
        }

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("enemyship");
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
    }
}
