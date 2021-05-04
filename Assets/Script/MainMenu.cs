using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Application.Quit();
    
    }
}
