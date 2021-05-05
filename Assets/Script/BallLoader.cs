using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

/// Script creating the balls if needed
public class BallLoader : MonoBehaviour
{
    /// Prefab for a ball
    public GameObject basicBall;

    /// Prefab for a mapRenderer
    public MapRenderer basicMapRenderer;

    /// Prefab for a text (used to show text above the balls)
    public TextMesh basicText;

    /// Canvas showing compas and score
    public GameObject compassCanvas;

    /// Object compass
    public Compass compass;

    /// Y coordinate of the balls
    public int yBall;

    /// Game object parent of the balls
    public GameObject ballsFolder;

    /// Camera 
    public GameObject cam;

    /// Radius arround the player where the balls are displayed
    public float displayRadius = 25f;

    // Start is called before the first frame update
    void Start()
    {
    	switch (StaticCoordinates.GetSelectedMode()) {
            case 0: // No balls
                compassCanvas.SetActive(false);
                break;
            case 1: // Placed balls
                spawnBalls();
                break;
            case 2: // Random balls
                spwanRandomBalls();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check ball colision
        foreach (Transform ball in ballsFolder.transform)
        {
            if (Vector3.Distance(ball.position, cam.transform.position) > displayRadius)
            {
                ball.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                ball.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    // Spawn balls at the coordinates defined in the Data
    void spawnBalls(){
        // Retrieve the map properties
        StaticCoordinates.Map map = StaticCoordinates.GetMap();

        foreach(StaticCoordinates.Ball b in map.balls){
            // Creating the ball
            GameObject ball = Instantiate(basicBall, ballsFolder.transform);
            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(b.lat, b.lon, 0));
            
            // Set the position of the ball yBall above the map
            ball.transform.position = pos + new Vector3(0f,yBall,0f);
            
            // Creating the text as a child of the ball
            TextMesh description = Instantiate(basicText, ball.transform);
            description.text = b.name;
            
            // Set the position of the text yBall+1 above the ball
            description.transform.position = pos + new Vector3(0f,yBall+1,0f);
            
            // Adding ball to the compass
            compass.AddMarker(ball);
        }
    }

    // Spawn 20 random balls around the player
    void spwanRandomBalls(){

        float delta = 0.5f;
        for (int i = 0; i < 20; i++){
            float latitude = StaticCoordinates.GetMap().lat + UnityEngine.Random.Range(-delta, delta);
            float longitude = StaticCoordinates.GetMap().lon + UnityEngine.Random.Range(-delta, delta);
            GameObject ball = Instantiate(basicBall, ballsFolder.transform);
            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(latitude, longitude, 0));
            // Set the position of the ball yBall above the map
            ball.transform.position = pos + new Vector3(0f,yBall,0f);
            // Adding ball to the compass
            compass.AddMarker(ball);
        }
    }
}