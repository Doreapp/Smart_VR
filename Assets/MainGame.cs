using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{

    
    public void PauseGame()
    {
        Debug.Log("pause activated");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    
     
}
