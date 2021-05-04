using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public GameObject basicButton;

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

    public void LoadMapsMenu()
    {
        float y = 20;
        for(int i = 0; i < StaticCoordinates.maps.Count(); i++){
            AddButton(i, y);
            y-=30;
        }
    }

    private void AddButton(int mapIndex, float y){
        var button = Instantiate(basicButton, 
            new Vector3(transform.position.x, y, 300), 
            Quaternion.Euler(0,0,0),
            transform);
        // Set the text
        button.transform.GetChild(0).GetComponent<Text>().text = StaticCoordinates.maps[mapIndex].name;

        //const int finalMapIndex = mapIndex;

        // Set the listener
        button.GetComponent<Button>().onClick.AddListener(() => { SetCity(mapIndex); });
    }

    public void SetCity(int mapIndex){
        StaticCoordinates.SelectedCity = mapIndex;
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