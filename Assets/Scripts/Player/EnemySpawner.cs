using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Spawners;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var spawner in Spawners)
            {
                var enemy = spawner.transform.GetChild(0);
                enemy.gameObject.SetActive(true);
            }
            Destroy(this);
        }
    }
}
