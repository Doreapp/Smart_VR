using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text cityText, modeText, machineText;

    void Start(){
        StaticCoordinates.FecthData();
        cityText.text = StaticCoordinates.GetMap().name;
        modeText.text = StaticCoordinates.GetMode().name;
        machineText.text = StaticCoordinates.GetMachine().name;
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
