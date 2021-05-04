using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class MenuScript : MonoBehaviour
{
    void Start(){
        StaticCoordinates.FecthMaps();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Application.Quit();
    
    }

    public void ny(){
        float[] manhattan = {40.754093101735f, -73.98019010522262f};
        float[] owtc = {40.713303878401994f, -74.01293587215311f};
        float[] liberty = {40.69064070311502f, -74.043466858791f};
        
        System.Random aleatoire = new System.Random();
        int lieu = aleatoire.Next(3);
    
        StaticCoordinates.CityNumber = lieu;
    }

    public void seattle(){
        StaticCoordinates.CityNumber = 3;
    }

    public void sf(){
        StaticCoordinates.CityNumber = 4;
    }

}