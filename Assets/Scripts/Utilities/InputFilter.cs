using System;
using UnityEngine;

namespace Utilities
{
    public class InputFilter : Singleton<InputFilter>
    {
        public event Action<int> OnNumericValueReceived;
        
        private void Update()
        {
            GetMouseWheelValue();
            GetNumericInput();
        }
        
        private short GetNumericInput()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                OnNumericValueReceived?.Invoke(0);
                return 0;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnNumericValueReceived?.Invoke(1);
                return 1;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnNumericValueReceived?.Invoke(2);
                return 2;
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnNumericValueReceived?.Invoke(3);
                return 3;
            }

            return -1;
        }
        


        
        private void GetMouseWheelValue()
        {
            //TODO pegar input da rodinha do mouse
        }
    }
}