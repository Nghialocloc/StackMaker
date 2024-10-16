using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Unpivote"))
        {
            Destroy(gameObject);
        }
    }
}
