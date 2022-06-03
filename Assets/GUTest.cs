using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GULab;
using Uduino;

public class GUTest : MonoBehaviour
{
    public string test;
    public UltralydInput input;

    private string[] dataInput;    

    void Start()
    {
        dataInput = new string[2];
      
    }

    // Update is called once per frame
    void Update()
    {
        //test = input.ReadValueAsFloat();
        //UduinoManager.Instance.sendCommand("p",triggerPin, echoPin);
        test = input.ReadOutput();
    }

 
}
