using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// Script used to manage the score 
public class Scoring : MonoBehaviour
{
    /// Camera
    public GameObject cam;
    
    /// GameObject parent of the balls
    public GameObject ballsFolder;

    /// Text displaying the score
    public Text scoreText;

    /// Distance between the player and a ball where a collision is considered
    public float collisionRadius = 1.5f; 

    /// Compass 
    public Compass compass;
    
    /// Starting time, used to calculate the score
    double startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeAsDouble;
    	
        /// Display initial count of balls to catch
        int ballsRemaining = ballsFolder.transform.childCount;
        scoreText.text = $"Balls restantes: {ballsRemaining}";
    }

    // Update is called once per frame
    void Update()
    {
        // Check collisions between the player and balls
        foreach (Transform ball in ballsFolder.transform)
        {
            if(Vector3.Distance (ball.position, cam.transform.position) < collisionRadius){
                // Collision --> Catch the ball, remove it and update score
                compass.DeleteMarker(ball.gameObject);
                Destroy(ball.gameObject);
                updateScore();
            }
        }
    }

    /// Update the displayed score
    public void updateScore(){
    	int ballsRemaining = ballsFolder.transform.childCount;
        scoreText.text = $"Balls restantes: {ballsRemaining-1}";
    	if(ballsFolder.transform.childCount <= 1){
            int score = (int) ((Time.timeAsDouble - startTime)*100);
    		Debug.Log(score);
            scoreText.text = $"Terminé! Score: {score}";
    	}
    }
}
