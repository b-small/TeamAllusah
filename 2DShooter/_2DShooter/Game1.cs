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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        public int enemyBulletDamage;
        public Texture2D menuImage;
        public Texture2D gameoverImage;

        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();

        Player p = new Player();
        Starfield sf = new Starfield();
        HUD hud = new HUD();
        SoundManager sm = new SoundManager();

        State gameState = State.Menu;

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
            menuImage = null;
            gameoverImage = null;
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
            sm.LoadContent(Content);
            try
            {
                menuImage = Content.Load<Texture2D>("menuImage");
                //gameoverImage = Content.Load<Texture2D>("skull");
            }
            catch (ImageNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (gameState)
            {
                case State.Playing:
                    {
                        UpdatePlaying(gameTime);
                        break;
                    }

                case State.Menu:
                    {
                        UpdateMenu(gameTime);
                        break;
                    }

                case State.Gameover:
                    {
                        UpdateGameover(gameTime);
                        break;
                    }

                default: break;
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (gameState)
            {
                case State.Playing:
                    {
                        DrawPlaying(gameTime);
                        break;
                    }

                case State.Menu:
                    {
                        sf.Draw(spriteBatch);
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        break;
                    }

                case State.Gameover:
                    {
                        spriteBatch.Draw(gameoverImage, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, "Final score - " + hud.playerScore.ToString(), new Vector2(235, 100), Color.Red);
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadAsteroids()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (asteroidList.Count < 5)
            {
                try
                {
                    asteroidList.Add(new Asteroid(Content.Load<Texture2D>("asteroid"), new Vector2(randX, randY)));
                }
                catch (ImageNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
                try
                {
                    enemyList.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));
                }
                catch (ImageNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].IsVisible)
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


        protected void UpdatePlaying(GameTime gameTime)
        {
            sf.speed = 5;

            foreach (Enemy e in enemyList)
            {
                if (e.BoundingBox.Intersects(p.BoundingBox))
                {
                    p.Health -= 40;
                    e.IsVisible = false;
                }

                //bullet collision
                for (int i = 0; i < e.BulletList.Count; i++)
                {
                    if (p.BoundingBox.Intersects(e.BulletList[i].boundingBox))
                    {
                        p.Health -= enemyBulletDamage;
                        e.BulletList[i].isVisible = false;
                    }
                }

                for (int i = 0; i < p.BulletList.Count; i++)
                {
                    if (p.BulletList[i].boundingBox.Intersects(e.BoundingBox))
                    {
                        sm.explodeSound.Play();
                        try
                        {
                            explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position.X, e.position.Y)));
                        }
                        catch (ImageNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        hud.playerScore += 20;
                        p.BulletList[i].isVisible = false;
                        e.IsVisible = false;
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
                if (a.boundingBox.Intersects(p.BoundingBox))
                {
                    p.Health -= 20;
                    a.isVisible = false;
                }

                //iterate through the bulletList and check for collision
                for (int i = 0; i < p.BulletList.Count; i++)
                {
                    if (a.boundingBox.Intersects(p.BulletList[i].boundingBox))
                    {
                        sm.explodeSound.Play();
                        try
                        {
                            explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(a.position.X, a.position.Y)));
                        }
                        catch (ImageNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        hud.playerScore += 5;
                        a.isVisible = false;
                        p.BulletList.ElementAt(i).isVisible = false;
                    }
                }

                a.Update(gameTime);
            }

            if (p.Health <= 0)
            {
                gameState = State.Gameover;
            }

            hud.Update(gameTime);
            p.Update(gameTime);
            sf.Update(gameTime);
            ManageExplosions();
            LoadAsteroids();
            LoadEnemies();
        }

        protected void UpdateMenu(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Enter))
            {
                gameState = State.Playing;
                MediaPlayer.Play(sm.bgMusic);
            }

            sf.Update(gameTime);
            sf.speed = 1;
        }

        protected void UpdateGameover(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                p.position = new Vector2(400, 900);
                enemyList.Clear();
                asteroidList.Clear();
                p.Health = 200;
                hud.playerScore = 0;
                gameState = State.Menu;
            }

            MediaPlayer.Stop();

        }

        protected void DrawPlaying(GameTime gameTime)
        {
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
        }
    }
}
