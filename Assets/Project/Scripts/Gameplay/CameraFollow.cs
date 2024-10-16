using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float smoothCamera = 0.5f;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0.13f, 12.85f, -11.89f);
        transform.eulerAngles = new Vector3(47f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.position = Vector3.Slerp(transform.position, player.position + offset, smoothCamera);
    }
}
