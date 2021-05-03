using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertie : MonoBehaviour
{
    public float game_friction;

    public GameObject head; 
    public Rigidbody cam;
    public GameObject player;
    public GameObject controller;
    public TextMesh logText;
    public float coefSpeed;
    public float minAngle = 0.3f;
    public float coefRotation;

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
        game_friction = 0.02f;
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

        /*
        if(Mathf.Abs(rotation.x) >= minAngle){
            Debug.Log($"rotate direction by {coefRotation*Mathf.Sign(rotation.x)}");
            direction = Quaternion.Euler(0,coefRotation*Mathf.Sign(rotation.x),0)*direction;
        }
        if(Mathf.Abs(rotation.z) >= minAngle){
            Debug.Log($"rotate direction by {coefRotation*Mathf.Sign(rotation.z)}");
            direction = Quaternion.Euler(0,coefRotation*Mathf.Sign(rotation.z),0)*direction;
        }*/
        log($"direction={direction} - rotation={rotation.y}");

        globalTime += Time.deltaTime;
        
        if(!COMPUTER_CONTROL){
            timeSpent += Time.deltaTime;
            if(timeSpent >= 0.1){
                
                var positionDiff = controller.transform.position - lastPosition;
                lastPosition = controller.transform.position;

                controllerSpeed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent); 
                if(controllerSpeed < MIN_SPEED) controllerSpeed = 0;

                timeSpent = 0;
            }
        }
    
        Vector3 resistanceForce = game_friction * velocity;
        Vector3 motorForce =  coefSpeed * controllerSpeed * direction;
        Vector3 force = motorForce - resistanceForce;
        cam.AddForce(force, ForceMode.VelocityChange);
        
        //log($"test; force={force}; direction={direction}");
        //log($"controllerPos={controller.transform.position}; controllerSpeed={controllerSpeed};");

        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.2){
            //Debug.Log($"motorForce={motorForce}; resistanceForce={resistanceForce}; force={force}; time={globalTime}; ");
            //Debug.Log($"direction={direction}; velocity={velocity};  time={globalTime}; ");
            //Debug.Log($"game_friction={game_friction}; velocity={velocity}; resistanceForce={resistanceForce}; time={globalTime}; ");
            //Debug.Log($"controllerSpeed={controllerSpeed}; motorForce={motorForce}; time={globalTime}; ");
            timeSpent = 0;
        }
        
    }


    private void log(string str) {
        //Debug.Log(str);
        logText.text = str;
    }
    
}