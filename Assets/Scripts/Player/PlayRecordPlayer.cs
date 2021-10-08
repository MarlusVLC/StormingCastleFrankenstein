using Audio;
using Player;
using UnityEngine;

public class PlayRecordPlayer : Interact
{
    protected override void Interaction(Transform item)
    {
        switch (item.name)
        {
            case "RecordPlayer1":
                FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                FindObjectOfType<AudioLogSound>().PlayAudioLogSound(0);
                item.gameObject.SetActive(false);
                break;
            case "RecordPlayer2":
                FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                FindObjectOfType<AudioLogSound>().PlayAudioLogSound(1);
                item.gameObject.SetActive(false);
                break;
            case "RecordPlayer3":
                FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                FindObjectOfType<AudioLogSound>().PlayAudioLogSound(2);
                item.gameObject.SetActive(false);
                break;
            case "RecordPlayer4":
                FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                FindObjectOfType<AudioLogSound>().PlayAudioLogSound(3);
                item.gameObject.SetActive(false);
                break;
            case "RecordPlayer5":
                FindObjectOfType<AudioLogSound>().StopAudioLogSound();
                FindObjectOfType<AudioLogSound>().PlayAudioLogSound(4);
                item.gameObject.SetActive(false);
                break;
        }
    }
}
