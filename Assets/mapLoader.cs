using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;

public class mapLoader : MonoBehaviour
{
    public GameObject basicMap;
    public int radius = 2;

    // Start is called before the first frame update
    void Start()
    {
        MapRenderer map = basicMap.GetComponent<MapRenderer>();
        for(int x = -radius; x <= radius; x++){
            for(int z = -radius; z <= radius; z++){
               GameObject mapBlock = Instantiate(basicMap);
               mapBlock.transform.position = transform.position + new Vector3(x * 3, 0, z * 3);
               mapBlock.GetComponent<MapRenderer>().Center = MapRendererTransformExtensions
                    .TransformWorldPointToLatLon(map, mapBlock.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        z -= 0.01f;
        z = z < 3 ? 10 : z;
        */
        
    }

    int isOutOfChunk() {
        return 0;
    }
}
