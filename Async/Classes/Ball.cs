using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Async.Classes
{
    public class Ball
    {
        public Ball()
        {
            Radius = 1.0f;
            Mass = 1.0f;
        }

        public Ball(Vector2 x, float radius, float mass)
        {
            X = x;
            Radius = radius;
            Mass = mass;
        }

        public Ball(
            Vector2 x, 
            Vector2 dx, 
            float radius, 
            float mass
        ) : this(x, radius, mass)
        {
            Dx = dx;
        }

        public Ball(
            Vector2 x, 
            Vector2 dx,
            Vector2 a,
            float radius, 
            float mass
        ) : this(x, dx, radius, mass)
        {
            A = a;
        }

        public Vector2 X { get; set; }
        public Vector2 Dx { get; set; }
        public Vector2 A { get; set; }
        public float Radius { get; set; }
        public float Mass { get; set; }
    }
}