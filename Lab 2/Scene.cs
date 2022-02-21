using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public class Scene
    {
        Graphics g = null;
        Pen p = new Pen(Color.Black, 2);
        public Scene(SplitContainer sc)
        {
            g = sc.Panel2.CreateGraphics();
            //g.Clear(Color.White);
        }

        public void Draw(Puck puck)
        {
            //g.Clear(Color.White);
            int x = Convert.ToInt32(puck.x);
            int y = Convert.ToInt32(puck.y);
            int r = Convert.ToInt32(puck.r);
            Rectangle rect = new Rectangle(x, y, r, r);
            g.DrawEllipse(p, rect);
        }

        internal void Draw(List<Segment> segments)
        {
            foreach(var seg in segments)
            {
                g.DrawLine(p, (int)seg.pointA.X, (int)seg.pointA.Y, (int)seg.pointB.X, (int)seg.pointB.Y);
            }
        }
    }
}
