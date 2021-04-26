using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerBasic playerBase;

    [Header("Fire")]
    public GameObject fireToRight;

    public GameObject fireToLeft;
    public Vector2 firePos;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    [Header("Player render")]
    public Rigidbody2D r2;

    public Animator anim;

    [Header("CheckPoint And Respawn")]
    public Vector3 respawnPoint;

    private float StaminaRegenTimer = 0.0f;
    private const float StaminaIncreasePerFrame = 5.0f;
    private const float StaminaTimeToRegen = 3.0f;

    private void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        respawnPoint = transform.position;
        playerBase.ourHealth = UIManager.Instance.hp;
        UIManager.Instance.player = this;
    }

    private float h;

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Grounded", playerBase.grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerBase.grounded)
            {
                playerBase.grounded = false;
                playerBase.doubleJump = true;
                r2.AddForce(Vector2.up * playerBase.jumpPow);
            }
            else
            {
                if (playerBase.doubleJump)
                {
                    playerBase.doubleJump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * playerBase.jumpPow * 0.8f);
                }
            }
            SoundManager.Instance.PlaySound("Jump");
        }

        // Fix jump - cai thien trong luc khi jumping
        if (r2.velocity.y < 0)
        {
            r2.velocity += Vector2.up * Physics2D.gravity.y * (playerBase.fallGravity - 1) * Time.deltaTime;
        }

        //Fire Effect
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (playerBase.stamina > 15)
                Fire();
        }

        // Stamina
        if (playerBase.stamina < playerBase.maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            { 
                playerBase.stamina = Mathf.Clamp(playerBase.stamina + (StaminaIncreasePerFrame * Time.deltaTime), 0.0f, playerBase.maxStamina);
                UIManager.Instance.SetValueStaminaSlider(playerBase.stamina);
            }
            else
                StaminaRegenTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        r2.AddForce(Vector2.right * playerBase.speed * h);

        if (r2.velocity.x > playerBase.maxSpeed)
            r2.velocity = new Vector2(playerBase.maxSpeed, r2.velocity.y);

        if (r2.velocity.x < -playerBase.maxSpeed)
            r2.velocity = new Vector2(-playerBase.maxSpeed, r2.velocity.y);

        if (r2.velocity.y > playerBase.maxJump)
            r2.velocity = new Vector2(r2.velocity.x, playerBase.maxJump);

        if (r2.velocity.y < -playerBase.maxJump)
            r2.velocity = new Vector2(r2.velocity.x, -playerBase.maxJump);

        if (h > 0 && !playerBase.faceRight)
        {
            Flip();
        }

        if (h < 0 && playerBase.faceRight)
        {
            Flip();
        }
    }

    private Vector3 Scale;

    public void Flip()
    {
        playerBase.faceRight = !playerBase.faceRight;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void AddHealth(int hp)
    {
        playerBase.ourHealth += hp;
        if (playerBase.ourHealth > playerBase.maxHealth)
            playerBase.ourHealth = playerBase.maxHealth;
        UIManager.Instance.SetValueHpSlider(playerBase.ourHealth);
    }

    public void Damage(int damage)
    {
        playerBase.ourHealth -= damage;
        if (playerBase.ourHealth <= 0)
        {
            playerBase.ourHealth = 0;
            transform.position = respawnPoint;
            playerBase.ourHealth = playerBase.maxHealth;
            UIManager.Instance.Death();
        }
        UIManager.Instance.SetValueHpSlider(playerBase.ourHealth);
    }

    public void KnockBack(float positionx, float Knockpow, Vector2 Knockdir)
    {
        if (positionx > this.transform.position.x)
        {
            //Debug.Log("Enemy > Player");
            //Debug.Log("Enemy Pos: " + enemyPos.transform.position.x + "PlayerPos: " + this.transform.position.x);
            r2.velocity = new Vector2(0, 0);
            // Huong x se bang gia tri cua nguoi choi * -100: tao mot luc bang gia tri nguoi choi
            // hien tai * nguoc lai
            r2.AddForce(new Vector2(Knockdir.x * -100, Knockdir.y * Knockpow));
        }
        else if (positionx < this.transform.position.x)
        {
            //Debug.Log("Enemy < Player");
            //Debug.Log("Enemy Pos: " + enemyPos.transform.position.x + "PlayerPos: " + this.transform.position.x);
            r2.velocity = new Vector2(0, 0);
            r2.AddForce(new Vector2(Knockdir.x * 100, Knockdir.y * Knockpow));
        }
    }

    public void Fire()
    {
        firePos = transform.position;
        playerBase.stamina -= 15;
        if (playerBase.stamina < 0)
        {
            playerBase.stamina = 0;
        }
        UIManager.Instance.SetValueStaminaSlider(playerBase.stamina);

        if (playerBase.faceRight)
        {
            firePos += new Vector2(+1f, -0.1f);
            Instantiate(fireToRight, firePos, Quaternion.identity);
        }
        else
        {
            firePos += new Vector2(-1f, -0.1f);
            Instantiate(fireToLeft, firePos, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == true && collision.CompareTag("Heart"))
        {
            SoundManager.Instance.PlaySound("Items");
            Destroy(collision.gameObject);
            UIManager.Instance.AddItemHP(1);
        }
        
        if (collision.isTrigger == true && collision.CompareTag("EndPoint"))
        {
            playerBase.ourHealth -= 100;
        }

        if (collision.isTrigger == true && collision.CompareTag("Shoes"))
        {
            SoundManager.Instance.PlaySound("Items");
            Destroy(collision.gameObject);
            UIManager.Instance.AddItemShoes(1);
        }

        if (collision.CompareTag("Magnet"))
        {
            SoundManager.Instance.PlaySound("Items");
            Destroy(collision.gameObject);
            UIManager.Instance.AddItemMagnet(1);
        }

        if (collision.CompareTag("Coins"))
        {
            SoundManager.Instance.PlaySound("Coin"); Destroy(collision.gameObject);
            UIManager.Instance.AddCoin(1);
        }

        if (collision.CompareTag("CheckPoint"))
        {
            respawnPoint = collision.transform.position;
        }
    }

    public void BouncePlayerWithBouncy(float force)
    {
        if (playerBase.grounded)
        {
            playerBase.grounded = false;
            r2.AddForce(new Vector2(0, force));
        }
    }
}