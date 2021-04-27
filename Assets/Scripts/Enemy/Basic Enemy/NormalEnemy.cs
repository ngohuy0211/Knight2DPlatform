using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BasicEnemy
{
    [Header("Enemy Action")]
    public Transform PointA;
    public Transform PointB;

    [SerializeField]
    private float thresholdOffset = 0.1f;

    private bool fromAtoB = false;

    public override void Damage(int damage)
    {
        base.Damage(damage);
        if(EnemyData.HP <= 0)
        {
            SoundManager.Instance.PlaySound("MonsterDied");
        }
    }
    private void FixedUpdate()
    {
        Vector2 move = new Vector2(EnemyData.Speed * Time.deltaTime * (fromAtoB ? 1f : -1f), 0);

        // Flip sprite
        characterSprite.flipX = fromAtoB;

        transform.Translate(move);

        ChangeDirection();
    }

    public void ChangeDirection()
    {
        if (fromAtoB && Mathf.Abs(transform.position.x - PointB.position.x) <= thresholdOffset)
        {
            fromAtoB = false;
        }
        else if (!fromAtoB && Mathf.Abs(transform.position.x - PointA.position.x) <= thresholdOffset)
        {
            fromAtoB = true;
        }
    }
}
