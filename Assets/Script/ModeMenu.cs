using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour
{
    public GameObject basicButton;
    public GameObject mainMenu;
    public Text modeText;

    // Start is called before the first frame update
    void Start()
    {
        LoadModeMenu();
    }

    public void LoadModeMenu()
    {
        float y = 20;
        for(int i = 0; i < StaticCoordinates.modes.Count(); i++){
            AddButton(i, y);
            y-=30;
        }
    }
    
    private void AddButton(int modeIndex, float y){
        var button = Instantiate(basicButton, 
            new Vector3(transform.position.x, y, 300), 
            Quaternion.Euler(0,0,0),
            transform);
        // Set the text
        button.transform.GetChild(0).GetComponent<Text>().text = StaticCoordinates.modes[modeIndex].name;

        // Set the listener
        button.GetComponent<Button>().onClick.AddListener(() => { SetMode(modeIndex); });
    }

    public void SetMode(int modeIndex){
        StaticCoordinates.SelectedMode = modeIndex;
        modeText.text = StaticCoordinates.GetMode().name;
        transform.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
