using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject mega;
    Player player;
    // Use this for initialization
    void Start()
    {
        player = (Player)GameObject.FindObjectOfType(typeof(Player));
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 320)
        {
            Instantiate(mega, new Vector3(310f, -3.5f, 0), Quaternion.identity);
            enabled = false;
        }
    }

}
