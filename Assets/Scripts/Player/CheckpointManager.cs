using System;
using GameManagement;
using UnityEngine;
using Utilities;
using Weapons;

namespace Player
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private Transform playerStartingPoint;
        [SerializeField] private LayerMask checkPointLayer;
        [SerializeField] private PlayerWeapons weaponHandler;
        
        private void Start()
        {
            PlayerData playerData = SaveManager.Instance.LoadGameState(); 
            if (playerData != null)
            {
                transform.position = playerData.playerStartingPosition.ToVector3;
                transform.rotation = playerData.playerStartingRotation.ToQuaternion;
                TransferWeaponsStates(playerData, true);
                Debug.Log("Data loaded!");
                return;
            }
            transform.position = playerStartingPoint.position;
            transform.rotation = playerStartingPoint.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (checkPointLayer.HasLayerWithin(other.gameObject.layer))
            {
                playerStartingPoint = other.transform;
                PlayerData playerData = new PlayerData(playerStartingPoint.position, playerStartingPoint.rotation);
                TransferWeaponsStates(playerData, false);
                SaveManager.Instance.SaveGameState(playerData);
                Debug.Log("Data saved!");
            }
        }

        private void TransferWeaponsStates(PlayerData playerData, bool isGettingData)
        {
            if (isGettingData)
            {
                for (int i = 0; i < playerData.unlockedWeapons.Length; i++)
                {
                    weaponHandler.ConfirmedGuns[i].IsUnlocked = playerData.unlockedWeapons[i];
                }
                return;
            }
            
            playerData.unlockedWeapons = new bool[weaponHandler.ConfirmedGuns.Length];
            for (int i = 0; i < weaponHandler.ConfirmedGuns.Length; i++)
            {
                playerData.unlockedWeapons[i] = weaponHandler.ConfirmedGuns[i].IsUnlocked;
            }
        }
    }
}