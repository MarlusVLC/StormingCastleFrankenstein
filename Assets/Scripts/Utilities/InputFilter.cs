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
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                OnNumericValueReceived?.Invoke(4);
                return 4;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                OnNumericValueReceived?.Invoke(5);
                return 5;
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                OnNumericValueReceived?.Invoke(6);
                return 6;
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
            {
                OnNumericValueReceived?.Invoke(7);
                return 7;
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                OnNumericValueReceived?.Invoke(8);
                return 8;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                OnNumericValueReceived?.Invoke(9);
                return 9;
            }

            return -1;
        }

        private void GetMouseWheelValue()
        {
            //TODO pegar input da rodinha do mouse
        }
    }
}