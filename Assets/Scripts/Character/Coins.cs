using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerController>().playerBase.isMagnet)
            {
                transform.parent.position = Vector3.MoveTowards(transform.position, collision.transform.position, 0.1f);
            }
        }
    }
}