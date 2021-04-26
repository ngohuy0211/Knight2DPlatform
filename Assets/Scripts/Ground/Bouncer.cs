using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float force = 500f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    IEnumerator AnimateBouncy()
    {
        anim.Play("up");
        yield return new WaitForSeconds(.5f);
        anim.Play("down");
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            target.gameObject.GetComponent<PlayerController>().BouncePlayerWithBouncy(force);
            StartCoroutine(AnimateBouncy());
        }        
    }
}
