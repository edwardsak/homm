using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Heroes.Core.Map.Terrain
{
    public class MapTerrain
    {
        TextReader tr;

        public int[] cellXY;
        public List<int[]> cellXYs;
        public List<List<int[]>> cellXYss;

        public Cell3[,] _cells;
        public Cell3[,] _cells2;

        Cell3 _curCell;

        public int _totalCellCol = 36;
        public int _totalCellRow = 36;
        public int cellWidth = 32;
        public int cellHeight = 32;

        public int startX;
        public int startY;

        Point bigMapPoint;

        public MapTerrain()
        {
            cellXYs = new List<int[]>();
            cellXYss = new List<List<int[]>>();

            CreateCells();
            DefineObstacles();
        }

        public void CreateCells()
        {
            startX = 71;
            startY = 71;

            int cellWidth = 32;
            int cellHeight = 32;

            _cells = new Cell3[_totalCellRow, _totalCellCol];
            _cells2 = new Cell3[_totalCellRow, _totalCellCol];

            int x = startX;
            int y = startY;

            for (int row1 = 0; row1 < _totalCellRow; row1++)
            {
                x = startX;
                cellXYs = new List<int[]>();
                for (int col1 = 0; col1 < _totalCellCol; col1++)
                {
                    Cell3 cell = new Cell3(x, y, cellWidth, cellHeight);

                    cellXY = new int[] { x, y, 0 };
                    cellXYs.Add(cellXY);

                    cell._row = row1;
                    cell._col = col1;

                    _cells[row1, col1] = cell;
                    _cells2[row1, col1] = cell.Clone();

                    x += cellWidth;
                }
                cellXYss.Add(cellXYs);
                y += cellHeight;
            }
        }

        public void DefineObstacles()
        {
            try
            {
                tr = new StreamReader(Application.StartupPath + @"\cellpassibility.txt");
                string s = tr.ReadLine();

                int a = 0;

                while (s != null)
                {
                    string[] ss = s.Split(',');
                    _cells[Convert.ToInt32(ss[0]), Convert.ToInt32(ss[1])]._passability = true;
                    cellXYss[Convert.ToInt32(ss[0])][Convert.ToInt32(ss[1])][2] = 1;
                    s = tr.ReadLine();
                }
            }
            catch
            { }
            finally
            {
                if (tr != null)
                    tr.Close();
            }
        }
        
        public Cell3 FindCell(int x, int y)
        {
            for (int row1 = 0; row1 < _totalCellRow; row1++)
            {
                for (int col1 = 0; col1 < _totalCellCol; col1++)
                {
                    if (_cells[row1, col1]._rect.Contains(x, y))
                    {
                        Debug.WriteLine("row="+row1+"  col="+col1);
                        return _cells[row1, col1];
                    }
                }
            }
            return null;
        }

        public int[] FindCell2(int x, int y)
        {
            int[] xy = new int[2];
            for (int row3 = 0; row3 < _totalCellRow; row3++)
            {
                for (int col3 = 0; col3 < _totalCellCol; col3++)
                {
                    if (_cells[row3, col3]._rect.Contains(x, y))
                    {
                        Debug.WriteLine("row=" + row3 + "  col=" + col3);
                        xy[0] = row3;
                        xy[1] = col3;
                        return xy;
                    }
                }
            }
            return null;
        }

        public void FindCell3(int eX, int eY)
        {
            gv._bigMapPtX_eX = eX + gv._bigMapPtX;
            gv._bigMapPtY_eY = eY + gv._bigMapPtY;
            
            int row6 = 0; 
            int col6 = 0;
            
            int foundCell = 0;
            bool lastRow = false;
            bool lastCol = false;

            while (foundCell == 0)
            {
                try
                {
                    int k = col6;
                    if (gv._bigMapPtX_eX < _cells[row6, k + 1]._rect.X)
                    { }
                }
                catch
                {
                    lastCol = true;
                }
                try
                {
                    int k = row6;
                    if (gv._bigMapPtY_eY < _cells[row6 + 1, col6]._rect.Y)
                    {}
                }
                catch
                {
                    lastRow = true;
                }

                if (lastCol == false)
                {
                    if (gv._bigMapPtX_eX > _cells[row6, col6 + 1]._rect.X)
                        col6 += 1;
                    else if (lastRow == false)
                    {
                        if (gv._bigMapPtY_eY > _cells[row6 + 1, col6]._rect.Y)
                            row6 += 1;
                        else
                            foundCell = 1;
                    }
                }
                else if (lastRow == false)
                {
                    if (gv._bigMapPtY_eY > _cells[row6 + 1, col6]._rect.Y)
                        row6 += 1;
                    else
                        foundCell = 1;
                }
                else
                    foundCell = 1;
            }

            gv._cellDrawPointX = _cells[row6, col6]._rect.X;
            gv._cellDrawPointY = _cells[row6, col6]._rect.Y;
            gv._row = row6;
            gv._col = col6;
        }

        public void FindCell4(int eX, int eY)
        {
            int col = 0;
            int row = 0;

            int foundX = 0;
            int foundY = 0;

            int x = 0;
            int y = 0;

            x = gv._bigMapPtX + eX;
            y = gv._bigMapPtY + eY;

            //if (gv._bigMapPtX == 0)
            //    x -= 71;
            //if (gv._bigMapPtY == 0)
            //    y -= 71;

            while (foundX == 0)
            {
                if (x > cellXYss[_totalCellRow-1][_totalCellCol-1][0] + cellWidth)
                {
                    //col = _totalCellCol-1;
                    col = -1;
                    row = -1;
                    foundX = 1;
                    foundY = 1;
                }
                else if (x <= cellXYss[row][col][0] + cellWidth)
                    foundX = 1;
                else
                    col += 1;
            }

            while (foundY == 0)
            {
                if (y > cellXYss[_totalCellRow-1][_totalCellCol-1][1] + cellHeight)
                {
                    //row = _totalCellRow-1;
                    col = -1;
                    row = -1;
                    foundX = 1;
                    foundY = 1;
                }
                else if (y <= cellXYss[row][col][1] + cellHeight)
                    foundY = 1;
                else
                    row += 1;
            }

            //gv._col = col;
            //gv._row = row;

            if (gv.StartRecordCellPassibility == 1)
            {
                gv.cellRecordRow = row;
                gv.cellRecordCol = col;
            }
            else
            {
                gv._col = col;
                gv._row = row;
            }
        }

        public void FindCellFromSmallMap()
        {
            int row6 = 0;
            int col6 = 0;
            
            int foundCell = 0;
            bool lastRow = false;
            bool lastCol = false;

            while (foundCell == 0)
            {
                try
                {
                    int k = col6;
                    if (gv._bigMapPtX < _cells[row6, k + 1]._rect.X)
                    { }
                }
                catch
                {
                    lastCol = true;
                }
                try
                {
                    int k = row6;
                    if (gv._bigMapPtY < _cells[row6 + 1, col6]._rect.Y)
                    { }
                }
                catch
                {
                    lastRow = true;
                }

                if (lastCol == false)
                {
                    if (gv._bigMapPtX > _cells[row6, col6 + 1]._rect.X)
                        col6 += 1;
                    else if (lastRow == false)
                    {
                        if (gv._bigMapPtY > _cells[row6 + 1, col6]._rect.Y)
                            row6 += 1;
                        else
                            foundCell = 1;
                    }
                }
                else if (lastRow == false)
                {
                    if (gv._bigMapPtY > _cells[row6 + 1, col6]._rect.Y)
                        row6 += 1;
                    else
                        foundCell = 1;
                }
                else
                    foundCell = 1;
            }
           
            if (gv._bigMapPtX == 0)
                gv._cellDrawPointX = 71;
            else
                gv._cellDrawPointX = _cells[row6, col6]._rect.X;

            if (gv._bigMapPtY == 0)
                gv._cellDrawPointY = 71;
            else
                gv._cellDrawPointY = _cells[row6, col6]._rect.Y;
            
            gv._row = row6;
            gv._col = col6;
        }

        public void FindCellFromSmallMap2()
        {
            int col = 0;
            int row = 0;

            int foundX = 0;
            int foundY = 0;

            if (gv._bigMapPtX == 0)
            {
                gv._cellDrawPointX = 71;
                gv._col = 0;
            }
            else
            {
                while (foundX == 0)
                {
                    if (gv._bigMapPtX-71 < _cells[row, col]._rect.X + cellWidth)
                    {
                        gv._cellDrawPointX = 0;
                        gv._bigMapPtX = _cells[row, col]._rect.X;
                        foundX = 1;
                    }
                    else
                        col += 1;
                }
            }

            if (gv._bigMapPtY == 0)
            {
                gv._cellDrawPointY = 71;
                gv._row = 0;
            }
            else
            {
                while (foundY == 0)
                {
                    if (gv._bigMapPtY - 71 < _cells[row, col]._rect.Y + cellHeight)
                    {
                        gv._cellDrawPointY = 0;
                        gv._bigMapPtY = _cells[row, col]._rect.Y;
                        foundY = 1;
                    }
                    else
                        row += 1;
                }
            }
            gv._row = row;
            gv._col = col;
            gv._bigMapPtX = cellXYss[row][col][0];
            gv._bigMapPtY = cellXYss[row][col][1];
        }

        public void findCellFromSmallMap3()
        {
            int foundX = 0;
            int foundY = 0;
            int row = 0;
            int col = 0;

            while (foundX == 0)
            {

                if (gv._bigMapPtX > cellXYss[row][col][0] + cellWidth)
                    col += 1;
                else
                    foundX = 1;
            }

            while (foundY == 0)
            {

                if (gv._bigMapPtY > cellXYss[row][col][1] + cellHeight)
                    row += 1;
                else
                    foundY = 1;
            }
            gv._row = row;
            gv._col = col;

            if (gv._row < 0)
                gv._row = 0;
            if (gv._col < 0)
                gv._col = 0;

            if (gv._bigMapPtX != 0)
                gv._bigMapPtX = _cells[row, col]._rect.X;
            if (gv._bigMapPtY != 0)
                gv._bigMapPtY = _cells[row, col]._rect.Y;
        }

        public void FindCellWhenMouseScroll()
        {
            gv._bigMapPtX = cellXYss[gv._row][gv._col][0];
            gv._bigMapPtY = cellXYss[gv._row][gv._col][1];
        }

        //public void Draw(Graphics g, int startDrawCellRow, int startDrawCellCol)
        //{
        //    //Cell3 startCellDraw = FindCell(frmMap3._miniMapPt.X, frmMap3._miniMapPt.Y);
        //    for (int row1 = startDrawCellRow; row1 < _totalCellRow; row1++)
        //    {
        //        for (int col1 = startDrawCellCol; col1 < _totalCellCol; col1++)
        //        {
        //            Cell3 cell = _cells[row1, col1];

        //            Point pt = frmMap3.TerrainToView(cell._rect.Location);

        //            g.DrawRectangle(new Pen(new SolidBrush(Color.Red), 1), pt.X, pt.Y, cell._rect.Width, cell._rect.Height);

        //            if (!cell._passability)
        //                g.FillRectangle(new SolidBrush(Color.FromArgb(96, Color.Red)), pt.X, pt.Y, cell._rect.Width, cell._rect.Height);
        //        }
        //    }
        //}

        public void Draw2(Graphics g)
        {
            if (gv._viewGridLine == 1)
            {
                int x = gv._cellDrawPointX;
                int y = gv._cellDrawPointY;
                //Debug.WriteLine(string.Format("***** {0},{1}", x, y));

                for (int row5 = gv._row; row5 < _totalCellRow; row5++)
                {
                    x = gv._cellDrawPointX;
                    for (int col5 = gv._col; col5 < _totalCellCol; col5++)
                    {
                        Cell3 cell = _cells[row5, col5];
                        Rectangle rect = new Rectangle(x, y, cellWidth, cellHeight);
                        //cell._rect.X = x;
                        //cell._rect.Y = y;


                        g.DrawRectangle(new Pen(new SolidBrush(Color.Red), 1), rect);
                        //g.DrawRectangle(new Pen(new SolidBrush(Color.Red), 1), cell._rect);

                        if (gv._viewPassibilityCell == 1)
                        {
                            if (gv.StartRecordCellPassibility == 1)
                            {
                                if (cellXYss[row5][col5][2] != 1)
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(96, Color.Red)), rect);

                            }
                            else
                            {
                                if (!cell._passability)
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(96, Color.Red)), rect);

                                    //if (col5 == _totalCellCol - 1)
                                    //    g.FillRectangle(new SolidBrush(Color.FromArgb(96, Color.Blue)), rect);
                                }
                            }
                        }
                        else
                        {
 
                        }
                        x += cellWidth;
                    }
                    y += cellHeight;
                }
            }
        }

        
        
    }
}
