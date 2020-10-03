using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int sections;
    public Vector2Int dimensions;
    public float cellDimensions;
    public Cell cell;
    public GameObject checkpoint;

    private Cell[,] maze;
    private Stack<Vector2Int> visits;

    private void Start()
    {
        maze = CreateMazeBase(Vector2.zero);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateMazePaths(maze);
        }
    }

    private Cell[,] CreateMazeBase(Vector2 startPos)
    {
        Cell[,] mazeGrid = new Cell[dimensions.x, dimensions.y];

        float startX = startPos.x;
        float startY = startPos.y;

        for (int r = 0; r < dimensions.x; r++)
        {
            for (int c = 0; c < dimensions.y; c++)
            {
                Cell box = Instantiate(cell);
                mazeGrid[r, c] = box;
                box.Create(cellDimensions);
                box.transform.position = new Vector3(startX, 0f, startY);
                startX += cellDimensions;
            }
            startX = startPos.x;
            startY -= cellDimensions;
        }

        return mazeGrid;
    }

    private Vector2 CreateMazePaths(Cell[,] m)
    {
        Vector2Int currentPos = new Vector2Int(0, 0);
        Vector2 exitPos = new Vector2();
        visits = new Stack<Vector2Int>();
        visits.Push(currentPos);
        
        bool complete = false;

        m[currentPos.x, currentPos.y].Visited = true;

        while (!complete)
        {
            bool success = Kill(m, ref currentPos, visits);
            if (!success)
            {
                complete = Hunt(m, visits, ref currentPos);
            }
        }

        //Now we need our exit, so look through all the cells in the last row
        

        return exitPos;
    }

    private bool Hunt(Cell[,] maze, Stack<Vector2Int> s, ref Vector2Int currentPos)
    {
        bool ended = true;
        int possibleExits = 0;

        while (s.Count > 0)
        {
            Vector2Int tryCell = s.Pop();
            possibleExits = AvailablePath(maze, tryCell).Count;
            if (possibleExits > 0)
            {
                currentPos = tryCell;
                return false;
            }
        }

        return ended;
    }

    private bool Kill(Cell[,] maze, ref Vector2Int index, Stack<Vector2Int> s)
    {
        Debug.Log($"Kill {index.x}, {index.y}");
        List<Vector2Int> exits = AvailablePath(maze, index);

        if (exits.Count == 0)
        {
            return false;
        }

        //Pick a random possible cell
        int i = Random.Range(0, exits.Count);
        //Debug.Log($"Chose {i}: {exits[i].x}, {exits[i].y} as neighbor out of {exits.Count} possible choices");
        Connect(maze, index, exits[i]);

        s.Push(exits[i]);
        index = exits[i];
        Debug.Log($"New index {index.x}, {index.y}");
        maze[index.x, index.y].Visited = true;

        return true;
    }

    private void Connect(Cell[,] maze, Vector2Int index1, Vector2Int index2)
    {
        //left/right or front/back?
        if (index1.x - index2.x != 0)
        {
            //up/down
            if (index1.x < index2.x)
            {
                maze[index1.x, index1.y].RemoveWall(Cell.Wall.Back);
                maze[index2.x, index2.y].RemoveWall(Cell.Wall.Front);

                //Debug.Log($"Removing {index1.x}, {index1.y} back wall; {index2.x}, {index2.y} front wall");
            }
            else
            {
                maze[index1.x, index1.y].RemoveWall(Cell.Wall.Front);
                maze[index2.x, index2.y].RemoveWall(Cell.Wall.Back);
                //Debug.Log($"Removing {index1.x}, {index1.y} front wall; {index2.x}, {index2.y} back wall");
            }
        }
        else
        {
            //right/left
            if (index1.y < index2.y)
            {
                maze[index1.x, index1.y].RemoveWall(Cell.Wall.Right);
                maze[index2.x, index2.y].RemoveWall(Cell.Wall.Left);
                //Debug.Log($"Removing {index1.x}, {index1.y} right wall; {index2.x}, {index2.y} left wall");
            }
            else
            {
                maze[index1.x, index1.y].RemoveWall(Cell.Wall.Left);
                maze[index2.x, index2.y].RemoveWall(Cell.Wall.Right);
                //Debug.Log($"Removing {index1.x}, {index1.y} left wall; {index2.x}, {index2.y} right wall");
            }
        }
    }

    private List<Vector2Int> AvailablePath(Cell[,] m, Vector2Int i)
    {
        List<Vector2Int> exits = new List<Vector2Int>();


        //front - x-1
        //right - y+1
        //back - x+1
        //left - y-1

        //not in top row
        if (i.x - 1 >= 0)
        {
            //Debug.Log($"{i.x}, {i.y} is not in the top row ({i.x} - 1 = {i.x - 1} bigger than/equal to 0)");
            if (!m[i.x - 1, i.y].Visited)
            {
                //Debug.Log("Added above");
                exits.Add(new Vector2Int(i.x - 1, i.y));
            }
        }
        //not in rightmost row
        if (i.y + 1 < m.GetLength(1))
        {
            //Debug.Log($"{i.x}, {i.y} is not in the rightmost row ({i.y} + 1 = {i.y + 1} smaller than {m.GetLength(1)})");
            if (!m[i.x, i.y + 1].Visited)
            {
                //Debug.Log("Added right");
                exits.Add(new Vector2Int(i.x, i.y + 1));
            }
        }
        //not in bottom row
        if (i.x + 1 < m.GetLength(1))
        {
            //Debug.Log($"{i.x}, {i.y} is not in the bottom row ({i.x} + 1 = {i.x + 1} smaller than {m.GetLength(1)})");
            if (!m[i.x + 1, i.y].Visited)
            {
                //Debug.Log("Added below");
                exits.Add(new Vector2Int(i.x + 1, i.y));
            }
        }
        //not in leftmost row
        if (i.y - 1 >= 0)
        {
            //Debug.Log($"{i.x}, {i.y} is not in the leftmost row ({i.y} - 1 = {i.y - 1} bigger than/equal to 0)");
            if (!m[i.x, i.y - 1].Visited)
            {
                //Debug.Log("Added left");
                exits.Add(new Vector2Int(i.x, i.y - 1));
            }
        }

        return exits;
    }
}
