using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{

    public enum SokobanElement
    {
        Void,
        Brick,
        Box,
        Target,
        Human,
        Grass
    }
   
    public class SokobanLevel
    {
        public int Height = 10;
        public int Width = 20;
        
        public Coord Human = new Coord(0, 0);
        public List<Coord> Boxes = new List<Coord>();
        public List<Coord> Grasses = new List<Coord>();
        public List<Coord> Bricks = new List<Coord>();
        public List<Coord> Targets = new List<Coord>();


        public SokobanElement[,] GetField()
        {
            SokobanElement[,] f = new SokobanElement[Width, Height];
            
            foreach (var item in Bricks) f[item.X, item.Y] = SokobanElement.Brick;
            foreach (var item in Grasses) f[item.X, item.Y] = SokobanElement.Grass;
            foreach (var item in Targets) f[item.X, item.Y] = SokobanElement.Target;
            foreach (var item in Boxes) f[item.X, item.Y] = SokobanElement.Box;

            f[Human.X, Human.Y] = SokobanElement.Human;

            return f;
        }

        public SokobanLevel(int h, int w) 
        {
            Height = h;
            Width = w;

            Human = new Coord(w/2,h/2);
        }
        
        public SokobanLevel() {


        }
    }

    public class Coord
    {
        public int X;
        public int Y;
        public Coord()
        { }

        public override bool Equals(object obj)
        {
            return ((Coord)obj).X == X && ((Coord)obj).Y == Y;
        }

        public Coord(int x, int y)
        { X = x; Y = y; }
    }

}
