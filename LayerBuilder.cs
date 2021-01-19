using System;
using System.Collections.Generic;
using System.Text;

namespace Brickwork
{
    public class Builder
    {
        
        Brick[][] FirstBrickLayer;
      
        Brick[][] SecondBrickLayer;

        //Position and moving of the bricks
        bool forward = true;
  
        int brickCount = 0;

        //size of the Brickwork
        int heightOfBrickwork;
        int widthOfBrickwork;

        //To finish the program
        bool isItBuild = false;
        //Method to Build thw Brickwork, using the Brick class as a base class
        public Brick[][] Build(Brick[][] FirstLayer)
        {
            FirstBrickLayer = FirstLayer;
            heightOfBrickwork = FirstBrickLayer.Length;
            widthOfBrickwork = FirstBrickLayer[0].Length;

            if (!IsNumberEncountedTwoTimes(FirstBrickLayer) && !AreTheHalfsNextToEachOther(FirstBrickLayer))
            {
                return null;
            }

            SecondBrickLayer = new Brick[heightOfBrickwork][];
            for (int i = 0; i < heightOfBrickwork; i++)
            { 
                SecondBrickLayer[i] = new Brick[widthOfBrickwork];
            }

            //Move the bricks by reference value
            for (int i = 0; i < heightOfBrickwork; i++)
            {
                for (int j = 0; j < widthOfBrickwork; j++)
                {
                    if (forward)
                    {
                        Forward(ref i, ref j, ref forward);
                    }
                    else
                    {
                        Backwards(ref i, ref j, ref forward);
                        if (isItBuild == true)
                        {
                            return null;
                        }
                    }
                }
            }

            if (!IsNumberEncountedTwoTimes(SecondBrickLayer) && !AreTheHalfsNextToEachOther(SecondBrickLayer))
            {
                return null;
            }

            
            DisplayBrickLayer(SecondBrickLayer);
            return SecondBrickLayer;
        }

        //Methods for moving and making rotation through the layers and the bricks
        private void Forward(ref int row, ref int col, ref bool forward)
        {
            //Creating a new Brick on every empty place
            if (SecondBrickLayer[row][col] == null)
            {
                brickCount++;
                SecondBrickLayer[row][col] = new Brick(brickCount);
            }

            
            if (!SecondBrickLayer[row][col].IsBaseOfBrickWork)
            {
                if (SecondBrickLayer[row][col].IsRightSideValid == false 
                    && col + 1 < widthOfBrickwork 
                    && FirstBrickLayer[row][col].Size != FirstBrickLayer[row][col + 1].Size)
                {
                    SecondBrickLayer[row][col + 1] = new Brick(brickCount);
                    SecondBrickLayer[row][col + 1].IsBaseOfBrickWork = true;
                    SecondBrickLayer[row][col].SecondLayerRow = row;
                    SecondBrickLayer[row][col].SecondLayerCol = col + 1;
                    SecondBrickLayer[row][col].IsRightSideValid = true;
                }
                
                else if (row + 1 < heightOfBrickwork && FirstBrickLayer[row + 1][col].Size != FirstBrickLayer[row][col].Size)
                {
                    SecondBrickLayer[row + 1][col] = new Brick(brickCount);
                    SecondBrickLayer[row + 1][col].IsBaseOfBrickWork = true;
                    SecondBrickLayer[row][col].SecondLayerRow = row + 1;
                    SecondBrickLayer[row][col].SecondLayerCol = col;
                    SecondBrickLayer[row][col].AreRightAndBottomSideValid = true;
                }
               //the bool forwars is used to rotate, showing the rotation position/way
                else
                {                  
                    forward = false;
                    SecondBrickLayer[row][col] = null;
                    brickCount--;

                    if (col > 0)
                    {
                        col -= 2;
                    }
                    else
                    {
                        col = widthOfBrickwork - 2;
                        row--;
                    }
                }
            }
        }

