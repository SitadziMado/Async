using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Async.Classes
{
    public class World
    {
        public void Add(Ball ball)
        {
            mBalls.Add(ball);
        }

        public void Tick(float elapsed)
        {
            // Довольно тормозный алгоритм, следует добавить сетку
            foreach (var a in mBalls)
                foreach (var b in mBalls)
                    Collide(a, b);

            foreach (var ball in mBalls)
            {
                ball.X += ball.Dx * elapsed;
                ball.Dx += ball.A * elapsed;
                ball.A = mGravity;
            }
        }

        private void Collide(Ball lhs, Ball rhs)
        {
            float xEntry, yEntry;
            float xExit, yExit;

            xEntry = rhs.X.X - lhs.X.X - rhs.Radius - lhs.Radius;
            yEntry = rhs.X.Y - lhs.X.Y - rhs.Radius - lhs.Radius;
            xExit = rhs.X.X - lhs.X.X + 2.0f * rhs.Radius + lhs.Radius;
            yExit = rhs.X.Y - lhs.X.Y + 2.0f * rhs.Radius + lhs.Radius;

            float xEntryTime, yEntryTime;
            float xExitTime, yExitTime;

            if (xEntry == 0.0f)
            {
                xEntryTime = float.NegativeInfinity;
                xExitTime = float.PositiveInfinity;
            }
            else
            {
                xEntryTime = xEntry / lhs.Dx.X;
                xExitTime = xExit / lhs.Dx.X;
            }

            if (yEntry == 0.0f)
            {
                yEntryTime = float.NegativeInfinity;
                yExitTime = float.PositiveInfinity;
            }
            else
            {
                yEntryTime = yEntry / lhs.Dx.Y;
                yExitTime = yExit / lhs.Dx.Y;
            }

            float entryTime = Math.Max(xEntryTime, yEntryTime);
            float exitTime = Math.Max(xExitTime, yExitTime);

            if (entryTime > exitTime ||
                xEntryTime < 0.0f && yEntryTime < 0.0f ||
                xEntryTime > 1.0f ||
                yEntryTime > 1.0f)
            {
                // Нет коллизии
            }
            else
            {
                // Обрабатываем коллизию как абсолютно упругий удар
                var masses = lhs.Mass + rhs.Mass;

                var u = ((lhs.Mass - rhs.Mass) * lhs.Dx + 2 * rhs.Mass * rhs.Dx) / (masses);
                var v = ((rhs.Mass - lhs.Mass) * rhs.Dx + 2 * lhs.Mass * lhs.Dx) / (masses);

                lhs.Dx = u;
                rhs.Dx = v;
            }
        }

        public IEnumerable<Ball> Balls => from v in mBalls select v;

        private readonly Vector2 mGravity = Vector2.Zero;

        private List<Ball> mBalls = new List<Ball>();
    }
}
