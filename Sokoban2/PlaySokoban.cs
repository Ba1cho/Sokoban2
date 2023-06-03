using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Coord Human;
        bool flalgs = false;
        public List<Coord> Boxes = new List<Coord>();
        public List<Coord> Targets = new List<Coord>();
        public List<Coord> Grasses = new List<Coord>();
        public List<bool> box_move = new List<bool>();
        public List<Coord> Last = new List<Coord>();

        public elements[,] ShowField()
        {
            elements[,] res = new elements[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    res[i, j] = elements.Brick;
                }
            }          

            

            foreach (var item in Grasses)
            {
                res[item.Y, item.X] = elements.Grass;
            }

            foreach (var item in Targets)
            {
                res[item.Y, item.X] = elements.Target;
            }

            foreach (var item in Boxes)
            {
                res[item.Y, item.X] = elements.Box;
            }

            res[Human.Y, Human.X] = elements.Human;

            return res;
        }

        public static PlaySokoban SampleLevel()
        {
            PlaySokoban play = new PlaySokoban();

            play.Human = new Coord(1,4);

            play.Grasses.Add(new Coord(1,2));
            play.Grasses.Add(new Coord(1, 3));
            play.Grasses.Add(new Coord(1, 4));
            play.Grasses.Add(new Coord(2, 2));
            play.Grasses.Add(new Coord(2, 4));
            play.Grasses.Add(new Coord(2, 5));
            play.Grasses.Add(new Coord(3, 1));
            play.Grasses.Add(new Coord(3, 2));
            play.Grasses.Add(new Coord(3, 4));
            play.Grasses.Add(new Coord(3, 5));
            play.Grasses.Add(new Coord(4, 1));
            play.Grasses.Add(new Coord(4, 2));
            play.Grasses.Add(new Coord(3, 1));

            play.Boxes.Add(new Coord(2, 4));

            play.Targets.Add(new Coord(3, 3));


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
            foreach(var target in Targets){
                foreach(var box in Boxes){
                    if (box.Y != target.Y || box.X != target.X)
                    {
                        return false;
                    }
                }
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
                    kuda.X--;
                    break;
                case direction.down:
                    kuda.X++;
                    break;
                case direction.left:
                    kuda.Y--;
                    break;
                case direction.right:
                    kuda.Y++;
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
                    if(kuda.X == item.X && kuda.Y == item.Y){
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
