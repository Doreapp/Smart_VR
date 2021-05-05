using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class Quit : MonoBehaviour
{
    [SerializeField]
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
    }

    void Update()
    {
        if (!device.isValid)
        {
            GetDevice();
        }

        // capturing trigger button press and release    
        bool primaryButtonValue = false;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue && !triggerIsPressed)
        {
            triggerIsPressed = true;
        }
        else if (!primaryButtonValue && triggerIsPressed)
        {
            triggerIsPressed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }
}