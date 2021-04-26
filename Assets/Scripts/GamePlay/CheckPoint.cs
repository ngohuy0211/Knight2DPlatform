using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite redCheckPoint;
    public Sprite greenCheckPoint;
    private SpriteRenderer checkPointRenderer;
    private CircleCollider2D gObj;
    public bool checkPointReached;
    void Start()
    {
        checkPointRenderer = GetComponent<SpriteRenderer>();
        gObj = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            checkPointRenderer.sprite = greenCheckPoint;
            checkPointReached = true;
            gObj.enabled = !gObj.enabled;
            SoundManager.Instance.PlaySound("BossDied");
        }
    }
}
