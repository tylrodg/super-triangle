using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    public Transform RightSpawn;
    public Transform LeftSpawn;
    public GameObject _bullet;

    public Agent agent;
    public AudioClip clip;
    public AudioSource aud;
    void Update()
    {
        bool shoot = Input.GetKeyDown(KeyCode.Z);
        float time = Time.time;
        if ((shoot && agent.gameObject.tag == "Player" || agent.followingPlayer && (agent.gameObject.tag == "Enemy" || agent.gameObject.CompareTag("Mega Enemy"))) && time > agent.lastFired + agent.firingReload)
        {
            aud.clip = clip;
            aud.Play();
            if (agent.facingRight)
            {
                GameObject newBullet = Instantiate(_bullet, RightSpawn.position, RightSpawn.rotation);
                newBullet.GetComponent<Bullet>().Init(new Vector2(1, .05f) * agent.bulletSpeed, Time.time + 1f);
                agent.lastFired = time;
            }
            else if (!agent.facingRight)
            {
                GameObject newBullet = Instantiate(_bullet, LeftSpawn.position, LeftSpawn.rotation);
                newBullet.GetComponent<Bullet>().Init(new Vector2(-1, .05f) * agent.bulletSpeed, Time.time + 1f);
                agent.lastFired = time;
            }
        }
    }
}
