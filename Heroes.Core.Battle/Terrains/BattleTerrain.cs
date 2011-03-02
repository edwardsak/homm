using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Heroes.Core.Battle.Rendering;

namespace Heroes.Core.Battle.Terrains
{
    public enum CellPartEnum
    {
        UpperLeft,
        UpperRight,
        CenterLeft,
        Center,
        CenterRight,
        LowerLeft,
        LowerRight
    }

    public class BattleTerrain
    {
        public const double DIGONAL_ANGLE = 65.2970;

        public Cell[,] _cells;
        int[] _rowTops;  // each row bottom
        int[] _rowBottoms;  // each row bottom
        int[,] _colLefts; // each col left
        int[,] _colRights; // each col right

        Controller _controller;
        public int _rowCount;
        public int _colCount;

        public BattleTerrain(Controller controller)
        {
            _controller = controller;

            _rowCount = 11;
            _colCount = 15;

            BuildCells(_controller);
        }

        public BattleTerrain()
        {
            _rowCount = 11;
            _colCount = 15;

            BuildCells();
        }

        private void BuildCells(Controller controller)
        {
            _cells = new Cell[_rowCount, _colCount];
            _rowTops = new int[_rowCount];
            _rowBottoms = new int[_rowCount];
            _colLefts = new int[_rowCount, _colCount];
            _colRights = new int[_rowCount, _colCount];

            int startx = 58;
            int starty = 86;
            int x = startx;
            int y = starty;

            for (int row = 0; row < _rowCount; row++)
            {
                _rowTops[row] = y;

                if (row % 2 == 1)
                    x = startx;
                else
                    x = startx + Cell.WIDTH_MIDDLE - 1;

                for (int col = 0; col < _colCount; col++)
                {
                    _colLefts[row, col] = x;

                    _cells[row, col] = new Cell(row, col, x, y, controller.TextureStore._texCell, controller.TextureStore._texCellShd);

                    x += Cell.WIDTH_BODY - 1;
                    _colRights[row, col] = x;
                }

                y += Cell.HEIGHT_HEAD + Cell.HEIGHT_BODY;
                _rowBottoms[row] = y + Cell.HEIGHT_HEAD;
            }

            // build surrounding cell
            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    Cell cell = _cells[row, col];

                    CellPartEnum direction = CellPartEnum.CenterRight;
                    Cell cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.CenterLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.LowerRight;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.LowerLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.UpperRight;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.UpperLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);
                }
            }
        }

        private void BuildCells()
        {
            _cells = new Cell[_rowCount, _colCount];
            _rowTops = new int[_rowCount];
            _rowBottoms = new int[_rowCount];
            _colLefts = new int[_rowCount, _colCount];
            _colRights = new int[_rowCount, _colCount];

            int startx = 58;
            int starty = 86;
            int x = startx;
            int y = starty;

            for (int row = 0; row < _rowCount; row++)
            {
                _rowTops[row] = y;

                if (row % 2 == 1)
                    x = startx;
                else
                    x = startx + Cell.WIDTH_MIDDLE - 1;

                for (int col = 0; col < _colCount; col++)
                {
                    _colLefts[row, col] = x;

                    _cells[row, col] = new Cell(row, col, x, y);

                    x += Cell.WIDTH_BODY - 1;
                    _colRights[row, col] = x;
                }

                y += Cell.HEIGHT_HEAD + Cell.HEIGHT_BODY;
                _rowBottoms[row] = y + Cell.HEIGHT_HEAD;
            }

            // build surrounding cell
            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    Cell cell = _cells[row, col];

                    CellPartEnum direction = CellPartEnum.CenterRight;
                    Cell cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.CenterLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.LowerRight;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.LowerLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.UpperRight;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);

                    direction = CellPartEnum.UpperLeft;
                    cell2 = GetNextCell(cell, direction);
                    cell._adjacentCells.Add(direction, cell2);
                }
            }
        }

        public void Draw()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    _cells[row, col].Draw(_controller.Sprite);
                }
            }
        }

        public CellPartEnum FindCellPart(int x, int y, Cell cell)
        {
            if (y <= cell._rect.Top + Cell.HEIGHT_HEAD)
            {
                // upper
                if (x <= cell._rect.Left + Cell.WIDTH_MIDDLE)
                {
                    // left
                    return CellPartEnum.UpperLeft;
                }
                else
                {
                    // right
                    return CellPartEnum.UpperRight;
                }
            }
            else if (y <= cell._rect.Top + Cell.HEIGHT_HEAD + Cell.HEIGHT_BODY)
            {
                // center
                if (x <= cell._rect.Left + Cell.WIDTH_BODY_LEFT)
                {
                    // left
                    return CellPartEnum.CenterLeft;
                }
                else if (x <= cell._rect.Left + Cell.WIDTH_BODY_LEFT + Cell.WIDTH_BODY_CENTER)
                {
                    // center
                    return CellPartEnum.Center;
                }
                else
                {
                    // right
                    return CellPartEnum.CenterRight;
                }
            }
            else
            {
                // lower
                if (x <= cell._rect.Left + Cell.WIDTH_MIDDLE)
                {
                    // left
                    return CellPartEnum.LowerLeft;
                }
                else
                {
                    // right
                    return CellPartEnum.LowerRight;
                }
            }
        }

        public Cell FindCell(int x, int y)
        {
            ArrayList hitCells = new ArrayList();

            // find which row
            ArrayList hitRows = new ArrayList();
            for (int row = 0; row < _rowCount; row++)
            {
                if (y <= _rowBottoms[row])
                {
                    hitRows.Add(row);

                    if (row + 1 < _rowCount)
                    {
                        if (y >= _rowTops[row + 1])
                            hitRows.Add(row + 1);
                    }

                    break;
                }
            }
            if (hitRows.Count < 1) return null;

            // find which col
            Hashtable hitCols = new Hashtable();
            foreach (int row in hitRows)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    if (x <= _colRights[row, col])
                    {
                        hitCols.Add(row, col);
                        break;
                    }
                }
            }
            if (hitCols.Count < 1) return null;

            // find hit cells
            foreach (int row in hitRows)
            {
                if (hitCols.ContainsKey(row))
                {
                    Cell cell = _cells[row, (int)hitCols[row]];
                    if (cell._rect.Contains(x, y))
                    {
                        hitCells.Add(cell);
                    }
                }
            }
            if (hitCells.Count < 1) return null;
            //if (hitCells.Count == 1) ((Cell)hitCells[0])._isHover = true;

            // find nearest corner
            int diff1 = 0;
            int diff2 = 0;
            int index = 0;
            Cell nearestCell = null;
            foreach (Cell cell in hitCells)
            {
                if (index == 0)
                {
                    diff1 = CalDiff(x, y, cell);
                    nearestCell = (Cell)hitCells[index];
                }
                else
                {
                    diff2 = CalDiff(x, y, cell);

                    if (diff2 < diff1)
                    {
                        nearestCell = (Cell)hitCells[index];
                        diff1 = diff2;
                    }
                }

                index += 1;
            }
            return nearestCell;
        }

        private int CalDiff(int x, int y, Cell cell)
        {
            if (y <= cell._rect.Top + Cell.HEIGHT_HEAD)
            {
                // upper
                if (x <= cell._rect.Left + Cell.WIDTH_MIDDLE)
                {
                    // left
                    return Math.Abs(cell._rect.Left + Cell.WIDTH_MIDDLE - x) + Math.Abs(cell._rect.Top + Cell.HEIGHT_HEAD - y);
                    //return new Rectangle(cell._rect.Left, cell._rect.Top, 23, 10);
                }
                else
                {
                    // right
                    return Math.Abs(cell._rect.Left + Cell.WIDTH_MIDDLE + 1 - x) + Math.Abs(cell._rect.Top + Cell.HEIGHT_HEAD - y);
                    //return new Rectangle(cell._rect.Left + 24, cell._rect.Top, 22, 10);
                }
            }
            else
            {
                // lower
                if (x <= cell._rect.Left + 23)
                {
                    // left
                    return Math.Abs(cell._rect.Left + Cell.WIDTH_MIDDLE - x) + Math.Abs(cell._rect.Top + Cell.HEIGHT_HEAD + Cell.HEIGHT_BODY - y);
                    //return new Rectangle(cell._rect.Left, cell._rect.Top + 42, 23, 10);
                }
                else
                {
                    // right
                    return Math.Abs(cell._rect.Left + Cell.WIDTH_MIDDLE + 1 - x) + Math.Abs(cell._rect.Top + Cell.HEIGHT_HEAD + Cell.HEIGHT_BODY - y);
                    //return new Rectangle(cell._rect.Left + 24, cell._rect.Top + 42, 22, 10);
                }
            }
        }

        public void ClearHover()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    Cell cell = _cells[row, col];
                    cell._isHover = false;

                    if (cell._character != null)
                        cell._character._isHover = false;
                }
            }
        }

        public Cell GetNextCell(Cell cell, CellPartEnum direction)
        {
            if (cell == null) return null;

            switch (direction)
            {
                case CellPartEnum.UpperLeft:
                    {
                        return GetCell(cell._row - 1, cell._rect.Left);
                    }
                case CellPartEnum.UpperRight:
                    {
                        return GetCell(cell._row - 1, cell._rect.Right);
                    }
                case CellPartEnum.CenterLeft:
                    {
                        if (cell._col - 1 < 0)
                            return null;
                        else
                        {
                            return _cells[cell._row, cell._col - 1];
                        }
                    }
                case CellPartEnum.CenterRight:
                    {
                        if (cell._col + 1 >= _colCount)
                            return null;
                        else
                        {
                            return _cells[cell._row, cell._col + 1];
                        }
                    }
                case CellPartEnum.LowerRight:
                    {
                        return GetCell(cell._row + 1, cell._rect.Right);
                    }
                case CellPartEnum.LowerLeft:
                    {
                        return GetCell(cell._row + 1, cell._rect.Left);
                    }
                default:
                    return null;
            }
        }

        private Cell GetCell(int row, int x)
        {
            if (row < 0) return null;
            if (row >= _rowCount) return null;

            for (int col = 0; col < _colCount; col++)
            {
                // x is beyond first column's left
                if (col == 0 && x < _colLefts[row, col])
                    return null;

                if (x <= _colRights[row, col])
                    return _cells[row, col];
            }

            return null;
        }

        public static CellPartEnum FindDirection(Point pointSrc, Point pointDest)
        {
            double degree = FindDegree(pointSrc, pointDest);
            if (degree >= 0 && degree < 30)
                return CellPartEnum.CenterRight;
            else if (degree >= 30 && degree < 90)
                return CellPartEnum.LowerRight;
            else if (degree >= 90 && degree < 150)
                return CellPartEnum.LowerLeft;
            else if (degree >= 150 && degree < 210)
                return CellPartEnum.CenterLeft;
            else if (degree >= 210 && degree < 270)
                return CellPartEnum.UpperLeft;
            else if (degree >= 270 && degree < 330)
                return CellPartEnum.UpperRight;
            else
                return CellPartEnum.CenterRight;
        }

        public static void FindDirection(Point pointSrc, Point pointDest, out ArrayList directions)
        {
            directions = new ArrayList();

            double degree = FindDegree(pointSrc, pointDest);

            // whole angle
            if (Math.Abs(degree) <= 0.05)
            {
                directions.Add(CellPartEnum.CenterRight);
                return;
            }
            else if (Math.Abs(degree - DIGONAL_ANGLE) <= 0.05)
            {
                directions.Add(CellPartEnum.LowerRight);
                return;
            }
            else if (Math.Abs(degree - (180 - DIGONAL_ANGLE)) <= 0.05)
            {
                directions.Add(CellPartEnum.LowerLeft);
                return;
            }
            else if (Math.Abs(degree - 180) <= 0.05)
            {
                directions.Add(CellPartEnum.CenterLeft);
                return;
            }
            else if (Math.Abs(degree - (180 + DIGONAL_ANGLE)) <= 0.05)
            {
                directions.Add(CellPartEnum.UpperLeft);
                return;
            }
            else if (Math.Abs(degree - (360 - DIGONAL_ANGLE)) <= 0.05)
            {
                directions.Add(CellPartEnum.UpperRight);
                return;
            }

            // in between angle
            if (degree > 0 && degree < DIGONAL_ANGLE)
            {
                directions.Add(CellPartEnum.CenterRight);
                directions.Add(CellPartEnum.LowerRight);
                return;
            }
            else if (degree > DIGONAL_ANGLE && degree < 180 - DIGONAL_ANGLE)
            {
                directions.Add(CellPartEnum.LowerRight);
                directions.Add(CellPartEnum.LowerLeft);
                return;
            }
            else if (degree > 180 - DIGONAL_ANGLE && degree < 180)
            {
                directions.Add(CellPartEnum.LowerLeft);
                directions.Add(CellPartEnum.CenterLeft);
                return;
            }
            else if (degree > 180 && degree < 180 + DIGONAL_ANGLE)
            {
                directions.Add(CellPartEnum.CenterLeft);
                directions.Add(CellPartEnum.UpperLeft);
                return;
            }
            else if (degree > 180 + DIGONAL_ANGLE && degree < 360 - DIGONAL_ANGLE)
            {
                directions.Add(CellPartEnum.UpperLeft);
                directions.Add(CellPartEnum.UpperRight);
                return;
            }
            else if (degree > 360 - DIGONAL_ANGLE)
            {
                directions.Add(CellPartEnum.UpperRight);
                directions.Add(CellPartEnum.CenterRight);
                return;
            }
        }

        public static double FindDegree(Point pointSrc, Point pointDest)
        {
            int w = pointDest.X - pointSrc.X;
            int h = pointDest.Y - pointSrc.Y;

            double degree = 0;
            if (h == 0)
            {
                if (w > 0)
                    return 0;
                else if (w < 0)
                    return 180;
            }
            else if (w == 0)
            {
                if (h > 0)
                    return 90;
                else if (h < 0)
                    return 270;
            }
            else
            {
                double radian = Math.Atan((double)h / (double)w);
                degree = RadianToDegree(radian);

                if (w > 0)
                {
                    if (degree < 0) degree = 360 + degree;
                }
                else if (w < 0)
                {
                    if (degree < 0) degree = 180 + degree;
                    else if (degree > 0) degree = 180 + degree;
                }

                return degree;
            }

            // same point, i.e w = 0, h = 0
            return 0;
        }

        private static double RadianToDegree(double radian)
        {
            return radian * 180 / (22 / 7);
        }

        #region Find Path
        public bool FindPath(Cell cellSrc, Cell cellDest, ArrayList path,
            bool ignoreDestCharacter, bool ignoreObstacle)
        {
            ArrayList directions = null;
            FindDirection(cellSrc.GetCenterPoint(), cellDest.GetCenterPoint(), out directions);

            return FindPath(cellSrc, cellDest, path, (CellPartEnum)directions[0],
                ignoreDestCharacter, ignoreObstacle, false, false);
        }

        public bool FindPath(Cell cellSrc, Cell cellDest, ArrayList path, CellPartEnum startingDirection,
            bool ignoreDestCharacter, bool ignoreObstacle, bool includeSrcCell, bool useStraightAngle)
        {
            if (includeSrcCell) path.Add(cellSrc);

            if (cellSrc.Equals(cellDest))
            {
                // end
                return true;
            }

            //CellPartEnum direction = FindDirection(cellSrc.GetCenterPoint(), cellDest.GetCenterPoint());
            CellPartEnum direction = startingDirection;
            Debug.WriteLine("--------------------------------------");
            Debug.WriteLine(string.Format("Start to find at {0}", direction));

            // detect unless cell
            ArrayList deadPath = new ArrayList();
            return FindPath_Sub(cellSrc, cellDest, path, direction, deadPath,
                ignoreDestCharacter, ignoreObstacle, useStraightAngle);
        }

        private bool FindPath_Sub(Cell cell, Cell cellDest, ArrayList path, CellPartEnum direction, ArrayList deadPath, 
            bool ignoreDestCharacter, bool ignoreObstacle, bool useStraightAngle)
        {
            Debug.WriteLine(string.Format("Find cell at {0}", direction));

            Cell cellNext = null;
            if (direction == CellPartEnum.Center)
                cellNext = null;
            else
                cellNext = cell._adjacentCells[direction];

            if (cellNext == null)
            {
                // no more cell found
                Debug.WriteLine(string.Format("No cell"));
                
                // get new direction
                CellPartEnum direction2 = FindDirection(cell.GetCenterPoint(), cellDest.GetCenterPoint());
                if (direction == direction2)
                {
                    switch (direction2)
                    {
                        case CellPartEnum.LowerRight:
                            direction2 = CellPartEnum.LowerLeft;
                            break;
                        case CellPartEnum.LowerLeft:
                            direction2 = CellPartEnum.LowerRight;
                            break;
                        case CellPartEnum.UpperLeft:
                            direction2 = CellPartEnum.UpperRight;
                            break;
                        case CellPartEnum.UpperRight:
                            direction2 = CellPartEnum.UpperLeft;
                            break;
                    }
                }
                direction = direction2;

                //Debug.WriteLine(string.Format("Find cell on {0}", direction));
                return FindPath_Sub(cell, cellDest, path, direction, deadPath, 
                    ignoreDestCharacter, ignoreObstacle, useStraightAngle);
            }

            if (!cellNext.HasPassability() || deadPath.Contains(cellNext))
            {
                // has character or dead cell
                //Debug.WriteLine(string.Format("Obstacle found"));

                if (ignoreDestCharacter)
                {
                    if (cellNext.Equals(cellDest))
                    {
                        path.Add(cellNext);

                        // end
                        return true;
                    }
                }

                if (ignoreObstacle)
                {
                    return FindPath_Good(cellNext, cellDest, direction, path, deadPath,
                        ignoreDestCharacter, ignoreObstacle, useStraightAngle);
                }
                else
                {
                    // change direction                    
                    Cell cellNextTest = GetPathNextCell(cell, cellDest, direction, deadPath);
                    if (cellNextTest != null)
                    {
                        // if go backward, remove unwanted (dead) cell
                        if (path.Contains(cellNextTest))
                        {
                            int index = path.IndexOf(cellNextTest);
                            while (path.Count - 1 > index)
                            {
                                Cell deadCell = (Cell)path[path.Count - 1];
                                deadPath.Add(deadCell);
                                path.RemoveAt(path.Count - 1);
                            }
                        }

                        //Debug.WriteLine(string.Format("ChangeDir, {0},{1},{2}", direction, cellNextTest._row, cellNextTest._col));
                        cellNext = cellNextTest;
                        path.Add(cellNext);

                        // continue to find next cell
                        cell = cellNext;
                    }
                    else
                    {
                        // cannot move
                        // end
                        Debug.WriteLine(string.Format("No more move"));
                        return false;
                    }

                    return FindPath_Sub(cell, cellDest, path, direction, deadPath, 
                        ignoreDestCharacter, ignoreObstacle, useStraightAngle);
                }
            }

            return FindPath_Good(cellNext, cellDest, direction, path, deadPath, 
                ignoreDestCharacter, ignoreObstacle, useStraightAngle);
        }

        private bool FindPath_Good(Cell cellNext, Cell cellDest, CellPartEnum direction, ArrayList path, ArrayList deadPath,
            bool ignoreDestCharacter, bool ignoreObstacle, bool useStraightAngle)
        {
            //Debug.WriteLine(string.Format("Accept cell {0},{1}", cellNext._row, cellNext._col));
            path.Add(cellNext);

            if (cellNext.Equals(cellDest))
            {
                // end
                //Debug.WriteLine("Reach Destination");
                return true;
            }

            if (useStraightAngle)
                direction = CellPartEnum.Center;
            else
            {
                // change direction if has whole/direct angle
                double degree = FindDegree(cellNext.GetCenterPoint(), cellDest.GetCenterPoint());
                if (Math.Abs(degree) <= 0.05)
                    direction = CellPartEnum.CenterRight;
                else if (Math.Abs(degree - DIGONAL_ANGLE) <= 0.05)
                    direction = CellPartEnum.LowerRight;
                else if (Math.Abs(degree - (180 - DIGONAL_ANGLE)) <= 0.05)
                    direction = CellPartEnum.LowerLeft;
                else if (Math.Abs(degree - 180) <= 0.05)
                    direction = CellPartEnum.CenterLeft;
                else if (Math.Abs(degree - (180 + DIGONAL_ANGLE)) <= 0.05)
                    direction = CellPartEnum.UpperLeft;
                else if (Math.Abs(degree - (360 - DIGONAL_ANGLE)) <= 0.05)
                    direction = CellPartEnum.UpperRight;
            }

            return FindPath_Sub(cellNext, cellDest, path, direction, deadPath, 
                ignoreDestCharacter, ignoreObstacle, useStraightAngle);
        }

        private Cell GetPathNextCell(Cell cell, Cell cellDest, CellPartEnum direction, ArrayList deadPath)
        {
            double degree = FindDegree(cell.GetCenterPoint(), cellDest.GetCenterPoint());

            switch (direction)
            {
                case CellPartEnum.CenterRight:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = GetNextCell(cell, direction2);
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                            {
                                //Debug.WriteLine(string.Format("Change direction to {0},{1},{2}", direction2, cellNextTest._row, cellNextTest._col));
                                return cellNextTest;
                            }
                        }
                    }
                    break;
                case CellPartEnum.CenterLeft:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = GetNextCell(cell, direction2);
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                            {
                                //Debug.WriteLine(string.Format("Change direction to {0},{1},{2}", direction2, cellNextTest._row, cellNextTest._col));
                                return cellNextTest;
                            }
                        }
                    }
                    break;
                case CellPartEnum.LowerRight:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = GetNextCell(cell, direction2);
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                            {
                                //Debug.WriteLine(string.Format("Change direction to {0},{1},{2}", direction2, cellNextTest._row, cellNextTest._col));
                                return cellNextTest;
                            }
                        }
                    }
                    break;
                case CellPartEnum.LowerLeft:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = GetNextCell(cell, direction2);
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                            {
                                //Debug.WriteLine(string.Format("Change direction to {0},{1},{2}", direction2, cellNextTest._row, cellNextTest._col));
                                return cellNextTest;
                            }
                        }
                    }
                    break;
                case CellPartEnum.UpperLeft:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = GetNextCell(cell, direction2);
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                                return cellNextTest;
                        }
                    }
                    break;
                case CellPartEnum.UpperRight:
                    {
                        // get preferred directions
                        ArrayList directions = GetPathFavourDirections(degree, direction);
                        foreach (CellPartEnum direction2 in directions)
                        {
                            Cell cellNextTest = cell._adjacentCells[direction2];
                            if (cellNextTest != null && cellNextTest.HasPassability() && !deadPath.Contains(cellNextTest))
                            {
                                //Debug.WriteLine(string.Format("Change direction to {0},{1},{2}", direction2, cellNextTest._row, cellNextTest._col));
                                return cellNextTest;
                            }
                        }
                    }
                    break;
            }

            Debug.WriteLine("Change direction but no cell found.");
            return null;
        }

        private ArrayList GetPathFavourDirections(double degree, CellPartEnum direction)
        {
            ArrayList directions = new ArrayList();

            switch (direction)
            { 
                case CellPartEnum.CenterRight:
                    if (degree <= 180)
                    {
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.CenterLeft);
                    }
                    else
                    {
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.CenterLeft);
                    }
                    break;
                case CellPartEnum.CenterLeft:
                    if (degree <= 180)
                    {
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.CenterRight);
                    }
                    else
                    {
                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.CenterRight);
                    }
                    break;
                case CellPartEnum.UpperLeft:
                    if (degree <= 180 + DIGONAL_ANGLE)
                    {
                        // preferred
                        directions.Add(CellPartEnum.CenterLeft);
                        directions.Add(CellPartEnum.LowerLeft);

                        // alternative
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.CenterRight);

                        // reverse
                        directions.Add(CellPartEnum.LowerRight);
                    }
                    else
                    {
                        directions.Add(CellPartEnum.UpperRight);
                        directions.Add(CellPartEnum.CenterRight);

                        directions.Add(CellPartEnum.CenterLeft);
                        directions.Add(CellPartEnum.LowerLeft);
                        
                        directions.Add(CellPartEnum.LowerRight);
                    }
                    break;
                case CellPartEnum.UpperRight:
                    if (degree <= 360 - DIGONAL_ANGLE)
                    {
                        // preferred
                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.CenterLeft);

                        // alternative
                        directions.Add(CellPartEnum.CenterRight);
                        directions.Add(CellPartEnum.LowerRight);

                        // reverse
                        directions.Add(CellPartEnum.LowerLeft);
                    }
                    else
                    {
                        directions.Add(CellPartEnum.CenterRight);
                        directions.Add(CellPartEnum.LowerRight);

                        directions.Add(CellPartEnum.UpperLeft);
                        directions.Add(CellPartEnum.CenterLeft);

                        directions.Add(CellPartEnum.LowerLeft);
                    }
                    break;
                case CellPartEnum.LowerLeft:
                    if (degree <= 180 - DIGONAL_ANGLE)
                    {
                        // preferred
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.CenterRight);

                        // alternative
                        directions.Add(CellPartEnum.CenterLeft);
                        directions.Add(CellPartEnum.UpperLeft);

                        // reverse
                        directions.Add(CellPartEnum.UpperRight);
                    }
                    else
                    {
                        // preferred
                        directions.Add(CellPartEnum.CenterLeft);
                        directions.Add(CellPartEnum.UpperLeft);

                        // alternative
                        directions.Add(CellPartEnum.LowerRight);
                        directions.Add(CellPartEnum.CenterRight);

                        // reverse
                        directions.Add(CellPartEnum.UpperRight);
                    }
                    break;
                case CellPartEnum.LowerRight:
                    if (degree <= DIGONAL_ANGLE)
                    {
                        // preferred
                        directions.Add(CellPartEnum.CenterRight);
                        directions.Add(CellPartEnum.UpperRight);

                        // alternative
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.CenterLeft);

                        // reverse
                        directions.Add(CellPartEnum.UpperLeft);
                    }
                    else
                    {
                        // preferred
                        directions.Add(CellPartEnum.LowerLeft);
                        directions.Add(CellPartEnum.CenterLeft);

                        // alternative
                        directions.Add(CellPartEnum.CenterRight);
                        directions.Add(CellPartEnum.UpperRight);

                        // reverse
                        directions.Add(CellPartEnum.UpperLeft);
                    }
                    break;
            }

            return directions;
        }
        #endregion

        #region Find Path (Adjacent empty cell)
        public bool FindPathAdjBest(Cell cellSrc, Cell cellDest,
            bool ignoreDestCharacter, bool ignoreObstacle, out ArrayList path)
        {
            path = null;

            ArrayList directions = new ArrayList();
            FindDirection(cellSrc.GetCenterPoint(), cellDest.GetCenterPoint(), out directions);

            // use fine path
            foreach (CellPartEnum direction in directions)
            {
                ArrayList path3 = new ArrayList();

                if (FindPathAdj(cellSrc, cellDest, direction,
                    ignoreDestCharacter, ignoreObstacle, false,
                    out path3))
                {
                    if (path == null || path3.Count < path.Count)
                    {
                        path = path3;
                    }
                }
            }

            //// use straight path
            //ArrayList path2 = null;
            //if (FindPathAdj(cellSrc, cellDest, (CellPartEnum)directions[0],
            //    ignoreDestCharacter, ignoreObstacle, true,
            //    out path2))
            //{
            //    if (path == null || path2.Count < path.Count)
            //    {
            //        path = path2;
            //    }
            //}

            return true;
        }

        private bool FindPathAdj(Cell cellSrc, Cell cellDest, CellPartEnum staringDirection,
            bool ignoreDestCharacter, bool ignoreObstacle, bool useStraightAngle, out ArrayList path)
        {
            path = null;
            bool hasObstacle = false;

            List<ArrayList> shortestPathAvoids = null;
            if (!FindPathAdj(cellSrc, cellDest, staringDirection,
                ignoreDestCharacter, ignoreObstacle, useStraightAngle,
                out shortestPathAvoids, out hasObstacle))
                return false;

            if (!hasObstacle)
            {
                if (shortestPathAvoids.Count > 0)
                {
                    path = shortestPathAvoids[0];

                    // remove srcCell
                    if (path.Count > 0) path.RemoveAt(0);
                }

                return true;
            }

            // find shortest path from srcCell to pathAvoid
            foreach (ArrayList shortestPathAvoid in shortestPathAvoids)
            {
                if (shortestPathAvoid.Count < 1) continue;

                int index = 0;
                while (index < shortestPathAvoid.Count)
                {
                    Cell cell = (Cell)shortestPathAvoid[index];

                    List<ArrayList> shortestPathAvoids2 = new List<ArrayList>();
                    bool hasObstacle2 = false;
                    if (!FindPathAdj(cellSrc, cell, staringDirection, 
                        false, false, true,
                        out shortestPathAvoids2, out hasObstacle2))
                    {
                        break;
                    }
                    else
                    {
                        foreach (ArrayList shortestPathAvoid2 in shortestPathAvoids2)
                        {
                            if (shortestPathAvoid2.Count - 1 < index)
                            {
                                // shorter found
                                Debug.WriteLine("Shorter found.");

                                // remove previous
                                while (!shortestPathAvoid[0].Equals(cell))
                                {
                                    shortestPathAvoid.RemoveAt(0);
                                }

                                // add cell
                                foreach (Cell cell2 in shortestPathAvoid2)
                                {
                                    shortestPathAvoid.Insert(0, cell2);
                                }
                            }
                        }
                    }
                    index += 1;
                }
            }

            List<ArrayList> shortestPathAvoids3 = null;
            FindShortestPathAvoid(shortestPathAvoids, out shortestPathAvoids3);
            
            path = shortestPathAvoids3[0];

            // remove srcCell
            if (path.Count > 0) path.RemoveAt(0);

            return true;
        }

        private bool FindPathAdj(Cell cellSrc, Cell cellDest, CellPartEnum startingDirection,
            bool ignoreDestCharacter, bool ignoreObstacle, bool useStraightAngle,
            out List<ArrayList> shortestPathAvoids, out bool hasObstacle)
        {
            shortestPathAvoids = new List<ArrayList>();
            hasObstacle = false;

            // source is destination
            if (cellSrc.Equals(cellDest)) return true;

            // destination cannot has character or obstacle
            if (!cellDest.HasPassability())
            {
                if (!ignoreDestCharacter && !ignoreObstacle) return false;
            }

            // find direct path (shortest path)
            ArrayList directPath = new ArrayList();
            if (!FindPath(cellSrc, cellDest, directPath, startingDirection, 
                true, true, true, useStraightAngle)) return false;

            // find all adjacent cells for obstacle
            ArrayList adjCells = new ArrayList();
            ArrayList obsCells = new ArrayList();
            GetAdjCells(directPath, cellDest, ignoreDestCharacter, adjCells, obsCells);

            // if no obstacles
            if (obsCells.Count < 1)
            {
                hasObstacle = false;
                shortestPathAvoids.Add(directPath);
                return true;
            }
            else
            {
                hasObstacle = true;
            }

            // find new path
            List<ArrayList> pathAvoids = null;
            FindPathAvoid(directPath, adjCells, obsCells, cellDest, out pathAvoids);

            if (pathAvoids.Count < 1) return false;

            // get shortest avoid path (might be many)
            FindShortestPathAvoid(pathAvoids, out shortestPathAvoids);

            if (shortestPathAvoids.Count < 1) return false;

            return true;
        }

        private void GetAdjCells(ArrayList directPath, Cell destCell, bool ignoreDestCharacter,
            ArrayList adjCells, ArrayList obstacles)
        {
            int index = 0;
            foreach (Cell cell in directPath)
            {
                if (cell.Equals(destCell) && ignoreDestCharacter)
                {
                    index += 1;
                    continue;
                }

                // index 0 is srcCell
                if (index > 0 && !cell.HasPassability())
                {
                    obstacles.Add(cell);

                    // get all adjacent cells
                    GetAdjCells(directPath, adjCells, obstacles, cell);                
                }

                index += 1;
            }
        }

        private void GetAdjCells(ArrayList directPath, ArrayList adjCells, ArrayList obstacles, Cell directCell)
        {
            foreach (Cell adjCell in directCell._adjacentCells.Values)
            {
                if (adjCell == null) continue;
                if (directPath.Contains(adjCell)) continue;
                if (adjCells.Contains(adjCell)) continue;

                if (!adjCell.HasPassability())
                {
                    if (obstacles.Contains(adjCell)) continue;

                    obstacles.Add(adjCell);
                    GetAdjCells(directPath, adjCells, obstacles, adjCell);
                }
                else
                    adjCells.Add(adjCell);
            }
        }

        private void FindPathAvoid(ArrayList directPath, ArrayList adjCells, ArrayList obsCells, Cell destCell,
            out List<ArrayList> pathAvoidLst)
        {
            pathAvoidLst = new List<ArrayList>();
            pathAvoidLst.Add(new ArrayList());
            int index = 0;
            ArrayList pathAvoid = null;
            while (index < pathAvoidLst.Count)
            {
                pathAvoid = (ArrayList)pathAvoidLst[index];

                if (pathAvoid.Count > 0 && pathAvoid[pathAvoid.Count - 1].Equals(destCell))
                {
                    index += 1;
                    continue;
                }
                else
                {
                    FindPathAvoid_Sub(directPath, adjCells, obsCells, pathAvoidLst, pathAvoid);
                    index = 0;
                }
            }
        }

        private void FindPathAvoid_Sub(ArrayList directPath, ArrayList adjCells, ArrayList obsCells,
            List<ArrayList> pathAvoidLst, ArrayList pathAvoid)
        {
            int index = 0;

            // get last index of avoid cell in direct path
            if (pathAvoid.Count > 0 && directPath.Contains(pathAvoid[pathAvoid.Count - 1]))
            {
                index = directPath.IndexOf(pathAvoid[pathAvoid.Count - 1]);
                index += 1;
            }

            while (index < directPath.Count)
            {
                Cell prevCell = null;
                if (pathAvoid.Count > 0)
                    prevCell = (Cell)pathAvoid[pathAvoid.Count - 1];

                Cell cell = (Cell)directPath[index];

                // index 0 is srcCell
                if (index == 0 || cell.HasPassability())
                {
                    pathAvoid.Add((Cell)directPath[index]);
                }
                else
                {
                    Debug.WriteLine(string.Format("PA_Sub, b4Obs:{0},{1} Obs:{2},{3}", prevCell._row, prevCell._col,
                        cell._row, cell._col));

                    FindPathAvoid_SubAdj(directPath, adjCells, obsCells,
                        pathAvoidLst, pathAvoid, prevCell);
                    return;
                }

                index += 1;
            }
        }

        private void FindPathAvoid_SubAdj(ArrayList directPath, ArrayList adjCells, ArrayList obsCells,
            List<ArrayList> pathAvoidLst, ArrayList pathAvoid, Cell prevCell)
        {
            ArrayList pathAvoid2 = null;
            foreach (Cell adjCell in prevCell._adjacentCells.Values)
            {
                if (pathAvoid.Contains(adjCell)) continue;
                if (obsCells.Contains(adjCell)) continue;
                if (!directPath.Contains(adjCell) && !adjCells.Contains(adjCell)) continue;

                Debug.WriteLine(string.Format("PA_SubAdj:{0},{1}", adjCell._row, adjCell._col));
                {
                    // clone a new pathAvoid
                    pathAvoid2 = new ArrayList();
                    foreach (Cell cellClone in pathAvoid)
                    {
                        pathAvoid2.Add(cellClone);
                    }

                    pathAvoidLst.Add(pathAvoid2);
                }

                if (directPath.Contains(adjCell))
                {
                    pathAvoid2.Add(adjCell);
                    continue;
                }

                pathAvoid2.Add(adjCell);

                FindPathAvoid_SubAdj(directPath, adjCells, obsCells, pathAvoidLst, pathAvoid2, adjCell);
            }

            pathAvoidLst.Remove(pathAvoid);
        }

        private void FindShortestPathAvoid(List<ArrayList> pathAvoids, out List<ArrayList> shortestPathAvoids)
        {
            shortestPathAvoids = new List<ArrayList>();

            ArrayList shortestPathAvoid = null;
            int count = 0;
            foreach (ArrayList pathAvoid in pathAvoids)
            {
                if (shortestPathAvoid == null)
                {
                    count = pathAvoid.Count;
                    shortestPathAvoid = pathAvoid;
                }
                else if (pathAvoid.Count < count)
                {
                    count = pathAvoid.Count;
                    shortestPathAvoid = pathAvoid;

                    shortestPathAvoids.Clear();
                }
                else if (pathAvoid.Count == count)
                {
                    shortestPathAvoids.Add(pathAvoid);
                }
            }

            shortestPathAvoids.Add(shortestPathAvoid);
        }
        #endregion

        private CellPartEnum GetClockwiseDirection(CellPartEnum direction)
        {
            switch (direction)
            {
                case CellPartEnum.CenterRight:
                    return CellPartEnum.LowerRight;
                case CellPartEnum.LowerRight:
                    return CellPartEnum.LowerLeft;
                case CellPartEnum.LowerLeft:
                    return CellPartEnum.CenterLeft;
                case CellPartEnum.CenterLeft:
                    return CellPartEnum.UpperLeft;
                case CellPartEnum.UpperLeft:
                    return CellPartEnum.UpperRight;
                case CellPartEnum.UpperRight:
                    return CellPartEnum.CenterRight;
                default:
                    return CellPartEnum.Center;
            }
        }

        private CellPartEnum GetAntiClockwiseDirection(CellPartEnum direction)
        {
            switch (direction)
            {
                case CellPartEnum.CenterRight:
                    return CellPartEnum.UpperRight;
                case CellPartEnum.UpperRight:
                    return CellPartEnum.UpperLeft;
                case CellPartEnum.UpperLeft:
                    return CellPartEnum.CenterLeft;
                case CellPartEnum.CenterLeft:
                    return CellPartEnum.LowerLeft;
                case CellPartEnum.LowerLeft:
                    return CellPartEnum.LowerRight;
                case CellPartEnum.LowerRight:
                    return CellPartEnum.CenterRight;
                default:
                    return CellPartEnum.Center;
            }
        }

        public static CellPartEnum GetOppositeDirection(CellPartEnum direction)
        {
            switch (direction)
            {
                case CellPartEnum.CenterLeft:
                    return CellPartEnum.CenterRight;
                case CellPartEnum.CenterRight:
                    return CellPartEnum.CenterLeft;
                case CellPartEnum.LowerRight:
                    return CellPartEnum.UpperLeft;
                case CellPartEnum.LowerLeft:
                    return CellPartEnum.UpperRight;
                case CellPartEnum.UpperRight:
                    return CellPartEnum.LowerLeft;
                case CellPartEnum.UpperLeft:
                    return CellPartEnum.LowerRight;
                default:
                    return CellPartEnum.Center;
            }
        }

    }
}
