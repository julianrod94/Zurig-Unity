using System;
using Boo.Lang;
using UnityEngine;

public class Maze {
    public enum MazePart {
        None = 0,
        Start,
        End,
        Block,
        Key, 
        Pipe,
        PipeTurning,
        Turning
    }

    private MazePart[,] _maze;
    public int Rows;
    public int Columns;
    public Maze(int rows, int columns) {
        _maze = new MazePart[rows,columns];
        this.Rows = rows;
        this.Columns = columns;
    }

    public MazePart this[int index] {
        get { return _maze[index / Rows,index % Rows];}
        set { _maze[index / Rows,index % Rows] = value;  }
    }
    
    public MazePart this[int row, int column] {
        get { return _maze[row,column];}
        set { _maze[row,column] = value;  }
    }

    public List<int> UnvisitedNeighbours(int index) {
        var row = index / Rows;
        var column = index % Rows;
        
        if (row % 2 == column % 2) {
            throw new ArgumentException("This is an empty or turning space");
        }
        var posibleValues = new List<int>();
        int[,] directions;
        if (row % 2 != 0) {
            directions = new int[,] {{-1, 1}, {0, 2}, {1, 1}, {0, -2}, {-1, -1}, {-1, 1}};
        } else {
            directions = new int[,] {{-1, 1}, {2, 0}, {1, 1}, {-2, 0}, {-1, -1}, {-1, 1}};
        }
        
        for (var i = 0; i < 6; i++) {
            var newRow = row + directions[i,0];
            var newColumn = column + directions[i,1];
            if(!isInside(newRow, newColumn)) { continue; }

            if (_maze[newRow, newColumn] == MazePart.None) {
                posibleValues.Add(newRow * Rows + newColumn);
            }
        }

        return posibleValues;
    }
   

    public bool isInside(int row, int column) {
        return row >= 0 && row < Rows && column >= 0 && column < Columns;
    }


}
