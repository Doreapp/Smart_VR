using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
	double startTime;
	double score;

    // Start is called before the first frame update
    void Start()
    {
    	score = 0;
        startTime = Time.timeAsDouble;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(){
    	if(GameObject.Find("Balises").transform.childCount <= 1){
    		score = Time.timeAsDouble - startTime;
    		Debug.Log(score);
    	}
    }
}
