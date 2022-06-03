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
        public enum ModeSelect { Standard, Ultralyd}
        public ModeSelect mode;
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

        #region Ultrasound Variables

        public pinSelect triggerPin;

        public pinSelect echoPin;


        private string[] dataInput;
        private string recieved;
        private string lastRecieved;

        #endregion

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
                    Debug.LogError("Pin " + selectPin + " is already in use in input: " + this.name);
                    samePinError = true;
                    init = true;
                    return;
                }
                  
            }
            
            UduinoManager.Instance.pinMode((int)selectPin, pinMode);
            init = true;
        }
        public float ReadValue()
        {
            switch (mode)
            {
                case ModeSelect.Standard:
                    CheckInit();
                    if (!remapValue)
                    {
                        return UduinoManager.Instance.analogRead((int)selectPin);
                    }
                    else
                    {
                        float readValue = UduinoManager.Instance.analogRead((int)selectPin);
                        return Remap(readValue, inputMin, inputMax, newMin, newMax);
                    }
                    

                case ModeSelect.Ultralyd:

                    CheckUSInit();

                    UduinoManager.Instance.sendCommand("p", (int)triggerPin, (int)echoPin);

                    return int.Parse(recieved);
                    

                default:
                    return 0;
                    
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



        #region Ultrasound funtions

        private void CheckUSInit()
        {

            if (!init)
            {
                InitTrigger(triggerPin);
                InitEcho(echoPin);
                dataInput = new string[2];
                UduinoManager.Instance.OnDataReceived += DataRecieved;
                return;
            }
        }
        public void InitTrigger(pinSelect pin)
        {
            for (int i = 0; i < UduinoManager.Instance.pins.Count; i++)
            {
                if (UduinoManager.Instance.pins[i].currentPin == (int)pin)
                {
                    Debug.LogError("Triggerpin " + pin + " is already in use in input: " + this.name);
                    samePinError = true;
                    init = true;
                    return;
                }

            }

            UduinoManager.Instance.pinMode((int)pin, Uduino.PinMode.Output);
            init = true;
        }
        public void InitEcho(pinSelect pin)
        {
            for (int i = 0; i < UduinoManager.Instance.pins.Count; i++)
            {
                if (UduinoManager.Instance.pins[i].currentPin == (int)pin)
                {
                    Debug.LogError("Echopin " + pin + " is already in use in input: " + this.name);
                    samePinError = true;
                    init = true;
                    return;
                }

            }

            UduinoManager.Instance.pinMode((int)pin, Uduino.PinMode.Input);
            init = true;
        }
        public string ReadOutput()
        {

            CheckInit();

            UduinoManager.Instance.sendCommand("p", (int)triggerPin, (int)echoPin);

            return recieved;

        }

        void DataRecieved(string data, UduinoDevice board)
        {
            dataInput = data.Split(" ");
            if (dataInput[0] == ((int)echoPin).ToString())
            {
                recieved = dataInput[1];
            }
            else
            {
                //recieved = data;
            }
        }
        #endregion

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
