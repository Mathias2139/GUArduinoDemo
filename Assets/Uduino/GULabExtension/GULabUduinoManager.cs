using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

namespace GULab
{
    public class GULabUduinoManager : MonoBehaviour
    {
        private UduinoManager manager;
        public float readValue;
        public float readXValue;
        public bool readValueAsBool;
        public ArduinoInput vægt;
        public ArduinoInput xInput;
        public ArduinoInput swInput;

        void Setup()
        {
            //UduinoManager.Instance.pinMode(9, PinMode.Output);
            //yInput.Init();
        }
        private void Start()
        {
            //ActivatePin();
            Setup();
        }
        public void ActivatePin()
        {
            manager.pinMode(5, PinMode.Output);
        }
        private void Update()
        {
            readValue = vægt.ReadValueAsFloat();
            readValue = UduinoManager.Instance.analogRead(5);


            readXValue = xInput.ReadValueAsFloat();
            readValueAsBool = swInput.ReadValueAsBool();
        }
    }
}

