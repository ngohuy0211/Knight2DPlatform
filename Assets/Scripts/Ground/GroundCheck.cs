using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MovingPlat move;
    public PlayerController player;

    public Vector3 movP;

    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("MovingFlat").GetComponent<MovingPlat>();
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    //Box collider thu2 la mot trigger, khi no va cham voi mot collider khac thi no se Stay hoac Exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            player.playerBase.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Water"))
        {
            player.playerBase.grounded = true;
        }
        if (collision.isTrigger == false && collision.CompareTag("MovingFlat"))
        {
            movP = player.transform.position;
            movP.x += move.speed * 2f;
            player.transform.position = movP;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Water"))
        {
            player.playerBase.grounded = false;
        }
    }
}