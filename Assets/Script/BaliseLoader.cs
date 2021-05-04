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
    public float yBalise;
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
    	GameObject balises = new GameObject("Balises");
        balises.transform.SetParent(this.transform);

        //TextAsset textAsset = Resources.Load<TextAsset>("../Maps/NewYork.json");
        Map map = JsonUtility.FromJson<Map>(file.text);

    	switch (mode) {
    		case 1 :
		        foreach(Balise b in map.balises){
		            // Creating the balise
		            GameObject balise = Instantiate(basicBalise, balises.transform);
		            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(b.latitude, b.longitude, 0));
		            // Set the position of the balise y above the map
		            balise.transform.position = pos + new Vector3(0f,yBalise,0f);
		            // Creating the text as a child of the balise
		            TextMesh description = Instantiate(basicText, balise.transform);
		            description.text = b.nom;
		            // Set the position of the text y+1 above the map
		            description.transform.position = pos + new Vector3(0f,yBalise+1,0f);
		        }
		    	break;	
	    	case 2 :
	    		float delta = 0.5f;
	    		for (int i = 0; i < 20; i++){
	    			float latitude = map.balises[0].latitude + UnityEngine.Random.Range(-delta, delta);
	    			float longitude = map.balises[0].longitude + UnityEngine.Random.Range(-delta, delta);
	    			GameObject balise = Instantiate(basicBalise, balises.transform);
		            Vector3 pos = MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(basicMapRenderer, new LatLonAlt(latitude, longitude, 0));
	    			// Set the position of the balise 1 above the map
		            balise.transform.position = pos + new Vector3(0f,yBalise,0f);
	    		}
	    		break;
	    }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject camera = GameObject.Find("Main Camera");
        foreach (Transform balise in GameObject.Find("Balises").transform)
        {
            if(Vector3.Distance (balise.position, camera.transform.position) < 1.5f){
                Destroy(balise.gameObject);
            }
        }
    }
}