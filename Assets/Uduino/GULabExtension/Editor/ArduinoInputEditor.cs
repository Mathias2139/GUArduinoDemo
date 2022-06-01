using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GULab;

[CustomEditor(typeof(ArduinoInput))]
public class ArduinoInputEditor : Editor
{
 
    public override void OnInspectorGUI() 
    {

        ArduinoInput input = (ArduinoInput)target;

        ArduinoInput.pinSelect lastPin = input.selectPin;
        input.selectPin = (ArduinoInput.pinSelect)EditorGUILayout.EnumPopup("Pin", input.selectPin);
        if(input.selectPin != lastPin)
        {
            input.RemoveError();
        }

        input.pinMode = (Uduino.PinMode)EditorGUILayout.EnumPopup("Pin Mode", input.pinMode);
        
        input.remapValue = EditorGUILayout.Toggle("Remap Output Value", input.remapValue);
        if (input.remapValue)
        {
            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.BeginHorizontal();
            input.inputMin = EditorGUILayout.FloatField("Input Min", input.inputMin);
            EditorGUILayout.Space(10);
            input.inputMax = EditorGUILayout.FloatField("Input Max", input.inputMax);
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            input.newMin = EditorGUILayout.FloatField("New Min", input.newMin);
            EditorGUILayout.Space(10);
            input.newMax = EditorGUILayout.FloatField("New Max", input.newMax);
            EditorGUILayout.EndHorizontal();
        }
        if (input.samePinError)
        {
            
            EditorGUILayout.HelpBox("The selected pin was already in use, please select a different pin", MessageType.Error);

        }
    }
}
