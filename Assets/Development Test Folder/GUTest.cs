using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GULab;
using Uduino;

public class GUTest : MonoBehaviour
{
    public float test;
    public Slider slider;
    public ArduinoInput ultrasound;

    private int[] lastValues;
    private int counter;
    private float average = 0;
    public float readValueInterval = 0.1f;

    void Start()
    {
        lastValues = new int[3];
        
        StartCoroutine(ReadUltrasoundValue());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public IEnumerator ReadUltrasoundValue()
    {
        while (true)
        {
            test = ultrasound.ReadValue();
            updateValue(ultrasound.ReadValue());
            yield return new WaitForSecondsRealtime(readValueInterval);
        }
    }

    public void updateValue(float value)
    {
        //Store values in array
        int readValue = Mathf.RoundToInt(value);
        if(readValue > 50) 
        {
            readValue = Mathf.RoundToInt(average);
        }
        else
        {
            readValue = Mathf.Clamp(readValue, 0, 30);
        }
        
        lastValues[counter % lastValues.Length] = readValue;
        counter++;
        //calcute avarage of values
        average = 0;
        for (int i = 0; i < lastValues.Length; i++)
        {
            average += lastValues[i];
        }
        average /=  (float)lastValues.Length;

        slider.value = Mathf.Lerp(average / 30f, slider.value, 0.2f);
        
    }
}
