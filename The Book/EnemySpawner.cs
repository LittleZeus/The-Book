using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    static class EnemySpawner
    {
        static Random rand = new Random();
        public static float inverseSpawnChance = 60;
        public static float maxInverseSpawnChance = 50;
        public static bool bossSpawn = false;

        public static void Update()
        {
            if (!PlayerShip.Instance.IsDead && EntityManager.Count < 200)
            {

                if (rand.Next((int)inverseSpawnChance) == 0)
                {
                    int spawnNumber = rand.Next(2);

                    if (spawnNumber == 0)
                    {
                        EntityManager.Add(Enemy.CreateSeeker(GetSpawnPosition()));
                    }
                    if (spawnNumber == 1)
                    {
                        EntityManager.Add(Enemy.CreateWanderer(GetSpawnPosition()));
                    }
                }
            }

            /*if (Round.round == "Boss" && bossSpawn == false)
            {
                EntityManager.Add(Enemy.CreateBoss(GetSpawnPosition()));
                bossSpawn = true;
            }
            */
            // slowly increase the spawn rate as time progresses
            if (inverseSpawnChance > maxInverseSpawnChance)
                inverseSpawnChance -= 0.005f;
        }

        private static Vector2 GetSpawnPosition()
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(rand.Next((int)GameRoot.ScreenSize.X), rand.Next((int)GameRoot.ScreenSize.Y));
            }
            while (Vector2.DistanceSquared(pos, PlayerShip.Instance.Position) < 250 * 250);

            return pos;
        }

        public static void SpawnRate()
        {
            inverseSpawnChance = 60;
            maxInverseSpawnChance = 50;
        }

        public static void Reset()
        {
            inverseSpawnChance = 60;
        }
    }
}
