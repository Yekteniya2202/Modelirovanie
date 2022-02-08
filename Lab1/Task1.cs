using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    static public class Task1
    {
        static double g = 9.8;
        public static double MakeRadians(double angle)
        {
            return angle * (Math.PI / 180.0);
        }

        public static Tuple<double, double> CalculateXY(double t, double startingSpeed, double radians)
        {
            double x = startingSpeed * t * Math.Cos(radians);
            double y = startingSpeed * t * Math.Sin(radians) - ((g * t * t) / 2.0);

            return new Tuple<double, double>(x, y);
        }

        public static double CalculateL(double startingSpeed, double radians)
        {
            return (startingSpeed * startingSpeed * Math.Sin(2 * radians)) / g;
        }


        public static double SpeedOnX(double startingSpeed, double radians)
        {
            return startingSpeed * Math.Cos(radians);
        }
        public static double SpeedOnX(double sprev, double ts, double k, double m)
        {
            return sprev - k * ts  * sprev / m;
        }
        public static double SpeedOnY(double previousSpeedOnY, double timeStep)
        {
            return previousSpeedOnY - g * timeStep;
        }

        public static double SpeedOnY(double sprev, double ts, double k, double m)
        {
            return sprev - g * ts - k * ts * sprev;
        }
        public static double SpeedOnXResV2(double sp, double time, double k, double rad, double aspeed)
        {
            return (sp * Math.Cos(rad) + aspeed) * Math.Exp(-k * g * time);
        }
        public static double SpeedOnYResV2(double sp, double time, double k, double rad)
        {
            return (sp * Math.Sin(rad) + 1 / k) * Math.Exp(-k * g * time) - 1 / k;
        }
        public static double StartingSpeedOnY(double startingspeed, double rad)
        {
            return startingspeed * Math.Sin(rad);
        }

        public static Tuple<double, double> CalculateXY2(double xprev, double yprev, double speedOnX, double speedOnY, double timeStep)
        {
            return new Tuple<double, double>(xprev + speedOnX * timeStep, yprev + speedOnY * timeStep);
        }

        public static Tuple<double, double> CalculateXYAirResistanse(double sp, double alpha, double k, double time, double x)
        {
            //double x = (sp * Math.Cos(alpha)) / (k * g) * (1 - Math.Pow(Math.E, -1 * k * g * time));
            //double y = (1 / (k * g)) * (sp * Math.Sin(alpha) + 1 / k) * (1 - Math.Pow(Math.E, -1 * k * g * time)) - (1 / k);
            double y = x * Math.Tan(alpha) - ((g * x * x)/(2 * sp * sp * Math.Cos(alpha) * Math.Cos(alpha))) - ((k * g * g * x * x * x) / (3 * sp * sp * sp * Math.Cos(alpha) * Math.Cos(alpha) * Math.Cos(alpha)));
            return new Tuple<double, double>(x, y);
        }

        public static double CalculateL2(double x, double y, double speedOnX, double speedOnY)
        {
            return x  + (speedOnX * y)/(speedOnY);
        }
    }
}
