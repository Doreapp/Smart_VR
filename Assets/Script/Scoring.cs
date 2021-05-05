using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Scoring : MonoBehaviour
{
    public GameObject cam;
    public GameObject ballsFolder;
    public Text scoreText;
    public Compass compass;
    
    double startTime;

    private int initialBallCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeAsDouble;
        initialBallCount = StaticCoordinates.GetMap().balls.Count();
    	
        int ballsRemaining = ballsFolder.transform.childCount;
        scoreText.text = $"Balises restantes: {ballsRemaining}";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform ball in ballsFolder.transform)
        {
            if(Vector3.Distance (ball.position, cam.transform.position) < 1.5f){
                compass.DeleteMarker(ball.gameObject);
                Destroy(ball.gameObject);
                updateScore();
            }
        }
    }

    public void updateScore(){
    	int ballsRemaining = ballsFolder.transform.childCount;
        scoreText.text = $"Balises restantes: {ballsRemaining-1}";
    	if(ballsFolder.transform.childCount <= 1){
            int score = (int) ((Time.timeAsDouble - startTime)*100);
    		Debug.Log(score);
            scoreText.text = $"Terminé! Score: {score}";
    	}
    }
}
