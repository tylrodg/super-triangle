using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 cam_offset;
    // Use this for initialization
    void Start()
    {
        cam_offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        if (player.transform.position.x > -10f)
        {
            transform.position = new Vector3(player.transform.position.x + cam_offset.x, -1, -20);
        }
    }
}
