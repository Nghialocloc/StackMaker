using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivote : MonoBehaviour
{
    [SerializeField] private bool isTrigger = false;
    [SerializeField] private GameObject brick;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isTrigger)
        {
            brick.SetActive(false);
            isTrigger = true;
            other.GetComponent<Player>().AddBrick();
        }
    }
}
