using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System;

namespace GULab
{
    [CreateAssetMenu(fileName = "Arduino Input", menuName = "Arduino Input", order = 1)]
    public class ArduinoInput : ScriptableObject
    {
        public enum pinSelect { D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, A0, A1, A2, A3, A4, A5 }
        public pinSelect selectPin;
        public Uduino.PinMode pinMode;
  
        private bool init = false;
        public bool remapValue;
        public float inputMin = 0;
        public float inputMax = 1023;
        public float newMin = -1;
        public float newMax = 1;

        public bool samePinError = false;

        private void OnEnable()
        {
            init = false;
            samePinError = false;
        }

        public void Init()
        {
            for (int i = 0; i < UduinoManager.Instance.pins.Count; i++)
            {
                if(UduinoManager.Instance.pins[i].currentPin == (int)selectPin)
                {
                    Debug.LogError("Pin " + selectPin +" is already in use " + this.name);
                    samePinError = true;
                    init = true;
                    return;
                }
                  
            }
            
            UduinoManager.Instance.pinMode((int)selectPin, pinMode);
            init = true;
        }
        public float ReadValueAsFloat()
        {
            
            CheckInit();
            if (!remapValue)
            {
                return UduinoManager.Instance.analogRead((int)selectPin);
            }
            else
            {
                float readValue = UduinoManager.Instance.analogRead((int)selectPin);
                return Remap(readValue,inputMin,inputMax,newMin,newMax);
            }
            
        }
        public bool ReadValueAsBool()
        {
            CheckInit();
            if (UduinoManager.Instance.analogRead((int)selectPin) > (inputMax + 1) / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void CheckInit()
        {



            if (!init)
            {
                Init();
                return;
            }
        }

        private static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }

        public void RemoveError()
        {
            samePinError = false;
        }
    }
}
