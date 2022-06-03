using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GULab;

[CustomEditor(typeof(UltralydInput))]
public class UltralydEditor : Editor
{
 
    public override void OnInspectorGUI() 
    {

        UltralydInput input = (UltralydInput)target;

        
        input.triggerPin = (UltralydInput.pinSelect)EditorGUILayout.EnumPopup("Trigger Pin", input.triggerPin);
        input.echoPin = (UltralydInput.pinSelect)EditorGUILayout.EnumPopup("Echo Pin", input.echoPin);
     

       
        
        
    }
}
