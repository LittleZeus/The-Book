using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    class Enemy : Entity
    {
        public static Random rand = new Random();

        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();
        private int timeUntilStart = 60;
        public bool IsActive { get { return timeUntilStart <= 0; } }
        public int PointValue { get; private set; }
        public int health;
        public bool isBoss;

        public Enemy(Texture2D image, Vector2 position)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
            color = Color.Transparent;
            PointValue = 1;
        }

        public static Enemy CreateSeeker(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.Seeker, position);
            enemy.AddBehaviour(enemy.FollowPlayer());
            enemy.PointValue = 2;
            enemy.health = 3;
            enemy.isBoss = false;

            return enemy;
        }
        public static Enemy CreateWanderer(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.Wanderer, position);
            enemy.AddBehaviour(enemy.FollowPlayer());
            enemy.PointValue = 2;
            enemy.health = 4;
            enemy.isBoss = false;

            return enemy;
        }
        public static Enemy CreateBoss(Vector2 position)
        {
            var enemy = new Enemy(GameRoot.Boss, position);
            enemy.AddBehaviour(enemy.FollowPlayer());
            enemy.PointValue = 2;
            enemy.health = 150;
            enemy.isBoss = true;

            return enemy;
        }

        public override void Update()
        {
            if (timeUntilStart <= 0)
                ApplyBehaviours();
            else
            {
                timeUntilStart--;
                color = Color.White * (1 - timeUntilStart / 60f);
            }

            Position += Velocity;
            Position = Vector2.Clamp(Position, GameRoot.StartWorldPoint, GameRoot.EndWorldPoint);

            Velocity *= 0.8f;
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                    behaviours.RemoveAt(i--);
            }
        }

        public void HandleCollision(Enemy other)
        {
            var d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared() + 1);
        }

        public void WasShot()
        {
            IsExpired = true;
            GameRoot.Explosion.Play(0.2f, rand.NextFloat(-0.2f, 0.2f), 0);

        }

        #region Behaviours
        IEnumerable<int> FollowPlayer(float acceleration = 1f)
        {
            while (true)
            {
                if (!PlayerShip.Instance.IsDead)
                    Velocity += (PlayerShip.Instance.Position - Position) * (acceleration / (PlayerShip.Instance.Position - Position).Length());

                if (Velocity != Vector2.Zero)
                    Orientation = Velocity.ToAngle();

                yield return 0;
            }
        }
        #endregion
    }
}
