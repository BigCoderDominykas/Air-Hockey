using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public TMP_Text playerScore, enemyScore;

    AudioSource source;
    public AudioClip hitSound;
    public AudioClip goalSound;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var distanceToCenter = Vector3.Distance(Vector3.zero, transform.position);
        if(distanceToCenter > 10)
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.clip = hitSound;    
        source.Play();

        if(collision.gameObject.name.Contains("Goal"))
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            source.clip = goalSound;
            source.Play();
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