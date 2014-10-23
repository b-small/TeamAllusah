using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DShooter
{
    public struct HighScoreData
    {
        public string[] PlayerName;
        public int[] Score;
   
        public int Count;

        public HighScoreData(int count)
        {
            PlayerName = new string[count];
            Score = new int[count];

            Count = count;
        }
    }
}
