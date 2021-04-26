using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPlat : MonoBehaviour
{
    public float speed;
    public float changeDirection = -1;

    private Vector3 Move;

    private void Start()
    {
        Move = this.transform.position;
    }

    private void Update()
    {
        // bien pause thu nhat la pause tim maincamera cua file movingplat bien pause thu 2 la bien
        // trong gile pauseMenu
        if (Time.timeScale == 0)
        {
            this.transform.position = this.transform.position;
        }
        else
        {
            // Moi mot frame thi vi tri x cua vat do se cong them speed
            Move.y += speed;
            this.transform.position = Move;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            // quay nguoc lai vi bien changeDirection co gia tri la -1
            speed *= changeDirection;
        }
    }
}
