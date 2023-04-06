using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    private const int BORDER_TILES_BUFFER = 2;

    [SerializeField]
    private float TileEdgesSmootingOffset = 2.0f;

    public Vector3 mapStartingPosition = Vector3.zero;

    public int mapSize = 10;                                    // this has to be +2 because of the border tiles
    public bool hasBorder = true;
    public List<GameObject> tileset = new List<GameObject>();   // tiles for the map
    public List<int> tilesWeights = new List<int>();            // weight for every tile
    public GameObject borderTile;
    public GameObject mapParentObject;

    private int sumOfWeights = 0;
    private int endMapSize;
    private float tileSize;                               // this has to be set based on used prefabs
    private bool isMapAlreadyCreated = false;

    private List<List<GameObject>> tilemap = new List<List<GameObject>>();

    public void CreateMap()
    {
        if(mapParentObject == null)
        {
            Debug.LogError("Set empty map parent object.");
            return;
        }
        if(mapParentObject.transform.childCount > 0)
        {
            Debug.LogError("This object already has other children.");
            return;
        }

        if (isMapAlreadyCreated)
        {
            Debug.LogError("Map has already been created.");
            return;
        }

        tileSize = tileset[0].GetComponent<MeshFilter>().sharedMesh.bounds.size.x - TileEdgesSmootingOffset;
        Debug.Log($"Tile size is: {tileSize}");
        NormalizeWeightsAndTilesetValues();
        if(hasBorder)
        {
            if(borderTile == null)
            {
                Debug.LogError("Border tile is not set!");
                return;
            }
            endMapSize = mapSize + BORDER_TILES_BUFFER;
        }
        else
        {
            endMapSize = mapSize;
        }
        sumOfWeights = tilesWeights.Sum();

        tilemap.Clear();
        tilemap = new List<List<GameObject>>(endMapSize);

        if (hasBorder)
        {
            // add first border column
            var firstColumn = new List<GameObject>(endMapSize);

            for (int z = 0; z < endMapSize; z++)
            {
                var zPosition = mapStartingPosition.z + z * tileSize;

                var borderTileForColumn = (GameObject)Instantiate(borderTile as GameObject, mapParentObject.transform);
                borderTileForColumn.transform.position = new Vector3(mapStartingPosition.x, mapStartingPosition.y, zPosition);
                borderTileForColumn.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                firstColumn.Add(borderTileForColumn);
            }
            tilemap.Add(firstColumn);

            // add middle columns
            for (int x = 1; x < endMapSize - 1; x++) // columns
            {
                var column = new List<GameObject>(endMapSize);
                var xPosition = mapStartingPosition.x + x * tileSize;

                var borderTileForColumnTop = (GameObject)Instantiate(borderTile as GameObject, mapParentObject.transform);
                borderTileForColumnTop.transform.position = new Vector3(xPosition, mapStartingPosition.y, mapStartingPosition.z);
                borderTileForColumnTop.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                column.Add(borderTileForColumnTop);

                for (int z = 1; z < endMapSize - 1; z++) // rows
                {
                    var randomizedFieldType = RandomizeField();
                    var tile = (GameObject)Instantiate(tileset[randomizedFieldType] as GameObject, mapParentObject.transform);
                    var zPosition = mapStartingPosition.z + z * tileSize;
                    tile.transform.position = new Vector3(xPosition, mapStartingPosition.y, zPosition);
                    tile.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                    column.Add(tile);
                }

                var borderTileForColumnBottom = (GameObject)Instantiate(borderTile as GameObject, mapParentObject.transform);
                borderTileForColumnBottom.transform.position = new Vector3(xPosition, mapStartingPosition.y, mapStartingPosition.z + tileSize * (endMapSize - 1));
                borderTileForColumnBottom.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                column.Add(borderTileForColumnBottom);

                tilemap.Add(column);
            }

            // add last border column
            var lastColumn = new List<GameObject>(endMapSize);

            for (int z = 0; z < endMapSize; z++)
            {
                var zPosition = mapStartingPosition.z + z * tileSize;

                var borderTileForColumn = (GameObject)Instantiate(borderTile as GameObject, mapParentObject.transform);
                borderTileForColumn.transform.position = new Vector3(mapStartingPosition.x + tileSize * (endMapSize - 1), mapStartingPosition.y, zPosition);
                borderTileForColumn.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                lastColumn.Add(borderTileForColumn);
            }
            tilemap.Add(lastColumn);

            isMapAlreadyCreated = true;
            return;
        }

        // set grid without borders
        for (int x = 0; x < endMapSize; x++) // columns
        {
            var column = new List<GameObject>(endMapSize);
            var xPosition = mapStartingPosition.x + x * tileSize;

            for (int z = 0; z < endMapSize; z++) // rows
            {
                var randomizedFieldType = RandomizeField();
                var tile = (GameObject)Instantiate(tileset[randomizedFieldType] as GameObject, mapParentObject.transform);
                var zPosition = mapStartingPosition.z + z * tileSize;
                tile.transform.position = new Vector3(xPosition, mapStartingPosition.y, zPosition);
                tile.transform.Rotate(0, 90 * Mathf.CeilToInt(RandomGaussian(0, 3)), 0);
                column.Add(tile);
            }
            tilemap.Add(column);
        }
        isMapAlreadyCreated = true;
        return;
    }

    private void NormalizeWeightsAndTilesetValues()
    {
        // these values change and I need to have them unchanged
        var startTilesWeightsCount = tilesWeights.Count; 
        var startTilesetCount = tileset.Count;

        if (startTilesWeightsCount > startTilesetCount)
        {
            Debug.LogError("There are weights in the tileWeights without a corresponding tile.");
            for (int i = 0; i < startTilesWeightsCount - startTilesetCount; i++)
            {
                tilesWeights.RemoveAt(tilesWeights.Count - 1); // remove excess values (for example when there are 4 different tiles and 5 different weight values)
                tilesWeights.TrimExcess();
                Debug.Log($"There are {tilesWeights.Count} elements in the tilesWeights.");
            }
            return;
        }
        if(startTilesetCount > startTilesWeightsCount) 
        {
            Debug.LogError("There are tiles in the tilesets without a corresponding weight.");
            for (int i = 0; i < startTilesetCount - startTilesWeightsCount; i++)
            {
                tileset.RemoveAt(tileset.Count - 1); // remove excess values (for example when there are 4 different tiles and 5 different weight values)
                tileset.TrimExcess();
                Debug.Log($"There are {tileset.Count} elements in the tileset.");
            }
            return;
        }
        return;
    }
    
    // returns an index of the prefab
    private int RandomizeField()
    {
        return CheckFieldTypeBasedOnWeights(RandomGaussian(0, sumOfWeights));
    }

    private int CheckFieldTypeBasedOnWeights(float randomizedWeightValue)
    {
        var currentSum = 0;
        for(int index = 0; index < tilesWeights.Count; index++)
        {
            currentSum += tilesWeights[index];
            //Debug.Log(currentSum);
            if(currentSum > randomizedWeightValue)
            {
                return index;
            }
        }
        return 0; // return first tile by default
    }

    private float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

}
