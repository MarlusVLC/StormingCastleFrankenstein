using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using UnityEngine;
using Utilities;

namespace GameManagement
{
    public class SaveManager : Singleton<SaveManager>
    {
        private BinaryFormatter _binaryFormatter;
        private string _savePath;

        public string SavePath => _savePath;

        private void Awake()
        {
            _binaryFormatter = new BinaryFormatter();
            _savePath = Application.persistentDataPath + "/SaveData.save";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteGameState();
            }
        }

        public void SaveGameState(PlayerData playerData)
        {
            FileStream file = File.Create(_savePath);
            _binaryFormatter.Serialize(file, playerData);
            file.Close();
        }

        public PlayerData LoadGameState()
        {
            if (File.Exists(_savePath))
            {
                try
                {
                    FileStream file = File.Open(_savePath, FileMode.Open);
                    PlayerData playerData = (PlayerData) _binaryFormatter.Deserialize(file);
                    file.Close();
                    return playerData;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);                    
                }
            }
            return null;
        }

        public void DeleteGameState()
        {
            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
                Debug.Log("File deleted");
            }
        }
        
        
    }
}