using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButtonAnalytic_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButtonImitational_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonModel_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            
            
            if (radioButtonAnalytic.Checked)
            {
                double sp = Convert.ToDouble(StartingSpeednumericUpDown.Value);
                double rad = Task1.MakeRadians(Convert.ToDouble(GradusnumericUpDown.Value));
                double res = Convert.ToDouble(AirResistansenumericUpDown.Value);
                int pointsCount = Convert.ToInt32(GrahpPointCountNumericUpDown.Value);
                if (ResOffradioButton.Checked)
                {
                    res = 0;
                }
                var timer = new Stopwatch();
                timer.Start();
                double L = Task1.CalculateL(sp, rad);
                timer.Stop();

                double t = 0;
                double x = 0;
                chart1.Series[0].Points.Clear();

                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                while (true)
                {
                    Tuple<double, double> tuple = null;
                        tuple = Task1.CalculateXY(t, sp, rad);
                    t += 0.1;
                    //textBox1.Text += $"x = {tuple.Item1}, y = {tuple.Item2}\r\n";
                    points.Add(tuple);
                    //chart1.Series[0].Points.AddXY(tuple.Item1, tuple.Item2);
                    if (tuple.Item2 <= 0 && tuple.Item1 != 0)
                        break;
                }
                int pointsCounter = 0;
                if (points.Count < pointsCount)
                {
                    pointsCount = points.Count;
                }
                foreach (var point in points)
                {
                    pointsCounter++;
                    if (pointsCounter % (points.Count / pointsCount) == 0)
                        chart1.Series[0].Points.AddXY(point.Item1, point.Item2);
                }
                textBox1.Text += $"Аналитический метод: дальность полета - {L}; время на вычисления (т.) - {timer.ElapsedTicks}\r\n";
            }
            else
            {
                double x = 0, y = 0;
                double sp = Convert.ToDouble(StartingSpeednumericUpDown.Value);
                double rad = Task1.MakeRadians(Convert.ToDouble(GradusnumericUpDown.Value));
                double ts = Convert.ToDouble(StepnumericUpDown.Value);
                double speedOnX = Task1.SpeedOnX(sp, rad);
                double speedOnY = Task1.StartingSpeedOnY(sp, rad);
                double res = Convert.ToDouble(AirResistansenumericUpDown.Value);
                double aspeed = Convert.ToDouble(AirSpeedNumericUpDown.Value);
                int pointsCount = Convert.ToInt32(GrahpPointCountNumericUpDown.Value);
                double l = 0;
                double t = 0;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY(0,0);

                List<Tuple<double,double>> points = new List<Tuple<double, double>>();
                var timer = new Stopwatch();
                timer.Start();
                while (true)
                {
                    var tuple = Task1.CalculateXY2(x, y, speedOnX, speedOnY, ts);
                    t += ts;
                    points.Add(tuple);


                    
                    if (tuple.Item2 <= 0 && Math.Abs(tuple.Item1) > 0)
                    {
                        break;
                    }
                    x = tuple.Item1;
                    y = tuple.Item2;

                    //скорости с учетом сопротивления воздуха
                    if (ResOnradioButton.Checked)
                    {
                        speedOnY = Task1.SpeedOnYResV2(sp, t, res, rad);
                        speedOnX = Task1.SpeedOnXResV2(sp, t, res, rad, aspeed);
                    }
                    else
                    {
                        speedOnY = Task1.SpeedOnY(speedOnY, ts);
                    }

                    l = Task1.CalculateL2(x, y, speedOnX, speedOnY);
                }
                timer.Stop();

                int pointsCounter = 0;
                if (points.Count < pointsCount)
                {
                    pointsCount = points.Count;
                }
                foreach (var point in points)
                {
                    pointsCounter++;
                    if (pointsCounter % (points.Count / pointsCount) == 0)
                        chart1.Series[0].Points.AddXY(point.Item1, point.Item2);
                }
                textBox1.Text += $"Имитационный метод: дальность - {l}; время на вычисления (т.) - {timer.ElapsedTicks}\r\n";
            }
        }

        private void CalculateTask2Button_Click(object sender, EventArgs e)
        {
            double radius = Convert.ToDouble(RadiusNumericUpDown.Value);
            int intRadius = Convert.ToInt32(radius);
            double width = Convert.ToDouble(WidthNumericUpDown.Value);
            double height = Convert.ToDouble(LengthNumericUpDown.Value);

            int intWidth = Convert.ToInt32(width);
            int intHeight = Convert.ToInt32(height);

            g = splitContainer2.Panel2.CreateGraphics();
            g.Clear(Color.White);
            if (AnalyticRadioButton.Checked)
            {
                DrawSomeShit(intRadius, intWidth, intHeight);
                var timer = new Stopwatch();
                timer.Start();
                double s = Task2.CalculateS(radius, width, height);
                timer.Stop();
                Task2LogtextBox.Text += $"Площадь пересечения - {s}; время на вычисления - {timer.ElapsedTicks}\r\n";
            }
            else
            {
                double bw, bh;
                bw = radius > width / 2 ? 2 * radius : width;
                bh = radius > height / 2 ? 2 * radius : height;

                double xMax = bw / 2;
                double xMin = -bw / 2;
                double yMax = bh / 2;
                double yMin = -bh / 2;
                int pointsInArea = 0;
                int pointsAll = Convert.ToInt32(PointsCountNmericUpDown.Value);
                var timer = new Stopwatch();
                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                timer.Start();

                Random rnd = new Random();
                for (int i = 0; i < pointsAll; i++)
                {
                    double x, y;
                    x = rnd.NextDouble() * (xMax - xMin) + xMin;
                    y = rnd.NextDouble() * (yMax - yMin) + yMin;

                    if (i < Convert.ToInt32(PointGraphCountNumericUpDown.Value))
                    {
                        points.Add(new Tuple<double, double>(x, y));
                    }
                    
                    if (Task2.IsInsideCircle(x, y, radius) && Task2.IsInsideRectangle(x, y, width, height))
                    {
                        pointsInArea++;
                    }
                }
                timer.Stop();

                foreach(var point in points)
                {
                    DrawPoint(Convert.ToInt32(point.Item1), Convert.ToInt32(point.Item2));
                }
                double s = (pointsInArea * 1.0 / pointsAll) * bw * bh;
                Task2LogtextBox.Text += $"Площадь пересечения - {s}; время на вычисления (т.) - {timer.ElapsedTicks}\r\n";

                DrawBigRect(intRadius, Convert.ToInt32(bw), Convert.ToInt32(bh));

                DrawSomeShit(intRadius, intWidth, intHeight);
            }
        }

        private void DrawPoint(int x, int y)
        {
            var panel = splitContainer2.Panel2;
            Pen p = new Pen(Color.DarkViolet, 1);

            Point center = new Point(panel.Width / 2, panel.Height / 2);
            Rectangle rect = new Rectangle(center.X + x, center.Y + y, 1, 1);
            g.DrawRectangle(p, rect);
        }

        private void DrawBigRect(int intRadius, int intWidth, int intHeight)
        {

            var panel = splitContainer2.Panel2;
            Pen p = new Pen(Color.Black, 2);
            Point center = new Point(panel.Width / 2, panel.Height / 2);
            Rectangle rect = new Rectangle(center.X - intWidth / 2, center.Y - intHeight / 2, intWidth, intHeight);
            g.DrawRectangle(p, rect);
        }

        private void DrawSomeShit(int intRadius, int intWidth, int intHeight)
        {
            var panel = splitContainer2.Panel2;
            Pen p = new Pen(Color.Black, 2);
            Point p1 = new Point(panel.Width / 2, 0);
            Point p2 = new Point(panel.Width / 2, panel.Height);
            g.DrawLine(p, p1, p2);
            p1 = new Point(0, panel.Height / 2);
            p2 = new Point(panel.Width, panel.Height / 2);
            g.DrawLine(p, p1, p2);

            p.Color = Color.Red;
            Point center = new Point(panel.Width / 2, panel.Height / 2);
            g.DrawEllipse(p, new Rectangle(new Point(center.X - intRadius, center.Y - intRadius), new Size(2 * intRadius, 2 * intRadius)));


            p.Color = Color.Blue;
            Rectangle rect = new Rectangle(center.X - intWidth / 2, center.Y - intHeight / 2, intWidth, intHeight);
            g.DrawRectangle(p, rect);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
