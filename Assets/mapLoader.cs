using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLoader : MonoBehaviour
{

    float z;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //z = transform.position.z;

        Transform Parent;
        GameObject PrefabGameObject;

        GameObject a = new GameObject("newMap");
        a.transform.SetParent(this.transform);

        GameObject go = GameObject.Instantiate(GameObject.Find("C"));
        go.transform.position = new Vector3(0f,0f,10f);

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
