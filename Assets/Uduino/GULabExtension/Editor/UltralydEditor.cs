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

        UltralydInput.pinSelect lastTrigger = input.echoPin;
        UltralydInput.pinSelect lastEcho = input.triggerPin;

        input.triggerPin = (UltralydInput.pinSelect)EditorGUILayout.EnumPopup("Trigger Pin", input.triggerPin);
        input.echoPin = (UltralydInput.pinSelect)EditorGUILayout.EnumPopup("Echo Pin", input.echoPin);

        if (input.triggerPin != lastTrigger)
        {
            input.RemoveTriggerError();
        }
        if (input.echoPin != lastEcho)
        {
            input.RemoveEchoError();
        }

        if (input.sameTriggerError)
        {

            EditorGUILayout.HelpBox("The selected triggerpin was already in use, please select a different pin", MessageType.Error);

        }
        if (input.sameEchoError)
        {

            EditorGUILayout.HelpBox("The selected echopin was already in use, please select a different pin", MessageType.Error);

        }



    }
}
