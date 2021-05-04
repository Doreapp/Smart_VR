using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class MenuScript : MonoBehaviour
{

    public TextMesh logText;
    /*[SerializeField]
    private XRNode xRNode = XRNode.RightHand;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    //to avoid repeat readings
    private bool triggerIsPressed;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xRNode, devices);
        device = devices.FirstOrDefault();
    }

    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }*/

    void Update()
    {
        /*if (!device.isValid)
        {
            GetDevice();
        }

        // capturing trigger button press and release    
        bool triggerButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue && !triggerIsPressed)
        {
        	triggerIsPressed = true;
            GameObject.Find("Command Background").GetComponent<Renderer>().material.SetColor("_SpecColor", Color.red);
        }
        else if (!triggerButtonValue && triggerIsPressed)
        {
            triggerIsPressed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }*/
    }

    public void PlayGame()
    {
        log("true");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void log(string str) {
        logText.text = str;
    }
}