using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MachineMenu : MonoBehaviour
{
    public GameObject basicButton;
    public GameObject mainMenu;
    public Text machineText;

    // Start is called before the first frame update
    void Start()
    {
        LoadMachineMenu();
    }

    public void LoadMachineMenu()
    {
        float y = 20;
        for(int i = 0; i < StaticCoordinates.machines.Count(); i++){
            AddButton(i, y);
            y-=30;
        }
    }
    
    private void AddButton(int machineIndex, float y){
        var button = Instantiate(basicButton, 
            new Vector3(transform.position.x, y, 300), 
            Quaternion.Euler(0,0,0),
            transform);
        // Set the text
        button.transform.GetChild(0).GetComponent<Text>().text = StaticCoordinates.machines[machineIndex].name;

        // Set the listener
        button.GetComponent<Button>().onClick.AddListener(() => { SetMachine(machineIndex); });
    }

    public void SetMachine(int machineIndex){
        StaticCoordinates.SelectedMachine = machineIndex;
        machineText.text = StaticCoordinates.GetMachine().name;
        transform.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
