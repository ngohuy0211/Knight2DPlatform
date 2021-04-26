using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3; // thoi gian hien thi cua bullet la 3s
    public Animator anim;
    public bool Fire = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("Damage", 10);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("FireEffect"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
