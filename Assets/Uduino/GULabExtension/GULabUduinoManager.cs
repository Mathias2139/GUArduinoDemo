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

        private void Start()
        {
           
        }
        public void ActivatePin()
        {
            
        }
        private void Update()
        {
            readValue = vægt.ReadValueAsFloat();

        }
    }
}

