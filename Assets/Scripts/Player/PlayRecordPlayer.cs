using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using TMPro;
using Utilities;

public class PlayRecordPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] recordPlayers;
    [SerializeField] private float range = 3f;
    [SerializeField] private KeyCode activateKey = KeyCode.E;
    [TextArea][SerializeField] private string actionMessage;
    [SerializeField] private TextMeshProUGUI actionMessageHUD;
    
    private bool _canBeInteractedWith;

    private void Awake()
    {
        actionMessageHUD.text = actionMessage;
    }

    private void Update()
    {
        foreach (var recordPlayer in recordPlayers)
        {
            if (Vector3.SqrMagnitude(recordPlayer.transform.position - gameObject.transform.position) < range * range)
            {
                actionMessageHUD.enabled = _canBeInteractedWith = true;
                if (Input.GetKey(activateKey))
                {
                    switch (recordPlayer.name)
                    {
                        case "RecordPlayer1":
                            FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                            FindObjectOfType<AudioLogSound>().PlayAudioLogSound(0);
                            break;
                        case "RecordPlayer2":
                            FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                            FindObjectOfType<AudioLogSound>().PlayAudioLogSound(1);
                            break;
                        case "RecordPlayer3":
                            FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                            FindObjectOfType<AudioLogSound>().PlayAudioLogSound(2);
                            break;
                        case "RecordPlayer4":
                            FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                            FindObjectOfType<AudioLogSound>().PlayAudioLogSound(3);
                            break;
                        case "RecordPlayer5":
                            FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                            FindObjectOfType<AudioLogSound>().PlayAudioLogSound(4);
                            break;
                    }
                }
            }
                
        }
    }
}
