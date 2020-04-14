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
    }
}
