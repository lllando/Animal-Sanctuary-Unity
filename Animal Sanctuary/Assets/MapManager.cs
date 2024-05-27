using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [Header("Objects")]

    [SerializeField] private Tilemap topMap;

    [SerializeField] private Tilemap bottomMap;

    [SerializeField] private Tile topTile;

    [SerializeField] private Tile bottomTile;

    [Header("Parameters")]

    [Range(0, 100)][SerializeField] private int initialChance;

    [Range(1, 8)][SerializeField] private int birthLimit;

    [Range(1, 8)][SerializeField] private int deathLimit;

    [Range(1, 10)][SerializeField] private int numR;

    private int _count;

    private int[,] _terrainMap;
    public Vector3Int _tMapSize;

    private int _width;
    private int _height;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        ClearMap(false);

        _width = _tMapSize.x;
        _height = _tMapSize.y;

        Debug.Log("Width: " + _width + " Height: " + _height);

        if (_terrainMap == null)
        {
            _terrainMap = new int[_width, _height];
            InitialisePosition();

            Debug.Log("Intialising Position");
        }

        for (int i = 0; i < numR; i++)
        {
            _terrainMap = GenerateTilePosition(_terrainMap);
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_terrainMap[x, y] == 1)
                {
                    topMap.SetTile(new Vector3Int(-x + _width / 2, -y + _height / 2, 0), topTile);
                    Debug.Log("Tile Assigned");
                }
                else
                {
                    bottomMap.SetTile(new Vector3Int(-x + _width / 2, -y + _height / 2, 0), bottomTile);
                    Debug.Log("fail: " + _terrainMap[x, y]);
                }
            }
        }
    }

    private int[,] GenerateTilePosition(int[,] oldMap)
    {
        int[,] newMap = new int[_width, _height];

        int neighbour = 0;
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                neighbour = 0;

                foreach (var pos in bounds.allPositionsWithin)
                {
                    if (pos.x == 0 && pos.y == 0)
                    {
                        continue;
                    }

                    if (x + pos.x >= 0 && x + pos.x < _width && y + pos.y >= 0 && y + pos.y < _height)
                    {
                        neighbour += oldMap[x + pos.x, y + pos.y];
                    }
                    else
                    {
                        neighbour++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighbour < deathLimit)
                    {
                        newMap[x, y] = 0;
                    }
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighbour > birthLimit)
                    {
                        newMap[x, y] = 1;
                    }
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }

        return newMap;
    }

    private void InitialisePosition()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _terrainMap[x, y] = Random.Range(1, 101);

                if (_terrainMap[x, y] < initialChance)
                {
                    _terrainMap[x, y] = 1;
                }
                else
                {
                    _terrainMap[x, y] = 0;
                }
            }
        }
    }

    private void ClearMap(bool complete)
    {
        topMap.ClearAllTiles();
        bottomMap.ClearAllTiles();

        if (complete)
        {
            _terrainMap = null;
        }
    }

}
