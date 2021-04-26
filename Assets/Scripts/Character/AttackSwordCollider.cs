using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwordCollider : MonoBehaviour
{
    [SerializeField] private PlayerBasic player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("Damage", player.playerNormalDamage);
        }
    }
}