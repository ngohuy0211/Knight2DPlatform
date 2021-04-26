using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerBasic
{
    [Header("Speed")]
    public float speed = 50f;

    public float maxSpeed = 3f;

    [Header("Jumping")]
    public float jumpPow = 220f;

    public bool doubleJump = false;
    public float fallGravity = 2.5f;
    public float upGravity = 2f;
    public float maxJump = 4;

    [Header("Health")]
    public int ourHealth = 100;
    public int maxHealth = 100;

    public float stamina = 100f;
    public float maxStamina = 100f;

    [Header("#")]
    public bool grounded = true;

    public bool faceRight = true;

    public int playerNormalDamage = 10;

    public bool isMagnet = false;

    public bool IsFullHP()
    {
        return ourHealth < maxHealth;
    }
}