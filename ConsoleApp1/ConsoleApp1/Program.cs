using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Coordinate
    {
        public int X { get; set; }    
        public int Y { get; set; }
    }

    public class Puck 
    {
        public static int OutOfMapIndex = -1;

        public int ID { get; set; }

        // owner name
        public string Owner { get; set; }
        // init false
        public bool InMill { get; set; } = false;
        // false ha kiütötték - init = true
        public bool InGame { get; set; } = true;
        // init true
        public bool Standby { get; set; } = true;

        public int MapIndex { get; set; } = OutOfMapIndex;

        public override string ToString()
        {
            return $"ID: {ID}, Owner: {Owner}, InMill: {InMill}, InGame: {InGame}, Standby: {Standby}, MapIndex: {MapIndex}";
        }
    }

    // felkerülö metod
    /*
     kordinátát kap
     standby = false
     */

    internal class Program
    {
        public static Puck[] pucks = new Puck[18];


        static string Player1Name = "Sári"; // a játékos neve
        static string Player2Name = "A Gép";

        static void Main(string[] args)
        {
            // nevek bekerese

            int id = 0;
            for (int i = 0; i < 9; i++)
            {
                pucks[id] = new Puck() { ID = id++, Owner = Player1Name };
                pucks[id] = new Puck() { ID = id++, Owner = Player2Name };
            }


            SetFieldValue(3, "Sári");
            SetFieldValue(0, "A Gép");

            // DrawLine(n3, "right" );
            DrawLayer(new Coordinate() { Y = 0, X = 5 }, 0);
            Console.ReadKey();
        }

        public static char GetFieldValue(int mapIndex)
        {
            foreach (var puck in pucks)
            {
                if (puck.MapIndex == mapIndex)
                {
                    return Char.ToUpper(puck.Owner[0]);
                }
            }

            return '0';
        }

        public static void SetFieldValue(int mapIndex, string Pname)
        {
            for (int i = 0; i < pucks.Length; i++)
            {
                if (pucks[i].Owner == Pname && pucks[i].MapIndex == Puck.OutOfMapIndex)
                {
                    pucks[i].MapIndex = mapIndex;
                    break;
                }
            }
        }

        public static void DrawTable()
        {

        }

        // right, left, up, down
        public static void DrawLine(Coordinate pos, int lineLength, string direction)
        {
            bool horizontal = direction == "left" || direction == "right";

            int directionValue = 0;

            switch (direction)
            {
                case "left": directionValue = -1; break;
                case "right": directionValue = 1; break;
                case "up": directionValue = -1; break;
                case "down": directionValue = 1; break;

                default:
                    break;
            }

            Console.SetCursorPosition(pos.X, pos.Y);

            for (int i = 0; i < lineLength; i++)
            {
                if (horizontal)
                {
                    Console.Write('-');
                    pos.X += directionValue;
                    Console.SetCursorPosition(pos.X, pos.Y);
                }
                else
                {
                    Console.Write("|");
                    pos.Y += directionValue;
                    Console.SetCursorPosition(pos.X, pos.Y);
                }
            }
        }

        // depth -> speckó szabály szerint
        // TODO: részletek leírása
        // depth: legkülső-> 0   és legbelső -> 2
        public static void DrawLayer(Coordinate pos, int depth)
        {
            int puckMapIndex = 0;
            int lineLength = 5;

            if (depth == 1) 
            {
                puckMapIndex = 8;
                lineLength = 3;
            } else if (depth == 2)
            {
                puckMapIndex = 16;
                lineLength = 1;
            }

            Console.SetCursorPosition(pos.X, pos.Y);
           

            Console.Write(GetFieldValue(puckMapIndex++));
            pos.X += 1;
            DrawLine(pos, lineLength, "right");
            Console.Write(GetFieldValue(puckMapIndex++));
            pos.X += 1;
            DrawLine(pos, lineLength, "right");
            Console.Write(GetFieldValue(puckMapIndex++));

            pos.Y += 1;
            DrawLine(pos, lineLength, "down");
            Console.Write(GetFieldValue(puckMapIndex++));
            pos.Y += 1;
            DrawLine(pos, lineLength, "down");
            Console.Write(GetFieldValue(puckMapIndex++));

            pos.X -= 1;
            DrawLine(pos, lineLength, "left");
            Console.Write(GetFieldValue(puckMapIndex++));
            pos.X -= 1;
            DrawLine(pos, lineLength, "left");
            Console.Write(GetFieldValue(puckMapIndex++));

            pos.Y -= 1;
            DrawLine(pos, lineLength, "up");
            Console.Write(GetFieldValue(puckMapIndex++));
            pos.Y -= 1;
            DrawLine(pos, lineLength, "up");

        }
    }
}
