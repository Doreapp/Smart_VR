using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
Go forward/backward up/down arrow
Rotate right/left right/left arrow
Move up/down Z/S key
**/

public class ComputerControls : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 0.01f;
    public GameObject environnement;

    // Current player direction 
    private Vector3 direction = new Vector3(0,0,1);
    private Vector3 UP_VECTOR = new Vector3(0,1,0);
    private Vector3 CENTER = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // movement backward / forward
        if(Input.GetKey(KeyCode.UpArrow)){
            // Move the map backward
            environnement.transform.position -= movementSpeed * direction;
        } else if (Input.GetKey(KeyCode.DownArrow)){
            // Move the map forward
            environnement.transform.position += movementSpeed * direction;
        }

        // movement upward / downward
        if(Input.GetKey(KeyCode.Z)){
            // Move downward the map
            environnement.transform.position -= movementSpeed * UP_VECTOR;
        } else if (Input.GetKey(KeyCode.S)) {
            // Move upward the map
            environnement.transform.position += movementSpeed * UP_VECTOR;
        }

        // Rotate the map 
        if(Input.GetKey(KeyCode.RightArrow)){
            // Rotate to the left (counter-clockwise) the map
            environnement.transform.RotateAround(CENTER, Vector3.up, -rotationSpeed);
        } else if(Input.GetKey(KeyCode.LeftArrow)){
            // Rotate to the left (counter-clockwise) the map
            environnement.transform.RotateAround(CENTER, Vector3.up, rotationSpeed);
        }
    }
}
