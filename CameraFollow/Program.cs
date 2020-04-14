using System;

namespace CameraFollow
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // do note that the better way of doing this would be like the following:
            // int[,] matrix = new int[3, 3];
            // matrix[0, 0] = 1;
            // matrix[0, 1] = 2;
            // matrix[0, 2] = 3;
            // matrix[1, 0] = 4;
            // matrix[1, 1] = 5;
            // matrix[1, 2] = 6;
            // matrix[2, 0] = 7;
            // matrix[2, 1] = 8;
            // matrix[2, 2] = 9;
            // Multi-Dimensional Array Syntax FTW!!!

            int[][] matrix = new int[3][];
            matrix[0] = new int[3];
            matrix[1] = new int[3];
            matrix[2] = new int[3];

            matrix[0][0] = 1;
            matrix[0][1] = 2;
            matrix[0][2] = 3;
            matrix[1][0] = 4;
            matrix[1][1] = 5;
            matrix[1][2] = 6;
            matrix[2][0] = 7;
            matrix[2][1] = 8;
            matrix[2][2] = 9;

            using (var game = new Game1())
                game.Run();
        }

        // Code rotates a 2d array clockwise 90 degrees
        public static void Rotate(int[][] matrix)
        {
            // The space complexity for this function is always O(1)
            // No more than 10 integers are created and used at a time
            int adjust = matrix.Length - 1;
            // adjusting will adjust as the loops run 
            int adjusting = adjust;

            // Note the nested loop. Even though nested, it is not truly O(n^2)
            // due to the conditionals, (one of which updates while ran), the time complexity
            // is really O(n) as rotation of each element occurs only once.
            for (int i = 0; i < matrix.Length / 2; i++)
            {
                for (int j = 0; j < adjusting; j++)
                {
                    // this if statement ensures that elements will be rotated only once
                    if (j == 0 && i > 0)
                    {
                        // starts j at the correct element
                        j = i;
                        // updates the loop's conditional
                        adjusting -= 1;
                        // For safety but probably unecesary:
                        if (j >= adjusting) break;
                    }

                    // rotate value 90 degrees
                    int indice1 = j;
                    int indice2 = adjust - i;
                    int storeValue = matrix[i][j];
                    int nextElementValue = matrix[indice1][indice2];
                    matrix[indice1][indice2] = storeValue;

                    // this int is 
                    int timesLeft = 3;
                    int oldFirst;
                    // yes another loop, still O(n), realize we are working with a 2d array here.
                    while (timesLeft > 0)
                    {
                        oldFirst = indice1;
                        indice1 = indice2;
                        indice2 = adjust - oldFirst;
                        storeValue = nextElementValue;
                        nextElementValue = matrix[indice1][indice2];
                        matrix[indice1][indice2] = storeValue;
                        timesLeft--;
                    }
                }
            }
        }
    }
}
