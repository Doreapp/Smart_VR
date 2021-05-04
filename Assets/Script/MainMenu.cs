using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text cityText;

    void Start(){
        StaticCoordinates.FecthMaps();
        cityText.text = StaticCoordinates.GetSelectedMap().name;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Application.Quit();
    
    }
}
