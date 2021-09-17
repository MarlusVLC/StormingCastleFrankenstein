using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject[] portals;
    [SerializeField] private float range = 3f;
    private KeyCode activateKey = KeyCode.E;

    private void Update()
    {
        foreach (var portal in portals)
        {
            if (Vector3.Distance(gameObject.transform.position, portal.transform.position) < range)
            {
                print("press " + activateKey + " to use");
                if (Input.GetKey(activateKey))
                    gameObject.transform.localPosition = Vector3.zero;
            }
        }
    }
}
