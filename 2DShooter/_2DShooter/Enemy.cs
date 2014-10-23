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
    public abstract class Enemy : GameCharacter
    {
        private Texture2D newTexture;
        private Texture2D newBulletTexture1;
        private Texture2D newBulletTexture2;
                      

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
            : base(newTexture, newBulletTexture)
        {          
        }

        public Enemy(Texture2D newTexture, Texture2D newBulletTexture1, Texture2D newBulletTexture2)
        {
            // TODO: Complete member initialization
            this.newTexture = newTexture;
            this.newBulletTexture1 = newBulletTexture1;
            this.newBulletTexture2 = newBulletTexture2;
        }

        public abstract void Update(GameTime gameTime)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch)
        {           
        }


        public abstract void UpdateBullets()
        {            
        }

        public abstract void Shoot()
        {
        }

        public override void LoadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }
    }
}
