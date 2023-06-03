using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban2
{
    public partial class Form1 : Form
    {
        PlaySokoban play = PlaySokoban.SampleLevel();

        public Form1()
        {
            InitializeComponent();
        }

        

        int size = 30;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            pictureBox1.Height = size * 8+1;
            pictureBox1.Width = size * 8+1;

            elements[,] cur = play.ShowField();

            //нарисовать элементы
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle r = new Rectangle(i * size, j * size, size, size);
                    switch (cur[i,j])
                    {
                        case elements.Box:
                            g.DrawImage(imageList1.Images[1], r);
                            break;
                        case elements.Target:
                            g.DrawImage(imageList1.Images[3], r);
                            break;
                        case elements.Brick:
                            g.DrawImage(imageList1.Images[2], r);
                            break;
                        case elements.Grass:
                            g.FillRectangle(Brushes.Green, r);
                            break;
                        case elements.Human:
                            g.DrawImage(imageList1.Images[0], r);
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                play.Tern(direction.up);
            }
            if (e.KeyCode == Keys.Left)
            {
                play.Tern(direction.left);
            }
            if (e.KeyCode == Keys.Down)
            {
                play.Tern(direction.down);
            }
            if (e.KeyCode == Keys.Right)
            {
                play.Tern(direction.right);
            }

            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