        private void Backwards(ref int row, ref int col, ref bool forward)
        {
            if (SecondBrickLayer[row][col].IsBaseOfBrickWork)
            {
                if (col > 0)
                {
                    col -= 2;
                }
                else
                {
                    col = widthOfBrickwork - 2;
                    row--;
                }
            }
            else if (!SecondBrickLayer[row][col].AreRightAndBottomSideValid)
            {
                int childRow = SecondBrickLayer[row][col].SecondLayerRow;
                int childColumn = SecondBrickLayer[row][col].SecondLayerCol;
                SecondBrickLayer[childRow][childColumn] = null;

                forward = true;

                if (col > 0)
                {
                    col--;
                }
                else
                {
                    col = widthOfBrickwork - 1;
                    row--;
                }
            }
            else
            {
                if (row == 0 && col == 0)
                {
                    Console.WriteLine("-1");
                    Console.WriteLine("The system could not find a compatible pattern!");
                    isItBuild = true;
                }

                int childRow = SecondBrickLayer[row][col].SecondLayerRow;
                int childColumn = SecondBrickLayer[row][col].SecondLayerCol;
                SecondBrickLayer[childRow][childColumn] = null;
                SecondBrickLayer[row][col] = null;
                brickCount--;

                if (col > 0)
                {
                    col -= 2;
                }
                else
                {
                    col = widthOfBrickwork - 2;
                    row--;
                }
            }
        }
        
        public bool IsNumberEncountedTwoTimes(Brick[][] Layer)
        {
            
            var dict = new Dictionary<int, int>();

            foreach (var row in Layer)
            {
                foreach (var item in row)
                {
                    if (dict.ContainsKey(item.Size))
                        dict[item.Size]++;
                    else
                        dict[item.Size] = 1;
                }
            }

            foreach (var pair in dict)
            {
                if (pair.Value != 2)
                {
                    Console.WriteLine("Incorrect brick layout!");
                    return false;
                }
            }

            return true;
        }

        public bool AreTheHalfsNextToEachOther(Brick[][]Layer)
        {
            for (int i = 0; i < heightOfBrickwork; i++)
            {
                for (int j = 0; j < widthOfBrickwork; j++)
                {
                    if (!Layer[i][j].IsLayerValid)
                    {

                        if (j < widthOfBrickwork - 1 && Layer[i][j].Size == Layer[i][j + 1].Size)
                        {
                            Layer[i][j].IsLayerValid = true;
                            Layer[i][j + 1].IsLayerValid = true;
                        }
                        
                        else if (i < heightOfBrickwork && Layer[i][j].Size == Layer[i + 1][j].Size)
                        {
                            Layer[i][j].IsLayerValid = true;
                            Layer[i + 1][j].IsLayerValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Bricks are not placed in the right order.");
                            return false;
                        }
                    }

                }
            }
            return true;

        }

        public void DisplayBrickLayer(Brick[][] Layer)
        {
            
            for (int row = 0; row < heightOfBrickwork; row++)
            {
                
                if (row == 0)
                {
                    for (int col = 0; col < widthOfBrickwork; col++)
                    {
                        if (col == 0)
                        {
                            Console.Write("*");
                        }
                        Console.Write("*********");
                    }
                    Console.WriteLine();
                }
                
                for (int i = 0; i < 3; i++)
                {
                    for (int col = 0; col < widthOfBrickwork; col++)
                    {
                        if (col == 0)
                        {
                            Console.Write("*");
                        }
                        
                        if (i == 1)
                        {
                            if ((col + 1 < widthOfBrickwork && Layer[row][col].Size 
                                != Layer[row][col + 1].Size) || col == widthOfBrickwork - 1)
                            {
                                Console.Write(Layer[row][col].Size.ToString().PadLeft(6) + "*".PadLeft(3));
                            }
                            else
                            {
                                Console.Write(Layer[row][col].Size.ToString().PadLeft(6) + " ".PadLeft(3));
                            }
                        }
                        
                        else
                        {
                            if ((col + 1 < widthOfBrickwork && Layer[row][col].Size 
                                != Layer[row][col + 1].Size) || col == widthOfBrickwork - 1)
                            {
                                Console.Write("*".PadLeft(9));
                            }
                            else
                            {
                                Console.Write(" ".PadLeft(9));
                            }
                        }
                    }
                    Console.WriteLine();
                }

                for (int col = 0; col < widthOfBrickwork; col++)
                {
                    if (col == 0)
                    {
                        Console.Write("*");
                    }
                    if ((row + 1 < heightOfBrickwork && Layer[row][col].Size 
                        != Layer[row + 1][col].Size) || row == heightOfBrickwork - 1)
                    {
                        Console.Write("*********");
                    }
                    else
                    {
                        Console.Write("*".PadLeft(9));
                    }
                }
                Console.WriteLine();

            }
            Console.WriteLine();
        }
    }
}
