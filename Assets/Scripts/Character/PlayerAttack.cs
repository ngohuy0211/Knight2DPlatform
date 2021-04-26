using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 0.3f;
    public bool attacking = false;

    public Animator anim;
    public GameObject colAttack;

    private void Awake()
    {
        colAttack.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            colAttack.SetActive(true);
            attacking = true;
            attackDelay = 0.3f;
            SoundManager.Instance.PlaySound("Sword");
        }
        else

        if (attacking)
        {
            if (attackDelay > 0)
            {
                attackDelay -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackDelay = 0;
                colAttack.SetActive(false);
            }
        }

        anim.SetBool("Attacking", attacking);
    }
}