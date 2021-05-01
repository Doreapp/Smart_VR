using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertie : MonoBehaviour
{
    public float game_friction;

    public GameObject head; 
    public Rigidbody cam;
    public GameObject controller;
    public TextMesh logText;
    public float coefSpeed;

    private Vector3 lastPosition = new Vector3(0,0,0);
    private const float MIN_SPEED = 0.0001f; 
    private float controllerSpeed;
    private float globalTime = 0;
    private float timeSpent = 0;

    private Vector3 meanDirection;


    // Start is called before the first frame update
    void Start()
    {
        cam.GetComponent<Rigidbody>().useGravity = false;
        game_friction = 0.02f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {       
        Vector3 direction = head.transform.forward;
        Vector3 velocity = cam.velocity;
        
        /*
        if (globalTime < 2) {
            //position += new Vector3(0.0f, 0.0f, 0.1f);
            controllerSpeed = 0.2f;
        }else
        {
            //Debug.Log("STOOP");
            controllerSpeed = 0;
        }
        */

        globalTime += Time.deltaTime;


        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.1){
            
            var positionDiff = controller.transform.position - lastPosition;
            lastPosition = controller.transform.position;

            controllerSpeed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent); 
            if(controllerSpeed < MIN_SPEED) controllerSpeed = 0;

            timeSpent = 0;
        }
        

        Vector3 resistanceForce = game_friction * velocity;
        Vector3 motorForce =  coefSpeed * controllerSpeed * direction;
        Vector3 force = motorForce - resistanceForce;

        //check if altitude is not too low
        if (cam.transform.position.y <= 0.5) {
            log($"cameraPos={cam.transform.position}; force={force};");
            if ( force.y < 0 ) {
                force.y  = - force.y;
            }
            else {
                force.y = Mathf.Abs(force.magnitude);
                force.x = 0;
                force.z = 0;
            }
        }

        //if we are capturing a rowing mouvement we only care about force in the plan (x, z) and in the same direction as our camera

        cam.AddForce(force, ForceMode.VelocityChange);
        
        //log($"test; force={force}; direction={direction}");
        

        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.2){
            Debug.Log($"motorForce={motorForce}; resistanceForce={resistanceForce}; force={force}; time={globalTime}; ");
            //Debug.Log($"direction={direction}; velocity={velocity};  time={globalTime}; ");
            //Debug.Log($"game_friction={game_friction}; velocity={velocity}; resistanceForce={resistanceForce}; time={globalTime}; ");
            //Debug.Log($"controllerSpeed={controllerSpeed}; motorForce={motorForce}; time={globalTime}; ");
            timeSpent = 0;
        }
        
    }


    private void log(string str) {
        logText.text = str;
    }

    public void setMeanDirectionForRowing(Vector3 directionRowing) {
        meanDirection = directionRowing; 
        Debug.Log($"MeanDirection={meanDirection}");
    } 
    
}
