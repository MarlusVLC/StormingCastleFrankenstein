using Audio;
using Player;
using UnityEngine;

public class PlayRecordPlayer : Interact
{
    protected override void Interaction(Transform item)
    {
        var audioIndex = -1;
        switch (item.name)
        {
            case "RecordPlayer1":
                audioIndex = 0;
                break;
            case "RecordPlayer2":
                audioIndex = 1;
                break;
            case "RecordPlayer3":
                audioIndex = 2;
                break;
            case "RecordPlayer4":
                audioIndex = 3;
                break;
            case "RecordPlayer5":
                audioIndex = 4;
                break;
        }

        FindObjectOfType<AudioLogSound>().PlayAudioLogSound(audioIndex);
        item.gameObject.SetActive(false);
    }
}
