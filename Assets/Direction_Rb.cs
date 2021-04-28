using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction_Rb : MonoBehaviour
{
    public float game_friction = 0.001f; 

    public GameObject head; 
    public Rigidbody environnement;
    public GameObject controller;
    public TextMesh logText;

    public Vector3 position;
    public Vector3 lastPosition;
    public Vector3 positionDiff;
    public float controllerSpeed;
    public float globalTime = 0;
    public float timeSpent = 0;


    // Start is called before the first frame update
    void Start()
    {
        environnement.GetComponent<Rigidbody>().useGravity = false;
        lastPosition = controller.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        Vector3 direction = head.transform.forward;
        Vector3 velocity = environnement.velocity;
        Vector3 resistanceForce = - velocity * game_friction;

        position = controller.transform.position;

        positionDiff = position - lastPosition;
        lastPosition = controller.transform.position;

        if (globalTime < 5 && controllerSpeed < 0.05f) {
            controllerSpeed += 0.003f;
        }else
        {
            controllerSpeed = 0;
        }
        globalTime += Time.deltaTime;

        // controllerSpeed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent);
        Vector3 motorForce =  direction * controllerSpeed;

        //environnement.AddForce(motorForce + resistanceForce);
        environnement.AddForce(motorForce);

        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.2){
            //Debug.Log($"motorForce={motorForce}; resistanceForce={resistanceForce}; time={globalTime}; ");
            //Debug.Log($"velocity={velocity}; controllerSpeed={controllerSpeed}; time={globalTime}; ");
            Debug.Log($"direction={direction}; controllerSpeed={controllerSpeed}; motorForce={motorForce}; time={globalTime}; ");
            timeSpent = 0;
        }
    }


    private void log(string str) {
        logText.text = str;
    }
}
