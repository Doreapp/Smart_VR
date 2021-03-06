using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;



public class BallLoader : MonoBehaviour
{
    public GameObject basicBall;
    public MapRenderer basicMapRenderer;
    public TextMesh basicText;
    public GameObject compassCanvas;
    public Compass compass;
    public int yBall;
    public GameObject ballsFolder;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
    	switch (StaticCoordinates.SelectedMode) {
            case 0:
                compassCanvas.SetActive(false);
                break;
            case 1:
                spawnBalls();
                break;
            case 2:
                spwanRandomBalls();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform ball in ballsFolder.transform)
        {
            if (Vector3.Distance(ball.position, cam.transform.position) > 25f)
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
            // Set the position of the text yBall+1 above the map
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