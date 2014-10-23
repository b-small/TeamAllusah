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
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace _2DShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private int enemyBulletDamage;
        private Texture2D menuImage;
        private Texture2D gameoverImage;

        List<IExplosible> asteroidList = new List<IExplosible>();
        List<IExplosible> enemyList = new List<IExplosible>();
        List<Explosion> explosionList = new List<Explosion>();

        Player p = new Player();
        Starfield sf = new Starfield();
        HUD hud = new HUD();
        SoundManager sm = new SoundManager();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        string currLastScoreToSave = "";

        State gameState = State.Menu;
        MouseState previousMouseState;

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
            this.EnemyBulletDamage = 10;
            this.MenuImage = null;
            this.GameoverImage = null;
        }

        public int EnemyBulletDamage
        {
            get { return this.enemyBulletDamage; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Enemy bullet damage can not be negative");
                }
                this.enemyBulletDamage = value;
            }
        }
        public Texture2D MenuImage
        {
            get { return this.menuImage; }
            set
            {
                this.menuImage = value;
            }
        }

        public Texture2D GameoverImage
        {
            get { return this.gameoverImage; }
            set { this.gameoverImage = value; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            previousMouseState = Mouse.GetState();
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
                this.MenuImage = Content.Load<Texture2D>("MenuImage");
                this.GameoverImage = Content.Load<Texture2D>("skull");
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
                        spriteBatch.Draw(this.MenuImage, new Vector2(275, 250), Color.White);
                        break;
                    }

                case State.Gameover:
                    {
                        spriteBatch.Draw(this.GameoverImage, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.PlayerScoreFont, "  G A M E   O V E R\nF I N A L   S C O R E - " + hud.PlayerScore.ToString(), new Vector2(235, 40), Color.Red);
                        currLastScoreToSave = hud.PlayerScore.ToString();
                        string highScores = LoadHighScores();
                        spriteBatch.DrawString(hud.PlayerScoreFont, highScores, new Vector2(600, 600), Color.Red);
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
                if (!asteroidList[i].IsVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadEnemies()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 550);

            if (enemyList.Count() < 3)
            {
                try
                {
                    enemyList.Add(new BlackEnemy(Content.Load<Texture2D>("shipRed"), new Vector2(randX, randY), Content.Load<Texture2D>("enemyLaser")));
                    enemyList.Add(new WhiteEnemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX + 200, randY + 15), Content.Load<Texture2D>("enemyLaser")));
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
                if (!explosionList[i].IsVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }

        void ManageCollision(List<IExplosible> explosible, int healthReducer, GameTime gameTime)
        {
            foreach (IExplosible e in explosible)
            {
                if (e.BoundingBox.Intersects(p.BoundingBox))
                {
                    p.Health -= healthReducer;
                    e.IsVisible = false;
                }

                for (int i = 0; i < p.BulletList.Count; i++)
                {
                    if (p.BulletList[i].BoundingBox.Intersects(e.BoundingBox))
                    {
                        sm.ExplodeSound.Play();
                        try
                        {
                            explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position2.X + 50, e.position2.Y + 100)));
                        }
                        catch (ImageNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        hud.PlayerScore += 20;
                        p.BulletList[i].IsVisible = false;
                        e.IsVisible = false;
                    }
                }

                //bullet collision
                for (int j = 0; j < e.BulletList.Count; j++)
                {
                    if (e.BulletList[j].BoundingBox.Intersects(p.BoundingBox))
                    {
                        sm.ExplodeSound.Play();
                        p.Health -= enemyBulletDamage;
                        e.BulletList[j].IsVisible = false;
                    }

                }
                e.Update(gameTime);
            }
        }
        protected void UpdatePlaying(GameTime gameTime)
        {
            sf.Speed = 5;
            ManageCollision(enemyList, 40, gameTime);
            ManageCollision(asteroidList, 20, gameTime);

            foreach (Explosion ex in explosionList)
            {
                ex.Update(gameTime);
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

            if (keyState.IsKeyDown(Keys.Enter) || previousMouseState.LeftButton == ButtonState.Released
            && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                gameState = State.Playing;
                MediaPlayer.Play(sm.BgMusic);
            }

            previousMouseState = Mouse.GetState();
            sf.Update(gameTime);
            sf.Speed = 1;
        }

        protected void UpdateGameover(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape) || keyState.IsKeyDown(Keys.Enter))
            {
                p.position = new Vector2(400, 900);
                enemyList.Clear();
                asteroidList.Clear();
                p.Health = 200;
                hud.PlayerScore = 0;
                gameState = State.Menu;
                SaveHighScoreData();
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

        public void SaveHighScoreData()
        {
            ManageHighScores();
            string dataTillNow = File.ReadAllText("highscores.txt");
            string dataToSave = dataTillNow + String.Format("{0}", currLastScoreToSave) + Environment.NewLine;
            File.WriteAllText("highscores.txt", dataToSave);
        }

        public string LoadHighScores()
        {
            string result = "";
            var top5HighScore = File.ReadLines("highscores.txt")
    .Select(line => Convert.ToInt32(line))
    .OrderByDescending(score => score)
    .Take(5)
    .Select((score, index) => string.Format("{0}. {1}pts\n", index + 1, score));

            result = String.Join("", top5HighScore);
            return result;
        }

        public void ManageHighScores()
        {
            var top5HighScore = File.ReadLines("highscores.txt")
    .Select(line => Convert.ToInt32(line))
    .OrderByDescending(score => score)
    .Take(5)
    .Select((score) => string.Format("{0}{1}", score, Environment.NewLine));

            string dataToSave = String.Join("", top5HighScore);
            File.WriteAllText("highscores.txt", dataToSave);//bla
        }

    }
}
