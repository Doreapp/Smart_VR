using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
    public GameObject basicButton;
    public GameObject mainMenu;
    public Text cityText;

    // Start is called before the first frame update
    void Start()
    {
        LoadMapsMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        // Set the listener
        button.GetComponent<Button>().onClick.AddListener(() => { SetCity(mapIndex); });
    }

    public void SetCity(int mapIndex){
        StaticCoordinates.SelectedCity = mapIndex;
        cityText.text = StaticCoordinates.GetSelectedMap().name;
        transform.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
