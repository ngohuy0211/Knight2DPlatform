using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public float lifeTime = 2;
    public float velX;
    public float velY;
    public Rigidbody2D r2;
    [SerializeField] private int damage;

    private void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        r2.velocity = new Vector2(velX, velY);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("Damage", damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}