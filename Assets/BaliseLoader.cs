using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;


public class BaliseLoader : MonoBehaviour
{
    public GameObject Balise;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject balise = GameObject.Instantiate(Balise, MapRendererTransformExtensions.TransformLatLonAltToLocalPoint(this.transform,new LatLonAlt(40.74877, -7398570,0)),this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
