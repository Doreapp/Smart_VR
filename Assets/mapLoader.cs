using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;

public class mapLoader : MonoBehaviour
{
    public GameObject basicMap;
    public int size = 3;
    public int renderSize = 10;
    List<GameObject> maps;
    int xOffset, zOffset;

    private MapRenderer basicMapRenderer;

    // Start is called before the first frame update
    void Start()
    {
    	// Creating some maps
        basicMapRenderer = basicMap.GetComponent<MapRenderer>();
    	maps = new List<GameObject>();
        for(int i = -size; i <= size; i++)
        {
            for(int j = -size; j <= size; j++)
            {
                GameObject tile = Instantiate(basicMap, new Vector3(i * 3, -1f, j * 3), Quaternion.Euler(0, 0, 0), this.transform);
                tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                    .TransformWorldPointToLatLon(basicMapRenderer, tile.transform.position);
                maps.Add(tile);
            }
        }
        xOffset = 0;
        zOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-0.005f,0f,0.002f);
        Vector3 camera = transform.position;

        //check if you're changing of row (X axe)
        if(Mathf.FloorToInt ((camera.x+1.5f)/3f) != xOffset){
        	xOffset = Mathf.FloorToInt ((camera.x+1.5f)/3f);
        	float xPos = camera.x - Mathf.Sign(xOffset) * (size+Mathf.Abs(xOffset)) * 3f;
        	for(int j = -size; j <= size; j++)
        	{  
                GameObject tile = GameObject.Instantiate(basicMap, new Vector3(xPos, -1f, camera.z - Mathf.Sign(zOffset) * (j+Mathf.Abs(zOffset)) * 3f), Quaternion.Euler(0, 0, 0), this.transform);
                tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                    .TransformWorldPointToLatLon(basicMapRenderer, tile.transform.position);
        		maps.Add(tile);
        	}
        	List<GameObject> toDestroy = new List<GameObject>();
        	// Selecting all the maps to delete
        	foreach(GameObject map in maps)
        	{
        		if( (xOffset >= 0 && ((Vector3)map.transform.position).x > -xPos) ||
    				(xOffset <  0 && ((Vector3)map.transform.position).x < -xPos)){
        			toDestroy.Add(map);
        		}
        	}
        	// Removing and deleting maps not needed anymore
        	foreach(GameObject map in toDestroy){
        		maps.Remove(map);
        		Destroy(map);
        	}
        }

        //check if you're changing of column (Z axe)
        if(Mathf.FloorToInt ((camera.z+1.5f)/3f) != zOffset){
        	zOffset = Mathf.FloorToInt ((camera.z+1.5f)/3f);
        	float zPos = camera.z - Mathf.Sign(zOffset) * (size+Mathf.Abs(zOffset)) * 3f;
        	for(int i = -size; i <= size; i++)
        	{  
                GameObject tile = GameObject.Instantiate(basicMap, new Vector3(camera.x - Mathf.Sign(xOffset) * (i+Mathf.Abs(xOffset)) * 3f, -1f, zPos), Quaternion.Euler(0, 0, 0), this.transform);
                tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                                    .TransformWorldPointToLatLon(basicMapRenderer, tile.transform.position);
        		maps.Add(tile);
        	}
        	List<GameObject> toDestroy = new List<GameObject>();
        	foreach(GameObject map in maps)
        	{
        		if( (zOffset >= 0 && ((Vector3)map.transform.position).z > -zPos) ||
    				(zOffset <  0 && ((Vector3)map.transform.position).z < -zPos)){
        			toDestroy.Add(map);
        		}
        	}
        	foreach(GameObject map in toDestroy){
        		maps.Remove(map);
        		Destroy(map);
        	}
        }
    }
}
