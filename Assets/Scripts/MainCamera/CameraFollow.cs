using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTimeX, smoothTimeY;

    public Vector2 velocity;
    public GameObject player;
    public Vector2 minPos, maxPos;
    public bool bound; // su dung de kiem soat fix loi gia tri lon nhat va nho nhat cua camera

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Gia tri cua hai bien nay se tang tu vi tri cua camera toi vi tri cua nguoi choi theo mot
        // gia tri la smoothTime
        float posX = Mathf.SmoothDamp(this.transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(this.transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        // gia tri vi tri cua camera moi se bang vector3 voi gia tri la x y va z hien tai
        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bound)
        {
            // Khi vi tri cua camera qua gioi han min max thi se Clamp ve gia tri min max
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                                            Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                                            Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
        }
    }
}