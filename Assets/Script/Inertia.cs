using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manage the movement of the player
public class Inertia : MonoBehaviour
{
    
    public float game_friction;

    public GameObject head; 
    public Rigidbody cam;
    public GameObject player;
    public GameObject controller;
    public TextMesh logText;
    public float coefSpeed;
    public float minAngle = 0.2f;
    public float coefRotation = 1;

    private Vector3 lastPosition = new Vector3(0,0,0);
    private const float MIN_SPEED = 0.0001f; 
    private float controllerSpeed;
    private float globalTime = 0;
    private float timeSpent = 0;
    private Vector3 direction = Vector3.forward; 

    public  bool COMPUTER_CONTROL = true;

    // Start is called before the first frame update
    void Start()
    {
        cam.GetComponent<Rigidbody>().useGravity = false;
        
        coefSpeed = StaticCoordinates.GetMachine().speedCoef;
        gameFriction = StaticCoordinates.GetMachine().gameFriction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {       
        Vector3 playerDirection = head.transform.forward;
        Vector3 velocity = cam.velocity;

        //player.transform.Rotate(0,1,0);
        
        if(COMPUTER_CONTROL){
            if (Input.GetKey("space")) {
                //position += new Vector3(0.0f, 0.0f, 0.1f);
                controllerSpeed = 0.2f;
            }else
            {
                //Debug.Log("STOOP");
                controllerSpeed = 0;
            }
        }

        // Rotation from direction (current direction of the motion) to the playerDirection (orientation of his eyes)
        Quaternion rotation = Quaternion.FromToRotation(direction, playerDirection);
        //Debug.Log($"rotation = {rotation.y}");

        // If the Y rotation (to the left/right) if enough large
        if(Mathf.Abs(rotation.y) >= minAngle){
            float yRotation = coefRotation * rotation.y; // Mathf.sign
            //Debug.Log($"rotate Y player by {yRotation}");
            // Rotate the player & head (camera) and the direction 
            player.transform.Rotate(0,yRotation,0);
            head.transform.Rotate(0,yRotation,0);
            direction = Quaternion.Euler(0,yRotation,0)*direction;
        }

        // Update direction in z
        direction.y = playerDirection.y;

        globalTime += Time.deltaTime;
        
        if(!COMPUTER_CONTROL){
            timeSpent += Time.deltaTime;
            if(timeSpent >= 0.1){
                
                Vector3 positionDiff = controller.transform.position - lastPosition;
                lastPosition = controller.transform.position;

                switch(StaticCoordinates.SelectedMachine)
                {
                // MANDALIER
                case 0:
                    //We only want to produce force for the controller movement in the plane (y, z)
                    positionDiff.x = 0;
                    break;
                // ROWING
                case 1:
                    //if the controller is going forward, in the rowing exercice, the user isn't doing any effort so he doesn't produce force.
                    if (positionDiff.z > 0) {
                        positionDiff = new Vector3(0, 0, 0);
                    }
                    //we only want to produce force for the controller movement in the plane (x, y)
                    positionDiff.y = 0;
                    break;
                // BUTTERFLY
                case 2:
                    //We only want to produce force for the controller movement in the plane (x, z)
                    positionDiff.y = 0;
                    break;
                }

                controllerSpeed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent); 
                if(controllerSpeed < MIN_SPEED) controllerSpeed = 0;

                timeSpent = 0;
            }
        }

        if (globalTime > 3) {
            Vector3 resistanceForce = gameFriction * velocity;
            Vector3 motorForce =  coefSpeed * controllerSpeed * direction;
            
            //prevent user to go too high or too low
            if (cam.transform.position.y <= 0.35) {
                if (motorForce.y <= 0) {
                    motorForce.y = 0;
                }
                if (cam.transform.position.y < 0.3) {
                    motorForce.y = 0.0025f;
                }
                if (cam.transform.position.y < 0.21) {
                    cam.transform.position = new Vector3 (cam.transform.position.y, 0.21f, cam.transform.position.z);
                }
            }else if(cam.transform.position.y >= 3.7 && motorForce.y >= 0) {
                    motorForce.y = - 0.01f;
            }

            Vector3 force = motorForce - resistanceForce;
            cam.AddForce(force, ForceMode.VelocityChange);
        }
        
    }


    private void log(string str) {
        //Debug.Log(str);
        logText.text = str;
    }
    
}
