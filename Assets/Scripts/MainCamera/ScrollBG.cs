using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public PlayerController player;
    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        Vector2 offset = GetComponent<MeshRenderer>().material.mainTextureOffset; // la gia tri offset hien tai
        offset.x = player.transform.position.x;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset * Time.deltaTime / 0.4f;
    }
}
