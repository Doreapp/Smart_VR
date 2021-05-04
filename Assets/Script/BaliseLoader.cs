using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

[Serializable]
public class Map
{
  public string nom,pays;
  public Balise[] balises;
}

[Serializable]
public class Balise
{
  public string nom;
  public float latitude,longitude;
}

public class BaliseLoader : MonoBehaviour
{
    public GameObject basicBalise;
    public MapRenderer basicMapRenderer;
    public TextMesh basicText;
    public TextAsset file;
    public Compass compass;
    public float yBalise;
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
    	GameObject balises = new GameObject("Balises");
        balises.transform.SetParent(this.transform);

        //TextAsset textAsset = Resources.Load<TextAsset>("../Maps/NewYork.json");
        Map map = JsonUtility.FromJson<Map>(file.text);

        foreach(Balise b in map.balises){
            // Creating the balise
            GameObject balise = Instantiate(basicBalise, balises.transform);
            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(b.latitude, b.longitude, 0));
            // Set the position of the balise 1 above the map
            balise.transform.position = pos + new Vector3(0f,1f,0f);
            // Creating the text as a child of the balise
            TextMesh description = Instantiate(basicText, balise.transform);
            description.text = b.nom;
            // Set the position of the text 2 above the map
            description.transform.position = pos + new Vector3(0f,2f,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject camera = GameObject.Find("Main Camera");
        foreach (Transform balise in GameObject.Find("Balises").transform)
        {
            if(Vector3.Distance (balise.position, camera.transform.position) < 1.5f){
                compass.DeleteMarker(balise.gameObject);
                Destroy(balise.gameObject);
            }
        }
    }
}