using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace _2DShooter
{
    public class SoundManager
    {
        private SoundEffect playerShootSound, explodeSound;
        private Song bgMusic;

        public SoundManager()
        {
            this.PlayerShootSound = null;
            this.ExplodeSound = null;
            this.BgMusic = null;
        }

        public SoundEffect PlayerShootSound
        {
            get { return this.playerShootSound; }
            set { this.playerShootSound = value; }
        }
        public SoundEffect ExplodeSound
        {
            get { return this.explodeSound; }
            set { this.explodeSound = value; }
        }
        public Song BgMusic
        {
            get { return this.bgMusic; }
            set { this.bgMusic = value; }
        }

        public void LoadContent(ContentManager Content)
        {
            playerShootSound = Content.Load<SoundEffect>("playershoot");
            explodeSound = Content.Load<SoundEffect>("explode");
            bgMusic = Content.Load<Song>("00. 21 Illuminati");
        }

    }
}
