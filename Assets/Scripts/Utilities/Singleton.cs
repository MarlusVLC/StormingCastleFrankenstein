using System;
using UnityEngine;

namespace Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static bool IsPermanent = false;
        
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        _instance = obj.AddComponent<T>();
                    }

                    Debug.Log($"{typeof(T).Name} is Permanent: {IsPermanent.ToString()}" );
                    if (IsPermanent) DontDestroyOnLoad ( _instance.gameObject );
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