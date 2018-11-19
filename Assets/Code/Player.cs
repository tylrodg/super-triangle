using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Agent
{
    private bool _jumping;
    private Rigidbody2D _rb;
    private float jumpVelocity = 0f;
    private float megaRepel = 2000;
    private float repel = 1000;
    [HideInInspector] public int score = 0;
    [HideInInspector] public int coins = 0;
    public Text HPText;
    public Text coinText;
    public Text scoreText;
    public Text winText;
    Object[] enemies;
    public AudioSource aud;
    public AudioClip jumpClip;
    public AudioClip HPUpClip;
    public Player()
    {
        bulletSpeed = 15f;
        speed = 5f;
        HP = 50;
        firingReload = 0f;
    }
    // Use this for initialization
    internal void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        DisplayTextManager();
        winText.text = "";
        enemies = GameObject.FindObjectsOfType(typeof(Enemy));

    }
    void FixedUpdate()
    {

        enemies = GameObject.FindObjectsOfType(typeof(Enemy));
        DisplayTextManager();
        if (coins >= 10)
        {
            HP += 10;
            coins -= 10;
            aud.clip = HPUpClip;
            aud.Play();
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool Left = Input.GetKey(KeyCode.LeftArrow);
        bool Right = Input.GetKey(KeyCode.RightArrow);
        bool moveVertical = Input.GetKeyDown(KeyCode.Space);
        if (moveVertical && !_jumping)
        {
            jumpVelocity = 17f;
            _jumping = true;
            aud.clip = jumpClip;
            aud.Play();
        }
        else jumpVelocity = 0f;

        if (Left)
        {
            facingRight = false;
        }
        if (Right)
        {
            facingRight = true;
        }
        Vector2 movement = new Vector2(moveHorizontal * 0.5f, jumpVelocity);

        _rb.AddForce(movement * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            _jumping = false;
        }

        else if (col.gameObject.tag == "Enemy" || col.gameObject.CompareTag("Mega Enemy"))
        {
            var force = transform.position - col.transform.position;
            force.Normalize();

            if (col.gameObject.CompareTag("Enemy"))
            {
                _rb.AddForce(force * repel);
                if (HP - 5 >= 0) { HP -= 5; }
                else { HP = 0; }
                if (score - 10 >= 0) { score -= 10; }
                else score = 0;
            }
            if (col.gameObject.CompareTag("Mega Enemy"))
            {
                _rb.AddForce(force * megaRepel);
                if (HP - 10 >= 0) { HP -= 10; }
                else { HP = 0; }
                if (score - 20 >= 0) { score -= 20; }
                else score = 0;
            }
            DisplayTextManager();
        }
        else if (col.gameObject.tag == "Enemy Bullet" || col.gameObject.CompareTag("Mega Enemy Bullet"))
        {
            // calculate force vector
            var force = transform.position - col.transform.position;
            // normalize force vector to get direction only and trim magnitude
            force.Normalize();
            if (col.gameObject.CompareTag("Enemy Bullet"))
            {
                _rb.AddForce(force * repel * 1.5f);
                if (HP - 10 >= 0) { HP -= 10; }
                else { HP = 0; }
                if (score - 20 >= 0) { score -= 20; }
                else score = 0;
            }
            if (col.gameObject.CompareTag("Mega Enemy Bullet"))
            {
                _rb.AddForce(force * megaRepel * 1.5f);
                if (HP - 20 >= 0) { HP -= 20; }
                else { HP = 0; }
                if (score - 40 >= 0) { score -= 40; }
                else score = 0;
            }

            DisplayTextManager();
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent = col.transform;
        }
        else
        {
            transform.parent = null;
        }

    }
    public void DisplayTextManager()
    {
        HPText.text = HP.ToString() + " HP";
        scoreText.text = score.ToString() + " pts";
        coinText.text = coins.ToString() + " coins";
        if (HP <= 0)
        {
            winText.text = "You Lose!";
            gameObject.GetComponent<Player>().enabled = !gameObject.GetComponent<Player>().enabled;
        }
        if (_rb.position.y <= -13f)
        {
            HPText.text = "HP: 0";
            winText.text = "You Lose!";
            gameObject.GetComponent<Player>().enabled = !gameObject.GetComponent<Player>().enabled;
        }
        if (enemies == null || enemies.Length == 0)
        {
            winText.text = "You Win!";
        }
    }
}
