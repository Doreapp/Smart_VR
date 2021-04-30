using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertie : MonoBehaviour
{
    public float game_friction;

    public GameObject head; 
    public Rigidbody environnement;
    public GameObject controller;
    public TextMesh logText;

    public Vector3 position;
    public Vector3 lastPosition;
    //public Vector3 positionDiff;

    public float controllerSpeed;
    private const float MIN_SPEED = 0.0001f; 

    public float globalTime = 0;
    public float timeSpent = 0;


    // Start is called before the first frame update
    void Start()
    {
        environnement.GetComponent<Rigidbody>().useGravity = false;
        lastPosition = controller.transform.position;
        game_friction = 0.25f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        Vector3 direction = head.transform.forward;
        Vector3 velocity = environnement.velocity;
        Vector3 resistanceForce = game_friction * velocity;

        
        if (globalTime < 5) {
            //position += new Vector3(0.0f, 0.0f, 0.1f);
            controllerSpeed = 0.2f;
        }else
        {
            //Debug.Log("STOOP");
            controllerSpeed = 0;
        }
        

        globalTime += Time.deltaTime;

        /*
        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.1){
            
            var positionDiff = controller.transform.position - lastPosition;
            lastPosition = controller.transform.position;

            controllerSpeed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent); 
            if(controllerSpeed < MIN_SPEED) controllerSpeed = 0;

            timeSpent = 0;
        }
        */

        Vector3 motorForce =  controllerSpeed * direction;
        Vector3 force = motorForce + resistanceForce;

        environnement.AddForce(-1.0f * force, ForceMode.VelocityChange);
        
        //log($"test; force={force}; direction={direction}");
        log($"resistanceForce={resistanceForce}; controllerSpeed={controllerSpeed}; mapPos={environnement.transform.position}");

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
    
}
