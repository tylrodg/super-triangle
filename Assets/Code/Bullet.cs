using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _deathtime;

    public void Init(Vector2 vel, float deathtime)
    {
        GetComponent<Rigidbody2D>().velocity = vel;
        _deathtime = deathtime;
    }
    internal void Update()
    {
        if (Time.time > _deathtime) { Destroy(gameObject); }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
