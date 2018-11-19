using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    [HideInInspector] public bool facingRight;
	[HideInInspector] public float firingReload;
	[HideInInspector] public bool followingPlayer;
	[HideInInspector] public float speed;
	[HideInInspector] public int HP;
	public float bulletSpeed;
	
    [HideInInspector] public float lastFired = 0f;
    public Agent()
    {
		facingRight = true;
		firingReload = 1.5f;
		followingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
