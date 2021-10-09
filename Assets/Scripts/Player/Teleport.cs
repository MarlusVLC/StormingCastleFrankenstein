using Player;
using UnityEngine;

public class Teleport : Interact
{
    protected override void Interaction(Transform item)
    {
        SceneUtil.ResetScene();
    }
    
}
