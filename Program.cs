using System;
using System.Collections.Generic;
using System.Linq;

namespace BunniesMutants
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int nRows = input[0];
            int mCols = input[1];

            int lastRow = -1;
            int lastCol = -1;
            int playerRow = -1;
            int playerCol = -1;

            bool isInRange = false;
            bool isWinner = false;
            bool isDead = false;

            List<int[]> bunnies = new List<int[]>();

            char[,] matrix = new char[nRows, mCols];

            for (int row = 0; row < nRows; row++)
            {
                string inputRow = Console.ReadLine();

                for (int col = 0; col < mCols; col++)
                {
                    matrix[row, col] = inputRow[col];
                }

            }

            string moves = Console.ReadLine();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == 'P') //case Player => 'P'
                    {

                        playerRow = row;
                        playerCol = col;

                    }

                }    //end of for cicle
            }
            

            for (int i = 0; i < moves.Length; i++)
            {
                char move = moves[i];

                int[] nextRowCol = GetPlayerMove(playerRow, playerCol, move);
                isInRange = CheckIsInRange(nextRowCol, matrix);


                if (isInRange)
                {
                    matrix[playerRow, playerCol] = '.';
                    playerRow = nextRowCol[0];
                    playerCol = nextRowCol[1];

                    if (matrix[playerRow, playerCol] == 'B')
                    {
                        isDead = true;
                        lastRow = playerRow;
                        lastCol = playerCol;
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = 'P';
                        lastRow = playerRow;
                        lastCol = playerCol;
                    }
                }
                else
                {
                    isWinner = true;
                    
                    matrix[playerRow, playerCol] = '.';
                    lastRow = playerRow;
                    lastCol = playerCol;
                }


                bunnies = GetBunniesCoordinates(matrix);

                for (int x = 0; x < bunnies.Count; x++)
                {

                    int[] position = bunnies[x];
                    int row = position[0];
                    int col = position[1];

                    if (!isDead)
                    {
                        isDead = ChechIsKilled(row, col, matrix); // ??
                    }

                    matrix = GetSpread(row, col, matrix);
                }

                if (isDead || isWinner)
                {
                    break;
                }

            }


            if (isDead)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        Console.Write(matrix[row, col]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"dead: {lastRow} {lastCol}");

            }

            if (isWinner)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        Console.Write(matrix[row, col]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"won: {lastRow} {lastCol}");

            }


        }

        public static int[] GetPlayerMove(int row, int col, char move)
        {
            int[] nextRowCol = new int[2];

            if (move == 'L')
            {
                nextRowCol[0] = row;
                nextRowCol[1] = col - 1;
            }

            if (move == 'R')
            {
                nextRowCol[0] = row;
                nextRowCol[1] = col + 1;
            }

            if (move == 'U')
            {
                nextRowCol[0] = row - 1;
                nextRowCol[1] = col;
            }

            if (move == 'D')
            {
                nextRowCol[0] = row + 1;
                nextRowCol[1] = col;
            }

            return nextRowCol;
        }

        public static bool CheckIsInRange(int[] nextRowCol, char[,] matrix)
        {
            bool isInRange = false;

            int row = nextRowCol[0];
            int col = nextRowCol[1];

            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
            {
                isInRange = true;

            }
            return isInRange;
        }

        public static char[,] GetSpread(int row, int col, char[,] matrix)
        {

            if (row - 1 >= 0)  //up
            {
                matrix[row - 1, col] = 'B';
            }
            if (row + 1 < matrix.GetLength(0))    //down
            {
                matrix[row + 1, col] = 'B';
            }
            if (col - 1 >= 0)   //left
            {
                matrix[row, col - 1] = 'B';
            }
            if (col + 1 < matrix.GetLength(1))    //right
            {
                matrix[row, col + 1] = 'B';
            }

            return matrix;
        }

        public static bool ChechIsKilled(int row, int col, char[,] matrix)
        {
            bool isDead = false;

            if (row - 1 >= 0 && matrix[row - 1, col] == 'P')  //up
            {
                isDead = true;
            }
            if (row + 1 < matrix.GetLength(0) && matrix[row + 1, col] == 'P')    //down
            {
                isDead = true;
            }
            if (col - 1 >= 0 && matrix[row, col - 1] == 'P')//left
            {
                isDead = true;
            }

            if (col + 1 < matrix.GetLength(1) && matrix[row, col + 1] == 'P')    //right
            {
                isDead = true;
            }

            return isDead;
        }


        private static List<int[]> GetBunniesCoordinates(char[,] matrix)
        {
            List<int[]> bunnies = new List<int[]>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == 'B')    //Buuny on the road :)
                    {
                        int[] currPositionBunny = new int[2];
                        currPositionBunny[0] = row;
                        currPositionBunny[1] = col;

                        bunnies.Add(currPositionBunny);  //get all positions of bunnies
                    }
                }
            }
            return bunnies;

        }

    }
}