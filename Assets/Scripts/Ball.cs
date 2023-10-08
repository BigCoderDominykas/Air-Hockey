using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public TMP_Text playerScore, enemyScore, goalText;

    AudioSource source;
    public AudioClip hitSound;
    public AudioClip goalSound;

    bool shouldRespawn;
    public Transform deathPoint, goalAnnouncer;
    float respawnTimer;
    Vector3 respawnPosition;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Teleports ball back to level if it goes out of bounds
        var distanceToCenter = Vector3.Distance(Vector3.zero, transform.position);
        if(distanceToCenter > 10)
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // Ball respawns 2 seconds after goal
        if (shouldRespawn)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= 2)
            {
                transform.position = respawnPosition;
                goalAnnouncer.position = deathPoint.position;
                respawnTimer = 0;
                shouldRespawn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.name.Contains("Goal"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            source.clip = goalSound;
            source.Play();

            shouldRespawn = true;
            transform.position = deathPoint.position;
            goalAnnouncer.position = Vector3.zero;


            if (collision.gameObject.name.Contains("Enemy Goal"))
            {
                goalText.text = "Player Goal!";
                playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
                respawnPosition = Vector3.right;
            }
            if (collision.gameObject.name.Contains("Player Goal"))
            {
                goalText.text = "Enemy Goal!";
                enemyScore.text = (int.Parse(enemyScore.text) + 1).ToString();
                respawnPosition = Vector3.left;
            }

            if (int.Parse(playerScore.text) >= 7 || int.Parse(enemyScore.text) >= 7)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            source.clip = hitSound;
            source.Play();
        }
    }
}