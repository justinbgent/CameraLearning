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

        public static void Rotate(int[][] matrix)
        {
            int adjust = matrix.Length - 1;
            int adjusting = adjust;

            for (int i = 0; i < matrix.Length / 2; i++)
            {
                for (int j = 0; j < adjusting; j++)
                {
                    if (j == 0 && i > 0)
                    {
                        j = i;
                        adjusting -= 1;
                        // For safety
                        if (j >= adjusting) break;
                    }

                    int firstIndice = j;                                        // 1
                    int secondIndice = adjust - i;                              // 2 - 0
                    int storeValue = matrix[i][j];                              // 2
                                                                                // save value for next rotated position
                    int nextElementValue = matrix[firstIndice][secondIndice];   // 6
                                                                                // input value of rotated element
                    matrix[firstIndice][secondIndice] = storeValue;             // 2

                    // rotate value 90 degrees
                    int timesLeft = 3;
                    int oldFirst;
                    while (timesLeft > 0)
                    {
                        oldFirst = firstIndice;
                        firstIndice = secondIndice;
                        secondIndice = adjust - oldFirst;
                        storeValue = nextElementValue;
                        nextElementValue = matrix[firstIndice][secondIndice];
                        matrix[firstIndice][secondIndice] = storeValue;
                        timesLeft--;
                    }
                }
            }
        }
    }
}
