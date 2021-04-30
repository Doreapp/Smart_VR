using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public GameObject head; 
    public GameObject environnement;
    public GameObject controller;
    public GameObject player;
    public TextMesh logText;
    public float speedMultiplier = 1;
    public int minHeight, maxHeight;

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

            //log($"update 100ms: directionVector={vecDirection}, speed={speed}, environnementPos={environnement.transform.position}");
            timeSpent = 0;
        }

        // Move the environnement backward
        /*Vector3 env = environnement.transform.position;
        env -= vecDirection;
        env.y = Mathf.Max(minHeight,env.y); // Check if you're under the minHeight
        env.y = Mathf.Min(maxHeight,env.y); // Check if you're over the maxHeight
        environnement.transform.position = env;*/

        // Move the camera forward
        /*Vector3 playerPos = player.transform.position;
        if(isNaN(playerPos.x))
            playerPos.x = 0;
        playerPos += vecDirection;
        playerPos.y = Mathf.Max(maxHeight,playerPos.y); // Check if you're under the minHeight
        playerPos.y = Mathf.Min(minHeight,playerPos.y); // Check if you're over the maxHeight
        player.transform.position = playerPos;*/
    }

    private void log(string str) {
        logText.text = str;
    }
}
