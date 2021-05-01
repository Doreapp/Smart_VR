using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rowing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject head;
    public TextMesh logText;
    public string warning = "Regardez dans la direction dans laquelle vous allez ramer";
    public TextMesh logCompteur;
    public int compteur = 10;
    private float globalTime = 0;

    private Vector3 meanDirection;
    private bool calibrationFinished = false;
    int compteurForMean = 0;

    public GameObject direction;

    void Start()
    {
        log(warning);
        logComp(compteur);
        logText.anchor = TextAnchor.MiddleCenter;
        logCompteur.anchor = TextAnchor.MiddleCenter;
        meanDirection = head.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        globalTime += Time.deltaTime;

        //calibration
        if (globalTime <= 3) {
            log(warning);
            logComp(10 - (int)globalTime);

            meanDirection += head.transform.forward;
            //Debug.Log($"meanDirection = {meanDirection}");
            compteurForMean++;
        } else if(calibrationFinished == false) {
            calibrationFinished = true;
            meanDirection = meanDirection / compteurForMean;
            //Debug.Log($"meanDirection = {meanDirection}");
            resetLog();
            direction.GetComponent<Inertie>().setMeanDirectionForRowing(meanDirection);
        }
        
    }

    private void log(string str) {
    logText.text = str;
    }

    private void logComp(int compt) {
    logCompteur.text = compt.ToString();
    }

    private void resetLog() {
    logCompteur.text = null;
    logText.text = null;
    }
}
