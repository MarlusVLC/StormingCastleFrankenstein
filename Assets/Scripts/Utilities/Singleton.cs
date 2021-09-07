using System;
using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    Debug.Log(typeof(T).Name + "has been found!");

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        public static bool HasInstance()
        {
            return _instance != null;
        }
    }
}