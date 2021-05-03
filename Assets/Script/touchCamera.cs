using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

public class touchCamera : MonoBehaviour
{
    void OnCollisionEnter(Collision info)
    {
        Destroy(this.gameObject);
        GameObject.Find("Main Camera").GetComponent<Scoring>().updateScore();
    }
}