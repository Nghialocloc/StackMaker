using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPivote : MonoBehaviour
{
    [SerializeField] private bool isTrigger = false;
    [SerializeField] private MeshRenderer whitePlatfrom;
    [SerializeField] private Material brick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            whitePlatfrom.material = brick;
            isTrigger = true;
            other.GetComponent<Player>().RemoveBrick();
        }
    }
}
