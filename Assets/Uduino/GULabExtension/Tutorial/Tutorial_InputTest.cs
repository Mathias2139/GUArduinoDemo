using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GULab;
using Uduino;

public class Tutorial_InputTest : MonoBehaviour
{

    public ArduinoInput inputAsset;
    public Slider slider;

    void Update()
    {
        slider.value = inputAsset.ReadValue();
    }

}
