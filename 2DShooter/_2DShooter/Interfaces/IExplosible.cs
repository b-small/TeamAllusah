using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace _2DShooter
{
    interface IExplosible
    {
         Rectangle BoundingBox
        {
            get;
            set;
        }
         List<Bullet> BulletList
         {
             get;
             set;
         }
         bool IsVisible
         {
             get;
             set;
         }

         Vector2 position2
         {
             get;
             set;
         }

        void Update(GameTime gameTime);
    }
}
