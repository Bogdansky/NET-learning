using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    class Grid
    {

        private int SizeX;
        private int SizeY;
        private Cell[,] cells;
        private static Random rnd;
        private Canvas drawCanvas;
        //private Ellipse[,] cellsVisuals;

        
        public Grid(Canvas c)
        {
            drawCanvas = c;
            rnd = new Random();
            SizeX = (int) (c.Width / 5);
            SizeY = (int)(c.Height / 5);
            cells = new Cell[SizeX, SizeY];
            //cellsVisuals = new Ellipse[SizeX, SizeY];
 
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    cells[i, j] = new Cell(i, j, 0, false);
                }

            SetRandomPattern();
            InitCellsVisuals();
            UpdateGraphics();
            
        }


        public void Clear()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    var ellipse = cells[i, j].Ellipse;
                    cells[i, j] = new Cell(i, j, 0, false);
                    cells[i, j].Ellipse = ellipse;
                    cells[i, j].Ellipse.Fill = Brushes.Gray;
                }
        }


        void MouseMove(object sender, MouseEventArgs e)
        {
            var cellVisual = sender as Ellipse;
            
            int i = (int) cellVisual.Margin.Left / 5;
            int j = (int) cellVisual.Margin.Top / 5;
            

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!cells[i, j].IsAlive)
                {
                    cells[i, j].IsAlive = true;
                    cells[i, j].Age = 0;
                    cellVisual.Fill = Brushes.White;
                }
            }
        }

        public void UpdateGraphics()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    cells[i, j].Ellipse.Fill = cells[i, j].IsAlive
                                                  ? (cells[i, j].Age < 2 ? Brushes.White : Brushes.DarkGray)
                                                  : Brushes.Gray;
        }

        public void InitCellsVisuals()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    cells[i, j].Ellipse = new Ellipse();
                    cells[i, j].Ellipse.Width = cells[i, j].Ellipse.Height = 5;

                    double left = cells[i, j].PositionX;
                    double top = cells[i, j].PositionY;
                    cells[i, j].Ellipse.Margin = new Thickness(left, top, 0, 0);
                    cells[i, j].Ellipse.Fill = Brushes.Gray;
                    drawCanvas.Children.Add(cells[i, j].Ellipse);

                    cells[i, j].Ellipse.MouseMove += MouseMove;
                    cells[i, j].Ellipse.MouseLeftButtonDown += MouseMove;
                 }
            UpdateGraphics();
                    
        }
        
        private void InitCell(int x, int y)
        {
            cells[x, y].Ellipse = new Ellipse();
            cells[x, y].Ellipse.Width = cells[x, y].Ellipse.Height = 5;

            double left = cells[x, y].PositionX;
            double top = cells[x, y].PositionY;
            cells[x, y].Ellipse.Margin = new Thickness(left, top, 0, 0);
            cells[x, y].Ellipse.Fill = Brushes.Gray;
            drawCanvas.Children.Add(cells[x, y].Ellipse);

            cells[x, y].Ellipse.MouseMove += MouseMove;
            cells[x, y].Ellipse.MouseLeftButtonDown += MouseMove;
        }


        public static bool GetRandomBoolean()
        {
            return rnd.NextDouble() > 0.8;
        }

        public void SetRandomPattern()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    cells[i, j].IsAlive = GetRandomBoolean();
        }
        

        public void Update()
        {
            bool alive = false;
            int age = 0;

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    //cells[i, j] = CalculateNextGeneration(i,j);          // UNOPTIMIZED
                    CalculateNextGeneration(i, j, ref alive, ref age);   // OPTIMIZED
                    cells[i, j].IsAlive = alive;  // OPTIMIZED
                    cells[i, j].Age = age;  // OPTIMIZED
                }
            }

            UpdateGraphics();
        }

        public Cell CalculateNextGeneration(int row, int column)    // UNOPTIMIZED
        {
            bool alive;
            int count, age;

            alive = cells[row, column].IsAlive;
            age = cells[row, column].Age;
            count = CountNeighbors(row, column);

            if (alive && count < 2)
                return new Cell(row, column, 0, false);
            
            if (alive && (count == 2 || count == 3))
            {
                cells[row, column].Age++;
                return new Cell(row, column, cells[row, column].Age, true);
            }

            if (alive && count > 3)
                return new Cell(row, column, 0, false);
            
            if (!alive && count == 3)
                return new Cell(row, column, 0, true);
            
            return new Cell(row, column, 0, false);
        }

        public void CalculateNextGeneration(int row, int column, ref bool isAlive, ref int age)     // OPTIMIZED
        {
            isAlive = cells[row, column].IsAlive;
            age = cells[row, column].Age;

            int count = CountNeighbors(row, column);

            if (isAlive && count < 2)
            {
                isAlive = false;
                age = 0;
            }

            if (isAlive && (count == 2 || count == 3))
            {
                cells[row, column].Age++;
                isAlive = true;
                age = cells[row, column].Age;
            }

            if (isAlive && count > 3)
            {
                isAlive = false;
                age = 0;
            }

            if (!isAlive && count == 3)
            {
                isAlive = true;
                age = 0;
            }
        }

        public int CountNeighbors(int i, int j)
        {
            int count = 0;

            if (i != SizeX - 1 && cells[i + 1, j].IsAlive) count++;
            if (i != SizeX - 1 && j != SizeY - 1 && cells[i + 1, j + 1].IsAlive) count++;
            if (j != SizeY - 1 && cells[i, j + 1].IsAlive) count++;
            if (i != 0 && j != SizeY - 1 && cells[i - 1, j + 1].IsAlive) count++;
            if (i != 0 && cells[i - 1, j].IsAlive) count++;
            if (i != 0 && j != 0 && cells[i - 1, j - 1].IsAlive) count++;
            if (j != 0 && cells[i, j - 1].IsAlive) count++;
            if (i != SizeX - 1 && j != 0 && cells[i + 1, j - 1].IsAlive) count++;

            return count;
        }

        private void ClearEllipse(Cell cell)
        {
            cell.Ellipse.MouseMove -= MouseMove;
            cell.Ellipse.MouseLeftButtonDown -= MouseMove;
            var ellipseRef = cell.Ellipse;
            cell.Ellipse = null;
        }
    }
}