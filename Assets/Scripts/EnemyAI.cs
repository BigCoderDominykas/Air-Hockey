using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform ball;
    public Transform defensePoint;

    public float attackSpeed = 20;
    public float defenseSpeed = 15;
    float speed;

    private Vector3 targetPosition;
    private Vector3 defensePointOffset;

    public float moveCooldown;

    private void Update()
    {
        moveCooldown -= Time.deltaTime;
        if (moveCooldown <= 0)
        {
            defensePointOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            moveCooldown = 2;
        }
        
        bool ballInRange = ball.position.x > 0;

        if (ballInRange)
        {
            // attack
            targetPosition = ball.position + defensePointOffset / 5f;
            speed = attackSpeed;
        }
        else
        {
            // defense
            targetPosition = defensePoint.position;
            targetPosition += defensePointOffset;
            speed = defenseSpeed;
        }

        var finalPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(finalPosition);
    }
}
