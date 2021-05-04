using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;

public class mapLoader : MonoBehaviour
{
    public GameObject basicMap;
    public GameObject player;
    public int size;
    public int tileSize = 3;

    List<GameObject> maps;

    int lastPlayerTileX = 0, lastPlayerTileZ = 0;

    // Int between 0 and 7, where the player is looking at (0=front, 1=front-left, 2=left, 3=back-left...)
    int lastPlayerAngle = 0;

    // Part of the 360° rotation to discretise
    private int angleCount;

    private MapRenderer basicMapRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        angleCount = size*2*4;

    	// Creating some maps
        basicMapRenderer = basicMap.GetComponent<MapRenderer>();
    	maps = new List<GameObject>();
        for(int x = -size; x <= size; x++)
        {
            for(int z = -1; z <= 2*size - 1; z++)
            {
                GameObject tile = Instantiate(basicMap, 
                    new Vector3(x * tileSize, transform.position.y, z * tileSize), 
                    Quaternion.Euler(0, 0, 0), this.transform);
                TransformWorldPointToLatLon(tile);

                maps.Add(tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(-0.005f,0f,0.002f);
        //Vector3 mapCenter = transform.position;
        Vector3 playerCenter = player.transform.position;

        // Get current player data : player position relative to a size x size grid + direction angle 
        int playerTileX = Mathf.FloorToInt((playerCenter.x+tileSize/2f)/(float)tileSize);
        int playerTileZ = Mathf.FloorToInt((playerCenter.z+tileSize/2f)/(float)tileSize);
        int playerAngle = normalizePlayerAngle(player.transform.rotation.eulerAngles.y);

        // If one of the value changed, updated tile display
        if(playerTileX != lastPlayerTileX || playerTileZ != lastPlayerTileZ || playerAngle != lastPlayerAngle){
            Debug.Log($"ppos=({playerTileX}, {playerTileZ}) rot={playerAngle}");
            updateTiles(playerAngle, playerTileX, playerTileZ);
            lastPlayerTileX = playerTileX;
            lastPlayerTileZ = playerTileZ;
            lastPlayerAngle = playerAngle;
        }
    }

    /**
    Update tiles display
    Find the bounds that must be displayed relative to player's angle and grid position (params)
    Remove tiles out of bounds
    Create tiles inexistantes inbounds
    **/
    private void updateTiles(int playerAngle, int playerTileX, int playerTileZ){
        float realAngle = playerAngle*360f * Mathf.Deg2Rad/(float)angleCount;
        // Bounds
        float xMin, xMax, zMin, zMax;
        // Player position
        float xCenter = playerTileX;
        float zCenter = playerTileZ;

        
        Debug.Log($"tileCenter : ({playerTileX + Mathf.Sin(realAngle)*size}, {playerTileZ + Mathf.Cos(realAngle)*size})");

        float floatingCenterX = playerTileX + Mathf.Sin(realAngle)*(size-1);
        float floatingCenterZ = playerTileZ + Mathf.Cos(realAngle)*(size-1);

        float tileCenterX = Mathf.Sign(floatingCenterX)*(int)(Mathf.Abs(floatingCenterX)+0.9f);
        float tileCenterZ = Mathf.Sign(floatingCenterZ)*(int)(Mathf.Abs(floatingCenterZ)+0.9f);

        Debug.Log($"tileCenter : ({tileCenterX}, {tileCenterZ})");

        xMin = (tileCenterX-size)*tileSize;
        xMax = (tileCenterX+size)*tileSize;
        zMin = (tileCenterZ-size)*tileSize;
        zMax = (tileCenterZ+size)*tileSize;

        // Remove out of bounds tiles and find current shown bounds
        float currentMaxX = xMin, currentMinX = xMax, currentMaxZ = zMin, currentMinZ = zMax;
        for(int i = 0; i < maps.Count; i++){
            GameObject tile = maps[i];
            Vector3 position = tile.transform.position;
            if(position.x < xMin || position.x > xMax || position.z < zMin || position.z > zMax){
                // Remove the object
        			Destroy(maps[i]);
                    maps.RemoveAt(i);
                    i--;
            } else {
                if(currentMaxX < position.x){
                    currentMaxX = position.x;
                } 
                if(currentMinX > position.x){
                    currentMinX = position.x;
                }
                if(currentMaxZ < position.z){
                    currentMaxZ = position.z;
                }
                if(currentMinZ > position.z){
                    currentMinZ = position.z;
                }
            }
        }

        Debug.Log($"nBounds[x:{xMin}-{xMax}, z:{zMin}-{zMax}] cBounds[x:{currentMinX}-{currentMaxX}, z:{currentMinZ}-{currentMaxZ}]");

        // Fill tiles inexistants and inbounds
        for(float x = currentMaxX+tileSize; x <= xMax; x+= tileSize){
            for(float z = zMin; z <= zMax; z+= tileSize){
                createMap(x,z);
            }
        }
        for(float x = xMin; x <= currentMinX-tileSize; x += tileSize){
            for(float z = zMin; z <= zMax; z+= tileSize){
                createMap(x,z);
            }
        }
        for(float z = currentMaxZ+tileSize; z <= zMax; z+= tileSize){
            for(float x = xMin; x <= xMax; x+= tileSize){
                createMap(x,z);
            }
        }
        for(float z = zMin; z <= currentMinZ-tileSize; z+= tileSize){
            for(float x = xMin; x <= xMax; x+= tileSize){
                createMap(x,z);
            }
        }
    } 

    /**
    Create a tile map at (x,0,z) coord and ask the api to populate it
    **/
    private void createMap(float x, float z){
        GameObject tile = Instantiate(basicMap, new Vector3(x, transform.position.y, z), transform.rotation, transform);
        TransformWorldPointToLatLon(tile);
        maps.Add(tile);
    }

    /**
    Normalized the player angle from degree base to an enumeration base (see player angle def)
    */
    private int normalizePlayerAngle(float angle){
        float anglePart = 360f/(float)angleCount;
        angle += anglePart/2f;
        while(angle < 0){
            angle += 3600;
        }
        angle = angle % 360;
        return (int) (angle/anglePart);
    }

    /**
    populate the tile thank to the api
    */
    void TransformWorldPointToLatLon(GameObject tile)
    {
        tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                                    .TransformWorldPointToLatLon(basicMapRenderer, /*mapCenter - */tile.transform.position);
    }
    
}