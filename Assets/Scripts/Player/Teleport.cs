using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

public class Teleport : MonoCache
{
    [SerializeField] private Transform[] portals;
    [SerializeField] private float range = 3f;
    [SerializeField] private KeyCode activateKey = KeyCode.E;
    [TextArea][SerializeField] private string actionMessage;
    [SerializeField] private TextMeshProUGUI actionMessageHUD;

    private bool _canBeInteractedWith;


    protected override void Awake()
    {
        base.Awake();
        actionMessageHUD.text = actionMessage;
    }
    
    private void Update()
    {
        foreach (var portal in portals)
        {
            if (Vector3.SqrMagnitude(portal.transform.position - gameObject.transform.position) < range*range)
            {
                actionMessageHUD.enabled = _canBeInteractedWith = true;
                if (Input.GetKey(activateKey))
                {
                    SceneUtil.ResetScene();
                }
            }
            if (Vector3.SqrMagnitude(portal.transform.position - gameObject.transform.position) > range*range)
            {
                actionMessageHUD.enabled = _canBeInteractedWith =  false;
            }
        }
    }
}
