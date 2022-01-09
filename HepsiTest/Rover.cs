using HepsiTest.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiTest
{
    public class Rover
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Plateau Plateau { get; set; }
        public Heading Heading { get; set; }
        public string Commands { get; set; }

        public Rover(int posX, int posY, Plateau plateau, Heading heading)
        {
            PositionX = posX;
            PositionY = posY;
            Plateau = plateau;
            Heading = heading;
            Commands = "";
        }

        public void SetCommand(string commands)
        {
            Commands = commands;
        }

        public void Move()
        {
            foreach (char chars in Commands)
            {
                switch (chars)
                {
                    case 'L':
                        Heading = (Heading)Mod(((int)Heading - 1), 4);
                        break;
                    case 'R':
                        Heading = (Heading)Mod(((int)Heading + 1), 4);
                        break;
                    case 'M':
                        switch (Heading)
                        {
                            case Heading.N:
                                if (++PositionY > Plateau.MaxY)
                                    throw new RoverOutOfBoundsException();
                                break;
                            case Heading.E:
                                if (++PositionX > Plateau.MaxX)
                                    throw new RoverOutOfBoundsException();
                                break;
                            case Heading.S:
                                if (--PositionY < Plateau.MinY)
                                    throw new RoverOutOfBoundsException();
                                break;
                            case Heading.W:
                                if (--PositionX < Plateau.MinX)
                                    throw new RoverOutOfBoundsException();
                                break;
                            default:
                                throw new InvalidInputException();
                        }
                        break;
                    default:
                        throw new InvalidInputException();
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"{PositionX} {PositionY} {Heading}");
        }

        private int Mod(int h, int m)
        {
            int temp = h % m;
            return temp < 0 ? temp + m : temp;
        }
    }
}
