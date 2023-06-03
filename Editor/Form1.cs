using Sokoban2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Editor
{
    public partial class Form1 : Form
    {

        SokobanLevel current = new SokobanLevel();

        public Form1()
        {
            InitializeComponent();

            current = new SokobanLevel((int)numericUpDown1.Value, (int)numericUpDown2.Value);
        }

        int aW;
        int aH;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            aW = (pictureBox1.Width - 1) / current.Width;
            aH = (pictureBox1.Height - 1) / current.Height;

            int rows = current.Height;
            int columns = current.Width;


            SokobanElement[,] field = current.GetField();

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Rectangle r = new Rectangle(i * aW, j * aH, aW, aH);
                    g.DrawRectangle(Pens.Black, r);

                    switch (field[i, j])
                    {
                        case SokobanElement.Void:
                            break;
                        case SokobanElement.Brick:
                            g.DrawImage(imageList1.Images[2], r);
                            break;
                        case SokobanElement.Box:
                            g.DrawImage(imageList1.Images[1], r);
                            break;
                        case SokobanElement.Target:
                            g.DrawImage(imageList1.Images[3], r);
                            break;
                        case SokobanElement.Human:
                            g.DrawImage(imageList1.Images[0], r);
                            break;
                        case SokobanElement.Grass:
                            break;
                        default:
                            break;
                    }

                }
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            current = new SokobanLevel((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            pictureBox1.Invalidate();
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / aW;
            int y = e.Y / aH;

            Coord cur = new Coord(x, y);
            if (radioButton1.Checked)
            {
                current.Human = cur;
            }

            if (radioButton2.Checked)
            {
                current.Targets.Add(cur);
            }
            if (radioButton3.Checked)
            {
                current.Boxes.Add(cur);
            }
            if (radioButton4.Checked)
            {
                current.Bricks.Add(cur);
            }
            if (radioButton5.Checked)
            {
                List<Coord> tmp = new List<Coord>();

                current.Bricks.Remove(cur);
                current.Boxes.Remove(cur);
                current.Targets.Remove(cur);

            }

            pictureBox1.Invalidate();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("..\\..\\tetx.txt");
            writer.WriteLine('m');
            writer.WriteLine(current.Width);
            writer.WriteLine(current.Height);
            writer.WriteLine('h');
            writer.WriteLine(current.Human.X);
            writer.WriteLine(current.Human.Y);
            writer.WriteLine('b');
            foreach (Coord i in current.Boxes)
            {
                writer.WriteLine(i.X);
                writer.WriteLine(i.Y);

            }
            writer.WriteLine('t');
            foreach (Coord i in current.Targets)
            {
                writer.WriteLine(i.X);
                writer.WriteLine(i.Y);

            }
            writer.WriteLine('/');
            foreach (Coord i in current.Bricks)
            {
                writer.WriteLine(i.X);
                writer.WriteLine(i.Y);

            }
            writer.Close();
            /*
            Form3 newForm = new Form3();
            newForm.Show();
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }
    }
}
