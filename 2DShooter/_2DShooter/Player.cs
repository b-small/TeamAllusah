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
    class Player
    {
        
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        // Collision Variables
        public bool isColliding;
        public Rectangle boundingBox;

        public Player()
        {
            texture = null;
            position = new Vector2(300, 300);
            speed = 10;
            isColliding = false;
        }

        // load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ship");
          
        }

        // draw 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        // update
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); // read the keyboard every frame

            // Ship Controls
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y = position.Y - speed;                
            }
            
            if (keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed;
            }
            
            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + speed;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed;
            }

            // Keep player ship in the screen bounds
            if (position.X <= 0)
            {
                position.X = 0;
            }

            if (position.X >= 800 - texture.Width)
            {
                position.X = 800 - texture.Width;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
            }

            if (position.Y >= 720 - texture.Height)
            {
                position.Y = 720 - texture.Height;
            }
        }

         
    }
}
