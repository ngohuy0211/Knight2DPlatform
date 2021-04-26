using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackCone : MonoBehaviour
{
    public TurretAI turret;
    public bool isLeft = true; // kiem tra xem player dung ben phai hay ben trai

    private void Awake()
    {
        turret = gameObject.GetComponentInParent<TurretAI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isLeft)
            {
                turret.Attack(false);
            } 
            else
            {
                turret.Attack(true);
            }
        }
    }
}
