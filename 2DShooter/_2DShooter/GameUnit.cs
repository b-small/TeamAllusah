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
    public abstract class GameUnit : IDrawable
    {
        private Rectangle boundingBox;
        private Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        private bool isVisible;
        private float speed;

        public GameUnit(Texture2D newTexture)
        {
            this.Speed = 10;
            this.Texture = newTexture;
            this.IsVisible = false;
        }

        public GameUnit(Texture2D newTexture, Vector2 newPosition)
        {
            this.Texture = newTexture;
            position = newPosition;
            this.Speed = 4;
            this.IsVisible = true;
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

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }
        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
