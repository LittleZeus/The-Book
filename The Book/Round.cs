using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    class Round
    {
        public static bool roundStart = true;
        public static bool gameStart = true;
        public static int rnd = 1;
        public static int roundTime = 30;
        public static String round = "1";
        public static String count = "3";
        public static float inverseSpawnChance = 60f;
        public static float maxInverseSpawnChance = 50f;

        public static void Update()
        {
            count = "3";
            if (Timer.currentRound <= 1)
            {
                roundStart = true;
            }
            else
            {
                roundStart = false;
            }
            if (Timer.current == 2)
            {
                count = "2";
            }
            if (Timer.current == 3)
            {
                count = "1";
            }
            if (Timer.current == 4)
            {
                count = "Go";
            }
        }

        public static void Next()
        {
            Timer.ResetRound();
            switch (rnd)
            {
                case 1:
                    rnd = 2;
                    round = "2";
                    roundTime = 30;
                    inverseSpawnChance = 50f;
                    maxInverseSpawnChance = 40f;
                    break;
                case 2:
                    rnd = 3;
                    round = "Boss";
                    roundTime = -1;
                    inverseSpawnChance = 40f;
                    maxInverseSpawnChance = 40f;
                    break;
                case 3:
                    rnd = 4;
                    round = "Infinite";
                    roundTime = -1;
                    inverseSpawnChance = 40f;
                    maxInverseSpawnChance = 20f;
                    break;
            }
            EnemySpawner.SpawnRate();
        }
    }
}
