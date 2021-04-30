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
    public TextAsset file;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = file.text.Split ("\n" [0]);
        for (var i = 1; i < lines.Length; i ++) {
            string[] parts = lines[i].Split ("|" [0]);
            // Creating the balise
            GameObject balise = Instantiate(basicBalise, this.transform);
            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(double.Parse(parts[1]), double.Parse(parts[2]), 0));
            // Set the position of the balise 1 above the map
            balise.transform.position = pos + new Vector3(0f,1f,0f);
            // Creating the text as a child of the balise
            TextMesh description = Instantiate(basicText, balise.transform);
            description.text = parts[0];
            // Set the position of the text 2 above the map
            description.transform.position = pos + new Vector3(0f,2f,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}