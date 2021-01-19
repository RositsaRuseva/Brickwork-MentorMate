using System;
using System.Collections.Generic;
using System.Text;

namespace BrickworkMentorMate
{using System;

namespace Brickwork
{
    class Program
    {
        
        static int widthOfTheBrickwork;
        //Area height
        static int heightOfTheBrickwork;
        //Brick layout from user input
        static Brick[][] InputBrickLayer;

        static int maxInput = 100;

        static void Main(string[] args)
        {
            bool succes;
            do
            {
                succes = Validation();
            } while (!succes);

            Builder builder = new Builder();

            if (builder.Build(InputBrickLayer) == null)
            {
                return;
            }

            Console.ReadKey();
        }

        private static bool Validation()
        {
            try
            {
                string[] InputHolder = Console.ReadLine().Split();

                if (InputHolder.Length != 2)
                {
                    Console.WriteLine("The entered numbers should be two");
                    return false;
                }

                heightOfTheBrickwork = int.Parse(InputHolder[0]);
                widthOfTheBrickwork = int.Parse(InputHolder[1]);

                //Used as validation of the size
                if (heightOfTheBrickwork > maxInput || widthOfTheBrickwork > maxInput 
                    || heightOfTheBrickwork % 2 != 0 || widthOfTheBrickwork % 2 != 0 
                    || heightOfTheBrickwork == 0 || widthOfTheBrickwork == 0)
                {
                    Console.WriteLine("The size should be under 100 and an even number");
                    return false;
                }

                

                //Starting Building - layers
                InputBrickLayer = new Brick[heightOfTheBrickwork][];

                for (int i = 0; i < heightOfTheBrickwork; i++)
                {

                    InputBrickLayer[i] = new Brick[widthOfTheBrickwork];

                    InputHolder = Console.ReadLine().Split();


                    if (InputHolder.Length != widthOfTheBrickwork)
                    {
                        Console.WriteLine("Incorect count");
                        return false;
                    }

                    //Filling the rows with elements
                    for (int j = 0; j < widthOfTheBrickwork; j++)
                    {
                        int BrickNumber = int.Parse(InputHolder[j]);
                        InputBrickLayer[i][j] = new Brick(BrickNumber);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input!");
                return false;
            }

            return true;
        }
    }
}
    class Validation
    {
    }
}
