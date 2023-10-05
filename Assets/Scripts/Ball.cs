using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public TMP_Text playerScore, enemyScore;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Player") || collision.gameObject.name.Contains("Enemy"))
            GetComponent<AudioSource>().Play();

        if(collision.gameObject.name.Contains("Goal"))
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (collision.gameObject.name.Contains("Enemy Goal"))
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }
        if (collision.gameObject.name.Contains("Player Goal"))
        {
            enemyScore.text = (int.Parse(enemyScore.text) + 1).ToString();
        }
    }
}
