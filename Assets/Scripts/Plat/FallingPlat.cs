using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlat : MonoBehaviour
{
    public float timeDelay = 2;  // thoi gian truoc khi vat the roi xuong

    public Rigidbody2D r2;

    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(fall());
        }        
    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(timeDelay);
        r2.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }
}
