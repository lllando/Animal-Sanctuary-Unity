using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static int MapWidth;
    public static int MapHeight;

    [Header("Objects")]

    [SerializeField] private Tilemap topMap;

    [SerializeField] private Tilemap bottomMap;

    [SerializeField] private Tile topTile;

    [SerializeField] private Tile bottomTile;

    [SerializeField] private Transform resourceObjectParent;

    [SerializeField] private Transform environmentObjectParent;

    [SerializeField] private Transform animalObjectParent;

    [Header("Parameters")]

    [SerializeField] MapParameters terrainParameters;

    [Header("Prefabs")]

    [SerializeField] private RandomPrefab[] resourcePrefabs;

    [SerializeField] private RandomPrefab[] environmentPrefabs;

    [SerializeField] private RandomPrefab[] animalPrefabs;

    private int[,] _terrainMap;

    public Vector3Int _tMapSize;

    private int _width;
    private int _height;

    private void Start()
    {
        MapWidth = _width;
        MapHeight = _height;

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

            InitialisePosition(_terrainMap, terrainParameters);

            Debug.Log("Intialising Position");
        }

        for (int i = 0; i < terrainParameters.iterations; i++)
        {
            _terrainMap = GenerateTilePosition(_terrainMap, terrainParameters);
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_terrainMap[x, y] == 1)
                {
                    Vector3Int position = new Vector3Int(-x + _width / 2, -y + _height / 2, 0);

                    topMap.SetTile(position, topTile);
                    Debug.Log("Tile Assigned");

                    Bounds bounds = GameManager.PlayerController.mapBounds;

                    if(position.x > bounds.min.x && position.y > bounds.min.y && position.x < bounds.max.x && position.y < bounds.max.y)
                    {
                        bool resourceSpawned = false;

                        for (int i = 0; i < resourcePrefabs.Length; i++)
                        {
                            float random = Random.Range(0, 10000);

                            if (resourcePrefabs[i].rarityThreshold >= random)
                            {
                                Instantiate(resourcePrefabs[i].prefab, position, Quaternion.identity, resourceObjectParent);
                                resourceSpawned = true;
                                break;
                            }
                        }

                        bool animalSpawned = false;

                        if(!resourceSpawned)
                        {
                            for (int i = 0; i < environmentPrefabs.Length; i++)
                            {
                                float random = Random.Range(0, 10000);

                                if (animalPrefabs[i].rarityThreshold >= random)
                                {
                                    Instantiate(animalPrefabs[i].prefab, position, Quaternion.identity, animalObjectParent);
                                    animalSpawned = true;
                                    break;
                                }
                            }
                        }

                        if (!animalSpawned)
                        {
                            for (int i = 0; i < environmentPrefabs.Length; i++)
                            {
                                float random = Random.Range(0, 10000);

                                if (environmentPrefabs[i].rarityThreshold >= random)
                                {
                                    Instantiate(environmentPrefabs[i].prefab, position, Quaternion.identity, environmentObjectParent);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    bottomMap.SetTile(new Vector3Int(-x + _width / 2, -y + _height / 2, 0), bottomTile);
                    Debug.Log("fail: " + _terrainMap[x, y]);
                }
            }
        }
    }

    private int[,] GenerateTilePosition(int[,] oldMap, MapParameters mapParams)
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
                    if (neighbour < mapParams.deathLimit)
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
                    if (neighbour > mapParams.birthLimit)
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

    private void InitialisePosition(int[,] tileMap, MapParameters mapParams)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                tileMap[x, y] = Random.Range(1, 101);

                if (tileMap[x, y] < mapParams.initialChance)
                {
                    tileMap[x, y] = 1;
                }
                else
                {
                    tileMap[x, y] = 0;
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

[System.Serializable]
public struct MapParameters
{
    [Range(0, 100)] public int initialChance;

    [Range(1, 8)] public int birthLimit;

    [Range(1, 8)] public int deathLimit;

    [Range(1, 10)] public int iterations;
}

[System.Serializable]
public struct RandomPrefab
{
    public Transform prefab;

    [Range(0, 101)] public int rarityThreshold;
}
