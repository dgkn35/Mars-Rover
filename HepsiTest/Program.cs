using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HepsiTest.Models.Exceptions;

namespace HepsiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string maxCoords = "";
            Match m;

            do
            {
                Console.WriteLine("Enter plateau top right coordinates(input format: x y)");
                maxCoords = Console.ReadLine();
                Regex regex = new Regex(@"[0-9]\s[0-9]");
                m = regex.Match(maxCoords);
                if (!m.Success)
                    Console.WriteLine("Invalid input");
            } while (!m.Success);

            string[] plateauCoords = maxCoords.Split(" ");

            Plateau plateau = new Plateau(Convert.ToInt32(plateauCoords[0]), Convert.ToInt32(plateauCoords[1]));

            List<Rover> rovers = GetRoverInfo(plateau);

            MoveRoversOnPlateau(rovers);
        }

        public static List<Rover> GetRoverInfo(Plateau plateau)
        {
            string roverInfo = "", command = "";
            List<Rover> rovers = new List<Rover>();
            Match m;

            do
            {
                do
                {
                    Console.WriteLine("Enter rover position and direction(input format: x y D)(leave blank to finish)");
                    roverInfo = Console.ReadLine();
                    if (roverInfo == "")
                        return rovers;

                    Regex regex = new Regex(@"[0-9]\s[0-9]\s(N|E|S|W)");
                    m = regex.Match(roverInfo);
                    if(!m.Success)
                        Console.WriteLine("Invalid input");

                } while (!m.Success);

                string[] coords = roverInfo.Split(" ");
                Heading heading;
                Enum.TryParse(coords[2], out heading);
                rovers.Add(new Rover(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1]), plateau, heading));

                do
                {
                    Console.WriteLine("Enter rover command");
                    command = Console.ReadLine();
                    Regex regex = new Regex(@"(^|,)(L|R|M)");
                    m = regex.Match(command);
                    if (!m.Success)
                        Console.WriteLine("Invalid input");
                } while (!m.Success);

                rovers[rovers.Count - 1].SetCommand(command);

            } while (roverInfo != "");

            return rovers;
        }

        public static void MoveRoversOnPlateau(List<Rover> rovers)
        {
            foreach (var rover in rovers)
            {
                try
                {
                    rover.Move();
                    rover.Print();
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case InvalidInputException:
                            Console.WriteLine("Invalid input");
                            break;
                        case RoverOutOfBoundsException:
                            Console.WriteLine("Rover has moved out of plateau bounds.");
                            break;
                        default:
                            throw;
                    }
                }
            }
        }
    }
}
