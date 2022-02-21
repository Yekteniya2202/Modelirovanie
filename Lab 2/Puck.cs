using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public class Puck
    {
        //центр 
        public double x, y;

        public readonly double r;
        
        //проекции по теореме пифагора определяют направление движения
        double V, Vx, Vy;

        double a, ax, ay;

        double alphaX, alphaY;

        //Трение
        double k;
        Direction dirX = Direction.LEFT;
        Direction dirY = Direction.DOWN;
        //Начальный момент времени
        public Puck(double x, double y, double r, double v, double alphaX, double alphaY)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            V = v;
            this.alphaX = alphaX;
            this.alphaY = Math.PI / 2 - alphaX;
            a = ax = ay = 0;
        }

        
        public bool Update(double timeStep, double k, List<Segment> segments)
        {

            foreach(var seg in segments)
            {
                CheckCollision(seg);
            }
            a = -k;
            V += a * timeStep;
            x += V * Math.Cos(alphaX) * timeStep;
            y += V * Math.Cos(alphaY) * timeStep;

            return V <= 0 ? false : true; 
        }

        private void CheckCollision(Segment seg)
        {

            Vector2 ABn = Vector2.Normalize(seg.vec);
            Vector2 AC = seg.pointA - new Vector2((float)x, (float)y);
            var t = Vector2.Dot(ABn, AC);
            var D = t * ABn;
            float l = D.Length();
            if (l <= r)
            {
                V = 0;
            }
        }
    }
}
