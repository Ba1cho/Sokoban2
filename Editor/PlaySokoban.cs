using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Sokoban2
{
    enum elements
    {
        Box,
        Target,
        Brick,
        Grass,
        Human
    }

    enum direction
    {
        up,
        down,
        left,
        right,
        z
    }

    class PlaySokoban
    {

        int x = -1;
        public int W = 12;
        public int H = 8;

        int counter = 0;
        public Coord Human;
        bool flalgs = false;
        public List<Coord> Boxes = new List<Coord>();
        public List<Coord> Grasses = new List<Coord>();
        public List<Coord> Targets = new List<Coord>();
        public List<Coord> Bricks = new List<Coord>();
        public List<bool> box_move = new List<bool>();
        public List<Coord> Last = new List<Coord>();

        public elements[,] ShowField()
        {
            using (StreamReader fs = new StreamReader("..\\..\\..\\Editor\\tetx.txt"))
            {
                for (int i = 0; i < 2; i++)
                {
                    string temp = fs.ReadLine();
                    // Если достигнут конец файла, прерываем считывание.
                    if (i == 2) break;
                    if (temp == "m") continue;
                    if (i == 0)
                    {
                        W = int.Parse(temp);
                    }
                    else
                        H = int.Parse(temp);
                }
            }
            elements[,] res = new elements[W + 1, H + 1];
            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {

                    res[i, j] = elements.Grass;
                }
            }



            foreach (var item in Bricks)
            {
                res[item.X, item.Y] = elements.Brick;
            }

            foreach (var item in Targets)
            {
                res[item.X, item.Y] = elements.Target;
            }

            foreach (var item in Boxes)
            {
                res[item.X, item.Y] = elements.Box;
            }

            res[Human.X, Human.Y] = elements.Human;

            return res;
        }

        public static PlaySokoban SampleLevel()
        {
            PlaySokoban play = new PlaySokoban();
            int pos_x = 0;
            int pos_y = 0;
            int WW = 0;
            int HH = 0;
            string position = "";
            using (StreamReader fs = new StreamReader("..\\..\\..\\Editor\\tetx.txt"))
            {
                while (true)
                {

                    // Читаем строку из файла во временную переменную.
                    string temp = fs.ReadLine();
                    // Если достигнут конец файла, прерываем считывание.
                    if (temp == null) break;
                    // Пишем считанную строку в итоговую переменную.
                    if (temp == "h" || temp == "b" || temp == "t" || temp == "/" || temp == "m")
                    {
                        position = temp;
                    }
                    else if (position == "m")
                    {
                        WW = int.Parse(temp);
                        HH = int.Parse(fs.ReadLine());
                        play.Human = new Coord(pos_x, pos_y);
                    }
                    else if (position == "h")
                    {
                        pos_x = int.Parse(temp);
                        pos_y = int.Parse(fs.ReadLine());
                        play.Human = new Coord(pos_x, pos_y);
                    }
                    else if (position == "b")
                    {
                        pos_x = int.Parse(temp);
                        pos_y = int.Parse(fs.ReadLine());
                        play.Boxes.Add(new Coord(pos_x, pos_y));
                    }
                    else if (position == "t")
                    {
                        pos_x = int.Parse(temp);
                        pos_y = int.Parse(fs.ReadLine());
                        play.Targets.Add(new Coord(pos_x, pos_y));
                    }
                    else if (position == "/")
                    {
                        pos_x = int.Parse(temp);
                        pos_y = int.Parse(fs.ReadLine());
                        play.Bricks.Add(new Coord(pos_x, pos_y));
                        play.Last.Add(new Coord(pos_x, pos_y));
                    }

                }
            }
            for (int i = 0; i < WW; i++)
            {
                for (int j = 0; j < HH; j++)
                {
                    bool flag = true;
                    foreach (Coord item in play.Last)
                    {
                        if (item.X == i && item.Y == j) flag = false;
                    }
                    if (flag)
                    {
                        play.Grasses.Add(new Coord(i, j));
                    }
                }
            }
            return play;

        }

        //Stack<>
        private Boolean CanTern(Coord kuda)
        {
            foreach (var item in Grasses)
            {
                if (item.X == kuda.X && item.Y == kuda.Y) return true;
            }
            foreach (var item in Targets)
            {
                if (item.X == kuda.X && item.Y == kuda.Y) return true;
            }
            return false;
        }

        public Boolean CanWin()
        {
            bool flag = false;
            foreach (var target in Targets)
            {
                foreach (var box in Boxes)
                {
                    if (box.Y != target.Y || box.X != target.X)
                    {
                        continue;
                    }
                    else
                        flag = true;

                }
                if (flag == false) return false;
                flag = false;
            }
            return true;
        }

        public void Tern(direction direct)
        {
            Coord kuda = new Coord(Human.X, Human.Y);
            bool MayTern = true;
            switch (direct)
            {
                case direction.up:
                    kuda.Y--;
                    break;
                case direction.down:
                    kuda.Y++;
                    break;
                case direction.left:
                    kuda.X--;
                    break;
                case direction.right:
                    kuda.X++;
                    break;
                default:
                    break;
            }

            if (CanTern(kuda))
            {
                Last.Add(new Coord(Human.X, Human.Y));
                x++;
                box_move.Add(false);
                foreach (var item in Boxes)
                {
                    if (kuda.X == item.X && kuda.Y == item.Y)
                    {
                        box_move[x] = true;
                        if (Human.X < kuda.X)
                        {
                            item.X++;
                            if (!CanTern(item))
                            {
                                MayTern = false;
                                item.X--;
                            }
                            break;
                        }
                        else if (Human.X > kuda.X)
                        {
                            item.X--;
                            if (!CanTern(item))
                            {
                                MayTern = false;
                                item.X++;
                            }
                            break;
                        }
                        else if (Human.Y < kuda.Y)
                        {
                            item.Y++;
                            if (!CanTern(item))
                            {
                                MayTern = false;
                                item.Y--;
                            }
                            break;

                        }
                        else if (Human.Y > kuda.Y)
                        {
                            item.Y--;
                            if (!CanTern(item))
                            {
                                MayTern = false;
                                item.Y++;
                            }
                            break;
                        }
                    }
                }
                if (MayTern)
                {
                    Human.X = kuda.X;
                    Human.Y = kuda.Y;

                }
                if (CanWin())
                {
                    Form2 newForm = new Form2();
                    newForm.Show();
                }
            }
        }
    }

    public class Coord
    {
        public int X;
        public int Y;
        public Coord() { }
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
