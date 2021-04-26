using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip coins;
    public AudioClip swords;
    public AudioClip jump;
    public AudioClip destroy;
    public AudioClip monsterDied;
    public AudioClip normalAttack;
    public AudioClip bossDied;
    public AudioClip health;
    public AudioClip select;
    public AudioClip item;

    public AudioSource audioSrc;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        coins = Resources.Load<AudioClip>("Coin");
        swords = Resources.Load<AudioClip>("Sword");
        jump = Resources.Load<AudioClip>("Jump");
        destroy = Resources.Load<AudioClip>("Rock_Crash");
        monsterDied = Resources.Load<AudioClip>("MonsterDied");
        normalAttack = Resources.Load<AudioClip>("AttackNormal");
        bossDied = Resources.Load<AudioClip>("BossDiedAndWin");
        health = Resources.Load<AudioClip>("Health");
        item = Resources.Load<AudioClip>("Item_Appears");
        select = Resources.Load<AudioClip>("Select");

        audioSrc = GetComponent<AudioSource>();

    }

    public void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Coin":
                audioSrc.clip = coins;
                audioSrc.PlayOneShot(coins, 0.6f);
                break;
            case "Sword":
                audioSrc.clip = swords;
                audioSrc.PlayOneShot(swords, 0.5f);
                break;
            case "Jump":
                audioSrc.clip = jump;
                audioSrc.PlayOneShot(jump, 0.6f);
                break;
            case "Rock_Crash":
                audioSrc.clip = destroy;
                audioSrc.PlayOneShot(destroy, 1f);
                break;
            case "MonsterDied":
                audioSrc.clip = monsterDied;
                audioSrc.PlayOneShot(monsterDied, 0.6f);
                break;
            case "AttackNormal":
                audioSrc.clip = normalAttack;
                audioSrc.PlayOneShot(normalAttack, 1f);
                break;
            case "BossDied":
                audioSrc.clip = bossDied;
                audioSrc.PlayOneShot(bossDied, 1f);
                break;
            case "Health":
                audioSrc.clip = health;
                audioSrc.PlayOneShot(health, 1f);
                break;
            case "Items":
                audioSrc.clip = item;
                audioSrc.PlayOneShot(item, 1f);
                break;
            case "Select":
                audioSrc.clip = select;
                audioSrc.PlayOneShot(select, 1f);
                break;
        }
    }
}
