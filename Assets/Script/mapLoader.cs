using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

public class mapLoader : MonoBehaviour
{
    public GameObject basicMap;
    public GameObject player;
    public int size;
    public int tileSize = 3;

    List<GameObject> maps;

    int lastPlayerTileX = 0, lastPlayerTileZ = 0;

    private MapRenderer basicMapRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        // Moving the camera above the terrain
        GameObject.Find("XR Rig").transform.position += new Vector3(0,3,0);

    	// Getting the map rendered 
        basicMapRenderer = basicMap.GetComponent<MapRenderer>();
    	// Position it above the selected Map
        StaticCoordinates.Map selectedMap = StaticCoordinates.GetSelectedMap();
        basicMapRenderer.Center = new LatLon(selectedMap.lat, selectedMap.lon);

        maps = new List<GameObject>();
        
        // Inflating the chunks
        for(int i = -size; i <= size; i++)
        {
            for(int j = -size; j <= size; j++)
            {
                GameObject tile = Instantiate(basicMap, 
                    new Vector3(i * tileSize, transform.position.y, j * tileSize), 
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
        Vector3 mapCenter = transform.position;
        Vector3 playerCenter = player.transform.position;

        int playerTileX = Mathf.FloorToInt((playerCenter.x+tileSize/2f)/(float)tileSize);
        int playerTileZ = Mathf.FloorToInt((playerCenter.z+tileSize/2f)/(float)tileSize);

        if(lastPlayerTileX != playerTileX){
            //Debug.Log($"We need to create another line (x) - Player Tile : ({playerTileX}, {playerTileZ})");
            float side = Mathf.Sign(playerTileX - lastPlayerTileX);

            // Remove
            float tilesXToRemove = (lastPlayerTileX - side*size)*tileSize;
            for(int i = 0; i < maps.Count; i++)
        	{
        		if(maps[i].transform.position.x == tilesXToRemove){
        			Destroy(maps[i]);
                    maps.RemoveAt(i);
                    i--;
        		}
        	}

            // Add new
            float tilesX = (playerTileX+side*size)*tileSize;
            for(float tileZIndex = -size; tileZIndex <= size; tileZIndex++){
                float tileZ = (lastPlayerTileZ + tileZIndex) * tileSize;
                GameObject tile = Instantiate(basicMap, new Vector3(tilesX, mapCenter.y, tileZ), transform.rotation, transform);
                TransformWorldPointToLatLon(tile);
                maps.Add(tile);
            }
            lastPlayerTileX = playerTileX;
        }

        if(lastPlayerTileZ != playerTileZ){
            //Debug.Log($"We need to create another line (z) - Player Tile : ({playerTileX}, {playerTileZ})");
            float side = Mathf.Sign(playerTileZ - lastPlayerTileZ);

            // Remove
            float tilesZToRemove = (lastPlayerTileZ - side*size)*tileSize;
            for(int i = 0; i < maps.Count; i++)
        	{
        		if(maps[i].transform.position.z == tilesZToRemove){
        			Destroy(maps[i]);
                    maps.RemoveAt(i);
                    i--;
        		}
        	}

            // Add new
            float tilesZ = (playerTileZ+side*size)*tileSize;
            for(float tileXIndex = -size; tileXIndex <= size; tileXIndex++){
                float tileX = (lastPlayerTileX + tileXIndex) * tileSize;
                GameObject tile = Instantiate(basicMap, new Vector3(tileX, mapCenter.y, tilesZ), transform.rotation, transform);
                TransformWorldPointToLatLon(tile);
                maps.Add(tile);
            }
            lastPlayerTileZ = playerTileZ;
        }

    }

    void TransformWorldPointToLatLon(GameObject tile)
    {
        // In order to test -->
        tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                                    .TransformWorldPointToLatLon(basicMapRenderer, /*mapCenter - */tile.transform.position);
    }
    
}