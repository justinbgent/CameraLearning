using Microsoft.Xna.Framework;

/* 
 * 1st
 * Make a function that takes in the screen's width and height and divies the dimensions
 * out into a grid of squares. Have an array hold all positions in grid (top left corners
 * of every squre as they are the place of origin where drawing starts). Make function
 * versatile so it can work across projects. (A thought was make some well named
 * variables at top that can quickly be modified based on changes in screen size for
 * example. That could potentially, if desired, expand grid square dimensions. Those
 * square dimensions grid is made out of should always be adjustable easily in the
 * function.)
*/

// Works for creating list of grid coordinates where tiles go 
// works so long as tiles are square

namespace CameraFollow
{
    struct GameGrid
    {
        private int screenWidth;
        private int screenHeight;

        public int GridSquareSize { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        
        public Vector2[,] Grid { get; private set; }

        public GameGrid(int screenWidth, int screenHeight, int gridSquareSize)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.GridSquareSize = gridSquareSize;
            this.Columns = (screenWidth / gridSquareSize) + ((screenWidth % gridSquareSize > 0) ? 1 : 0);
            this.Rows = (screenHeight / gridSquareSize) + ((screenHeight % gridSquareSize > 0) ? 1 : 0);

            int[] xCoordinates = new int[Columns];
            int[] yCoordinates = new int[Rows];

            for (int i = 0; i < xCoordinates.Length; i++) { xCoordinates[i] = i * gridSquareSize; }
            for (int i = 0; i < yCoordinates.Length; i++) { yCoordinates[i] = i * gridSquareSize; }

            this.Grid = new Vector2[Columns, Rows];

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    this.Grid[x, y] = new Vector2(xCoordinates[x], yCoordinates[y]);
                }
            }
        }
    }
}
