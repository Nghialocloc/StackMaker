using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotePush : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask wallLayer;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetInteger("zhuanjiaoSet", 1);
            Player p = other.GetComponent<Player>();
            StartCoroutine(Push(p));
        }
    }


    public IEnumerator Push(Player player)
    {
        yield return new WaitForSeconds(0.1f);
        if (player.chooseDir == Direct.foward || player.chooseDir == Direct.back)
        {
            if (Physics.Raycast(anim.gameObject.transform.position,Vector3.right, 1f, wallLayer))
            {
                player.SearchDirection(Direct.left);
                player.chooseDir = Direct.left;
            }
            else
            {
                player.SearchDirection(Direct.right);
                player.chooseDir = Direct.right;
            }
        }
        else if(player.chooseDir == Direct.left || player.chooseDir == Direct.right) 
        {
            if (Physics.Raycast(anim.gameObject.transform.position, Vector3.forward, 1f, wallLayer))
            {
                player.SearchDirection(Direct.back);
                player.chooseDir = Direct.back;
            }
            else
            {
                player.SearchDirection(Direct.foward);
                player.chooseDir = Direct.foward;
            }
        }

        anim.SetInteger("zhuanjiaoSet", 0);
    }
}
