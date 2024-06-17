using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMaze : Maze
{
    public int positionPlayerInitX;
    public int positionPlayerInitZ;
    public override void Generate()
    {
        int x_x = positionPlayerInitX;
        int z_z = positionPlayerInitZ;
        map[x_x, z_z] = 1;
        Stack<MapLocation> stack = new Stack<MapLocation>();
        MapLocation start = new MapLocation(Random.Range(1, width), Random.Range(1, depth));
        stack.Push(start);

        while (stack.Count > 0)
        {
            MapLocation current = stack.Pop();
            int x = current.x;
            int z = current.z;

            if (CountSquareNeighbours(x, z) < 2)
            {
                map[x, z] = 0;
                direction.Shuffle();

                foreach (var dir in direction)
                {
                    int newX = x + dir.x;
                    int newZ = z + dir.z;
                    if (IsInBounds(newX, newZ) && map[newX, newZ] == 1)
                    {
                        stack.Push(new MapLocation(newX, newZ));
                    }
                }
            }
        }
    }

    private bool IsInBounds(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < depth;
    }
}
