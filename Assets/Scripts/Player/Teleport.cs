using System.Runtime.CompilerServices;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Teleport : Interact
{
    protected override void Interaction(Transform item)
    {
        Portal portal = item.GetComponent<Portal>();
        _characterController.enabled = false;
        transform.position = portal.pairPortal.telePoint.transform.position;
        _characterController.enabled = true;
    }
}
