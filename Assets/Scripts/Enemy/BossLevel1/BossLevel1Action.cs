using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel1Action : BasicEnemy
{
    public bool walk = false;
    public float wakeToAttack;
    public float stoppingDistance;
    public bool faceRight = true;
    public float wakeRange;
    public float distance;

    private Transform target;
    public Animator anim;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
       
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        else
            BossRangeCheck();

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
    public void BossRangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < wakeRange)
        {
            walk = true;
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                anim.SetBool("Walk", walk);
                transform.position = Vector2.MoveTowards(transform.position, target.position, EnemyData.Speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, target.position) < wakeToAttack)
                {
                    anim.SetBool("Attacking", true);
                }
                else
                {
                    anim.SetBool("Attacking", false);
                }
            }
        }
        else
        {
            walk = false;
            anim.SetBool("Walk", walk);
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        if (EnemyData.HP < 0)
        {
            SoundManager.Instance.PlaySound("BossDied");
        }
    }
}