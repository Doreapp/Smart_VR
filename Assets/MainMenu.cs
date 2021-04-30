using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;





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
 Debug.Log("MAN" + manhattan);

 float[] owtc = {40.713303878401994f, -74.01293587215311f};
 float[] liberty = {40.69064070311502f, -74.043466858791f};
 List<float> coord_NY = new List<float>();
 coord_NY.AddRange(manhattan);
 coord_NY.AddRange(owtc);
 coord_NY.AddRange(liberty);
 
 System.Random aleatoire = new System.Random();
 int lieu = aleatoire.Next(3);
 Debug.Log("Lieu " + lieu);
 
 var lieu_spone = coord_NY[lieu];
 
 Debug.Log("ici"+lieu_spone);
 
 }
 
 public void seattle(){
  float[] coord = {47.6267658388242f, -122.36167001066251f};

 }
 
 public void sf(){
  float[] coord = {37.820f, -122.478f};

 }
 
     
}
