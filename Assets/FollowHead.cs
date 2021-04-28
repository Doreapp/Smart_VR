using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public GameObject head; 
    public GameObject environnement;
    public GameObject controller;
    public TextMesh logText;
    public float speedMultiplier = 1;

    private Vector3 lastPosition = new Vector3(0,0,0);
    private float timeSpent = 0;
    private float speed = 0;
    private const float MIN_SPEED = 0.0001f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get direction of the head, with the right speed
        var vecDirection = head.transform.rotation * new Vector3(0, 0, speed*speedMultiplier);

        // Bien call, et timespent evolue
        timeSpent += Time.deltaTime;
        if(timeSpent >= 0.1){
            
            var positionDiff = controller.transform.position - lastPosition;
            lastPosition = controller.transform.position;

            speed = Mathf.Abs(positionDiff.sqrMagnitude / timeSpent); 
            if(speed < MIN_SPEED) speed = 0;

            log($"update 100ms: directionVector={vecDirection}, speed={speed}, environnementPos={environnement.transform.position}");
            timeSpent = 0;
        }

        // Move the environnement backward
        environnement.transform.position -= vecDirection;
    }

    private void log(string str) {
        logText.text = str;
    }
}
