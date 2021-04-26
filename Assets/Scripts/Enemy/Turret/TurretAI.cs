using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public int curHealth = 100;

    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 5;
    public float bulletTimer;

    public bool awake = false;
    public bool lookingRight = true;
    public bool attacking = true;


    public GameObject bullet; // Lay trang thai de ban vien dan

    public Transform target;
    public Animator anim;
    public Transform shootPointL, shootPointR;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        anim.SetBool("Awake", awake);
        //anim.SetBool("LookRight", lookingRight);
        //anim.SetBool("Attacking", attacking);

        RangeCheck();

        if (target.transform.position.x > this.transform.position.x)
        {
            lookingRight = true;
        }
        else
        {
            lookingRight = false;
        }

        if (curHealth < 0)
        {
            SoundManager.Instance.PlaySound("Rock_Crash");
            Destroy(gameObject);
        }
    }

    private void RangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < wakeRange)
        {
            awake = true;
        }
        else
        {
            awake = false;
        }
    }

    public void Attack(bool attackRight)
    {
        // Tri hoan thoi gian tan cong cua turret
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (attackRight)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootPointR.transform.position, shootPointR.transform.rotation) as GameObject;
                // Thay doi van toc cua rigidbody2D thanh huong toi cua nguoi choi va * toc do cua
                // vien dan
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
            else
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootPointL.transform.position, shootPointL.transform.rotation) as GameObject;
                // Thay doi van toc cua rigidbody2D thanh huong toi cua nguoi choi va * toc do cua
                // vien dan
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }
}