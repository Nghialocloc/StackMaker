using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject closeChest;
    [SerializeField] private GameObject openChest;
    [SerializeField] private ParticleSystem effect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            effect.Play();
            other.GetComponent<Player>().OnFinish();
            closeChest.SetActive(false);
            openChest.SetActive(true);
            LevelManager.Ins.OnFinish();
        }
    }
}
