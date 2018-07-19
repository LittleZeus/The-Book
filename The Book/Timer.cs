using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    class Timer
    {

        static Stopwatch stopwatchRound = new Stopwatch();
        static Stopwatch stopwatch = new Stopwatch();
        public static long current;
        public static long currentRound;
        public static long currentReset;

        public static void Update()
        {
            if (stopwatchRound.IsRunning == false)
            {
                stopwatchRound.Start();
            }
            currentRound = stopwatchRound.ElapsedMilliseconds;
            currentRound = currentRound / 1000;

        }
        public static void Time()
        {
            if (stopwatch.IsRunning == false)
            {
                stopwatch.Start();
            }
            current = stopwatch.ElapsedMilliseconds;
            current = current / 1000;

            if (current == currentReset + 3 && GameRoot.pause == true)
            {
                GameRoot.pause = false;
            }
            if (currentRound >= Round.roundTime && Round.roundTime > 0)
            {
                Round.Next();
                EnemySpawner.Reset();
                //GameRoot.pause = true;
            }
        }
        public static void ResetRound()
        {
            stopwatchRound.Reset();
            currentRound = 0;
            currentReset = current;
        }
        public static void Reset()
        {
            stopwatch.Restart();
            current = 0;
            ResetRound();
        }
    }
}
