using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

public class touchCamera : MonoBehaviour
{
    void OnCollisionEnter(Collision info)
    {
        Debug.Log(info.collider);
        Destroy(this.gameObject);
    }
}