using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : BasicEnemy
{
    public float stoppingDistance;
    public bool faceRight = true;
    public float wakeRange;
    public float distance;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        RangeCheck();

        if (this.transform.position.x < target.transform.position.x && !faceRight)
            Flip();

        if (this.transform.position.x > target.transform.position.x && faceRight)
            Flip();
    }

    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void RangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < wakeRange)
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
                transform.position = Vector2.MoveTowards(transform.position, target.position, EnemyData.Speed * Time.deltaTime);
        }
    }
}
