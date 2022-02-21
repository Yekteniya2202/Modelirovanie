using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        Scene s = null;
        Engine eng = null;
        List<Segment> segments = new List<Segment>();
        public Form1()
        {
            InitializeComponent();
            segments.Add(new Segment(new Vector2(200, 0), new Vector2(400, 100)));
        }

        private void buttonStart_ClickAsync(object sender, EventArgs e)
        {

            s = new Scene(splitContainer1);
            decimal alpha = numericUpDownAlpha.Value;
            decimal speed = numericUpDownSpeed.Value;
            double rad = decimal.ToDouble(alpha) * (Math.PI / 180.0);
            double dSpeed = decimal.ToDouble(speed);

            Puck p = new Puck(0, 0, 20, dSpeed, rad, rad);

            eng = new Engine(p);

            SceneDrawing(p);
        }

        void SceneDrawing(Puck p)
        {
            double timeStep = 0;

            while (p.Update(timeStep, 0.1, segments))
            {
                s.Draw(p);
                s.Draw(segments);
                //Thread.Sleep(1);
                timeStep += 0.01;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
