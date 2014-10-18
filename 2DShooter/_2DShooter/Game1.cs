using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        public int enemyBulletDamage;

        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();

        Player p = new Player();
        Starfield sf = new Starfield();
        HUD hud = new HUD();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            // want the screen to be full?
            graphics.IsFullScreen = false;

            // the width of the screen
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            this.Window.Title = "Our 2D Shooter Game";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p.LoadContent(Content);
            sf.LoadContent(Content);
            hud.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (Enemy e in enemyList)
            {
                if (e.boundingBox.Intersects(p.boundingBox))
                {
                    p.health -= 40;
                    e.isVisible = false;
                }

                //bullet collision
                for (int i = 0; i < e.bulletList.Count; i++)
                {
                    if (p.boundingBox.Intersects(e.bulletList[i].boundingBox))
                    {
                        p.health -= enemyBulletDamage;
                        e.bulletList[i].isVisible = false;
                    }
                }

                for (int i = 0; i < p.bulletList.Count; i++)
                {
                    if (p.bulletList[i].boundingBox.Intersects(e.boundingBox))
                    {
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position.X, e.position.Y)));
                        hud.playerScore += 20;
                        p.bulletList[i].isVisible = false;
                        e.isVisible = false;
                    }
                }

                e.Update(gameTime);
            }

            foreach (Explosion ex in explosionList)
            {
                ex.Update(gameTime);
            }

            //update&check asteroids for collision
            foreach (Asteroid a in asteroidList)
            {
                if (a.boundingBox.Intersects(p.boundingBox))
                {
                    p.health -= 20;
                    a.isVisible = false;
                }

                //iterate through the bulletList and check for collision
                for (int i = 0; i < p.bulletList.Count; i++)
                {
                    if (a.boundingBox.Intersects(p.bulletList[i].boundingBox))
                    {
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(a.position.X, a.position.Y)));
                        hud.playerScore += 5;
                        a.isVisible = false;
                        p.bulletList.ElementAt(i).isVisible = false;
                    }
                }

                a.Update(gameTime);
            }

            hud.Update(gameTime);
            p.Update(gameTime);
            sf.Update(gameTime);
            ManageExplosions();
            LoadAsteroids();
            LoadEnemies();

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            sf.Draw(spriteBatch);
            p.Draw(spriteBatch);

            foreach (Explosion ex in explosionList)
            {
                ex.Draw(spriteBatch);
            }

            foreach (Asteroid a in asteroidList)
            {
                a.Draw(spriteBatch);
            }

            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }

            hud.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadAsteroids()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (asteroidList.Count < 5)
            {
                asteroidList.Add(new Asteroid(Content.Load<Texture2D>("asteroid"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadEnemies()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (enemyList.Count() < 3)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
