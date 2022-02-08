using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    
    static class Task2
    {
        public static bool IsInsideCircle(double x, double y, double radius)
        {
            return x * x + y * y < radius * radius;
        }

        public static bool IsInsideRectangle(double x, double y, double width, double heigth)
        {
            return x <= width / 2 && x >= -width / 2 && y <= heigth / 2 && y >= -heigth / 2; ;
        }
        public static double CalculateS(double r, double width, double height)
        {
            if (2 * r <= width && 2 * r <= height)
            {
                return Math.PI * r * r;
            }
            if (Math.Sqrt(width*width + height*height) <= 2 * r)
            {
                return width * height;
            }
            double half = 0;
            if (width > height)
                half = height / 2;
            else
                half = width / 2;
            double S1 = 0.5 * (Math.Sqrt(r*r - half * half) * half);
            double AB = Math.Sqrt(r * r - half * half);
            double S2 = 0.5 * Math.Asin(half / r) * r * r;
            return 4 * S1 + 4 * S2;
        }
    }
}
