using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalControls : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z)){
            player.transform.position += new Vector3(0,0,0.01f);
        }
    }
}
