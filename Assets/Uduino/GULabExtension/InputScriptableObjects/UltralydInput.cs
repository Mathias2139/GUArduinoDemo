using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System;

namespace GULab
{
    [CreateAssetMenu(fileName = "Arduino Input", menuName = "Arduino Special Input/Ultralyd Input", order = 1)]
    public class UltralydInput : ScriptableObject
    {
        public enum pinSelect { D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, A0, A1, A2, A3, A4, A5 }
        public pinSelect triggerPin;
       
        public pinSelect echoPin;
        
  
        private bool init = false;
  

        private string[] dataInput;
        private string recieved;
        private string lastRecieved;

        public bool samePinError = false;

        private void OnEnable()
        {
            init = false;
            samePinError = false;
        }

        public void InitTrigger(pinSelect pin)
        {
            for (int i = 0; i < UduinoManager.Instance.pins.Count; i++)
            {
                if(UduinoManager.Instance.pins[i].currentPin == (int)pin)
                {
                    Debug.LogError("Pin " + pin +" is already in use " + this.name);
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
                    Debug.LogError("Pin " + pin + " is already in use " + this.name);
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

        void Update()
        {
            //test = input.ReadValueAsFloat();
            

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
                recieved = data;
            }
        }

        private void CheckInit()
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

       
      
    }
}
