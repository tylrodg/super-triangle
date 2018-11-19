using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    Player player;
    // Use this for initialization
    void Start()
    {
        player = (Player)GameObject.FindObjectOfType(typeof(Player));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player Bullet") || col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Mega Coin"))
            {
                player.coins += 100;
                player.score += 20;
            }
            else
            {
                player.coins++;
                player.score += 5;
            }
            Destroy(gameObject);
            player.DisplayTextManager();
        }
    }
}
