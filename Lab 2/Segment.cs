using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace Lab_2
{
    public class Segment
    {
        public Vector2 pointA;
        public Vector2 pointB;
        public Vector2 vec;
        public Segment(Vector2 pointA, Vector2 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            vec = pointB - pointA;
        }
    }
}
