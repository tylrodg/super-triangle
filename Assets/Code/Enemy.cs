using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
    /*learned from https://www.youtube.com/watch?v=rhoQd6IAtDo */
    private double startFollow = 10;
    private double stopFollow = 1;
    private Transform target;
    private Rigidbody2D _rb;
    private Vector3 velocity;
    private Vector3 oneFrameAgo;
    private Vector3 og_pos;
    Player player;
    public Enemy()
    {
        firingReload = .05f;
        facingRight = false;
        bulletSpeed = 10f;
        speed = 2f;
        HP = 10;
    }
    // Use this for initialization
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        oneFrameAgo = transform.position;
        player = (Player)GameObject.FindObjectOfType(typeof(Player));
        if (gameObject.CompareTag("Mega Enemy"))
        {
            og_pos = transform.position;
            HP = 50;
            bulletSpeed = 15f;
            firingReload = 3f;
            startFollow = 25;
        }
    }
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        FollowPlayer();
        velocity = transform.position - oneFrameAgo;
        oneFrameAgo = transform.position;

        if (velocity.x > 0.0f)
        {
            facingRight = true;
        }
        else facingRight = false;
        if (transform.position.y <= -11f)
        {
            if (gameObject.CompareTag("Mega Enemy"))
            {
                transform.position = og_pos;
            }
            else { Destroy(gameObject); }
        }
    }
    void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) > stopFollow && Vector2.Distance(transform.position, target.position) < startFollow)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            followingPlayer = true;
        }
        else { followingPlayer = false; }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player Bullet")
        {
            HP -= 10;
            player.score += 10;
        }
    }
}
