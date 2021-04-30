using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEditor;





public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void PauseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    
    }
    
     public void OnValueChanged()
 {
    float SliderforceGet;
    Slider Sliderforce;
    Sliderforce = GameObject.Find ("Slider_force").GetComponent <Slider> ();
    SliderforceGet = Sliderforce.value;
    
     if (SliderforceGet<0.25)
     {
        Sliderforce.value = 0.25f;
     }
     else if (SliderforceGet>0.25 && SliderforceGet<0.50)
     {
        Sliderforce.value = 0.50f;
     }
     else if (SliderforceGet>0.5 && SliderforceGet<0.75)
     {
        Sliderforce.value = 0.75f;
     }
     else if (SliderforceGet>0.75)
     {
        Sliderforce.value = 1.0f;
     }
     
     Debug.Log(Sliderforce.value);

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
