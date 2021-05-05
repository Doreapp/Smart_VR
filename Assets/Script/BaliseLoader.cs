using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;



public class BaliseLoader : MonoBehaviour
{
    public GameObject basicBalise;
    public MapRenderer basicMapRenderer;
    public TextMesh basicText;
    public Compass compass;
    public GameObject ballsFolder;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        StaticCoordinates.Map map = StaticCoordinates.GetSelectedMap();

        foreach(StaticCoordinates.Ball b in map.balls){
            // Creating the balise
            GameObject balise = Instantiate(basicBalise, ballsFolder.transform);
            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(b.lat, b.lon, 0));
            // Set the position of the balise 1 above the map
            balise.transform.position = pos + new Vector3(0f,1f,0f);
            // Creating the text as a child of the balise
            TextMesh description = Instantiate(basicText, balise.transform);
            description.text = b.name;
            // Set the position of the text 2 above the map
            description.transform.position = pos + new Vector3(0f,2f,0f);
            //Ajout des balises � la boussole
            compass.AddMarker(balise);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform balise in ballsFolder.transform)
        {
            if (Vector3.Distance(balise.position, cam.transform.position) > 25f)
            {
                balise.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                balise.localScale = new Vector3(1, 1, 1);
            }

        }
    }
}