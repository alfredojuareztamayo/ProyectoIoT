using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
public class Maze : MonoBehaviour
{

    /// <summary>
    /// height and width of the maze
    /// </summary>
    [Tooltip("This is the depth of your maze")]
    public int depth = 30;
    [Tooltip("This is the Width of your maze")]
    public int width = 30;

    public byte[,] map;

    [Tooltip("This is the scale of your maze")]
    public int scale = 6;


    [Tooltip("This is the your main character, always will spawn in the start of the maze")]
    public GameObject player;


    public List<MapLocation> direction = new List<MapLocation>()
    {
    new MapLocation(0,1),
    new MapLocation(0,-1),
    new MapLocation(1,0),
    new MapLocation(-1,0)
    };

    public GameObject[] itemsPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
        InstantiateItems();
    }
    /// <summary>
    /// Function to initialise the map 
    /// </summary>
    void InitialiseMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                    map[x, z] = 1;
                // 1 = wall and 0= corridor
            }
        }
    }
    
    /// <summary>
    /// Function
    /// </summary>
    public virtual void Generate()
    {
        
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    map[x, z] = 0;
                }

            }
        }
        
    }
    /// <summary>
    /// Function to fill or draw the map
    /// </summary>

    void DrawMap()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new (x*scale, 0, z*scale);
                if (map[x, z] == 1)
                {
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new(scale,scale,scale);
                    wall.transform.position = pos;
                }

            }
        }
    }

    void InstantiateItems()
    {
        int numberItems = itemsPrefabs.Length;
        int iter = 0;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new(x * scale, -1.5f, z * scale);
                //if (x == 1 && z == 1) continue;
                if (map[x, z] == 0 && iter < numberItems)
                {
                    if(x > width- numberItems && z > depth- numberItems)
                    {
                        GameObject item = Instantiate(itemsPrefabs[iter]);
                        item.transform.position = pos;
                        x = width - 1;
                        z += 1;
                        iter++;
                    }
                    if(Random.Range(1,100) > 70)
                    {
                        GameObject item = Instantiate(itemsPrefabs[iter]);
                        item.transform.position = pos;
                        x = width - 1;
                        z += 1;
                        iter++;
                    }
                    
                }

            }
        }
    }
    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x-1,z]==0) count++;
        if (map[x+1,z]==0) count++;
        if (map[x,z+1]==0) count++;
        if (map[x,z-1]==0) count++;

        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x+1,z+1]==0) count++;
        if (map[x+1,z-1]==0 ) count++;
        if (map[x-1, z+1] == 0) count++;
        if (map[x-1,z-1] == 0) count++;
        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountDiagonalNeighbours(x, z) + CountSquareNeighbours(x,z);
    }
}
