using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;


public class mapLoader : MonoBehaviour
{
    public GameObject basicMap;
    public int size;
    public TextMesh logText;
    List<GameObject> maps;
    int xOffset, zOffset;
    int xMin,xMax,zMin,zMax;

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
                GameObject tile = Instantiate(basicMap, new Vector3(i * 3, transform.position.y, j * 3), Quaternion.Euler(0, 0, 0), this.transform);
                TransformWorldPointToLatLon(tile);
                maps.Add(tile);
            }
        }
        zMin = -size;
        zMax = size;
        xMin = -size;
        xMax = size;
        xOffset = 0;
        zOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(-0.005f,0f,0.002f);
        Vector3 camera = transform.position;

        //check if you're changing of row (X axe)
        if(Mathf.FloorToInt ((camera.x+1.5f)/3f) != xOffset){
        	int direction = Mathf.FloorToInt ((camera.x+1.5f)/3f) - xOffset;
        	xMax += direction;
        	xMin += direction;
        	xOffset = Mathf.FloorToInt ((camera.x+1.5f)/3f);
            List<GameObject> toDestroy = new List<GameObject>();
            // Selecting all the maps to delete
            foreach (GameObject map in maps)
            {
                if ((direction >= 0 && map.transform.position.x > -camera.x + xMax * 3f) ||
                    (direction < 0 && map.transform.position.x < -camera.x + xMin * 3f))
                {
                    toDestroy.Add(map);
                }
            }
            // Removing and deleting maps not needed anymore
            foreach (GameObject map in toDestroy)
            {
                maps.Remove(map);
                Destroy(map);
            }
            for (int j = -size; j <= size; j++)
        	{  
                GameObject tile = GameObject.Instantiate(basicMap, new Vector3(camera.x - (direction>0?xMax:xMin)*3f, camera.y, camera.z - Mathf.Sign(zOffset) * (j+Mathf.Abs(zOffset)) * 3f), Quaternion.Euler(0, 0, 0), this.transform);
                TransformWorldPointToLatLon(tile);
                maps.Add(tile);
        	}
        }

        //check if you're changing of column (Z axe)
        if(Mathf.FloorToInt ((camera.z+1.5f)/3f) != zOffset){
        	int direction = Mathf.FloorToInt ((camera.z+1.5f)/3f) - zOffset;
        	zMax += direction;
        	zMin += direction;
        	zOffset = Mathf.FloorToInt ((camera.z+1.5f)/3f);
            List<GameObject> toDestroy = new List<GameObject>();
            foreach (GameObject map in maps)
            {
                if ((direction >= 0 && map.transform.position.z > -camera.z + zMax * 3f) ||
                    (direction < 0 && map.transform.position.z < -camera.z + zMin * 3f))
                {
                    toDestroy.Add(map);
                }
            }
            foreach (GameObject map in toDestroy)
            {
                maps.Remove(map);
                Destroy(map);
            }
            for (int i = -size; i <= size; i++)
        	{  
                GameObject tile = GameObject.Instantiate(basicMap, new Vector3(camera.x - Mathf.Sign(xOffset) * (i+Mathf.Abs(xOffset)) * 3f, camera.y, camera.z - (direction>0?zMax:zMin)*3f), Quaternion.Euler(0, 0, 0), this.transform);
                TransformWorldPointToLatLon(tile);
                maps.Add(tile);
        	}
        }
    }

    void TransformWorldPointToLatLon(GameObject tile)
    {
        tile.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                                    .TransformWorldPointToLatLon(basicMapRenderer, - transform.position + tile.transform.position);
    }

    private void log(string str) {
        logText.text = str;
    }
}
