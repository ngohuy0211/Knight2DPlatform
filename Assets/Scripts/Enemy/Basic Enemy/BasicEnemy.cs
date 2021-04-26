using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy Render")]
    public EnemyData EnemyData;
    public PlayerController player;
    public SpriteRenderer characterSprite;
    public float knockPow;

    public void Damage(int damage)
    {
        EnemyData.HP -= damage;
        if (EnemyData.HP <= 0)
        {
            SoundManager.Instance.PlaySound("MonsterDied");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            player.Damage(EnemyData.damage);
            player.KnockBack(this.transform.position.x, knockPow, player.transform.position);
        }
    }
}